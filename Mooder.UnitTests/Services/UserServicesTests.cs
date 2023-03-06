
using AutoMapper;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Service.Services;

namespace Mooder.UnitTests.Services;

public class UserServicesTests
{
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
        Assert.Equal(true,result);
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
        Assert.Equal(false,result);
    }
}
