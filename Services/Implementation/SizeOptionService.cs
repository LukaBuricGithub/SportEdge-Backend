using SportEdge.API.Mappings;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;
using SportEdge.API.Repositories.Implementation;
using SportEdge.API.Repositories.Interface;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Services.Implementation
{

    /// <summary>
    /// Provides implementation for size option-related service operations.
    /// </summary>
    public class SizeOptionService : ISizeOptionService
    {
        private readonly ISizeOptionRepository sizeOptionRepository;
        private readonly SizeOptionMapping mapping;


        public SizeOptionService(ISizeOptionRepository sizeOptionRepository, SizeOptionMapping mapping)
        {
            this.sizeOptionRepository = sizeOptionRepository;
            this.mapping = mapping;
        }

        /// <inheritdoc/>
        public async Task<SizeOptionDto> CreateAsync(CreateSizeOptionRequestDto request)
        {
            var sizeOptionDomain = mapping.ToDomain(request);
            var createdSizeOption = await sizeOptionRepository.CreateAsync(sizeOptionDomain);
            return mapping.ToDto(createdSizeOption);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await sizeOptionRepository.DeleteAsync(id);
            if (!deleted)
            {
                throw new KeyNotFoundException($"Size option with ID {id} not found.");
            }
            return true;
        }

        /// <inheritdoc/>
        public async Task<List<SizeOptionDto>> GetAllAsync()
        {
            var sizeOptions = await sizeOptionRepository.GetAllAsync();
            return sizeOptions.Select(s => mapping.ToDto(s)).ToList();
        }

        /// <inheritdoc/>
        public async Task<SizeOptionDto> GetAsync(int id)
        {
            var sizeOption = await sizeOptionRepository.GetAsync(id);
            if(sizeOption == null)
            {
                throw new KeyNotFoundException($"Size option with ID {id} not found.");
            }
            return mapping.ToDto(sizeOption);
        }

    
        /// <inheritdoc/>
        public async Task<SizeOptionDto> UpdateAsync(int id, UpdateSizeOptionRequestDto request)
        {
            var existingSizeOption = await sizeOptionRepository.GetAsync(id);
            if (existingSizeOption == null)
            {
                throw new KeyNotFoundException($"Size option with ID {id} not found.");
            }

            var sizeOptionDomain = mapping.ToDomain(request);

            existingSizeOption.SizeName = sizeOptionDomain.SizeName;
            existingSizeOption.GenderId = sizeOptionDomain.GenderId;


            var updatedSizeOption = await sizeOptionRepository.UpdateAsync(existingSizeOption);
            return mapping.ToDto(updatedSizeOption);
        }



        /// <inheritdoc/>
        public async Task<List<SizeOptionDto>> GetSizeOptionsByGenderIdAsync(int genderId)
        {
            var sizeOptions = await sizeOptionRepository.GetSizeOptionsByGenderIdAsync(genderId);
            return sizeOptions.Select(s => mapping.ToDto(s)).ToList();
        }
    }
}
