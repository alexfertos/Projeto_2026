namespace appointmentapi.Models.AppointmentEntity;

public class UnitSchedule
{
    public int Id { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
    public int DayOfWeek { get; set; }
    public int SlotDurationMinutes { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; }
}

//💡 Exemplo de dados:
//Segunda | 08:00 | 17:00 | Disponível ✅
//Sábado | 08:00 | 12:00 | Indisponível ❌
