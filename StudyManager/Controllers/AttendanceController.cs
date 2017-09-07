using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using StudyManager.DB;
using StudyManager.Models;
using StudyManager.ViewModels;

namespace StudyManager.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        public ActionResult Index()
        {
            return View();
        }

        //Executed when Attendance Form is submitted
        [HttpPost]
        public ActionResult Create(Attendance Att)
        {
            //Build-in method. Convert Date to Day
            string DayOfWeek = Att.AttDate.DayOfWeek.ToString();

            if (DayOfWeek == "Sunday"|| DayOfWeek == "Saturday") //Input Validation
            {
                return View("Error");
            }
            else
            {
                string CourseCode = Database.ConvertDayToCourseCode(DayOfWeek);
                Database.InsertAttendance(Att, CourseCode);

                return RedirectToAction("List");
            }

        }

        //Display Attendance Record retrived from DB
        public ActionResult List()
        {
            List<AttendanceRecordVM> attendanceRecordVm = Database.GetAllAttendances();
            attendanceRecordVm = convertAttRecordToMark(attendanceRecordVm);
            attendanceRecordVm = attendanceRecordVm.OrderByDescending(q => q.AttDate).ToList();
            return View("List", attendanceRecordVm);
        }

        private List<AttendanceRecordVM> convertAttRecordToMark(List<AttendanceRecordVM> attendanceRecordVm)
        {
            foreach (var attendance in attendanceRecordVm)
            {
                if (attendance.AttRecord == "Attend")
                {
                    attendance.AttRecord = "o";
                }
                else if (attendance.AttRecord == "Absence")
                {
                    attendance.AttRecord = "x";
                }
                else
                {
                    attendance.AttRecord = "Holiday";
                }
            }
            return attendanceRecordVm;
        }

    }
}