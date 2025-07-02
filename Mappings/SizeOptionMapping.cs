using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;

namespace SportEdge.API.Mappings
{

    /// <summary>
    /// Provides mapping functionality between Size option domain models and DTOs.
    /// </summary>
    public class SizeOptionMapping
    {


        /// <summary>
        /// Maps a CreateSizeOptionRequestDto to a Size option domain model.
        /// </summary>
        /// <param name="sizeOption">The DTO containing data for creating a size option.</param>
        /// <returns>A new Size option domain model instance.</returns>
        public SizeOption ToDomain(CreateSizeOptionRequestDto sizeOption)
        {
            return new SizeOption()
            {
                SizeName = sizeOption.SizeName,
                GenderId = sizeOption.GenderId
            };
        }


        /// <summary>
        /// Maps an UpdateSizeOptionRequestDto to a Size option domain model.
        /// </summary>
        /// <param name="sizeOption">The DTO containing updated size option data.</param>
        /// <returns>A Size option domain model instance with updated values.</returns>
        public SizeOption ToDomain(UpdateSizeOptionRequestDto sizeOption)
        {
            return new SizeOption()
            {
                SizeName = sizeOption.SizeName,
                GenderId = sizeOption.GenderId
            };
        }


        /// <summary>
        /// Maps a Size option domain model to a SizeOptionDto.
        /// </summary>
        /// <param name="sizeOption">The domain model to convert.</param>
        /// <returns>A SizeOptionDto containing the mapped data.</returns>
        public SizeOptionDto ToDto(SizeOption sizeOption) 
        {
            return new SizeOptionDto()
            {
                Id = sizeOption.Id,
                SizeName = sizeOption.SizeName,
                GenderId = sizeOption.GenderId,
                GenderName = sizeOption.Gender?.Name
            };
        }

    }
}
