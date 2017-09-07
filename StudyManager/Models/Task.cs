using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyManager.Models
{
    public class Task
    {
        public Guid TaskId { get; set; }
        public string CourseCode { get; set; }
        public DateTime DueDate { get; set; }
        public string TaskDescription { get; set; }
        public bool Completion { get; set; }
    }
}