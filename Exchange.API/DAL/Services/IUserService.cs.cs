using Exchange.API.Models.DTOs;
using Exchange.API.Models.Requests;

namespace Exchange.API.DAL.Services
{
    public interface IUserService
    {
        Task<UserDTO?> Login(AuthenticateRequest model);
        Task<UserDTO> Register(string Email, string Password);
        Task<UserDTO> GetById(string id);
    }
}
