
using AutoMapper;
using Domain.Entity;
using DTO;
using Microsoft.AspNetCore.Identity;
using Service.Services;

namespace Mooder.UnitTests.Services;

public class UserServicesTests
{
    #region CheckUserExists

    [Fact]
    public async Task CheckUserExists_InvokeUserManager_Success()
    {
        // Given
        var config = new MapperConfiguration(cfg=>{

        });
        var mapper = config.CreateMapper();

        var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        mockUserManager.Setup(service=>service.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync(new User());
        var userService = new UserService(mockUserManager.Object,mapper);
        // When
        var result = await userService.CheckUserExists("teste");
        // Then
        mockUserManager.Verify(service=>service.FindByEmailAsync(It.IsAny<string>()),Times.Once());
    }

    [Fact]
    public async Task CheckUserExists_ValidEmail_UserExist()
    {
        // Given
        var config = new MapperConfiguration(cfg=>{

        });
        var mapper = config.CreateMapper();

        var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        mockUserManager.Setup(service=>service.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync(new User());
        var userService = new UserService(mockUserManager.Object,mapper);
        // When
        var result = await userService.CheckUserExists("teste");
        // Then
        Assert.True(result);
    }

    [Fact]
    public async Task CheckUserExists_ValidEmail_UserDontExist()
    {
        // Given
        var config = new MapperConfiguration(cfg=>{

        });
        var mapper = config.CreateMapper();

        var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        mockUserManager.Setup(service=>service.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync((User) null);
        var userService = new UserService(mockUserManager.Object,mapper);
        // When
        var result = await userService.CheckUserExists("teste");
        // Then
        Assert.False(result);
    }

    #endregion


    #region Register
    [Fact]
    public async Task Register_ValidUser_Success()
    {
        // Given
        var config = new MapperConfiguration(cfg=>{
            cfg.CreateMap<User,RegisterDTO>().ReverseMap();
        });
        var mapper = config.CreateMapper();

        var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        mockUserManager.Setup(service=>service.CreateAsync(It.IsAny<User>(),It.IsAny<string>()))
                        .ReturnsAsync(IdentityResult.Success);
        var userService = new UserService(mockUserManager.Object,mapper);

        var user = new RegisterDTO(){
            UserName = "testecom8dig",
            Email = "teste@gmail.com",
            Password = "Teste#1234",
            ConfirmPassword = "Teste#1234"
        };
        // When
        var result = await userService.Register(user);
        // Then
        Assert.True(result);
    }
    [Fact]
    public async Task Register_ValidUser_Failure()
    {
        // Given
        var config = new MapperConfiguration(cfg=>{
            cfg.CreateMap<User,RegisterDTO>().ReverseMap();
        });
        var mapper = config.CreateMapper();

        var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        mockUserManager.Setup(service=>service.CreateAsync(It.IsAny<User>(),It.IsAny<string>()))
                        .ReturnsAsync(IdentityResult.Failed(new IdentityError{
                                                            Code="0001",
                                                            Description="Error"}));
        var userService = new UserService(mockUserManager.Object,mapper);

        var user = new RegisterDTO(){
            UserName = "testecom8dig",
            Email = "teste@gmail.com",
            Password = "Teste#1234",
            ConfirmPassword = "Teste#1234"
        };
        // When
        var result = await userService.Register(user);
        // Then
        Assert.False(result);
    }

    [Fact]
    public async Task Register_NullUser_Exception()
    {
        // Given
        var config = new MapperConfiguration(cfg=>{
            cfg.CreateMap<User,RegisterDTO>().ReverseMap();
        });
        var mapper = config.CreateMapper();

        var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        mockUserManager.Setup(service=>service.CreateAsync(It.IsAny<User>(),It.IsAny<string>()))
                        .ReturnsAsync(IdentityResult.Success);
        var userService = new UserService(mockUserManager.Object,mapper);
        // When
        var exception = await Record.ExceptionAsync(()=> userService.Register(null));
        // Then
        Assert.IsType<ArgumentNullException>(exception);
    }
    [Fact]
    public async Task Register_InvalidUser_Exception()
    {
        // Given
        var config = new MapperConfiguration(cfg=>{
            cfg.CreateMap<User,RegisterDTO>().ReverseMap();
        });
        var mapper = config.CreateMapper();

        var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        mockUserManager.Setup(service=>service.CreateAsync(It.IsAny<User>(),It.IsAny<string>()))
                        .ReturnsAsync(IdentityResult.Success);
        var userService = new UserService(mockUserManager.Object,mapper);

        RegisterDTO invalidUser = new RegisterDTO(){
            UserName="teste",
            Email="teste",
            Password = "Teste#1234",
            ConfirmPassword = "Teste#1234"
        };
        // When
        var exception = await Record.ExceptionAsync(()=> userService.Register(invalidUser));
        // Then
        Assert.IsType<FluentValidation.ValidationException>(exception);
    }

    #endregion


    #region Login
        
        [Fact]
        public void Login_ValidUser_Succes()
        {
            // Given
            var config = new MapperConfiguration(cfg=>{
                cfg.CreateMap<User,LoginDTO>().ReverseMap();
            });
            var mapper = config.CreateMapper();

            var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            mockUserManager.Setup(service=>service.CreateAsync(It.IsAny<User>(),It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Success);
            var userService = new UserService(mockUserManager.Object,mapper);
            // When
        
            // Then
        }
    #endregion
}
