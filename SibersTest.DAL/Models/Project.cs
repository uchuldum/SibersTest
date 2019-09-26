using System;
using System.Collections.Generic;

namespace SibersTest.DAL.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjectsEmployees = new HashSet<ProjectsEmployees>();
        }

        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Customer { get; set; }
        public string Performer { get; set; }
        public int? LeadId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int? Priority { get; set; }

        public virtual Employee Lead { get; set; }
        public virtual ICollection<ProjectsEmployees> ProjectsEmployees { get; set; }
        //public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
