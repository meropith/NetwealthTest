using Exchange.UI.Models;

namespace Exchange.UI.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponseDTO> Login(AuthenticationDTO userFromAuthentication);

    }
}
