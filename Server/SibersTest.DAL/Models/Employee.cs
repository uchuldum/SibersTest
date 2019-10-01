using System;
using System.Collections.Generic;

namespace SibersTest.DAL.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Projects = new HashSet<Project>();
            ProjectsEmployees = new HashSet<ProjectsEmployees>();
        }

        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<ProjectsEmployees> ProjectsEmployees { get; set; }
        //public ICollection<ProjectTask> TaskAuthor { get; set; }
        //public ICollection<ProjectTask> TaskExecutor { get; set; }
    }
}
