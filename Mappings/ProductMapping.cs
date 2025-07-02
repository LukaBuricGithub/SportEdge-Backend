using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;

namespace SportEdge.API.Mappings
{

    /// <summary>
    /// Provides mapping functionality between Product domain models and DTOs.
    /// </summary>
    public class ProductMapping
    {

        /// <summary>
        /// Maps a CreateProductRequestDto to a Product domain model.
        /// </summary>
        /// <param name="request">The DTO containing data for creating a product.</param>
        /// <param name="categories">List of categories associated with a product.</param>
        /// <returns>A new Product domain model instance.</returns>
        public Product ToDomain(CreateProductRequestDto request, List<Category> categories)
        {
            return new Product()
            {
                Name = request.Name,
                ShortDescription = request.ShortDescription,
                Price = request.Price,
                DiscountedPrice = request.DiscountedPrice,
                //StockQuantity = request.StockQuantity,
                BrandId = request.BrandId,
                //GenderId = request.GenderId,
                Categories = categories,
                GenderId = request.GenderId
            };
        }


     
        /// <summary>
        /// Maps an UpdateProductRequestDto to a Product domain model.
        /// </summary>
        /// <param name="request">The DTO containing updated product data.</param>
        /// <param name="categories">List of categories associated with an updated product.</param>
        /// <returns>A Product domain model instance with updated values.</returns>
        public Product ToDomain(UpdateProductRequestDto request, List<Category> categories)
        {
            return new Product()
            {
                Name = request.Name,
                ShortDescription = request.ShortDescription,
                Price = request.Price,
                DiscountedPrice = request.DiscountedPrice,
                //StockQuantity = request.StockQuantity,
                BrandId = request.BrandId,
                //GenderId = request.GenderId,
                Categories = categories
            };
        }


        /// <summary>
        /// Maps a Product domain model to a ProductDto.
        /// </summary>
        /// <param name="product">The domain model to convert.</param>
        /// <returns>A ProductDto containing the mapped data.</returns>
        public ProductDto ToDto(Product product) 
        {
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                Price = product.Price,
                DiscountedPrice = product.DiscountedPrice,
                BrandId = product.BrandId,
                //GenderId = product.GenderId,
                BrandName = product.Brand?.Name,
                CategoryNames = product.Categories?.Select(c => c.Name).ToList() ?? new List<string>(),
                CategoryIds = product.Categories?.Select(c => c.Id).ToList() ?? new List<int>(),
                Variations = product.ProductVariations?.Select(v => new ProductVariationDto
                {
                    Id = v.Id,
                    ProductId = v.ProductId,
                    ProductName = product.Name,
                    SizeOptionId = v.SizeOptionId,
                    SizeOptionName = v.SizeOption?.SizeName,
                    QuantityInStock = v.QuantityInStock
                }).ToList(),
                Quantity = product.ProductVariations?.Sum(v => v.QuantityInStock) ?? 0,
                ImageFilenames = product.Images?.Select(img => img.Filename).ToList(),
                GenderId = product.GenderId,
                GenderName = product.Gender?.Name

            };
        }
    }
}
