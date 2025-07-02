using Microsoft.EntityFrameworkCore;
using SportEdge.API.Data;
using SportEdge.API.Models.Domain;
using SportEdge.API.Repositories.Interface;

namespace SportEdge.API.Repositories.Implementation
{
    /// <summary>
    /// Repository implementation for size option-related data operations.
    /// </summary>
    public class SizeOptionRepository : ISizeOptionRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SizeOptionRepository(ApplicationDbContext dbContext) 
        { 
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<SizeOption> CreateAsync(SizeOption sizeOption)
        {
            await dbContext.SizeOptions.AddAsync(sizeOption);
            await dbContext.SaveChangesAsync();

            return sizeOption;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int id)
        {
            var sizeOption =  await dbContext.SizeOptions.FindAsync(id);

            if (sizeOption == null)
            {
                return false;
            }

            dbContext.SizeOptions.Remove(sizeOption);
            await dbContext.SaveChangesAsync();
            return true;

        }


        /// <inheritdoc/>
        public async Task<List<SizeOption>> GetAllAsync()
        {
            return await dbContext.SizeOptions
                .Include(so => so.Gender)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<SizeOption> GetAsync(int id)
        {
            return await dbContext.SizeOptions
                .Include(so => so.Gender)
                .FirstOrDefaultAsync(so => so.Id == id);
        }

        /// <inheritdoc/>
        public async Task<List<SizeOption>> GetSizeOptionsByGenderIdAsync(int genderId)
        {
            return await dbContext.SizeOptions
                .Include(so => so.Gender)
                .Where(so => so.GenderId == genderId)
                .ToListAsync();
        }


        /// <inheritdoc/>
        public async Task<SizeOption> UpdateAsync(SizeOption sizeOption)
        {
            dbContext.SizeOptions.Update(sizeOption);
            await dbContext.SaveChangesAsync();
            return sizeOption;
        }
    }
}
