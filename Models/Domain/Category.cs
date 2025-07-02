using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.Domain
{
    /// <summary>
    /// Domain model for representing a category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Unique identifier for the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category name (required, max 150 characters).
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }


        /// <summary>
        /// Parent category's ID (can be null).
        /// </summary>
        public int? ParentCategoryId { get; set; }

        /// <summary>
        /// Navigation property for the parent category.
        /// </summary>
        public Category ParentCategory { get; set; }


        /// <summary>
        /// List of child categories associated with parent category.
        /// </summary>
        public ICollection<Category> ChildCategories { get; set; } = new List<Category>();


        /// <summary>
        /// List of products associated with category.
        /// </summary>
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
