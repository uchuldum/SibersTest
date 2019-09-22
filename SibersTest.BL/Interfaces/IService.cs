using System;
using System.Collections.Generic;
using System.Text;
using SibersTest.BL.ModelsDTO;
namespace SibersTest.BL.Interfaces
{
    public interface IService<T> where T: class
    {
        void Create(T item);
             
        void Edit(T source, T dest);

        void Delete(T item);

        T Get(T item);

        IEnumerable<T> GetAll();

        void Dispose();
    }
}
