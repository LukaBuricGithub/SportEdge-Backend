using Microsoft.EntityFrameworkCore;
using SportEdge.API.Data;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.Enums;
using SportEdge.API.Repositories.Interface;

namespace SportEdge.API.Repositories.Implementation
{
    /// <summary>
    /// Repository implementation for brand-related data operations.
    /// </summary>
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BrandRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<Brand> CreateAsync(Brand brand)
        {
            await dbContext.Brands.AddAsync(brand);
            await dbContext.SaveChangesAsync();

            return brand;
        }

        /// <inheritdoc/>
        public async Task<Brand> GetAsync(int id)
        {
            return await dbContext.Brands.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<List<Brand>> GetAllAsync()
        {
            return await dbContext.Brands.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Brand> UpdateAsync(Brand brand)
        {
            dbContext.Brands.Update(brand);
            await dbContext.SaveChangesAsync();
            return brand;
        }

        /// <inheritdoc/>
        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var brand = await dbContext.Brands.FindAsync(id);
            
            if (brand == null)
            {
                return DeleteResult.NotFound;
            }

            bool hasProducts = await dbContext.Products.AnyAsync(p => p.BrandId == id);
            if (hasProducts)
            {
                return DeleteResult.HasRelatedObjects;
            }

            dbContext.Brands.Remove(brand);
            await dbContext.SaveChangesAsync();
            return DeleteResult.Success;
        }

        /// <inheritdoc/>
        public async Task<List<Brand>> GetBrandsByNameAsync(string search)
        {
            var brands = await dbContext.Brands
                            .Where(b => EF.Functions.Like(b.Name, $"%{search}%"))
        .                   ToListAsync();
            return brands;
        }
    }
}
