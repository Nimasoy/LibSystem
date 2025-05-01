using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ValueObjects.Book
{
    public class Year
    {
        public int Value { get; }
        public Year(int value)
        {
            if (value > DateTime.UtcNow.Year) throw new ArgumentException("Invalid publication year.");

            Value = value;
        }
    }
}
