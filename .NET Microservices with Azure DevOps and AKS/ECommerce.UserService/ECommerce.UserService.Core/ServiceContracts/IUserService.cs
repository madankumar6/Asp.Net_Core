
using ECommerce.UserService.Core.Dtos;

namespace ECommerce.UserService.Core.ServiceContracts
{
    public interface IUserService
    {
        /// <summary>
        /// Authenticates a user with the specified email address and password.
        /// </summary>
        /// <param name="email">The email address of the user attempting to log in. Cannot be null or empty.</param>
        /// <param name="password">The password associated with the specified email address. Cannot be null or empty.</param>
        /// <returns>An AuthenticationResponse object containing the result of the authentication attempt, including user
        /// information and authentication status.</returns>
        Task<AuthenticationResponse?> Login(string email, string password);

        /// <summary>
        /// Registers a new user with the provided registration details and returns an authentication response upon
        /// successful registration.
        /// </summary>
        /// <param name="request">An object containing the user's registration information, such as username, password, and any additional
        /// required fields. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an AuthenticationResponse with
        /// details about the newly registered user and authentication tokens.</returns>
        Task<AuthenticationResponse> RegisterUser(RegisterUserRequest request);
    }
}
