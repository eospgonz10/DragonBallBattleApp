using DragonBallBattles.Application.DTOs;
using DragonBallBattles.Application.Services;
using DragonBallBattles.Domain.Interfaces;
using DragonBallBattles.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DragonBallBattles.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class BattlesController : ControllerBase
{
    private readonly IBattleScheduler _battleScheduler;
    private readonly ICharacterRepository _characterRepository;

    public BattlesController(IBattleScheduler battleScheduler, ICharacterRepository characterRepository)
    {
        _battleScheduler = battleScheduler;
        _characterRepository = characterRepository;
    }

    [HttpPost("{numeroParticipantes}/schedule")]
    public async Task<IActionResult> ScheduleBattles(int numeroParticipantes)
    {
        try
        {
            var characters = await _characterRepository.GetCharactersAsync(numeroParticipantes);
            var startDate = DateTime.UtcNow.Date.AddDays(30);
            var battles = _battleScheduler.ScheduleBattles(characters, startDate);
            var result = new
            {
                batallas = battles.Select(b => new BattleDto
                {
                    Batalla = b.GetBattleName(),
                    Fecha = b.Date.ToString("yyyy/MM/dd")
                }).ToList()
            };
            return Ok(result);
        }
        catch (DomainException ex)
        {
            // Excepciones de negocio - devolver BadRequest sin loguear como error
            return BadRequest(new { success = false, error = ex.Message });
        }
    }
}
