using appointmentapi.DTOs.UnitSpecialty;
using appointmentapi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace appointmentapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnitSpecialtyController : Controller
{
    private readonly ISpecialtyService _specialtyService;
    private readonly IUnitService _unitService;

    public UnitSpecialtyController(ISpecialtyService specialtyService, IUnitService unitService)
    {
        _specialtyService = specialtyService;
        _unitService = unitService;
    }

    [HttpGet("Specialties")]
    public async Task<IActionResult> GetSpecialties()
    {
        var specialties = await _specialtyService.GetAllSpecialtiesAsync();

        return Ok(specialties);
    }

    [HttpGet("specialty/{specialtyId}")]
    public async Task<ActionResult<ICollection<UnitDto>>> GetUnitsBySpecialty(int specialtyId)
    {
        var units = await _unitService.GetUnitsBySpecialtyAsync(specialtyId);
        return Ok(units);
    }

}
