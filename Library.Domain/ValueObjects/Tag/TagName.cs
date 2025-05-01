namespace Library.Domain.ValueObjects.Tag
{
    public class TagName
    {
        public string Value { get; }
        public TagName(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Tag name cannot be empty");

            Value = value;
        }
    }
}
