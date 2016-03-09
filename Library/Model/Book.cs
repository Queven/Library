using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
 public class Book
    {
        public Book( string title, string publisher, int? year, int? authorId, int boxId)
        {
            Title = title;
            Publisher = publisher;
            Year = year;
            AuthorId = authorId;
            BoxId = boxId;
        }
        public Book()
        {
        }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }

        public int? Year { get; set; }
        public int? AuthorId { get; set; }
        public int BoxId { get; set; }
        public virtual Author Author { get; set; }
        public virtual Box Box { get; set; }
    }
}
