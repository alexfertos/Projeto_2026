using appointmentapi.Models.AppointmentEntity;

namespace appointmentapi.Repositories.Interface.AppointmentInterface
{
    public interface IAppointmentRepository
    {
        Task<Appointment> AddAsync(Appointment appointment);          // Criar um novo agendamento
        Task<Appointment?> GetByIdAsync(int id);                     // Obter um agendamento pelo Id
        Task<ICollection<Appointment>> GetByPersonIdAsync(int personId); // Obter todos os agendamentos de uma pessoa
        Task<ICollection<Appointment>> GetByUnitIdAsync(int unitId);     // Obter todos os agendamentos de uma unidade
    }
}
