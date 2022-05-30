using Exchange.API.Controllers;
using Exchange.API.DAL.Services;
using Exchange.API.Models.DTOs;
using Exchange.API.Models.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Exchange.API.UnitTests
{
    public class AuthControllerTests
    {
        private readonly ILogger<AuthController> mockedLogger = new Mock<ILogger<AuthController>>().Object;
        private readonly Mock<IUserService> mockedUserService = new Mock<IUserService>();
             
        static readonly AuthenticateRequest dummyAuth = new AuthenticateRequest { Username = "tierapi@tierapi.com", Password = "159632478A@a" };
        static readonly UserDTO dummyUser = new() { Id = "1", Username = "DummyUser@test.com", Email = "DummyUser@test.com", Role = "FREE", Token = "ASAdfy12547" };

        [Fact]
        public async Task AuthController_Login_With_Valid_Credentials_Returns_Correct_Status_Code()
        {
                      
            mockedUserService.Setup(user => user.Login(dummyAuth)).ReturnsAsync(dummyUser);

            var controller = new AuthController(mockedUserService.Object, new ApiSettings()
            {
                Secret = "Qzzzzzz-zzzzzzzzzzqqqq-qqqqqqqquz"
            }, mockedLogger);

            var actionResult = await controller.Authenticate(dummyAuth);

            var result = actionResult as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }


        [Fact]
        public async Task AuthController_Login_With_Valid_Credentials_Returns_Token()
        {
            mockedUserService.Setup(user => user.Login(dummyAuth)).ReturnsAsync(dummyUser);

            var controller = new AuthController(mockedUserService.Object, new ApiSettings()
            {
                Secret = "Qzzzzzz-zzzzzzzzzzqqqq-qqqqqqqquz"
            }, mockedLogger);

            var actionResult = await controller.Authenticate(dummyAuth);

            var result = actionResult as OkObjectResult;
            var value = result.Value as UserDTO;

            value.Token.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task AuthController_Login_With_Invalid_Credentials_Returns_Error()
        {
            var invalidCredentials = new AuthenticateRequest { Username = "user", Password = "wrongpassword" };

            mockedUserService.Setup(user => user.Login(invalidCredentials)).ReturnsAsync(new UserDTO());

            var controller = new AuthController(mockedUserService.Object, new ApiSettings()
            {
                Secret = "Qzzzzzz-zzzzzzzzzzqqqq-qqqqqqqquz"
            }, mockedLogger);

            var actionResult = await controller.Authenticate(invalidCredentials);

            var result = actionResult as BadRequestObjectResult;
            result.StatusCode.Should().Be(400);
        }
    }
}