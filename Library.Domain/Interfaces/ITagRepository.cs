using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface ITagRepository
    {
        Task<Tag?> GetByIdAsync(int id);
        Task<List<Tag>> GetAllAsync();
        Task AddAsync(Tag tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(Tag tag);

        Task<Tag?> GetByNameAsync(string name);
        Task<bool> ExistsAsync(string name);


    }
}