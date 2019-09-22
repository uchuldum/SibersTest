using System;
using System.Collections.Generic;
using System.Text;

namespace SibersTest.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(T item);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T source, T dest);
        void Delete(T item);
        T IsExist(T item);
    }
}
