using Library.Model.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dao
{
    public class ContextManager
    {
        private static LibraryContext context = new LibraryContext();
        private ContextManager()
        {
          
        }
        public static LibraryContext Context
        {
            get { return context; }
        }
    }
    
}
