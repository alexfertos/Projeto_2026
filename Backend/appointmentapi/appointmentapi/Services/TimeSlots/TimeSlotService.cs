using appointmentapi.DTOs.TimeSchedule;
using appointmentapi.DTOs.UnitSpecialty;
using appointmentapi.Models.AppointmentEntity;
using appointmentapi.Repositories.Interface.AppointmentInterface;
using appointmentapi.Services.Interface;

namespace appointmentapi.Services.TimeSlots;

public class TimeSlotService : ITimeSlotService
{
    private readonly ITimeSlotRepository _timeSlotRepository;
    private readonly IUnitScheduleRepository _unitScheduleRepository;

    public TimeSlotService(
            ITimeSlotRepository timeSlotRepository,
            IUnitScheduleRepository unitScheduleRepository)
    {
        _timeSlotRepository = timeSlotRepository;
        _unitScheduleRepository = unitScheduleRepository;
    }

    public async Task<AvailableSlotDto> GetAvailableSlotsAsync(int unitId, int specialtyId, DateTime date)
    {
        var slots = await _timeSlotRepository.GetAvailableSlotsAsync(unitId, specialtyId, date);

        // Caso não existam slots no banco, podemos gerar
        if (!slots.Any())
        {
            await GenerateDailySlotsAsync(unitId, specialtyId, date);
            slots = await _timeSlotRepository.GetAvailableSlotsAsync(unitId, specialtyId, date);
        }

        bool hasAvailable = slots.Any(s => s.IsAvailable);

        var result = new AvailableSlotDto
        {
            UnitId = unitId,
            SpecialtyId = specialtyId,
            IsAvailable = hasAvailable,
            AvailableTimes = slots
            .Select(s => new TimeSlotInfoDto
            {
                StartTime = s.StartTime,
                IsAvailable = s.IsAvailable
            })
            .ToList()
        };

        return result;
    }

    public async Task GenerateDailySlotsAsync(int unitId, int specialtyId, DateTime date)
    {
        var existingSlots = await _timeSlotRepository.GetAvailableSlotsAsync(unitId, specialtyId, date);

        if (existingSlots != null && existingSlots.Any())
            return;

        var schedules = await _unitScheduleRepository.GetByUnitIdAsync(unitId);

        int dayOfWeek = (int)date.DayOfWeek;

        if (dayOfWeek == 0) dayOfWeek = 7;

        var schedule = schedules.FirstOrDefault(s => s.DayOfWeek == dayOfWeek);
        if (schedule == null || !schedule.IsAvailable)
            return;

        var start = schedule.StartTime;
        var end = schedule.EndTime;
        var interval = TimeSpan.FromMinutes(schedule.SlotDurationMinutes);

        var slots = new List<TimeSlot>();

        for (var time = start; time < end; time = time.Add(interval))
        {
            slots.Add(new TimeSlot
            {
                UnitId = unitId,
                SpecialtyId = specialtyId,
                Date = date.Date,
                StartTime = time,
                EndTime = time.Add(interval),
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            });
        }

        foreach (var slot in slots)
            await _timeSlotRepository.AddAsync(slot);
    }

    public async Task<WeeklyAvailableSlotsDto> GetWeeklyAvailableSlotsAsync(int unitId, int specialtyId, DateTime weekStart, DateTime weekEnd)
    {
        // 1 — GERA OS SLOTS DO DIA SE NECESSÁRIO
        for (DateTime date = weekStart; date <= weekEnd; date = date.AddDays(1))
        {
            await GenerateDailySlotsAsync(unitId, specialtyId, date);
        }

        // 2 — CARREGA TODOS OS SLOTS DA SEMANA
        var slots = await _timeSlotRepository.GetWeeklyAvailableSlotsAsync(
            unitId, specialtyId, weekStart, weekEnd
        );

        // 3 — CASO NÃO TENHA NADA
        if (!slots.Any())
        {
            return new WeeklyAvailableSlotsDto
            {
                UnitId = unitId,
                SpecialtyId = specialtyId,
                WeekStart = weekStart,
                WeekEnd = weekEnd,
                Days = new List<DayAvailableSlotsDto>()
            };
        }

        // 4 — AGRUPA POR DIA E MONTA OS DTOs
        var groupedByDay = slots
            .GroupBy(s => s.Date.Date)
            .OrderBy(g => g.Key)
            .Select(g => new DayAvailableSlotsDto
            {
                Date = g.Key,
                DayOfWeek = g.Key.ToString("dddd", new System.Globalization.CultureInfo("pt-BR")),
                IsAvailable = g.Any(x => x.IsAvailable),
                AvailableTimes = g
                    .Select(x => new TimeSlotInfoDto
                    {
                        StartTime = x.StartTime,
                        IsAvailable = x.IsAvailable
                    })
                    .ToList()
            })
            .ToList();

        // 5 — RETORNA O DTO FINAL
        return new WeeklyAvailableSlotsDto
        {
            UnitId = unitId,
            SpecialtyId = specialtyId,
            WeekStart = weekStart,
            WeekEnd = weekEnd,
            Days = groupedByDay
        };
    }
}
