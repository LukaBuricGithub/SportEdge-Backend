using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;

namespace SportEdge.API.Mappings
{
    /// <summary>
    /// Provides mapping functionality between Gender domain models and DTOs.
    /// </summary>
    public class GenderMapping
    {
   
        /// <summary>
        /// Maps a Gender domain model to a GenderDto.
        /// </summary>
        /// <param name="gender">The domain model to convert.</param>
        /// <returns>A GenderDto containing the mapped data.</returns>
        public GenderDto ToDto(Gender gender)
        {
            return new GenderDto()
            {
                Id = gender.Id,
                Name = gender.Name
            };
        }


        /// <summary>
        /// Maps a CreateGenderRequestDto to a Gender domain model.
        /// </summary>
        /// <param name="gender">The DTO containing data for creating a gender.</param>
        /// <returns>A new Gender domain model instance.</returns>
        public Gender ToDomain(CreateGenderRequestDto gender)
        {
            return new Gender()
            {
                Name = gender.Name
            };
        }

        /// <summary>
        /// Maps an UpdateGenderRequestDto to a Gender domain model.
        /// </summary>
        /// <param name="gender">The DTO containing updated gender data.</param>
        /// <returns>A Gender domain model instance with updated values.</returns>
        public Gender ToDomain(UpdateGenderRequestDto gender)
        {
            return new Gender()
            {
                Name = gender.Name
            };
        }
    }
}
