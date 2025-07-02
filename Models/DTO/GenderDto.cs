namespace SportEdge.API.Models.DTO
{
    /// <summary>
    ///DTO representing a gender.
    /// </summary>
    public class GenderDto
    {
        /// <summary>
        /// Unique identifier for the gender.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Gender name.
        /// </summary>
        public string Name { get; set; }
    }
}
