using System;
using System.Collections.Generic;
using System.Text;

namespace SibersTest.DAL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Models.Employee> Employees { get;}
        IRepository<Models.Project> Projects { get; }
        IRepository<Models.ProjectsEmployees> ProjectsEmployees { get; }
        void Save();
    }
}
