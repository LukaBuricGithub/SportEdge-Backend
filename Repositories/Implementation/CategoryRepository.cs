using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportEdge.API.Data;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.Enums;
using SportEdge.API.Repositories.Interface;

namespace SportEdge.API.Repositories.Implementation
{
    /// <summary>
    /// Repository implementation for category-related data operations.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        /// <inheritdoc/>
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }



        /// <inheritdoc/>
        public async Task<Category> GetAsync(int id)
        {
            /*
                 return await dbContext.Categories
                     .Include(c => c.ChildCategories)
                     .Include(c => c.ParentCategory) // If ParentCategory is a single object
                     .FirstOrDefaultAsync(c => c.Id == id);
             */

            var category = await dbContext.Categories
                .Include(c => c.ChildCategories)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) 
            {
                return null;
            }
            
            category.ParentCategory = await LoadParentCategories(category.ParentCategoryId);

            return category;
        }




        /// <summary>
        /// Recursively loads the parent categories for a given category by its parent ID.
        /// </summary>
        /// <param name="parentId">The ID of the parent category to load. Can be null.</param>
        /// <returns>The parent category with its hierarchy fully loaded, or null if no parent exists.</returns>
        private async Task<Category> LoadParentCategories(int? parentId)
        {
            if (parentId == null)
            {
                return null;
            }

            var parent = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == parentId);
            if (parent != null)
            {
                parent.ParentCategory = await LoadParentCategories(parent.ParentCategoryId);
            }

            return parent;
        }

        /// <inheritdoc/>
        public async Task<List<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Category> UpdateAsync(Category category)
        {
            dbContext.Categories.Update(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        /// <inheritdoc/>
        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var category = await dbContext.Categories
                    .Include(c => c.Products)
                    .Include(c => c.ChildCategories)
                    .Include(c => c.ParentCategory)
                    .FirstOrDefaultAsync(c => c.Id == id);

          
            if (category == null)
            {
                return DeleteResult.NotFound;
            }

            if (category.Products.Any())
            {
                return DeleteResult.HasRelatedObjects;
            }

            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
            return DeleteResult.Success;
        }

        /// <inheritdoc/>
        public async Task<List<Category>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await dbContext.Categories
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
        }


        /// <inheritdoc/>
        public async Task<List<Category>> GetRootCategoriesAsync()
        {
            return await dbContext.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToListAsync();
        }
    }
}
