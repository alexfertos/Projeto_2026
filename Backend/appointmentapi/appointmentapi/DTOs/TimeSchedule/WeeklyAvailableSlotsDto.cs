namespace appointmentapi.DTOs.TimeSchedule;

public class WeeklyAvailableSlotsDto
{
    public int UnitId { get; set; }
    public int SpecialtyId { get; set; }
    public DateTime WeekStart { get; set; }            // data inicio (segunda)
    public DateTime WeekEnd { get; set; }              // data fim (sábado)
    public IList<DayAvailableSlotsDto> Days { get; set; }  // 6 itens (segunda→sábado)
}
