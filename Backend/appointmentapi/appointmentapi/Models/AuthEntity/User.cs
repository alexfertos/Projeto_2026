namespace appointmentapi.Models.AuthEntity;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; } 
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public bool Active { get; set; }
    public int PersonId { get; set; } 
    public Person Person { get; set; }
}
