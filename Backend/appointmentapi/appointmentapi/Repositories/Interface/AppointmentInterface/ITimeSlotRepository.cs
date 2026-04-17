using appointmentapi.Models.AppointmentEntity;

namespace appointmentapi.Repositories.Interface.AppointmentInterface;

public interface ITimeSlotRepository
{
    Task<ICollection<TimeSlot>> GetAvailableSlotsAsync(int unitId, int specialtyId, DateTime date);
    Task<ICollection<TimeSlot>> GetWeeklyAvailableSlotsAsync(int unitId, int specialtyId, DateTime weekStart, DateTime weekEnd);
    Task<TimeSlot?> GetByIdAsync(int id);
    Task<TimeSlot> AddAsync(TimeSlot timeSlot);
    Task UpdateAvailabilityAsync(int id, bool isAvailable);

}
