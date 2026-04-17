using appointmentapi.Data;
using appointmentapi.Models.AppointmentEntity;
using appointmentapi.Repositories.Interface.AppointmentInterface;
using Microsoft.EntityFrameworkCore;

namespace appointmentapi.Repositories.AppointmentRepositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;

    public async Task<Appointment> AddAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment?> GetByIdAsync(int id)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Include(a => a.Person)
            .Include(a => a.Unit)
            .Include(a => a.Specialty)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<ICollection<Appointment>> GetByPersonIdAsync(int personId)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Include(a => a.Unit)
            .Include(a => a.Specialty)
            .Where(a => a.PersonId == personId)
            .ToListAsync();
    }

    public async Task<ICollection<Appointment>> GetByUnitIdAsync(int unitId)
    {
        return await _context.Appointments
          .AsNoTracking()
          .Include(a => a.Person)
          .Include(a => a.Specialty)
          .Where(a => a.UnitId == unitId)
          .ToListAsync();
    }

}
