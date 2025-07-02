using Microsoft.EntityFrameworkCore;
using SportEdge.API.Mappings;
using SportEdge.API.Models.DTO;
using SportEdge.API.Models.Enums;
using SportEdge.API.Repositories.Interface;
using SportEdge.API.Services.Interface;
using System.Linq;

namespace SportEdge.API.Services.Implementation
{
    /// <summary>
    /// Provides implementation for brand-related service operations.
    /// </summary>
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository brandRepository;
        private readonly BrandMapping mapping;

        public BrandService(IBrandRepository brandRepository, BrandMapping mapping)
        {
            this.brandRepository = brandRepository;
            this.mapping = mapping;
        }

        /// <inheritdoc/>
        public async Task<BrandDto> CreateAsync(CreateBrandRequestDto request)
        {
            var brandDomain = mapping.ToDomain(request);
            var createdBrand = await brandRepository.CreateAsync(brandDomain);
            return mapping.ToDto(createdBrand);
        }

        /// <inheritdoc/>
        public async Task<List<BrandDto>> GetAllAsync()
        {
            var brands = await brandRepository.GetAllAsync();
            return brands.Select(b=>mapping.ToDto(b)).ToList();
        }

        /// <inheritdoc/>
        public async Task<BrandDto> GetAsync(int id)
        {
            var brand = await brandRepository.GetAsync(id);
            if (brand == null)
            {
                throw new KeyNotFoundException($"Brand with ID {id} not found.");
            }
            return mapping.ToDto(brand);
        }

        /// <inheritdoc/>
        public async Task<BrandDto> UpdateAsync(int id, UpdateBrandRequestDto request)
        {
            var existingBrand = await brandRepository.GetAsync(id);
            if (existingBrand == null) 
            {
                throw new KeyNotFoundException($"Brand with ID {id} not found.");
            }

            var brandDomain = mapping.ToDomain(request);

            existingBrand.Name = brandDomain.Name;

            var updatedBrand = await brandRepository.UpdateAsync(existingBrand);
            return mapping.ToDto(updatedBrand);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int id)
        {
            var result = await brandRepository.DeleteAsync(id);

            return result switch
            {
                DeleteResult.Success => true,
                DeleteResult.NotFound => throw new KeyNotFoundException($"Brand with ID {id} not found."),
                DeleteResult.HasRelatedObjects => throw new InvalidOperationException($"Brand with ID {id} cannot be deleted because it has related objects."),
                _ => throw new Exception("Unexpected deletion result.")
            };
        }

        /// <inheritdoc/>
        public async Task<List<BrandDto>> GetBrandsByNameAsync(string search)
        {
            var brands = await brandRepository.GetBrandsByNameAsync(search);
            return brands.Select(b => mapping.ToDto(b)).ToList();
        }

    }
}
