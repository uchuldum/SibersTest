using Microsoft.EntityFrameworkCore;
using SibersTest.DAL.Interfaces;
using SibersTest.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SibersTest.DAL.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private SibersTestDBContext db;

        public ProjectRepository(SibersTestDBContext sibersTestDB)
        {
            db = sibersTestDB;
        }

        public Project IsExist(Project project)
        {
            if (project == null) return null;
            Project e = db.Projects.AsNoTracking().Include(p => p.Lead).AsNoTracking().FirstOrDefault(x => x.Name == project.Name && x.Customer == project.Customer && x.StartDate == project.StartDate && x.FinishDate == project.FinishDate);
            if (e != null)
                return e;
            return null;
        }

        public void Create(Project project)
        {
            Project p = IsExist(project);
            if(p == null)
                db.Projects.Add(project);
        }

        public void Delete(Project project)
        {
            Project p = IsExist(project);
            if (p != null)
                db.Projects.Remove(project);
        }

        public IEnumerable<Project> Find(Func<Project, bool> predicate)
        {
            return db.Projects.Include(e => e.ProjectsEmployees).Where(predicate).ToList();
        }

        public Project Get(Project project)
        {
            return IsExist(project);
        }

        public IEnumerable<Project> GetAll()
        {
            return db.Projects;
        }

        public void Update(Project source, Project dest)
        {
            if(IsExist(source) != null)
            db.Entry(dest).State = EntityState.Modified;
        }
    }
}
