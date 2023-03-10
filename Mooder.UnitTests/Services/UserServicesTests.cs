
using AutoMapper;
using Domain.Entity;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Mooder.UnitTests.Helpers;
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

        var mockUserManager = GetMockUserManager();
        mockUserManager.Setup(service=>service.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync(new User());
        var mockSignInManager = GetMockSignInManager(mockUserManager);
        var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);
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

        var mockUserManager = GetMockUserManager();
        mockUserManager.Setup(service=>service.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync(new User());
        var mockSignInManager = GetMockSignInManager(mockUserManager);
        var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);
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

        var mockUserManager = GetMockUserManager();
        mockUserManager.Setup(service=>service.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync((User) null);
        var mockSignInManager = GetMockSignInManager(mockUserManager);
        var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);
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

        var mockUserManager = GetMockUserManager();
        mockUserManager.Setup(service=>service.CreateAsync(It.IsAny<User>(),It.IsAny<string>()))
                        .ReturnsAsync(IdentityResult.Success);
        var mockSignInManager = GetMockSignInManager(mockUserManager);
        var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);

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

        var mockUserManager = GetMockUserManager();
        mockUserManager.Setup(service=>service.CreateAsync(It.IsAny<User>(),It.IsAny<string>()))
                        .ReturnsAsync(IdentityResult.Failed(new IdentityError{
                                                            Code="0001",
                                                            Description="Error"}));
        var mockSignInManager = GetMockSignInManager(mockUserManager);
        var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);

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

        var mockUserManager = GetMockUserManager();
        mockUserManager.Setup(service=>service.CreateAsync(It.IsAny<User>(),It.IsAny<string>()))
                        .ReturnsAsync(IdentityResult.Success);
        var mockSignInManager = GetMockSignInManager(mockUserManager);
        var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);
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

        var mockUserManager = GetMockUserManager();
        mockUserManager.Setup(service=>service.CreateAsync(It.IsAny<User>(),It.IsAny<string>()))
                        .ReturnsAsync(IdentityResult.Success);
        var mockSignInManager = GetMockSignInManager(mockUserManager);
        var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);
        // When
        var exception = await Record.ExceptionAsync(()=> userService.Register(DummyData.invalidUser1));
        // Then
        Assert.IsType<FluentValidation.ValidationException>(exception);
    }

    #endregion


    #region Login
        
        [Fact]
        public async Task Login_ValidateUser_Succes()
        {
            // Given
            var config = new MapperConfiguration(cfg=>{
                cfg.CreateMap<User,LoginDTO>().ReverseMap();
            });
            var mapper = config.CreateMapper();

            var mockUserManager = GetMockUserManager();
            var mockSignInManager = GetMockSignInManager(mockUserManager);
            mockSignInManager.Setup(service=>service.PasswordSignInAsync(It.IsAny<User>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<bool>(),
                                                                        false))
                             .ReturnsAsync(SignInResult.Success);
            var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);

            LoginDTO logUser = new LoginDTO(){
                Email="Teste@gmail.com",
                Password="Teste#1234"
            };
            // When
            var result = userService.Login(logUser);
            // Then
            Assert.True(result.IsCompletedSuccessfully);
        }

        [Fact]
        public async Task Login_ValidateUser_Fail()
        {
            // Given
            var config = new MapperConfiguration(cfg=>{
                cfg.CreateMap<User,LoginDTO>().ReverseMap();
            });
            var mapper = config.CreateMapper();

            var mockUserManager = GetMockUserManager();
            var mockSignInManager = GetMockSignInManager(mockUserManager);
            mockSignInManager.Setup(service=>service.PasswordSignInAsync(It.IsAny<User>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<bool>(),
                                                                        false))
                             .ReturnsAsync(SignInResult.Success);
            var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);

            LoginDTO invalidUser = new LoginDTO(){
                Email="Teste",
                Password="Teste#1234"
            };
            // When
            var exception = await Record.ExceptionAsync(()=> userService.Login(invalidUser));
            // Then
            Assert.IsType<FluentValidation.ValidationException>(exception);
        }
        [Fact]
        public async Task Login_NullUser_Exception()
        {
            // Given
            var config = new MapperConfiguration(cfg=>{
                cfg.CreateMap<User,LoginDTO>().ReverseMap();
            });
            var mapper = config.CreateMapper();

            var mockUserManager = GetMockUserManager();
            var mockSignInManager = GetMockSignInManager(mockUserManager);
            mockSignInManager.Setup(service=>service.PasswordSignInAsync(It.IsAny<User>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<bool>(),
                                                                        false))
                             .ReturnsAsync(SignInResult.Success);
            var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);

            // When
            var exception = await Record.ExceptionAsync(()=> userService.Login((LoginDTO) null));
            // Then
            Assert.IsType<ArgumentNullException>(exception);
        }
        [Fact]
        public async Task Login_SingIn_Success()
        {
            // Given
            var config = new MapperConfiguration(cfg=>{
                cfg.CreateMap<User,LoginDTO>().ReverseMap();
            });
            var mapper = config.CreateMapper();

            var mockUserManager = GetMockUserManager();
            var mockSignInManager = GetMockSignInManager(mockUserManager);
            mockSignInManager.Setup(service=>service.PasswordSignInAsync(It.IsAny<User>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<bool>(),
                                                                        false))
                             .ReturnsAsync(SignInResult.Success);
            var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);
        
            // When
            LoginDTO User = new LoginDTO(){
                Email="Teste@gmail.com",
                Password="Teste#1234"
            };
            var result = await userService.Login(User);
            // Then
            Assert.True(result);
        }

        [Fact]
        public async Task Login_SingIn_Failed()
        {
            // Given
            var config = new MapperConfiguration(cfg=>{
                cfg.CreateMap<User,LoginDTO>().ReverseMap();
            });
            var mapper = config.CreateMapper();

            var mockUserManager = GetMockUserManager();
            var mockSignInManager = GetMockSignInManager(mockUserManager);
            mockSignInManager.Setup(service=>service.PasswordSignInAsync(It.IsAny<User>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<bool>(),
                                                                        false))
                             .ReturnsAsync(SignInResult.Failed);
            var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);
        
            // When
            LoginDTO User = new LoginDTO(){
                Email="Teste@gmail.com",
                Password="Teste#1234"
            };
            var result = await userService.Login(User);
            // Then
            Assert.False(result);
        }
    #endregion

    #region Logout
    
    [Fact]
    public async Task Logout_Success()
    {
        // Given
        var config = new MapperConfiguration(cfg=>{
                cfg.CreateMap<User,LoginDTO>().ReverseMap();
            });
        var mapper = config.CreateMapper();

        var mockUserManager = GetMockUserManager();
        var mockSignInManager = GetMockSignInManager(mockUserManager);
        mockSignInManager.Setup(service=>service.SignOutAsync());
        var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);
        // When
        Task signoutuser = userService.Logout();
        signoutuser.Wait();
        // Then
        Assert.True(signoutuser.IsCompletedSuccessfully);
    }

        
    #endregion

    #region Delete
        [Fact]
        public async Task Delete_Success()
        {
            // Given
            var config = new MapperConfiguration(cfg=>{});
            var mapper = config.CreateMapper();

            var mockUserManager = GetMockUserManager();
            var mockSignInManager = GetMockSignInManager(mockUserManager);
            mockUserManager.Setup(service=>service.DeleteAsync(It.IsAny<User>()))
                            .ReturnsAsync(IdentityResult.Success);
            var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);
            // When
            var result = await userService.Delete("teste");
            // Then
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_Failed()
        {
            // Given
            var config = new MapperConfiguration(cfg=>{});
            var mapper = config.CreateMapper();

            var mockUserManager = GetMockUserManager();
            var mockSignInManager = GetMockSignInManager(mockUserManager);
            mockUserManager.Setup(service=>service.DeleteAsync(It.IsAny<User>()))
                            .ReturnsAsync(IdentityResult.Failed(new IdentityError{
                                Code="0001",
                                Description = "Error"
                            }));
            var userService = new UserService(mockUserManager.Object,mapper,mockSignInManager.Object);
            // When
            var result = await userService.Delete("teste");
            // Then
            Assert.False(result);
        }
    #endregion


    private Mock<UserManager<User>> GetMockUserManager(){
        return new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(),
                                            null,null,null,null,null,null,null,null);
    }

    private Mock<SignInManager<User>> GetMockSignInManager(Mock<UserManager<User>> managerMock){
        return new Mock<SignInManager<User>>(managerMock.Object,
                                            Mock.Of<IHttpContextAccessor>(),
                                            Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                                            null,null,null,null);
    }
}
