using appointmentapi.Data;
using appointmentapi.Models.AppointmentEntity;
using appointmentapi.Repositories.Interface.AppointmentInterface;
using Microsoft.EntityFrameworkCore;

namespace appointmentapi.Repositories.AppointmentRepositories;

public class UnitRepository : IUnitRepository
{
    private readonly AppDbContext _context;

    public UnitRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Unit>> GetAllAsync()
    {
        return await _context.Units
             .AsNoTracking()
             .Include(u => u.Address)
             .Include(u => u.UnitSpecialties)
                 .ThenInclude(us => us.Specialty)
             .ToListAsync();
    }

    public async Task<Unit?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Units
            .AsNoTracking()
            .Include(u => u.Address)
            .Include(u => u.UnitSpecialties)
                .ThenInclude(us => us.Specialty)
            .Include(u => u.UnitSchedules)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<ICollection<TimeSpan>> GetAvailableSlotsAsync(int unitId, int specialtyId, DateTime date)
    {
        var diaSemana = (int)date.DayOfWeek; // 0=Domingo ... 6=Sábado

       var schedules = await _context.UnitSchedules
        .Where(us => us.UnitId == unitId 
                  && us.DayOfWeek == diaSemana 
                  && us.IsAvailable)
        .Select(us => us.StartTime)
        .ToListAsync();

        if (schedules == null || !schedules.Any())
            return new List<TimeSpan>();

        var agendamentos = await _context.Appointments
        .Where(a => a.UnitId == unitId
                 && a.SpecialtyId == specialtyId
                 && a.Date.Date == date.Date)
        .Select(a => a.Date.TimeOfDay)
        .ToListAsync();

        var disponiveis = schedules.Except(agendamentos).ToList();
        return disponiveis;
    }


    public async Task<ICollection<Unit>> GetUnitsBySpecialtyAsync(int specialtyId)
    {
        return await _context.Units
            .AsNoTracking()
            .Include(u => u.Address)
            .Include(u => u.Phone)
            .Include(u => u.UnitSpecialties)
                .ThenInclude(us => us.Specialty)
            .Where(u => u.UnitSpecialties.Any(us => us.SpecialtyId == specialtyId))
            .ToListAsync();
    }
}
