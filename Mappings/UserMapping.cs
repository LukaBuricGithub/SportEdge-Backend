using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;

namespace SportEdge.API.Mappings
{
    /// <summary>
    /// Provides mapping functionality between User domain models and DTOs.
    /// </summary>
    public class UserMapping
    {

        /// <summary>
        /// Maps a CreateUserRequestDto to a User domain model.
        /// </summary>
        /// <param name="user">The DTO containing data for creating a user.</param>
        /// <returns>A new User domain model instance.</returns>
        public User ToDomain(CreateUserRequestDto user)
        {
            return new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                IsAdmin = false,
                DateOfBirth = user.DateOfBirth,
                PasswordResetToken = null,
                ResetTokenExpiration = null,
                Country = user.Country,
                City = user.City,
                Address = user.Address
            };
        }

        /// <summary>
        /// Maps an UpdateUserRequestDto to a User domain model.
        /// </summary>
        /// <param name="user">The DTO containing updated user data.</param>
        /// <returns>A User domain model instance with updated values.</returns>
        public User ToDomain(UpdateUserRequestDto user)
        {
            return new User()
            {
                FirstName = user.Firstname,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Country = user.Country,
                City = user.City,
                Address = user.Address
            };
        }

        /// <summary>
        /// Maps an LoginUserRequestDto to a User domain model.
        /// </summary>
        /// <param name="user">The DTO containing login data of a user.</param>
        /// <returns>A User domain model instance with login values.</returns>
        public User ToDomain(LoginUserRequestDto user)
        {
            return new User()
            {
                Email = user.Email,
                Password = user.Password
            };
        }

        /// <summary>
        /// Maps a User domain model to a UserDto.
        /// </summary>
        /// <param name="user">The domain model to convert.</param>
        /// <returns>A UserDto containing the mapped data.</returns>
        public UserDto ToDto(User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                IsAdmin = user.IsAdmin,
                DateOfBirth = user.DateOfBirth,
                Country = user.Country,
                City = user.City,
                Address = user.Address
            };
        }
    }
}
