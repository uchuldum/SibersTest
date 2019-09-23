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
    public class ProjectsEmployeesRepository : IRepository<ProjectsEmployees>
    {
        private SibersTestDBContext db;

        public ProjectsEmployeesRepository(SibersTestDBContext sibersTestDB)
        {
            db = sibersTestDB;
        }

        public async Task<ProjectsEmployees> IsExist(ProjectsEmployees projectsEmployees)
        {
            return null;
        }

        public async Task Create(ProjectsEmployees projectsEmployees)
        {
            await db.ProjectsEmployees.AddAsync(projectsEmployees);
        }

        public async Task Delete(ProjectsEmployees projectsEmployees)
        {
            //ProjectsEmployees projectsEmployees = db.ProjectsEmployees.Find(id);
            if (projectsEmployees != null)
                db.ProjectsEmployees.Remove(projectsEmployees);
        }

        public IEnumerable<ProjectsEmployees> Find(Func<ProjectsEmployees, bool> predicate)
        {
            return db.ProjectsEmployees.Include(e => e.EmployeeId).Include(p => p.ProjectId).Where(predicate).ToList();
        }

        public async Task<ProjectsEmployees> Get(int? id)
        {
            return null;
        }

        public async Task<IEnumerable<ProjectsEmployees>> GetAll()
        {
            return await db.ProjectsEmployees.ToListAsync();
        }

        public async Task Update(ProjectsEmployees source, ProjectsEmployees dest)
        {
            throw new NotImplementedException();
        }
    }
}
