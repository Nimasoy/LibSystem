namespace Library.Domain.ValueObjects.Category
{
    public class CategoryName
    {
        public string Value { get;}
        public CategoryName(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Category name cannot be empty");

            Value = value;
        }
    }
}
