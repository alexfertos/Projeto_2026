namespace appointmentapi.DTOs.TimeSchedule;

public class AvailableSlotDto
{
    public int UnitId { get; set; }
    public int SpecialtyId { get; set; }
    public bool IsAvailable { get; set; }
    public IList<TimeSlotInfoDto> AvailableTimes { get; set; }
}
