using Application.Controllers;
using Domain.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Mooder.UnitTests.Helpers;
using Service.Services;

namespace Mooder.UnitTests.Controllers;

public class UsersControllerTests
{

    #region Register
        [Fact]
        public async Task Register_IfUserExists_DontFind()
        {
            // Given
            var mockUserService = GetMockUserService();
            mockUserService.Setup(service=>service.CheckUserExists(It.IsAny<string>()))
                            .ReturnsAsync(false);
            var sut = new UsersController(mockUserService.Object);
            // When
            var actresult = (BadRequestObjectResult)await sut.Register(DummyData.invalidUser1);
            // Then
            actresult.StatusCode.Should().Be(400);
        }
        [Fact]
        public async Task Register_IfInvalidUser_BadRequest()
        {
            // Given
            var mockUserService = GetMockUserService();
            mockUserService.Setup(service=>service.CheckUserExists(It.IsAny<string>()))
                            .ReturnsAsync(true);
            mockUserService.Setup(service=>service.Register(It.IsAny<RegisterDTO>()))
                            .Throws(new FluentValidation.ValidationException("error"));
            var sut = new UsersController(mockUserService.Object);
            // When
            var actresult = (BadRequestResult)await sut.Register(DummyData.invalidUser1);
            // Then
            actresult.StatusCode.Should().Be(400);  
        }
        [Fact]
        public async Task Register_ValidUser_Failure()
        {
            // Given
            var mockUserService = GetMockUserService();
            mockUserService.Setup(service=>service.CheckUserExists(It.IsAny<string>()))
                            .ReturnsAsync(true);
            mockUserService.Setup(service=>service.Register(It.IsAny<RegisterDTO>()))
                            .ReturnsAsync(false);
            var sut = new UsersController(mockUserService.Object);
            // When
            var actresult = (BadRequestResult)await sut.Register(DummyData.validUser1);
            // Then
            actresult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Register_ConfirmPasswordDifferent_BadRequest()
        {
            // Given
            var mockUserService = GetMockUserService();
            mockUserService.Setup(service=>service.CheckUserExists(It.IsAny<string>()))
                            .ReturnsAsync(true);
            mockUserService.Setup(service=>service.Register(It.IsAny<RegisterDTO>()))
                            .ReturnsAsync(false);
            var sut = new UsersController(mockUserService.Object);
            // When
            var actresult = (BadRequestObjectResult)await sut.Register(DummyData.invalidUser2);
            // Then
            actresult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Register_ValidUser_Success()
        {
            // Given
            var mockUserService = GetMockUserService();
            mockUserService.Setup(service=>service.CheckUserExists(It.IsAny<string>()))
                            .ReturnsAsync(true);
            mockUserService.Setup(service=>service.Register(It.IsAny<RegisterDTO>()))
                            .ReturnsAsync(true);
            var sut = new UsersController(mockUserService.Object);
            // When
            var actresult = (OkResult)await sut.Register(DummyData.validUser1);
            // Then
            actresult.StatusCode.Should().Be(200);
        }
    #endregion

    #region Login
        [Fact]
        public async Task Login_InvalidLogin_BadRequest()
        {
            // Given
            var mockUserService = GetMockUserService();
            mockUserService.Setup(service=>service.Login(It.IsAny<LoginDTO>()))
                            .Throws(new FluentValidation.ValidationException("Error"));
            var sut = new UsersController(mockUserService.Object);
            // When
            var result = (BadRequestObjectResult)await sut.Login(DummyData.invalidLogin1);
            // Then
            result.StatusCode.Should().Be(400);
        }
        [Fact]
        public async Task Login_ValidLogin_Failed()
        {
            // Given
            var mockUserService = GetMockUserService();
            mockUserService.Setup(service=>service.Login(It.IsAny<LoginDTO>()))
                            .ReturnsAsync(false);
            var sut = new UsersController(mockUserService.Object);
            // When
            var result = (BadRequestResult)await sut.Login(DummyData.validLogin1);
            // Then
            result.StatusCode.Should().Be(400);
        }
        [Fact]
        public async Task Login_ValidLogin_Success()
        {
            // Given
            var mockUserService = GetMockUserService();
            mockUserService.Setup(service=>service.Login(It.IsAny<LoginDTO>()))
                            .ReturnsAsync(true);
            var sut = new UsersController(mockUserService.Object);
            // When
            var result = (OkResult)await sut.Login(DummyData.validLogin1);
            // Then
            result.StatusCode.Should().Be(200);
        }
    #endregion

    private Mock<IUserService> GetMockUserService()
    {
        return new Mock<IUserService>();
    }
}
