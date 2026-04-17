namespace appointmentapi.DTOs.TimeSchedule;

public class TimeSlotInfoDto
{
    public TimeSpan StartTime { get; set; }
    public bool IsAvailable { get; set; }
}
