using appointmentapi.Repositories.Interface.AppointmentInterface;
using appointmentapi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace appointmentapi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TimeSlotController : ControllerBase
{
    private readonly ITimeSlotService _timeSlotService;

    public TimeSlotController(ITimeSlotService timeSlotService)
    {
        _timeSlotService = timeSlotService;
    }
    [HttpGet("Available")]
    public async Task<IActionResult> GetAvaibleSlots(
        [FromQuery] int unitId,
        [FromQuery] int specialtyId, 
        [FromQuery] DateTime date)
    {
        var result = await _timeSlotService.GetAvailableSlotsAsync(unitId, specialtyId, date);

        if (result == null || !result.AvailableTimes.Any())
            return NotFound("Nenhum horário disponível para essa data.");
        if (result == null || !result.AvailableTimes.Any())
                return NotFound("Nenhum horário disponível para essa data.");

        return Ok(result);
    }

    [HttpGet("WeeklyAvailable")]
    public async Task<IActionResult> GetWeeklyAvailableSlots(
    [FromQuery] int unitId,
    [FromQuery] int specialtyId,
    [FromQuery] DateTime weekStart)
    {
        // Calcula o final da semana (sábado)
        var weekEnd = weekStart.AddDays(5); // segunda a sábado

        var result = await _timeSlotService.GetWeeklyAvailableSlotsAsync(unitId, specialtyId, weekStart, weekEnd);

        if (result == null || result.Days == null || !result.Days.Any())
            return NotFound("Nenhum horário disponível para esta semana.");

        return Ok(result);
    }

    [HttpPost("Generate")]
    public async Task<IActionResult> GenerateDailySlots(
            [FromQuery] int unitId,
            [FromQuery] int specialtyId,
            [FromQuery] DateTime date)
    {
        await _timeSlotService.GenerateDailySlotsAsync(unitId, specialtyId, date);
        return Ok("Horários do dia gerados com sucesso.");
    }

}
