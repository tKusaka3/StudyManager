using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyManager.ViewModels
{
    public class AttendanceRecordVM
    {
        public DateTime AttDate{ get; set; }
        public string DayOfWeek { get; set; }
        public string CourseName { get; set; }
        public string AttRecord { get; set; }
        public string AttDateFormatted { get; set; }
    }
}