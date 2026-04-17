using appointmentapi.DTOs.SpecialtyDTO;

namespace appointmentapi.Services.Interface;

public interface ISpecialtyService
{
    Task<ICollection<SpecialtyDto>> GetAllSpecialtiesAsync();
}
