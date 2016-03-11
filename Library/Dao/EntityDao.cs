using Library.Model.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dao
{
    public abstract class EntityDao
    {

        protected LibraryContext Context
        {
            get
            {
                return ContextManager.Context;
            }
        }
    }

}
