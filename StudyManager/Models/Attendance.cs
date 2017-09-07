using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyManager.Models
{
    public class Attendance

    {
        public Guid AttId { get; set; }
        public string AttRecord { get; set; }
        public DateTime AttDate { get; set; }
    }
}