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




        public async Task<FilteredProductsResultDomain> GetFilteredProductsAsync( ProductFilterWithTextDto filter)
        {
            var products = dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Categories)
                .Include(p => p.ProductVariations)
                    .ThenInclude(pv => pv.SizeOption)
                .Include(p => p.Images)
                .Include(p => p.Gender)
                .AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(filter.searchText))
            {
                var terms = filter.searchText.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var term in terms)
                {
                    products = products.Where(p =>
                        EF.Functions.Like(p.Name.ToLower(), $"%{term}%") ||
                        EF.Functions.Like(p.Brand.Name.ToLower(), $"%{term}%") ||
                        p.Categories.Any(c => EF.Functions.Like(c.Name.ToLower(), $"%{term}%")));
                }
            }

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

            if (filter.MinPrice.HasValue)
            {
                products = products.Where(p => (p.DiscountedPrice ?? p.Price) >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                products = products.Where(p => (p.DiscountedPrice ?? p.Price) <= filter.MaxPrice.Value);
            }

            products = filter.SortBy?.ToLower() switch
            {
                "name_asc" => products.OrderBy(p => p.Name),
                "name_desc" => products.OrderByDescending(p => p.Name),
                "price_asc" => products.OrderBy(p => (p.DiscountedPrice ?? p.Price)),
                "price_desc" => products.OrderByDescending(p => (p.DiscountedPrice ?? p.Price)),
                _ => products.OrderBy(p => p.Name) // default
            };

            var totalCount = await products.CountAsync();

            var pagedProducts = await products
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new FilteredProductsResultDomain
            {
                ProductsDomain = pagedProducts,
                TotalCountDomain = totalCount
            };
        }



        /// <inheritdoc/>
        public async Task<FilteredProductsResultDomain> SearchProductsAsync(string? query, ProductFilterDto filter)
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
            var totalCount = await products.CountAsync();

            var pagedProducts = await products
             .Skip((filter.PageNumber - 1) * filter.PageSize)
             .Take(filter.PageSize)
             .ToListAsync();

            return new FilteredProductsResultDomain
            {
                ProductsDomain = pagedProducts,
                TotalCountDomain = totalCount
            };
        }

        /// <inheritdoc/>
        public async Task<FilteredProductsResultDomain> GetProductsByGenderTypeAsync(string name, ProductFilterDto filter)
        {
            //return await dbContext.Products
            //    .Include(p => p.Brand)
            //    .Include(p => p.Categories)
            //    .Include(p => p.ProductVariations)
            //        .ThenInclude(pv => pv.SizeOption)
            //    .Include(p => p.Images)
            //    .Include(p => p.Gender)
            //    .Where(p => p.Gender != null && p.Gender.Name.ToLower() == name.ToLower())
            //    .ToListAsync();


            var query =  dbContext.Products
             .Include(p => p.Brand)
             .Include(p => p.Categories)
             .Include(p => p.ProductVariations)
                 .ThenInclude(pv => pv.SizeOption)
             .Include(p => p.Images)
             .Include(p => p.Gender)
             .Where(p => p.Gender != null && p.Gender.Name.ToLower() == name.ToLower());

            var totalCount = await query.CountAsync();

            var pagedProducts = await query
             .Skip((filter.PageNumber - 1) * filter.PageSize)
             .Take(filter.PageSize)
             .ToListAsync();

            return new FilteredProductsResultDomain
            {
                ProductsDomain = pagedProducts,
                TotalCountDomain = totalCount
            };

        }


        /// <inheritdoc/>
        public async Task<FilteredProductsResultDomain> GetProductsByCategoryIdAsync(int categoryId,ProductFilterDto filter)
        {
            var query = dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Categories)
                .Include(p => p.ProductVariations)
                    .ThenInclude(pv => pv.SizeOption)
                .Include(p => p.Images)
                .Include(p => p.Gender)
                .Where(p => p.Categories.Any(c => c.Id == categoryId));

            var totalCount = await query.CountAsync();

            var pagedProducts = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new FilteredProductsResultDomain
            {
                ProductsDomain = pagedProducts,
                TotalCountDomain = totalCount
            };

        }

        public async Task<FilteredProductsResultDomain> FilterProductsAsync(ProductFilterDto filter)
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


            products = filter.SortBy?.ToLower() switch
            {

                "name_asc" => products.OrderBy(p => p.Name),
                "name_desc" => products.OrderByDescending(p => p.Name),
                "price_asc" => products.OrderBy(p => (p.DiscountedPrice ?? p.Price)),
                "price_desc" => products.OrderByDescending(p => (p.DiscountedPrice ?? p.Price)),
                _ => products.OrderBy(p => p.Name)
            };

            var totalCount = await products.CountAsync();

            var pagedProducts = await products
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();


            return new FilteredProductsResultDomain
            {
                ProductsDomain = pagedProducts,
                TotalCountDomain = totalCount
            };
            //FilteredProductsResultDomain
            //return await products.ToListAsync();

        }

    }
}
