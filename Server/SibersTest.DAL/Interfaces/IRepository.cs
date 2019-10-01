using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SibersTest.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int? id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        Task Create(T item);
        Task Update(T source, T dest);
        Task Delete(T item);
        Task<T> IsExist(T item);
    }
}
