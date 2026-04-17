using appointmentapi.Models.AppointmentEntity;

namespace appointmentapi.Repositories.Interface.AppointmentInterface;

public interface IUnitRepository
{
    Task<ICollection<Unit>> GetAllAsync();
    Task<Unit?> GetByIdWithDetailsAsync(int id);
    Task<ICollection<Unit>> GetUnitsBySpecialtyAsync(int specialtyId);
    Task<ICollection<TimeSpan>> GetAvailableSlotsAsync(int unitId, int specialtyId, DateTime date);
}
