
using ECommerce.UserService.Core.Common.Enums;

namespace ECommerce.UserService.Core.Dtos
{
    public record RegisterUserRequest(string Email, string Password, string PersonName, GenderOptions Gender);
}
