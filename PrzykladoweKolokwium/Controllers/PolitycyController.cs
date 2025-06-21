using Microsoft.AspNetCore.Mvc;
using PrzykładoweKolokwium.DTOs;
using PrzykładoweKolokwium.Models;
using PrzykładoweKolokwium.Services;

namespace PrzykładoweKolokwium.Controllers;

[ApiController]
[Route("[controller]")]
public class PolitycyController(IDbService dbService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPolitykDetails()
    {
        return Ok(await dbService.GetPolitykDetails());
    }

    [HttpPost]
    public async Task<IActionResult> CreatePolityk([FromBody] CreatePolitykRequest request)
    {
        try
        {
            var polityk = await dbService.CreatePolityk(request);
            return Created($"polityk/{polityk.Id}", polityk);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}