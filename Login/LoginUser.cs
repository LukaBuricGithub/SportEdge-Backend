using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;
using SportEdge.API.Repositories.Interface;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Login
{
    /// <summary>
    /// Handles the login process for users by verifying their credentials.
    /// </summary>
    public class LoginUser (IUserService userService, IPasswordHasher passwordHasher)
    {

        /// <summary>
        /// Represents the request data required for user login.
        /// </summary>
        /// <param name="Email">The email address of the user.</param>
        /// <param name="Password">The password of the user.</param>
        public record Request(string Email,string Password);

        /// <summary>
        /// Handles the login process by verifying the user's credentials.
        /// </summary>
        /// <param name="request">The login request containing email and password.</param>
        /// <returns>A UserDto object containing user details if login is successful.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the user is not found.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown if the provided credentials are invalid.</exception>

        public async Task<UserDto> Handle(Request request)
        {
            var user = await userService.GetByEmail(request.Email);

            if (user == null) 
            {
                throw new InvalidOperationException("The user was not found");
            }

            bool verified = passwordHasher.Verify(request.Password,user.Password);

            if (!verified)
            {
                throw new UnauthorizedAccessException("Incorrect email or password");
            }

            return user;
        }
    }
}
