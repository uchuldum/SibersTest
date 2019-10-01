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
    public class ProjectsEmployeesRepository<T> : IRepository<ProjectsEmployees>
    {
        private SibersTestDBContext db;

        public ProjectsEmployeesRepository(SibersTestDBContext sibersTestDB)
        {
            db = sibersTestDB;
        }

        public async Task<ProjectsEmployees> IsExist(ProjectsEmployees projectsEmployees)
        {
            if (projectsEmployees == null)
                return null;
            ProjectsEmployees pe = await db.ProjectsEmployees
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(p =>
                                            p.EmployeeId == projectsEmployees.EmployeeId &&
                                            p.ProjectId == projectsEmployees.ProjectId);
            if (pe == null) return null;
            return pe;
        }

        public async Task Create(ProjectsEmployees projectsEmployees)
        {
            ProjectsEmployees pe = await IsExist(projectsEmployees);
            if (pe == null)
                await db.ProjectsEmployees.AddAsync(projectsEmployees);
        }

        public async Task Delete(ProjectsEmployees projectsEmployees)
        {
            ProjectsEmployees pe = await IsExist(projectsEmployees);
            if (projectsEmployees != null && projectsEmployees.Id == pe.Id)
                db.ProjectsEmployees.Remove(projectsEmployees);
        }

        public IEnumerable<ProjectsEmployees> Find(Func<ProjectsEmployees, bool> predicate)
        {
            IEnumerable<ProjectsEmployees> pe = db.ProjectsEmployees.Where(predicate).ToList();
            if (pe.Count() < 1) return null;
            return pe;
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
            ProjectsEmployees s = await IsExist(source);
            ProjectsEmployees d = await IsExist(dest);
            if(s != null && d == null)
            {
                dest.Id = s.Id;
                db.Entry(dest).State = EntityState.Modified;
            }
        }
    }
}