using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using DTO;
using FluentValidation;
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
        Validate(newuser,Activator.CreateInstance<UserValidate>());
        try
        {
            User user = _mapper.Map<User>(newuser);
            var result = await _manager.CreateAsync(user,newuser.Password);  
            return result.Succeeded;
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }

    private void Validate(RegisterDTO entity, AbstractValidator<RegisterDTO> validator)
    {
        if(entity ==null){
            throw new Exception("Registro não detectado");
        }

        validator.ValidateAndThrow(entity);
    }
}
