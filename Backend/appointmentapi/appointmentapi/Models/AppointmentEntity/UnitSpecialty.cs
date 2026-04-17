namespace appointmentapi.Models.AppointmentEntity;

public class UnitSpecialty
{
    public int Id { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
    public int SpecialtyId { get; set; }
    public Specialty Specialty { get; set; }
}
