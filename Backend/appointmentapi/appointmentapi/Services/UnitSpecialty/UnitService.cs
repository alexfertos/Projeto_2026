using appointmentapi.DTOs.TimeSchedule;
using appointmentapi.DTOs.UnitSpecialty;
using appointmentapi.Models.AppointmentEntity;
using appointmentapi.Repositories.AppointmentRepositories;
using appointmentapi.Repositories.Interface.AppointmentInterface;
using appointmentapi.Services.Interface;

namespace appointmentapi.Services.UnitSpecialty
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public UnitService(IUnitRepository unitRepository, ITimeSlotRepository timeSlotRepository)
        {
            _unitRepository = unitRepository;
            _timeSlotRepository = timeSlotRepository;
        }
        public async Task<AvailableSlotDto> GetAvailableSlotsAsync(int unitId, int specialtyId, DateTime date)
        {
            var slots = await _timeSlotRepository.GetAvailableSlotsAsync(unitId, specialtyId, date);

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


        public async Task<ICollection<UnitDto>> GetUnitsBySpecialtyAsync(int specialtyId)
        {
            var units = await _unitRepository.GetUnitsBySpecialtyAsync(specialtyId);

            return units.Select(u => new UnitDto
            {
                Id = u.Id,
                Name = u.Name,
                FullAddress = $"{u.Address?.Name}, {u.Address?.Number}, {u.Address?.City} - {u.Address?.State}, {u.Address?.ZipCode}",
                Phone = u.Phone?.Number
            }).ToList();
        }
    }
}
