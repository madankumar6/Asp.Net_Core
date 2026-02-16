namespace ECommerce.UserService.Core.Dtos
{
    public record AuthenticationResponse(Guid UserId, string Email, string Name, string? Gender, string Token, bool IsSuccessful)
    {
        public AuthenticationResponse() : this(Guid.Empty, string.Empty, string.Empty, null, string.Empty, false)
        {
        }
    }

    //public class AuthenticationResponse
    //{
    //    public Guid UserId { get; set; }
    //    public string Email { get; set; }
    //    public string Name { get; set; }
    //    public string? Gender { get; set; }
    //    public string? Token { get; set; }
    //    public bool IsSuccessful { get; set; }
    //}
}
