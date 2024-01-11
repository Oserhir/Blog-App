﻿using TheBlogProject.Models;

namespace TheBlogProject.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task AddNewCategoryAsync(Category category);
        public Task<List<Category>> GetAllCategoriesAsync();
        public Task<IQueryable<Category>> GetCategoriesAsync();
        public Task<Category> GetCategoryByIdAsync(int CategoryId);
        public Task<List<Post>> GetPostsByCategory(int CategoryId);
    }
}
