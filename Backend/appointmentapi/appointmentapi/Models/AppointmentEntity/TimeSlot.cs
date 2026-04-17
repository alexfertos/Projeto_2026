namespace appointmentapi.Models.AppointmentEntity;

public class TimeSlot
{
    public int Id { get; set; }

    public int UnitId { get; set; }
    public Unit Unit { get; set; }

    public int SpecialtyId { get; set; }
    public Specialty Specialty { get; set; }

    public DateTime Date { get; set; }   // dia específico
    public TimeSpan StartTime { get; set; } // hora de início
    public TimeSpan EndTime { get; set; }   // hora de fim
    public bool IsAvailable { get; set; } = true;

    public int? AppointmentId { get; set; }
    public Appointment? Appointment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
