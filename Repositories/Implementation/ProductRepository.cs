using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using SportEdge.API.Data;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;
using SportEdge.API.Repositories.Interface;
using System.Runtime.InteropServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SportEdge.API.Repositories.Implementation
{
    /// <summary>
    /// Repository implementation for product-related data operations.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<Product> CreateAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await dbContext.Products
                    .Include(p => p.Categories)
                    .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return false;
            }

            product.Categories.Clear();
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync ();
            return true;
        }

        /// <inheritdoc/>
        public async Task<List<Product>> GetAllAsync()
        {
            /*
            return await dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Categories)
                .ToListAsync();
            */

            return await dbContext.Products
           .Include(p => p.Brand)
           .Include(p => p.Categories)
           .Include(p => p.ProductVariations)
               .ThenInclude(pv => pv.SizeOption)
           .Include(p => p.Images)
           .Include(p => p.Gender)
           .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Product> GetAsync(int id)
        {
            return await dbContext.Products
            .Include(p => p.Brand)
            .Include(p => p.Categories)
            .Include(p => p.ProductVariations)
                .ThenInclude(pv => pv.SizeOption)
            .Include(p => p.Images)
            .Include(p => p.Gender)
            .FirstOrDefaultAsync(p => p.Id == id);
        }




     


        /*
        public async Task<Product> UpdateAsync(int id, Product product, IEnumerable<int> categoryIds)
        {

            var existingProduct = await dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingProduct == null)
            {
                return null;
            }


            existingProduct.Name = product.Name;
            existingProduct.ShortDescription = product.ShortDescription;
            existingProduct.Price = product.Price;
            //existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.BrandId = product.BrandId;
            //existingProduct.GenderId = product.GenderId;


            existingProduct.Categories.Clear();

            var newCategories = await dbContext.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .ToListAsync();

            foreach (var category in newCategories)
            {
                existingProduct.Categories.Add(category);
            }

            //dbContext.Products.Update(existingProduct);
            await dbContext.SaveChangesAsync();

            return existingProduct;
        }

        */

        /// <inheritdoc/>
        public async Task<Product> UpdateAsync(Product product)
        {
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        /// <inheritdoc/>
        public async Task<List<Product>> SearchProductsAsync(string? query)
        {
      
            var products = dbContext.Products
        .Include(p => p.Brand)
        .Include(p => p.Categories)
        .Include(p => p.ProductVariations)
            .ThenInclude(pv => pv.SizeOption)
        .Include(p => p.Images)
        .Include(p => p.Gender)
        .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                var terms = query
                    .ToLower()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var term in terms)
                {
                    products = products.Where(p =>
                        EF.Functions.Like(p.Name, $"%{term}%") ||
                        EF.Functions.Like(p.Brand.Name, $"%{term}%") ||
                        p.Categories.Any(c => EF.Functions.Like(c.Name, $"%{term}%")));
                }
            }

            return await products.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Product>> GetProductsByGenderTypeAsync(string name)
        {
            return await dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Categories)
                .Include(p => p.ProductVariations)
                    .ThenInclude(pv => pv.SizeOption)
                .Include(p => p.Images)
                .Include(p => p.Gender)
                .Where(p => p.Gender != null && p.Gender.Name.ToLower() == name.ToLower())
                .ToListAsync();
        }


        /// <inheritdoc/>
        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Categories)
                .Include(p => p.ProductVariations)
                    .ThenInclude(pv => pv.SizeOption)
                .Include(p => p.Images)
                .Include(p => p.Gender)
                .Where(p => p.Categories.Any(c => c.Id == categoryId))
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Product>> FilterProductsAsync(ProductFilterDto filter)
        {
            var products = dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Categories)
                .Include(p => p.ProductVariations)
                    .ThenInclude(pv => pv.SizeOption)
                .Include(p => p.Images)
                .Include(p => p.Gender)
                .AsQueryable();


            if (filter.CategoryIds != null && filter.CategoryIds.Any())
            {
                products = products.Where(p => p.Categories.Any(c => filter.CategoryIds.Contains(c.Id)));
            }

            if (filter.GenderId.HasValue)
            {
                products = products.Where(p => p.GenderId == filter.GenderId.Value);
            }

            if (filter.BrandId.HasValue)
            {
                products = products.Where(p => p.BrandId == filter.BrandId.Value);
            }

            //if (filter.MinPrice.HasValue)
            //{
            //    products = products.Where(p => p.Price >= filter.MinPrice.Value);
            //}

            //if (filter.MaxPrice.HasValue)
            //{
            //    products = products.Where(p => p.Price <= filter.MaxPrice.Value);
            //}


            if (filter.MinPrice.HasValue)
            {
                products = products.Where(p =>
                    (p.DiscountedPrice ?? p.Price) >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                products = products.Where(p =>
                    (p.DiscountedPrice ?? p.Price) <= filter.MaxPrice.Value);
            }

            return await products.ToListAsync();
        }

    }
}
