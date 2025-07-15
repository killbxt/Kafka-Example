namespace Auth.Domain.Models
{
    public class RegisterRequest
    {
        public required string Username { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
    }
}
