using SportEdge.API.Models.DTO;

namespace SportEdge.API.Services.Interface
{
    /// <summary>
    /// Defines service operations for managing users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="request">DTO containing data for the new user.</param>
        /// <returns>The created user as a DTO.</returns>
        Task<UserDto> CreateAsync(CreateUserRequestDto request);


        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of user DTOs.</returns>
        Task<List<UserDto>> GetAllAsync();


        /// <summary>
        /// Retrieves a user by its ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>The user DTO.</returns>
        Task<UserDto> GetAsync(int id);



        /// <summary>
        /// Retrieves a user by its email.
        /// </summary>
        /// <param name="email">The user email.</param>
        /// <returns>The user DTO.</returns>
        Task<UserDto> GetByEmail(string email);



        /// <summary>
        /// Updates a user with new data.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="request">DTO containing updated data.</param>
        /// <returns>The updated user DTO.</returns>
        Task<UserDto> UpdateAsync(int id, UpdateUserRequestDto request);


        /// <summary>
        /// Deletes a user by its ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>True if the user was deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);


        /// <summary>
        /// Tries to login a user.
        /// </summary>
        /// <param name="request">DTO containing login data.</param>
        /// <returns>String which is generated JWT for a user.</returns>
        Task<string> LoginRequest(LoginUserRequestDto request);


        /// <summary>
        /// Generates a password reset token and sends it to user email.
        /// </summary>
        /// <param name="request">DTO containing user email.</param>
        Task GeneratePasswordResetTokenAsync(UserForgotPasswordRequestDto request);


        /// <summary>
        /// Resets password for a user which is found by reset token.
        /// </summary>
        /// <param name="request">DTO containing reset token and new password.</param>
        Task ResetPasswordAsync(UserResetPasswordRequestDto request);



        /// <summary>
        /// Sends a message to SportEdge email address.
        /// </summary>
        /// <param name="request">DTO containing message data.</param>
        Task CustomerServiceSendMessageAsync(UserSendMessageDto request);


        /// <summary>
        /// Sends an receipt to user's email address.
        /// </summary>
        /// <param name="request">DTO containing receipt data.</param>
        Task SendReceiptEmailAsync(UserSendReceiptDto request);

    }
}
