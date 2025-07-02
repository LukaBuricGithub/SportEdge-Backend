using SportEdge.API.Mappings;
using SportEdge.API.Models.DTO;
using SportEdge.API.Repositories.Implementation;
using SportEdge.API.Repositories.Interface;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Services.Implementation
{
    /// <summary>
    /// Provides implementation for product variation-related service operations.
    /// </summary>
    public class ProductVariationService : IProductVariationService
    {
        private readonly IProductVariationRepository productVariationRepository;
        private readonly ProductVariationMapping mapping;
        
        public ProductVariationService(IProductVariationRepository productVariationRepository,ProductVariationMapping mapping)
        {
            this.productVariationRepository = productVariationRepository;
            this.mapping = mapping;
        }


        /// <inheritdoc/>
        public async Task<ProductVariationDto> CreateAsync(CreateProductVariationRequestDto request)
        {
            var productVariationDomain = mapping.ToDomain(request);
            var createdProductVariation = await productVariationRepository.CreateAsync(productVariationDomain);
            return mapping.ToDto(createdProductVariation);
        }


        /// <inheritdoc/>
        public async Task<List<ProductVariationDto>> GetAllAsync()
        {
            var productVariations = await productVariationRepository.GetAllAsync();
            return productVariations.Select(p => mapping.ToDto(p)).ToList();
        }


        /// <inheritdoc/>
        public async Task<ProductVariationDto> GetAsync(int id)
        {
            var productVariation = await productVariationRepository.GetAsync(id);
            if(productVariation == null)
            {
                throw new KeyNotFoundException($"Product variation with ID {id} not found.");
            }

            return mapping.ToDto(productVariation);  
        }


        /// <inheritdoc/>
        public async Task<ProductVariationDto> UpdateAsync(int id, UpdateProductVariationRequestDto request)
        {
            var existingProductVariation = await productVariationRepository.GetAsync(id);
            if(existingProductVariation == null)
            {
                throw new KeyNotFoundException($"Product variation with ID {id} not found.");
            }

            var productVariationDomain = mapping.ToDomain(request);

            existingProductVariation.SizeOptionId = productVariationDomain.SizeOptionId;
            existingProductVariation.ProductId = productVariationDomain.ProductId;
            existingProductVariation.QuantityInStock = productVariationDomain.QuantityInStock;

            var updatedProductVariation = await productVariationRepository.UpdateAsync(existingProductVariation);
            return mapping.ToDto(updatedProductVariation);
        }
    }
}
