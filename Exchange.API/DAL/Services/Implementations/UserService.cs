using Exchange.API.Models.DTOs;
using Exchange.API.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace Exchange.API.DAL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserDTO> GetById(string id)
        {
            try
            {
                var user = new UserDTO();
                var existinUser = await _userManager.FindByIdAsync(id);
                if (existinUser == null)
                {
                    return user;
                }


                var Role = await _userManager.GetRolesAsync(existinUser);
                user = new UserDTO()
                {
                    Id = existinUser.Id,
                    Username = existinUser.UserName,
                    Email = existinUser.UserName,
                    Role = (Role == null) ? "Not assigned" : Role.First(),

                };

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDTO?> Login(AuthenticateRequest model)
        {
            try
            {

                var existingUser = await _userManager.FindByEmailAsync(model.Username);

                // return null if user not found
                if (existingUser == null)
                {
                    return null;
                }

                if (existingUser.EmailConfirmed == false)
                {

                    return null;
                }

                var userHasValidPassword = await _userManager.CheckPasswordAsync(existingUser, model.Password);
                if (!userHasValidPassword)
                {

                    return null;
                }

                var Role = await _userManager.GetRolesAsync(existingUser);
                var user = new UserDTO()
                {
                    Id = existingUser.Id,
                    Username = existingUser.UserName,
                    Role = (Role == null) ? "Not assigned" : Role.First(),
                    Email = existingUser.UserName,
                };
                return user;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public async Task<UserDTO> Register(string Email, string Password)
        {

            try
            {
                var user = new UserDTO();
                var existinUser = await _userManager.FindByEmailAsync(Email);
                if (existinUser != null)
                {
                    return user;
                }

                var newUser = new IdentityUser
                {
                    Email = Email,
                    UserName = Email
                };
                var createdUser = await _userManager.CreateAsync(newUser, Password);
                if (!createdUser.Succeeded)
                {
                    return user;
                }

                existinUser = await _userManager.FindByEmailAsync(Email);
                await _userManager.AddToRoleAsync(existinUser, "FREE");

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
