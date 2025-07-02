using SportEdge.API.Models.Domain;

namespace SportEdge.API.Models.DTO
{

    /// <summary>
    /// DTO representing a category.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Unique identifier for the category.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Category name.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// List of parent categories (their names).
        /// </summary>
        public string[] ParentCategories { get; set; }

        /// <summary>
        /// List of child categories (their names).
        /// </summary>
        public string[] ChildCategories { get; set; }


        /// <summary>
        /// List of parent categories (their IDs).
        /// </summary>
        public int[] ParentCategoriesIDs { get; set; }


        /// <summary>
        /// List of child categories (their IDs).
        /// </summary>
        public int[] ChildCategoriesIDs { get; set; }


        //public int? ParentCategoryId { get; set; }

        /*
        public List<CategoryDto> ChildCategories { get; set; } = new List<CategoryDto>();
        */
    }
}
