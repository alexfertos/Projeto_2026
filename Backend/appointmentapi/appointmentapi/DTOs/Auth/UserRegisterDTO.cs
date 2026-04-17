namespace appointmentapi.DTOs.Auth;

public class UserRegisterDTO
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string CPF { get; set; }
    public DateTime Birthdate { get; set; }
    public string Password { get; set; }
}
