using appointmentapi.Models.AppointmentEntity;

namespace appointmentapi.Models.AuthEntity;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int? PhoneId { get; set; }
    public Phone Phone { get; set; }

    public DateTime BirthDate { get; set; }
    public string CPF { get; set; }

}
