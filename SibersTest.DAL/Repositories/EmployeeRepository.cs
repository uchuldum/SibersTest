using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SibersTest.DAL.Interfaces;
using SibersTest.DAL.Models;

namespace SibersTest.DAL.Repositories
{
    public class EmployeeRepository<T> : IRepository<Employee>
    {
        private SibersTestDBContext db;

        public EmployeeRepository(SibersTestDBContext sibersTestDBContext)
        {
            db = sibersTestDBContext;
        }

        public Employee IsExist(Employee emp)
        {
            if (emp == null) return null;
            Employee e = db.Employees.AsNoTracking().FirstOrDefault(x => x.Name == emp.Name && x.SurName == emp.SurName && x.Patronymic == emp.Patronymic && x.Email == emp.Email);
            if (e != null)
                return e;
            return null;
        }

        public void Create(Employee emp)
        {
            Employee e = IsExist(emp);
            if (e == null)
                db.Employees.Add(emp);
        }

        public void Delete(Employee emp)
        {
            Employee e = IsExist(emp); 
            if(e != null)
            {
                db.Employees.Remove(e);
            }
        }

        public IEnumerable<Employee> Find(Func<Employee, bool> predicate)
        {
            return db.Employees.Include(o => o.ProjectsEmployees).Where(predicate).ToList();
        }

        public Employee Get(Employee emp)
        {
            return IsExist(emp);
        }

        public IEnumerable<Employee> GetAll()
        {
            return db.Employees;
        }

        public void Update(Employee source, Employee dest)
        {
            Employee s = IsExist(source);
            Employee d = IsExist(dest);
            if(d == null && s != null)
            {
                dest.EmployeeId = s.EmployeeId;
                db.Entry(dest).State = EntityState.Modified;
            }
        }
    }
}
