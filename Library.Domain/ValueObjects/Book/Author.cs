using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ValueObjects.Book
{
    public class Author
    {
        public string Value { get; }
        public Author(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Author cannot be empty.");

            Value = value;
        }
    }
}
