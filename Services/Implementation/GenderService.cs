using SportEdge.API.Mappings;
using SportEdge.API.Models.DTO;
using SportEdge.API.Repositories.Implementation;
using SportEdge.API.Repositories.Interface;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Services.Implementation
{
    /// <summary>
    /// Provides implementation for gender-related service operations.
    /// </summary>
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository genderRepository;
        private readonly GenderMapping mapping;

        public GenderService(IGenderRepository genderRepository, GenderMapping mapping)
        {
            this.genderRepository = genderRepository;
            this.mapping = mapping;
        }

        /// <inheritdoc/>
        public async Task<GenderDto> CreateAsync(CreateGenderRequestDto request)
        {
            var genderDomain = mapping.ToDomain(request);
            var createdGender = await genderRepository.CreateAsync(genderDomain);
            return mapping.ToDto(createdGender);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await genderRepository.DeleteAsync(id);
            if (!deleted)
            {
                throw new KeyNotFoundException($"Gender with ID {id} not found.");
            }
            return true;
        }

        /// <inheritdoc/>
        public async Task<List<GenderDto>> GetAllAsync()
        {
            var genders = await genderRepository.GetAllAsync();
            return genders.Select(g=>mapping.ToDto(g)).ToList();
        }

        /// <inheritdoc/>
        public async Task<GenderDto> GetAsync(int id)
        {
            var gender = await genderRepository.GetAsync(id);
            if (gender == null)
            {
                throw new KeyNotFoundException($"Gender with ID {id} not found.");
            }
            return mapping.ToDto(gender);
        }

        /// <inheritdoc/>
        public async Task<GenderDto> UpdateAsync(int id, UpdateGenderRequestDto request)
        {
            var existingGender = await genderRepository.GetAsync(id);
            if (existingGender == null) 
            {
                throw new KeyNotFoundException($"Gender with ID {id} not found.");
            }

            var genderDomain = mapping.ToDomain(request);

            existingGender.Name = genderDomain.Name;

            var updatedGender = await genderRepository.UpdateAsync(existingGender);
            return mapping.ToDto(updatedGender);
        }
    }
}
