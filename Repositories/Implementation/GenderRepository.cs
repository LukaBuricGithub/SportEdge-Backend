using Microsoft.EntityFrameworkCore;
using SportEdge.API.Data;
using SportEdge.API.Models.Domain;
using SportEdge.API.Repositories.Interface;

namespace SportEdge.API.Repositories.Implementation
{
    /// <summary>
    /// Repository implementation for gender-related data operations.
    /// </summary>
    public class GenderRepository : IGenderRepository
    {
        private readonly ApplicationDbContext dbContext;

        public GenderRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<Gender> CreateAsync(Gender gender)
        {
            await dbContext.Genders.AddAsync(gender);
            await dbContext.SaveChangesAsync();
            return gender;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int id)
        {
            var gender = await dbContext.Genders.FindAsync(id);

            if (gender == null)
            {
                return false;
            }

            dbContext.Genders.Remove(gender);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<List<Gender>> GetAllAsync()
        {
            return await dbContext.Genders.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Gender> GetAsync(int id)
        {
            return await dbContext.Genders.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<Gender> UpdateAsync(Gender gender)
        {
            dbContext.Genders.Update(gender);
            await dbContext.SaveChangesAsync();
            return gender;
        }
    }
}
