using Microsoft.EntityFrameworkCore;
using SportEdge.API.Data;
using SportEdge.API.Models.Domain;
using SportEdge.API.Repositories.Interface;
using System.Reflection;

namespace SportEdge.API.Repositories.Implementation
{
    /// <summary>
    /// Repository implementation for product variation-related data operations.
    /// </summary>
    public class ProductVariationRepository : IProductVariationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public ProductVariationRepository(ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ProductVariation> CreateAsync(ProductVariation productVariation)
        {
            await dbContext.ProductVariations.AddAsync(productVariation);
            await dbContext.SaveChangesAsync();
            return productVariation;
        }

        /// <inheritdoc/>
        public async Task<List<ProductVariation>> GetAllAsync()
        {
            return await dbContext.ProductVariations
                .Include(p => p.Product)
                .Include(p => p.SizeOption)
                .ToListAsync();

        }

        /// <inheritdoc/>
        public async Task<ProductVariation> GetAsync(int id)
        {
            return await dbContext.ProductVariations
                .Include(p => p.Product)
                .Include(p => p.SizeOption)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <inheritdoc/>
        public async Task<ProductVariation> UpdateAsync(ProductVariation productVariation)
        {
            dbContext.ProductVariations.Update(productVariation);
            await dbContext.SaveChangesAsync();
            return productVariation;
        }
    }
}
