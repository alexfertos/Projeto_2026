namespace appointmentapi.DTOs.TimeSchedule;

public class DayAvailableSlotsDto
{
    public DateTime Date { get; set; }                 // ex: 2025-11-17 (ISO)
    public string DayOfWeek { get; set; }              // ex: "Segunda-feira"
    public bool IsAvailable { get; set; }              // true se houver pelo menos 1 timeslot disponível naquele dia
    public IList<TimeSlotInfoDto> AvailableTimes { get; set; }
}