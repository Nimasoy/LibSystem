using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ValueObjects.Book
{
    public class TotalCopies
    {
        public int Value { get; }
        public TotalCopies(int value)
        {
            if (value < 0) throw new ArgumentException("Invalid number of copies.");

            Value = value;
        }
    }
}
