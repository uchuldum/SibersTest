using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<Employee> IsExist(Employee emp)
        {
            if (emp == null) return null;
            Employee e = await db.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Name == emp.Name && x.SurName == emp.SurName && x.Patronymic == emp.Patronymic || x.Email == emp.Email);
            if (e != null)
                return e;
            return null;
        }

        public async Task Create(Employee emp)
        {
            Employee e = await IsExist(emp);
            if (e == null)
                db.Employees.Add(emp);
        }

        public async Task Delete(Employee emp)
        {
            Employee e = await IsExist(emp);
            if (e != null && e.EmployeeId == emp.EmployeeId)
                db.Employees.Remove(e);
        }

        public IEnumerable<Employee> Find(Func<Employee, bool> predicate)
        {
            return db.Employees.Include(o => o.ProjectsEmployees).Where(predicate).ToList();
        }

        public async Task<Employee> Get(int? id)
        {
            if (id == null) return null;
            return await db.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await db.Employees.ToListAsync();
        }

        public async Task Update(Employee source, Employee dest)
        {
            Employee s = await IsExist(source);
            Employee d = await IsExist(dest);
            if (s != null && d == null)
            {
                dest.EmployeeId = s.EmployeeId;
                db.Entry(dest).State = EntityState.Modified;
            }
        }
    }
}
