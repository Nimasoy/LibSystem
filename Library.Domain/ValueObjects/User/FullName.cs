namespace Library.Domain.ValueObjects.User
{
    public class FullName
    {
        public string Value { get; set; }
        public FullName(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name cannot be empty.");

            Value = value;
        }
    }
}