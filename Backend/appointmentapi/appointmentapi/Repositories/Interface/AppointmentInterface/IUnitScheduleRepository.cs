using appointmentapi.Models.AppointmentEntity;

namespace appointmentapi.Repositories.Interface.AppointmentInterface
{
    public interface IUnitScheduleRepository
    {
        Task<ICollection<UnitSchedule>> GetAllAsync();
        Task<ICollection<UnitSchedule>> GetByUnitIdAsync(int unitId);
        Task<UnitSchedule?> GetByIdAsync(int id);
    }
}
