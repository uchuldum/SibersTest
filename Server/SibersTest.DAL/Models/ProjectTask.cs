using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SibersTest.DAL.Models
{
    public class ProjectTask
    {
        public int ProjectTaskId { get; set; }
        public string Name { get; set; }

        public int ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }

        public int AuthorID { get; set; }
        [ForeignKey("AuthorID")]
        public virtual Employee EmployeeAuthor { get; set; }

        public int ExecutorID { get; set; }
        [ForeignKey("ExecutorID")]
        public virtual Employee EmployeeExecutor { get; set; }

        public Status? Status { get; set; }

        public string Comment { get; set; }

        public int Priority { get; set; }
    }
    public enum Status
    {
        ToDo, InProgress, Done
    }
}
