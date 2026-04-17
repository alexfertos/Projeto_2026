using appointmentapi.Data;
using appointmentapi.Models.AuthEntity;
using appointmentapi.Repositories.Interface.AuthInterface;
using Microsoft.EntityFrameworkCore;

namespace appointmentapi.Repositories.Auth;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Person> AddPersonAsync(Person person)
    {
        await _context.Persons.AddAsync(person);
        await _context.SaveChangesAsync();

        return person;
    }

    public Task<Person?> GetByEmailAsync(string email)
    {
        return _context.Persons.FirstOrDefaultAsync(p => p.Email == email);
    }

}
