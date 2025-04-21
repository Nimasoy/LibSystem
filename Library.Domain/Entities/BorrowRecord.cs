using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class BorrowRecord
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }

        public DateTime BorrowedAt { get; set; }
        public DateTime DueAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public Book Book { get; set; }

        public bool IsOverdue => ReturnedAt == null && DueAt < DateTime.Now;
    }
}
