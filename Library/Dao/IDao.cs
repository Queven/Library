using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dao
{
    public interface IDao<TEntity>
    {
        TEntity Save(TEntity entity);
        void Delete(TEntity entity);
        void SaveChanges();
    }
}
