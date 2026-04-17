using appointmentapi.Data;
using appointmentapi.Models.AppointmentEntity;
using appointmentapi.Repositories.Interface.AppointmentInterface;
using Microsoft.EntityFrameworkCore;

namespace appointmentapi.Repositories.AppointmentRepositories;

public class TimeSlotRepository : ITimeSlotRepository
{
    private readonly AppDbContext _context;

    public TimeSlotRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<TimeSlot>> GetAvailableSlotsAsync(int unitId, int specialtyId, DateTime date)
    {
        return await _context.TimeSlots
            .AsNoTracking()
            .Where(ts => ts.UnitId == unitId
                      && ts.SpecialtyId == specialtyId
                      && ts.Date.Date == date.Date)
            .OrderBy(ts => ts.StartTime)
            .ToListAsync();
    }

    public async Task<ICollection<TimeSlot>> GetWeeklyAvailableSlotsAsync(int unitId, int specialtyId, DateTime weekStart, DateTime weekEnd)
    {
        return await _context.TimeSlots
            .AsNoTracking()
            .Where(ts => ts.UnitId == unitId
                      && ts.SpecialtyId == specialtyId
                      && ts.Date >= weekStart
                      && ts.Date <= weekEnd)
            .OrderBy(ts => ts.Date)
            .ThenBy(ts => ts.StartTime)
            .ToListAsync();
    }

    public async Task<TimeSlot?> GetByIdAsync(int id)
    {
        return await _context.TimeSlots
            .AsNoTracking()
            .FirstOrDefaultAsync(ts => ts.Id == id);
    }

    public async Task<TimeSlot> AddAsync(TimeSlot timeSlot)
    {
        _context.TimeSlots.Add(timeSlot);
        await _context.SaveChangesAsync();
        return timeSlot;
    }

    public async Task UpdateAvailabilityAsync(int id, bool isAvailable)
    {
        var slot = await _context.TimeSlots.FindAsync(id);
        if (slot != null)
        {
            slot.IsAvailable = isAvailable;
            slot.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }


}

