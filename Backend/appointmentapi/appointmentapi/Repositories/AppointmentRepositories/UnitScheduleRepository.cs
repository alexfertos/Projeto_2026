using appointmentapi.Data;
using appointmentapi.Models.AppointmentEntity;
using appointmentapi.Repositories.Interface.AppointmentInterface;
using Microsoft.EntityFrameworkCore;

namespace appointmentapi.Repositories.AppointmentRepositories;

public class UnitScheduleRepository : IUnitScheduleRepository
{
    private readonly AppDbContext _context;

    public UnitScheduleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<UnitSchedule>> GetAllAsync()
    {
        return await _context.UnitSchedules
            .AsNoTracking()
            .Include(us => us.Unit)
            .ToListAsync();
    }

    public async Task<UnitSchedule?> GetByIdAsync(int id)
    {
        return await _context.UnitSchedules
            .AsNoTracking()
            .Include(us => us.Unit)
            .FirstOrDefaultAsync(us => us.Id == id);
    }

    public async Task<ICollection<UnitSchedule>> GetByUnitIdAsync(int unitId)
    {
        return await _context.UnitSchedules
            .AsNoTracking()
            .Where(us => us.UnitId == unitId)
            .Include(us => us.Unit)
            .ToListAsync();
    }
}
