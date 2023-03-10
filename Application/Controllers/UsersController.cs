using Domain.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDTO user)
    {
        try
        {
            var exists = await _service.CheckUserExists(user.Email);
            if (!exists)
            {
                return BadRequest();
            }

            var result = await _service.Register(user);
            if(!result){
                return BadRequest();
            }
            return Ok();
        }
        catch (FluentValidation.ValidationException)
        { 
            return BadRequest();
        }
        
    }
}
