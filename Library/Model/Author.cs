﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
  public  class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public virtual ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new HashSet<Book>();
        }
        public override string ToString()
        {
            return FullName;
        }

    }
}
