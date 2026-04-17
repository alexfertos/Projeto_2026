using appointmentapi.DTOs.SpecialtyDTO;
using appointmentapi.Repositories.Interface.AppointmentInterface;
using appointmentapi.Services.Interface;

namespace appointmentapi.Services.Specialty;

public class SpecialtyService : ISpecialtyService
{
    private readonly ISpecialtyRepository _specialtyRepository;

    public SpecialtyService(ISpecialtyRepository specialtyRepository)
    {
        _specialtyRepository = specialtyRepository;
    }

    public async Task<ICollection<SpecialtyDto>> GetAllSpecialtiesAsync()
    {
        var specialties = await _specialtyRepository.GetAllAsync();

        var result = specialties.Select(s => new SpecialtyDto
        {
            Id = s.Id,
            Name = s.Name
        }).ToList();
       
        return result;
    }
}
