namespace ECommerce.UserService.Core.Dtos
{
    public record AuthenticationResponse(Guid UserID, string Email, string PersonName, string? Gender, string Token, bool Success)
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
