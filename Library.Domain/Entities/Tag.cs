using Library.Domain.ValueObjects.Tag;
namespace Library.Domain.Entities
{
    public class Tag(TagName name)
    {
        public int Id { get; private set; }
        public TagName Name { get; private set; } = name ?? throw new ArgumentNullException(nameof(name));

        public ICollection<Book> Books { get; private set; } = [];
    }
}