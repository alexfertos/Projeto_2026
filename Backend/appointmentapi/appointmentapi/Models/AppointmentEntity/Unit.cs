namespace appointmentapi.Models.AppointmentEntity;

public class Unit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public int PhoneId { get; set; }
    public Phone Phone { get; set; }
    public ICollection<UnitSpecialty> UnitSpecialties { get; set; }
    public ICollection<UnitSchedule> UnitSchedules { get; set; }

}
