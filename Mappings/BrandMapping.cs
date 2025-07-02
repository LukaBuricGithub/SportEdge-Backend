using SportEdge.API.Models.DTO;
using SportEdge.API.Models.Domain;

namespace SportEdge.API.Mappings
{

    /// <summary>
    /// Provides mapping functionality between Brand domain models and DTOs.
    /// </summary>
    public class BrandMapping
    {

        /// <summary>
        /// Maps a CreateBrandRequestDto to a Brand domain model.
        /// </summary>
        /// <param name="brand">The DTO containing data for creating a brand.</param>
        /// <returns>A new Brand domain model instance.</returns>
        public Brand ToDomain(CreateBrandRequestDto brand)
        {
            return new Brand()
            {
                Name = brand.Name
            };
        }


        /// <summary>
        /// Maps an UpdateBrandRequestDto to a Brand domain model.
        /// </summary>
        /// <param name="brand">The DTO containing updated brand data.</param>
        /// <returns>A Brand domain model instance with updated values.</returns>
        public Brand ToDomain(UpdateBrandRequestDto brand)
        {
            return new Brand()
            {
                Name = brand.Name
            };
        }


        /// <summary>
        /// Maps a Brand domain model to a BrandDto.
        /// </summary>
        /// <param name="brand">The domain model to convert.</param>
        /// <returns>A BrandDto containing the mapped data.</returns>
        public BrandDto ToDto(Brand brand)
        {
            return new BrandDto()
            {
                Id = brand.Id,
                Name = brand.Name
            };
        }
    }
}
