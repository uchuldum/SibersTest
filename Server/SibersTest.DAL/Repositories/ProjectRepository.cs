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
    public class ProjectRepository<T> : IRepository<Project>
    {
        private SibersTestDBContext db;

        public ProjectRepository(SibersTestDBContext sibersTestDB)
        {
            db = sibersTestDB;
        }
        public async Task<Project> IsExist(Project project)
        {
            if (project == null) return null;
            Project e = await db.Projects.AsNoTracking().Include(p => p.Lead)
                .FirstOrDefaultAsync
                (x =>
                    x.Name == project.Name &&
                    x.Customer == project.Customer &&
                    x.StartDate == project.StartDate &&
                    x.FinishDate == project.FinishDate &&
                    x.LeadId == project.LeadId &&
                    x.Performer == project.Performer &&
                    x.Priority == project.Priority
                );
            if (e != null)
                return e;
            return null;
        }

        public async Task Create(Project project)
        {
            Project p = await IsExist(project);
            if (p == null)
                db.Projects.Add(project);
             
            
        }

        public async Task Delete(Project project)
        {
            Project p = await IsExist(project);
            if (p != null && project.ProjectId == p.ProjectId)
                db.Projects.Remove(project);
        }

        public IEnumerable<Project> Find(Func<Project, bool> predicate)
        {
            return db.Projects.Include(e => e.ProjectsEmployees).Where(predicate).ToList();
        }

        public async Task<Project> Get(int? id)
        {
            if (id == null) return null;
            return await db.Projects.FindAsync(id);
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await db.Projects.ToListAsync();
        }

        public async Task Update(Project source, Project dest)
        {
            Project s = await IsExist(source);
            Project d = await IsExist(dest);
            if (s != null && d == null)
            {
                dest.ProjectId = s.ProjectId;
                db.Entry(dest).State = EntityState.Modified;
            }
        }
    }
}
