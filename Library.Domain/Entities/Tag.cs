using Library.Domain.ValueObjects.Tag;
namespace Library.Domain.Entities
{
    public class Tag
    {
        public int Id { get; private set; }
        public TagName Name { get; private set; }

        public ICollection<Book> Books { get; private set; } = [];
        
        private Tag() { } // For EF Core

        public Tag(TagName name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }

}