using Microsoft.EntityFrameworkCore;
using SibersTest.DAL.Interfaces;
using SibersTest.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SibersTest.DAL.Repositories
{
    public class ProjectsEmployeesRepository : IRepository<ProjectsEmployees>
    {
        private SibersTestDBContext db;

        public ProjectsEmployeesRepository(SibersTestDBContext sibersTestDB)
        {
            db = sibersTestDB;
        }

        public ProjectsEmployees IsExist(ProjectsEmployees projectsEmployees)
        {
            return null;
          
        }

        public void Create(ProjectsEmployees projectsEmployees)
        {
            db.ProjectsEmployees.Add(projectsEmployees);
        }

        public void Delete(ProjectsEmployees projectsEmployees)
        {
            //ProjectsEmployees projectsEmployees = db.ProjectsEmployees.Find(id);
            if (projectsEmployees != null)
                db.ProjectsEmployees.Remove(projectsEmployees);
        }

        public IEnumerable<ProjectsEmployees> Find(Func<ProjectsEmployees, bool> predicate)
        {
            return db.ProjectsEmployees.Include(e => e.EmployeeId).Include(p => p.ProjectId).Where(predicate).ToList();
        }

        public ProjectsEmployees Get(ProjectsEmployees projectsEmployees)
        {
            return null;
        }

        public IEnumerable<ProjectsEmployees> GetAll()
        {
            return db.ProjectsEmployees;
        }

        public void Update(ProjectsEmployees source, ProjectsEmployees dest)
        {
            throw new NotImplementedException();
        }
    }
}
