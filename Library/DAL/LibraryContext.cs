using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.DAL
{
   public class LibraryContext:DbContext
    {
        public LibraryContext() : base("LibraryContext")
        {
        }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Box> Box { get; set; }
   
    }
}
