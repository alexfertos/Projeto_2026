using appointmentapi.DTOs.UnitSpecialty;
using appointmentapi.Models.AppointmentEntity;
using appointmentapi.DTOs.TimeSchedule;

namespace appointmentapi.Services.Interface;

public interface IUnitService
{
    Task<ICollection<UnitDto>> GetUnitsBySpecialtyAsync(int specialtyId);
    Task<AvailableSlotDto> GetAvailableSlotsAsync(int unitId, int specialtyId, DateTime date);
}
