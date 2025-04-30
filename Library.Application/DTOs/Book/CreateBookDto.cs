using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTOs.Book
{
    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int TotalCopies { get; set; }
        public int CategoryId { get; set; }
    }
}
