namespace Library.Domain.ValueObjects.User
{
    public class Email
    {
        public string Value { get; }
        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
                throw new ArgumentException("Invalid email.");

            Value = value;
        }
    }
}