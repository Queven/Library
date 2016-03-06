using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
  public class Box
    {
        public int BoxId { get; set; }
        public int Number { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Box()
        {
            Books = new HashSet<Book>();
        }
    }
}
