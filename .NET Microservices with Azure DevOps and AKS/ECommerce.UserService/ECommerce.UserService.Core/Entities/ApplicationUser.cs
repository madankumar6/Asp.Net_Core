
namespace ECommerce.UserService.Core.Entities
{
    /// <summary>
    /// Represents an application user with identity and profile information.
    /// </summary>
    public class ApplicationUser
    {
        public Guid UserId { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
    }
}
