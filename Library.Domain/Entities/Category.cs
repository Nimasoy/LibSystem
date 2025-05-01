using Library.Domain.ValueObjects.Category;

namespace Library.Domain.Entities
{
    public class Category(CategoryName name)
    {
        public int Id { get; private set; }
        public CategoryName Name { get; private set; } = name ?? throw new ArgumentNullException(nameof(name));
        public ICollection<Book> Books { get; private set; } = [];
    }
}