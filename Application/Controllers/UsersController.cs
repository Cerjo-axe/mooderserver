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
                ModelState.AddModelError("Email","Email já utilizado por outro usuário");
                return BadRequest(ModelState);
            }
            if(user.Password != user.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword","Senhas não coincidem");
                return BadRequest(ModelState);
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

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDTO user)
    {
        try
        {
            var result = await _service.Login(user);
            if(!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        catch (FluentValidation.ValidationException)
        {
            ModelState.AddModelError("Login","Login inválido");
            return BadRequest(ModelState);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        try
        {
            _service.Logout();
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] string email)
    {
        try
        {
            var result = await _service.Delete(email);
            if(!result){
                return BadRequest();
            }
            return Ok();
        }
        catch (System.Exception)
        {
            
            return BadRequest(); 
        }
    }
}
