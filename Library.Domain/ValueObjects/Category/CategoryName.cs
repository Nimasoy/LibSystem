namespace Library.Domain.ValueObjects.Category
{
    public class CategoryName
    {
        // Private parameterless constructor for Entity Framework Core materialization
        private CategoryName() { }

        public string Value { get; private set; }

        // Public constructor for domain logic
        public CategoryName(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Category name cannot be empty");
            Value = value;
        }
    }
}
