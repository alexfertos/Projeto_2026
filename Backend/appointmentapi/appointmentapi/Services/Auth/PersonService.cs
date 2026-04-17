using appointmentapi.Models.AppointmentEntity;
using appointmentapi.Models.AuthEntity;
using appointmentapi.Repositories.Interface.AuthInterface;

namespace appointmentapi.Services.Auth
{
    public class PersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> CreatePersonAsync(string fullName, string email, string phoneNumber, string cpf, DateTime birthdate)
        {
            var existing = await _personRepository.GetByEmailAsync(email);
            if(existing != null)
                throw new Exception("Email already in use");

            var phone = new Phone
            {
                Number = phoneNumber
            };

            var newPerson = new Person
            {
                Name = fullName,
                Email = email,
                Phone = phone,
                CPF = cpf,
                BirthDate = birthdate
                
            };

            await _personRepository.AddPersonAsync(newPerson);
            return newPerson;
        }

        public Task<Person?> GetByEmailAsync(string email)
        {
            return _personRepository.GetByEmailAsync(email);
        }
    }
}
