using SportEdge.API.Mappings;
using SportEdge.API.Models.DTO;
using SportEdge.API.Models.Enums;
using SportEdge.API.Repositories.Implementation;
using SportEdge.API.Repositories.Interface;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Services.Implementation
{
    /// <summary>
    /// Provides implementation for category-related service operations.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly CategoryMapping mapping;


        public CategoryService(ICategoryRepository categoryRepository, CategoryMapping mapping)
        {
            this.categoryRepository = categoryRepository;
            this.mapping = mapping;
        }

        /// <inheritdoc/>
        public async Task<CategoryDto> CreateAsync(CreateCategoryRequestDto request)
        {
            var categoryDomain = mapping.ToDomain(request);
            var createdCategory = await categoryRepository.CreateAsync(categoryDomain);
            return mapping.ToDto(createdCategory);

        }

        /// <inheritdoc/>
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await categoryRepository.GetAllAsync();
            return categories.Select(c=>mapping.ToDto(c)).ToList();
        }

        /// <inheritdoc/>
        public async Task<CategoryDto> GetAsync(int id)
        {

            var category = await categoryRepository.GetAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            return mapping.ToDto(category);
        }

        /// <inheritdoc/>
        public async Task<CategoryDto> UpdateAsync(int id, UpdateCategoryRequestDto request)
        {

            var existingCategory = await categoryRepository.GetAsync(id);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            var categoryDto = mapping.ToDto(existingCategory);

            if (categoryDto.ChildCategories.Count() > 0)
            {
                throw new InvalidOperationException("Cannot directly update category with children.");
            }
            
            var categoryDomain = mapping.ToDomain(request);

            existingCategory.Name = categoryDomain.Name;
            existingCategory.ParentCategoryId = categoryDomain.ParentCategoryId;


            var updatedCategory = await categoryRepository.UpdateAsync(existingCategory);

            return mapping.ToDto(updatedCategory);
        }


        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            var categoryDto = mapping.ToDto(category);

            if (categoryDto.ChildCategories.Count() > 0)
            {
                throw new InvalidOperationException("Cannot directly delete category with children.");
            }


            var result = await categoryRepository.DeleteAsync(id);

            return result switch
            {
                DeleteResult.Success => true,
                DeleteResult.NotFound => throw new KeyNotFoundException($"Category with ID {id} not found."),
                DeleteResult.HasRelatedObjects => throw new InvalidOperationException($"Category with ID {id} cannot be deleted because it has related objects."),
                _ => throw new Exception("Unexpected deletion result.")

            };

        }


        /// <inheritdoc/>
        public async Task<List<CategoryDto>> GetRootCategoriesAsync()
        {
            var categories = await categoryRepository.GetRootCategoriesAsync();
            return categories.Select(c => mapping.ToDto(c)).ToList();
        }
    }
}
