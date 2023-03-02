using Domain.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoodDayController : ControllerBase
{
    private readonly IMoodDayService _dayservice;

    public MoodDayController(IMoodDayService service)
    {
        _dayservice = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var objects = await _dayservice.GetAll();
        if(objects == null){
            return BadRequest();
        }
        return Ok(objects);
    }

    [HttpPost]
    public async Task<IActionResult> NewDay([FromBody] MoodDayDTO day)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }
        MoodDayDTO newday = await _dayservice.Add(day);
        return Ok(newday);
    }
}
