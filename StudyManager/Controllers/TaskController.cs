using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using StudyManager.DB;
using StudyManager.Models;
using StudyManager.ViewModels;

namespace StudyManager.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index()
        {
            //Create an list of cours names
            List<Course> courseNames = Database.GetAllCourseNames(); //From class DB
            return View(courseNames);
        }

        // Send Task to DB
        [HttpPost]
        public ActionResult Create(Task task)
        {
            Database.InsertTask(task);

            return RedirectToAction("List");
        }

        // Retrieve list of task from DB
        public ActionResult List()
        {
            List<TaskListVM> taskList = Database.GetAllTasks();
            taskList = taskList.OrderByDescending(q => q.DueDate).ToList();
            return View("List", taskList);
        }

        // Change status of Completion
        public ActionResult Complete(Guid id)
        { 
            Database.UpdateCompletion(id);
            return RedirectToAction("List");
        }


}
}