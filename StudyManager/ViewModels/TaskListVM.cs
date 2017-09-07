using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyManager.Models;

namespace StudyManager.ViewModels
{
    public class TaskListVM
    {
        public Guid TaskId { get; set; }
        public DateTime DueDate { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string TaskDescription { get; set; }
        public bool Completion { get; set; }
    }
}