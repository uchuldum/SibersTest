using Microsoft.EntityFrameworkCore;
using SibersTest.DAL.Interfaces;
using SibersTest.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibersTest.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private SibersTestDBContext db;
        private EmployeeRepository<Employee> employeeRepository;
        private ProjectRepository projectRepository;
        private ProjectsEmployeesRepository projectsEmployeesRepository;

        public EFUnitOfWork(SibersTestDBContext sibersTestDBContext)
        {
            db = sibersTestDBContext;
        }

        public IRepository<Employee> Employees
        {
            get
            {
                if (employeeRepository == null)
                    employeeRepository = new EmployeeRepository<Employee>(db);
                return employeeRepository;
            }
        }

        public IRepository<Project> Projects
        {
            get
            {
                if (projectRepository == null)
                    projectRepository = new ProjectRepository(db);
                return projectRepository;
            }
        }

        public IRepository<ProjectsEmployees> ProjectsEmployees
        {
            get
            {
                if (projectsEmployeesRepository == null)
                    projectsEmployeesRepository = new ProjectsEmployeesRepository(db);
                return projectsEmployeesRepository;
            }
        }

        public async Task Save()
        {
           await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
