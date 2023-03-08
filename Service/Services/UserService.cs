using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using DTO;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Service.Validators;

namespace Service.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _manager;
    private readonly IMapper _mapper;
    private readonly SignInManager<User> _signin;

    public UserService(UserManager<User> manager, IMapper mapper, SignInManager<User> signIn)
    {
        _manager = manager;
        _mapper = mapper;
        _signin = signIn;
    }

    public Task Delete(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Login(LoginDTO loguser)
    {
        try
        {
            Validation(loguser);
            new LoginValidator().ValidateAndThrow(loguser);
            var user = await  _manager.FindByEmailAsync(loguser.Email);
            var result = await _signin.PasswordSignInAsync(user,loguser.Password,false,false);
            return result.Succeeded;
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
}
