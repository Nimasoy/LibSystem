using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ValueObjects.Book
{
    public class Title
    {
        public string Value { get;}
        
        public Title(string value)
        {
            if(string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Title cannot be empty.");

            Value = value;
        }
    }
}
