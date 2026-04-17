using appointmentapi.Models.AppointmentEntity;
using appointmentapi.Models.AuthEntity;

namespace appointmentapi.DTOs.AppointmentDTO;

public class AppointmentDTO
{
    public int PersonId { get; set; }
    public int SpecialtyId { get; set; }
    public int UnitId { get; set; }
    public int AddressId { get; set; }
    public int PhoneId { get; set; }
    public DateTime Date { get; set; }

}
