using Domain.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService service, ILogger<UsersController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDTO user)
    {
        try
        {
            _logger.LogInformation("Iniciando serviço de registro de usuário");
            var exists = await _service.CheckUserExists(user.Email);
            if (exists)
            {
                _logger.LogWarning($"Usuario com email: {user.Email}, já existente");
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
                _logger.LogError($"Erro no serviço de registro de usuário {user.Email}");
                return BadRequest();
            }
            _logger.LogInformation($"Sucesso na operação de registro do usuário {user.Email}");
            return Ok();
        }
        catch (FluentValidation.ValidationException)
        { 
            _logger.LogError($"Erro de validação com o usuário {user.Email}");
            ModelState.AddModelError("Condições","Formulário com uma ou mais informações com má formatação");
            return BadRequest();
        }
        
    }

    [Route("login")]
    [HttpPost]
    public async Task<ActionResult> Login([FromBody] LoginDTO user)
    {
        try
        {
            _logger.LogInformation($"Iniciando serviço de Login do usuário {user.Email}");
            var result = await _service.Login(user);
            if(result is null)
            {
                _logger.LogError($"Falha no login do usuário {user.Email}");
                return BadRequest();
            }
            _logger.LogInformation($"Sucesso no login do usuário {user.Email}");
            return Ok(result);
        }
        catch (FluentValidation.ValidationException)
        {
            ModelState.AddModelError("Login","Login inválido");
            return BadRequest(ModelState);
        }
    }
    [Route("logout")]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        try
        {
            _logger.LogInformation("Iniciando processo de logout do usuário");
            await _service.Logout();
            _logger.LogInformation("Sucesso no Logout no usuário");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Falha no processo de logout", ex);
            return BadRequest();
        }
    }

    [Route("delete")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] string email)
    {
        try
        {
            _logger.LogInformation($"Iniciando serviço para deletar usuario {email}");
            var result = await _service.Delete(email);
            if(!result){
                _logger.LogError($"Falha no processo de deleção do usuário {email}");
                return BadRequest();
            }
            _logger.LogInformation($"Deleção do usuário {email} realizada com sucesso");
            return Ok();
        }
        catch (System.Exception)
        {
            
            return BadRequest(); 
        }
    }
}
