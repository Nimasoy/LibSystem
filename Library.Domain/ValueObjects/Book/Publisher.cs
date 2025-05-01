using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ValueObjects.Book
{
    public class Publisher
    {
        public string Value { get; }
        public Publisher(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Publisher cannot be empty");

            Value = value;
        }
    }
}
