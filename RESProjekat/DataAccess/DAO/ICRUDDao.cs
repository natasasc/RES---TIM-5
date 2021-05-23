using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public interface ICRUDDao<T>
    {
        void Save(T entity);

        IEnumerable<T> FindAll();
    }
}
