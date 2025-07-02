using Microsoft.EntityFrameworkCore;
using SportEdge.API.Mappings;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;
using SportEdge.API.Repositories.Implementation;
using SportEdge.API.Repositories.Interface;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Services.Implementation
{

    /// <summary>
    /// Provides implementation for product-related service operations.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IGenderRepository genderRepository;
        private readonly ISizeOptionRepository sizeOptionRepository;
        private readonly IProductVariationRepository productVariationRepository;
        private readonly ProductMapping mapping;
        private readonly ProductVariationMapping productVariationMapping;

        public ProductService(IProductRepository productRepository,ICategoryRepository categoryRepository,
            IGenderRepository genderRepository,ISizeOptionRepository sizeOptionRepository, 
            IProductVariationRepository productVariationRepository, ProductMapping mapping, ProductVariationMapping productVariationMapping) 
        { 
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.genderRepository = genderRepository;
            this.sizeOptionRepository = sizeOptionRepository;
            this.productVariationRepository = productVariationRepository;
            this.mapping = mapping;
            this.productVariationMapping = productVariationMapping;
        }

        /// <inheritdoc/>
        public async Task<ProductDto> CreateAsync(CreateProductRequestDto request)
        {
            var categories = await categoryRepository.GetByIdsAsync(request.CategoryIds);
            if (!categories.Any())
            {
                throw new InvalidDataException("Product needs categories.");
            }
            
            var productDomain = mapping.ToDomain(request, categories);
            var createdProduct = await productRepository.CreateAsync(productDomain);

            var sizeOptions = await sizeOptionRepository.GetSizeOptionsByGenderIdAsync(request.GenderId);

            foreach (var sizeOption in sizeOptions)
            { 
                var newProductVariation = new CreateProductVariationRequestDto 
                {   ProductId = createdProduct.Id, 
                    SizeOptionId = sizeOption.Id,
                    QuantityInStock = 0
                };

                var createdProductVariation = await productVariationRepository.CreateAsync(productVariationMapping.ToDomain(newProductVariation));
            }

            return mapping.ToDto(createdProduct);

        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await productRepository.DeleteAsync(id);
            if (!deleted) 
            {
                throw new KeyNotFoundException($"Product with ID {id} not found."); ;
            }
            return true;
        }

        /// <inheritdoc/>
        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await productRepository.GetAllAsync();
            return products.Select(p=>mapping.ToDto(p)).ToList();
        }


        /// <inheritdoc/>
        public async Task<ProductDto> GetAsync(int id)
        {
            var product = await productRepository.GetAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found."); ;
            }
            return mapping.ToDto(product);
        }

       

        /// <inheritdoc/>
        public async Task<ProductDto> UpdateAsync(int id, UpdateProductRequestDto request)
        {

            var existingProduct = await productRepository.GetAsync(id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found."); ;
            }

            var categories = await categoryRepository.GetByIdsAsync(request.CategoryIds);
            if (!categories.Any())
            {
                throw new InvalidDataException("Product needs categories.");
            }

            var productDomain = mapping.ToDomain(request, categories);


            existingProduct.Name = productDomain.Name;
            existingProduct.ShortDescription = productDomain.ShortDescription;
            existingProduct.Price = productDomain.Price;
            existingProduct.DiscountedPrice = productDomain.DiscountedPrice;
            existingProduct.BrandId = productDomain.BrandId;

            existingProduct.Categories.Clear();


            var newCatategories = await categoryRepository.GetByIdsAsync(request.CategoryIds);
            foreach (var category in newCatategories) 
            { 
                existingProduct.Categories.Add(category);
            }

            var updatedProduct = await productRepository.UpdateAsync(existingProduct);
            return mapping.ToDto(updatedProduct);


            /*
             * Ovaj dio radi
             * 
             * 
            var categories = await categoryRepository.GetByIdsAsync(request.CategoryIds);
            var productDomain = mapping.ToDomain(request, categories);


            var updatedProduct = await productRepository.UpdateAsync(id,productDomain,request.CategoryIds);

            return updatedProduct == null ? null : mapping.ToDto(updatedProduct);
             */
        }

        /// <inheritdoc/>
        public async Task<List<ProductDto>> SearchProductsAsync(string? query)
        {
            var products = await productRepository.SearchProductsAsync(query);
            return products.Select(p => mapping.ToDto(p)).ToList();

        }
    }
}
