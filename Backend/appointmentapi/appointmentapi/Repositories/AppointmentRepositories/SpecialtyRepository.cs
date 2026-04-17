using appointmentapi.Data;
using appointmentapi.Models.AppointmentEntity;
using appointmentapi.Repositories.Interface;
using appointmentapi.Repositories.Interface.AppointmentInterface;
using Microsoft.EntityFrameworkCore;

namespace appointmentapi.Repositories.AppointmentRepositories;

public class SpecialtyRepository : ISpecialtyRepository
{
    private readonly AppDbContext _context;
    public SpecialtyRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ICollection<Specialty>> GetAllAsync()
    {
        return await _context.Specialties.AsNoTracking()
            .Include(s => s.UnitSpecialties)
            .ThenInclude(us => us.Unit)
            .ToListAsync();
    }

    public async Task<Specialty?> GetByIdAsync(int id)
    {
        return await _context.Specialties
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}
