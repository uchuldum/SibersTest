using System;
using System.Collections.Generic;
using System.Text;

namespace SibersTest.BL.ModelsDTO
{
    public class ProjectDTO
    {
        //public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Customer { get; set; }
        public string Performer { get; set; }
        public int LeadId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int? Priority { get; set; }
    }
}
