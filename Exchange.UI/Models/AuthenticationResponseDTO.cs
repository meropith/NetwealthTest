namespace Exchange.UI.Models
{
    public class AuthenticationResponseDTO
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; } = String.Empty;
        public string Token { get; set; } = String.Empty;

        public string Id { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Role { get; set; } = String.Empty;
        public UserDTO UserDTO { get; set; } = new UserDTO();
    }
}
