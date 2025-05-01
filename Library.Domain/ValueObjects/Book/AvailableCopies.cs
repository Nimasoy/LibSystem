using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ValueObjects.Book
{
    public class AvailableCopies
    {
        public int Value { get; }
        public AvailableCopies(int value)
        {
            if (value < 0) throw new ArgumentException("Invalid number of copies.");

            Value = value;
        }
    }
}
