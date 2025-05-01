using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ValueObjects.Book
{
    public class ISBN
    {
        public string Value { get; }
        public ISBN(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("ISBN cannot be empty.");
            if (value.Length != 13 && value.Length != 10) throw new ArgumentException("ISBN must have 10 or 13 digits");
            if (value.Contains(' ') || value.Contains('-')) throw new ArgumentException("ISBN cannot contain spaces or hyphens.");

            Value = value;
        }
    }
}
