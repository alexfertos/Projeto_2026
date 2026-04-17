using appointmentapi.Models.AuthEntity;

namespace appointmentapi.Repositories.Interface.AuthInterface;

public interface IPersonRepository
{
    Task<Person> AddPersonAsync(Person person);
    Task<Person?> GetByEmailAsync(string email);
}
