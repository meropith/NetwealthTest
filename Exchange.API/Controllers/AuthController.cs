using Exchange.API.DAL.Services;
using Exchange.API.Helpers;
using Exchange.API.Models.DTOs;
using Exchange.API.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Exchange.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ApiSettings _apiSettings;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IUserService userService, ApiSettings apiSettings, ILogger<AuthController> Logger)
        {
            _userService = userService;
            _apiSettings = apiSettings;
            _logger = Logger;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Authorizes the user", Description = "Authorizes the user and returns the JWT")]
        [SwaggerResponse(200, "Auth Success")]
        [SwaggerResponse(400, "Bad Request or invalid auth")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {            

            try
            {
                var userDTO = await _userService.Login(model);
                if (userDTO != null && !string.IsNullOrEmpty(userDTO.Id))
                {
                    userDTO.Token = AuthHelper.GenerateJwtToken(userDTO.Id, _apiSettings.Secret, userDTO.Role);                    
                    return Ok(userDTO);
                }
                else
                {
                    return BadRequest("Authentication failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Authentication failed: " + ex.Message);
                return BadRequest("Authentication failed");
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Register teacher", Description = "Registers a new teacher")]
        [SwaggerResponse(200, "Register Success")]
        [SwaggerResponse(400, "Bad Request or invalid registration")]
        public async Task<IActionResult> Register(string email, string pwd)
        {
            //To prevent new registrations
            return null;

            var userDTO = new UserDTO();
            try
            {
                userDTO = await _userService.Register(email, pwd);
                if (userDTO != null && !string.IsNullOrEmpty(userDTO.Id))
                {
                    userDTO.Token = AuthHelper.GenerateJwtToken(userDTO.Id, _apiSettings.Secret, userDTO.Role);      
                    return Ok(userDTO);
                }
                else
                {
                    return BadRequest("Registration not completed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Registration failed: " + ex.Message);
                return BadRequest("Registration not completed");

            }
        }
    }
}
