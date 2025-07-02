using Microsoft.AspNetCore.Identity;
using SportEdge.API.Mappings;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;
using SportEdge.API.Models.Enums;
using SportEdge.API.Repositories.Implementation;
using SportEdge.API.Repositories.Interface;
using SportEdge.API.Services.Interface;
using System.Security.Cryptography;

namespace SportEdge.API.Services.Implementation
{
    /// <summary>
    /// Provides implementation for user-related service operations.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly UserMapping mapping;
        private readonly IPasswordHasher passwordHasher;
        private readonly IEmailSenderService emailSenderService;

        public UserService(IUserRepository userRepository,UserMapping mapping, IPasswordHasher passwordHasher, IEmailSenderService emailSenderService)
        {
            this.userRepository = userRepository;
            this.mapping = mapping;
            this.passwordHasher = passwordHasher;
            this.emailSenderService = emailSenderService;
        }


        /// <inheritdoc/>
        public async Task<UserDto> CreateAsync(CreateUserRequestDto request)
        {
            var user = await userRepository.GetByEmail(request.Email);

            if (user != null)
            {
                throw new InvalidOperationException("The email is already in use.");
            }

            var userDomain = mapping.ToDomain(request);
            var createdUser = await userRepository.CreateAsync(userDomain);
            return mapping.ToDto(createdUser);

        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int id)
        {
            //var deleted = await userRepository.DeleteAsync(id);

            //if(!deleted)
            //{
            //    throw new KeyNotFoundException($"User with ID {id} not found.");
            //}

            //return true;
            var result = await userRepository.DeleteAsync(id);

            return result switch
            {
                DeleteResult.Success => true,
                DeleteResult.NotFound => throw new KeyNotFoundException($"User with ID {id} not found."),
                DeleteResult.HasRelatedObjects => throw new InvalidOperationException($"User with ID {id} cannot be deleted because it has related objects (e.g., orders or cart)."),
                _ => throw new Exception("Unexpected deletion result.")
            };
            
        }

        /// <inheritdoc/>
        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await userRepository.GetAllAsync();
            return users.Select(u => mapping.ToDto(u)).ToList();
        }


        /// <inheritdoc/>
        public async Task<UserDto> GetByEmail(string email)
        {
            var user = await userRepository.GetByEmail(email);
            if(user == null)
            {
                throw new KeyNotFoundException($"User with email {email} not found.");
            }

            return mapping.ToDto(user);
        }


        /// <inheritdoc/>
        public async Task<string> LoginRequest(LoginUserRequestDto request)
        {
            var user = await userRepository.GetByEmail(request.Email);
            
            if (user == null)
            {
                throw new KeyNotFoundException($"Incorrect email or password.");
            }

            var loggedUser = await userRepository.LoginRequest(user, request.Password);

            if (loggedUser == null)
            {
                throw new UnauthorizedAccessException("Incorrect email or password.");
            }

            return loggedUser;

            //return mapping.ToDto(loggedUser);
        
        }

        /// <inheritdoc/>
        public async Task<UserDto> GetAsync(int id)
        {
            var user = await userRepository.GetAsync(id);
            if(user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return mapping.ToDto(user);
        }

        /// <inheritdoc/>
        public async Task<UserDto> UpdateAsync(int id, UpdateUserRequestDto request)
        {
            var existingUser = await userRepository.GetAsync(id);
            if(existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            var userDomain = mapping.ToDomain(request);

            existingUser.FirstName = userDomain.FirstName;
            existingUser.LastName = userDomain.LastName;
            existingUser.DateOfBirth = userDomain.DateOfBirth;
            existingUser.Country = userDomain.Country;
            existingUser.City = userDomain.City;
            existingUser.Address = userDomain.Address;


            var updatedUser = await userRepository.UpdateAsync(existingUser);
            return mapping.ToDto(updatedUser);
        }

        /// <inheritdoc/>
        public async Task GeneratePasswordResetTokenAsync(UserForgotPasswordRequestDto request)
        {
            var user = await userRepository.GetByEmail(request.Email);
            if (user == null) 
            {
                throw new KeyNotFoundException($"User with email {request.Email} not found.");
            }

            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            user.PasswordResetToken = token;
            user.ResetTokenExpiration = DateTime.UtcNow.AddHours(1);

            await userRepository.UpdateAsync(user);

            var emailTo = request.Email;
            var emailSubject = "Password Reset Request";    
            var emailBody = $"Please reset your password using this token: {token}";

            await emailSenderService.SendEmailAsync(emailTo, emailSubject, emailBody);
        }



        /// <inheritdoc/>
        public async Task ResetPasswordAsync(UserResetPasswordRequestDto request)
        {
            var user = await userRepository.GetByResetTokenAsync(request.Token);
            if (user == null || user.ResetTokenExpiration < DateTime.UtcNow) 
            {
                throw new UnauthorizedAccessException("Invalid or expired token.");

            }

            user.Password = passwordHasher.Hash(request.NewPassword);
            user.PasswordResetToken = null;
            user.ResetTokenExpiration = null;

            await userRepository.UpdateAsync(user);
        }

        /// <inheritdoc/>
        public async Task CustomerServiceSendMessageAsync(UserSendMessageDto request)
        {
            var senderEmail = request.Email;
            var emailTo = "sport.edge@outlook.com";
            var emailSubject = $"{request.FirstName} {request.LastName } ({request.Email}) {request.Subject}";
            var emailBody = $"{request.Content}";

            await emailSenderService.SendEmailAsync(emailTo, emailSubject, emailBody);

        }
    }
}
