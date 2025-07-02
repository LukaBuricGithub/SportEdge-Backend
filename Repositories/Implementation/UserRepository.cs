using Microsoft.EntityFrameworkCore;
using SportEdge.API.Data;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.Enums;
using SportEdge.API.Repositories.Interface;
using SportEdge.API.Services.Interface;
using SportEdge.API.Users;

namespace SportEdge.API.Repositories.Implementation
{
    /// <summary>
    /// Repository implementation for user-related data operations.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPasswordHasher passwordHasher;
        private readonly TokenProvider tokenProvider;

        public UserRepository(ApplicationDbContext dbContext,IPasswordHasher passwordHasher,TokenProvider tokenProvider)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
            this.tokenProvider = tokenProvider;
        }

        /// <inheritdoc/>
        public async Task<User> CreateAsync(User user)
        {
            var newUser = user;
            newUser.Password = passwordHasher.Hash(user.Password);
            await dbContext.Users.AddAsync(newUser);
            await dbContext.SaveChangesAsync();

            return newUser;
        }

        /// <inheritdoc/>
        public async Task<string> LoginRequest(User user, string requestPassword)
        {
            bool verified = passwordHasher.Verify(requestPassword, user.Password);
            if (!verified)
            {
                return null;
            }

            string token = tokenProvider.Create(user);

            return token;

            //return user;
        }


        /// <inheritdoc/>
        public async Task<DeleteResult> DeleteAsync(int id)
        {
            //var user = await dbContext.Users.FindAsync(id);

            //if (user == null)
            //{
            //    return false;
            //}

            //dbContext.Users.Remove(user);
            //await dbContext.SaveChangesAsync();
            //return true;
            var user = await dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return DeleteResult.NotFound;
            }

            bool hasOrders = await dbContext.Orders.AnyAsync(o => o.UserId == id);
            bool hasCart = await dbContext.Carts.AnyAsync(c => c.UserId == id);

            if (hasOrders || hasCart)
            {
                return DeleteResult.HasRelatedObjects;
            }

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return DeleteResult.Success;
        }


        /// <inheritdoc/>
        public async Task<List<User>> GetAllAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<User> GetAsync(int id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<User> GetByEmail(string email)
        {
            return await dbContext.Users.SingleOrDefaultAsync(u=>u.Email == email);
        }


        /// <inheritdoc/>
        public async Task<User> UpdateAsync(User user)
        {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        /// <inheritdoc/>
        public async Task<User> GetByResetTokenAsync(string token)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == token);
        }
    }
}
