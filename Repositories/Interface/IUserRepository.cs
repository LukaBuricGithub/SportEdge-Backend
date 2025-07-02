using Microsoft.EntityFrameworkCore;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.Enums;

namespace SportEdge.API.Repositories.Interface
{
    /// <summary>
    /// Interface for user-related data operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">The user entity to create.</param>
        /// <returns>The created user entity.</returns>
        Task<User> CreateAsync(User user);


        /// <summary>
        /// Retrieves a user by its ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user entity if found; otherwise, null.</returns>
        Task<User> GetAsync(int id);



        /// <summary>
        /// Retrieves a user by its email.
        /// </summary>
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>The user entity if found; otherwise, null.</returns>
        Task<User> GetByEmail(string email);


        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all user entities.</returns>
        Task<List<User>> GetAllAsync();


        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="user">The user entity with updated data.</param>
        /// <returns>The updated user entity.</returns>
        Task<User> UpdateAsync(User user);



        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>>Delete result enum which contains information if entity is deleted.</returns>
        Task<DeleteResult> DeleteAsync(int id);


        /// <summary>
        /// Compares user's stored password with entered password.
        /// </summary>
        /// <param name="user">The user's password in database.</param>
        /// <param name="requestPassword">Entered password.</param>
        /// <returns>If login is successful returns generated JWT.</returns>
        Task<string> LoginRequest(User user,string requestPassword);


        /// <summary>
        /// Retrieves a user by reset token.
        /// </summary>
        /// <param name="token">The token of the user to retrieve.</param>
        /// <returns>The user entity if found; otherwise, null.</returns>
        Task<User> GetByResetTokenAsync(string token);



    }
}
