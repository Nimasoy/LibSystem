using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(int id);
        Task<List<Category>> GetAllAsync();
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);

        Task<bool> ExistsAsync(int categoryId);
        Task<Category?> GetByNameAsync(string name);

    }
}