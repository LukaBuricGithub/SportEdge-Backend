using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;

namespace SportEdge.API.Mappings
{

    /// <summary>
    /// Provides mapping functionality between Category domain models and DTOs.
    /// </summary>
    public class CategoryMapping
    {


        /// <summary>
        /// Maps a CreateCategoryRequestDto to a Category domain model.
        /// </summary>
        /// <param name="category">The DTO containing data for creating a category.</param>
        /// <returns>A new Category domain model instance.</returns>
        public Category ToDomain(CreateCategoryRequestDto category)
        {
            return new Category()
            {
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId
            };
        }


        /// <summary>
        /// Maps an UpdateCategoryRequestDto to a Category domain model.
        /// </summary>
        /// <param name="category">The DTO containing updated category data.</param>
        /// <returns>A Category domain model instance with updated values.</returns>
        public Category ToDomain(UpdateCategoryRequestDto category)
        {
            return new Category()
            {
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId
            };
        }


        /// <summary>
        /// Maps a Category domain model to a CategoryDto.
        /// </summary>
        /// <param name="category">The domain model to convert.</param>
        /// <returns>A CategoryDto containing the mapped data.</returns>
        public CategoryDto ToDto (Category category)
        {
            return new CategoryDto()
            {
                Id = category.Id, 
                Name = category.Name,
                ParentCategories = GetParentCategories(category).ToArray(),
                ChildCategories = category.ChildCategories?.Select(c => c.Name).ToArray() ?? new string[0],
                ParentCategoriesIDs = GetParentCategoriesIDs(category).ToArray(),
                ChildCategoriesIDs = category.ChildCategories?.Select(c => c.Id).ToArray() ?? new int[0]
            };
        }



        // Retrieves the names of all parent categories for a given category.
        private List<string> GetParentCategories(Category category)
        {
            List<string> parents = new List<string>();

            while (category.ParentCategory != null)
            {
                category = category.ParentCategory;
                parents.Add(category.Name);
            }

            return parents;
        }

        // Retrieves the IDs of all parent categories for a given category.
        private List<int> GetParentCategoriesIDs(Category category)
        {
            List<int> parents = new List<int>();

            while(category.ParentCategory != null)
            {
                category = category.ParentCategory;
                parents.Add(category.Id);

            }

            return parents;
        }




        /*
        --->Calling function ChildCategories = GetChildCategories(category).ToArray()

        private List<string> GetChildCategories(Category category)
        {
            List<string> children = new List<string>();

            foreach (var child in category.ChildCategories)
            {
                children.Add(child.Name);
                children.AddRange(GetChildCategories(child));
            }

            return children;
        }*/
    }
}
