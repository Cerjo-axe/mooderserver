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

    public UserService(UserManager<User> manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public Task Delete(string email)
    {
        throw new NotImplementedException();
    }

    public Task Login(LoginDTO loguser)
    {
        throw new NotImplementedException();
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Register(RegisterDTO newuser)
    {
        try
        {
            Validation(newuser,new UserValidator());
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

    private void Validation(RegisterDTO entity, AbstractValidator<RegisterDTO> validator)
    {
        try
        {
            if(entity ==null){
                throw new ArgumentNullException("Formulário vazio.");
            }

            validator.ValidateAndThrow(entity);     
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }
}
