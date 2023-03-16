using System.Security.Claims;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using DTO;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Service.Validators;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Service.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _manager;
    private readonly IMapper _mapper;
    private readonly SignInManager<User> _signin;
    private readonly IConfiguration _configuration;

    public UserService(UserManager<User> manager, IMapper mapper, SignInManager<User> signIn, IConfiguration config)
    {
        _manager = manager;
        _mapper = mapper;
        _signin = signIn;
        _configuration = config;
    }

    public async Task<bool> Delete(string email)
    {
        try
        {
            User user = await _manager.FindByEmailAsync(email);
            var result = await _manager.DeleteAsync(user);
            return result.Succeeded;
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }

    public async Task<string> Login(LoginDTO loguser)
    {
        try
        {
            Validation(loguser);
            new LoginValidator().ValidateAndThrow(loguser);
            var user = await  _manager.FindByEmailAsync(loguser.Email);
            var result = await _signin.PasswordSignInAsync(user,loguser.Password,false,false);
            if(result.Succeeded)
            {
                var token = GenerateToken(user);
                return token;

            }
            return null;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public async Task Logout()
    {
        try
        {
            await _signin.SignOutAsync();
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }

    public async Task<bool> Register(RegisterDTO newuser)
    {
        try
        {
            Validation(newuser);
            new RegisterValidator().ValidateAndThrow(newuser);
            User user = _mapper.Map<User>(newuser);
            var result = await _manager.CreateAsync(user,newuser.Password);  
            return result.Succeeded;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<bool> CheckUserExists(string email)
    {
        try
        {
            var user = await _manager.FindByEmailAsync(email);
            if(user == null)
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }

    private void Validation(UserDTO entity)
    {
        if(entity ==null){
            throw new ArgumentNullException("Formulário vazio.");
        }   
    }

    private string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWT:Expiremin"]));

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

        var secToken = new JwtSecurityTokenHandler().WriteToken(token);
        return secToken;
    }
}
