using Microsoft.EntityFrameworkCore;
using SportEdge.API.Data;
using SportEdge.API.Models.Domain;
using SportEdge.API.Repositories.Interface;

namespace SportEdge.API.Repositories.Implementation
{
    /// <summary>
    /// Repository implementation for product image-related data operations.
    /// </summary>
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProductImageRepository(ApplicationDbContext dbContext) 
        { 
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task AddImagesAsync(List<ProductImage> images)
        {
            await dbContext.ProductImages.AddRangeAsync(images);
            await dbContext.SaveChangesAsync();
        }


        /// <inheritdoc/>
        public async Task DeleteByProductIdAsync(int productId)
        {
            var existingImages = dbContext.ProductImages.Where(pi => pi.ProductId == productId); 
            dbContext.ProductImages.RemoveRange(existingImages);
            await dbContext.SaveChangesAsync();
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId)
        {
            return await dbContext.ProductImages.Where(pi => pi.ProductId == productId).ToListAsync();
        }

    }
}
