using appointmentapi.DTOs.TimeSchedule;
using appointmentapi.DTOs.UnitSpecialty;

namespace appointmentapi.Services.Interface;

public interface ITimeSlotService
{
    Task<AvailableSlotDto> GetAvailableSlotsAsync(int unitId, int specialtyId, DateTime date);
    Task GenerateDailySlotsAsync(int unitId, int specialtyId, DateTime date);
    Task<WeeklyAvailableSlotsDto> GetWeeklyAvailableSlotsAsync(int unitId, int specialtyId, DateTime weekStart, DateTime weekEnd);
}
