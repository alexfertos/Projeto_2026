using appointmentapi.Models.AppointmentEntity;

namespace appointmentapi.Repositories.Interface.AppointmentInterface
{
    public interface ISpecialtyRepository
    {
        Task<ICollection<Specialty>> GetAllAsync();
        Task<Specialty?> GetByIdAsync(int id); 
    }
}
