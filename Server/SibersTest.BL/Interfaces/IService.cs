using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace SibersTest.BL.Interfaces
{
    public interface IService<T> where T: class
    {
        Task<IEnumerable<T>> Create(T item);

        Task Edit(T source, T dest);

        Task Delete(T item);

        Task<T> Get(int? id);

        Task<IEnumerable<T>> Find(int? id);

        Task<IEnumerable<T>> Find(string id);

        Task<IEnumerable<T>> GetAll();

        void Dispose();
    }
}
