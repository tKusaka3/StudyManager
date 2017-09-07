using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using StudyManager.Models;
using StudyManager.ViewModels;

namespace StudyManager.DB
{
    public class Database
    {
        private static readonly string ConnectionString;

        static Database() //Constructor. Gets the ConnectionStringsSection from WebConfig
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        public static string ConvertDayToCourseCode(string DayOfWeek) //Convert Day to CourseCode
        {
            string Result = string.Empty;

            //Get connection string from Web Config
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.Open();

            //Provide a SQL command
            SqlCommand command = new SqlCommand("SELECT CourseCode FROM Course WHERE DayOfCourse = @DayOfWeek",
                Connection);
            command.Parameters.AddWithValue("@DayOfWeek", DayOfWeek);

            //Execute SQL command and read data from DB
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Result = (string) reader["CourseCode"];
                }
            }
            Connection.Close();

            return Result;

        }

        public static void InsertAttendance(Attendance attendance, string courseCode) //Insert Record of Attendance
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) //Connect to DB
            {
                String query =
                    "Insert Into dbo.Attendance (AttRecord, CourseCode, AttDate) Values(@AttRecord, @CourseCode, @AttDate)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AttRecord", attendance.AttRecord);
                    command.Parameters.AddWithValue("@AttDate", attendance.AttDate);
                    command.Parameters.AddWithValue("@CourseCode", courseCode);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<AttendanceRecordVM> GetAllAttendances()
        {
            //Create an empty list of AttendanceRecordVms
            List<AttendanceRecordVM> attendanceRecordVms = new List<AttendanceRecordVM>();

            //Get connection string from Web Config
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.Open();

            //Provide a SQL command
            SqlCommand command =
                new SqlCommand(
                    "SELECT AttDate, DayOfCourse, CourseName, AttRecord FROM Course INNER JOIN Attendance ON Attendance.CourseCode = Course.CourseCode; ",
                    Connection);

            //Execute SQL command and read data from DB
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    AttendanceRecordVM record = new AttendanceRecordVM(); //record is one row of following 4 data
                    record.AttDate = (DateTime) reader["AttDate"];
                    record.DayOfWeek = (string) reader["DayOfCourse"];
                    record.CourseName = (string) reader["CourseName"];
                    record.AttRecord = (string) reader["AttRecord"];
                    record.AttDateFormatted = record.AttDate.Date.ToShortDateString();
                    attendanceRecordVms.Add(record); //add new record into Vms(List)
                }
            }

            Connection.Close();

            return attendanceRecordVms;
        }

        public static List<Course> GetAllCourseNames() //Course List for dropdown
        {
            //Create an empty list of GetAllCourseNames
            List<Course> courseNames = new List<Course>();

            //Get connection string from Web Config
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.Open();

            //Provide a SQL command
            SqlCommand command = new SqlCommand("SELECT CourseCode, CourseName FROM Course;", Connection);

            //Execute SQL command and read data from DB
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Course course = new Course(); //course is one row of following 2 data
                    course.CourseCode = (string) reader["CourseCode"];
                    course.CourseName = (string) reader["CourseName"];
                    courseNames.Add(course); //add new course into courseName(List)
                }
            }

            Connection.Close();

            return courseNames;
        }

        public static void InsertTask(Task task) //Insert Task
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) //Connect to DB
            {
                String query =
                    "Insert Into Task (DueDate, TaskDescription, Completion, CourseCode) Values(@DueDate, @TaskDescription, @Completion, @CourseCode);";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    task.Completion = false;

                    command.Parameters.AddWithValue("@DueDate", task.DueDate);
                    command.Parameters.AddWithValue("@TaskDescription", task.TaskDescription);
                    command.Parameters.AddWithValue("@Completion", task.Completion);
                    command.Parameters.AddWithValue("@CourseCode", task.CourseCode);


                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

        }

        public static List<TaskListVM> GetAllTasks()
        {
            //Create an empty list of AttendanceRecordVms
            List<TaskListVM> taskListVms = new List<TaskListVM>();

            //Get connection string from Web Config
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.Open();

            //Provide a SQL command
            SqlCommand command =
                new SqlCommand(
                    "SELECT TaskId, DueDate, CourseName, TaskDescription, Completion FROM Course INNER JOIN Task ON Task.CourseCode = Course.CourseCode; ",
                    Connection);

            //Execute SQL command and read data from DB
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    TaskListVM tasks = new TaskListVM(); //task is one row of following 4 data
                    tasks.TaskId = (Guid) reader["TaskId"];
                    tasks.DueDate = (DateTime) reader["DueDate"];
                    tasks.CourseName = (string) reader["CourseName"];
                    tasks.TaskDescription = (string) reader["TaskDescription"];
                    tasks.Completion = (bool) reader["Completion"];
                    taskListVms.Add(tasks); //add new task into Vms(List)
                }
            }

            Connection.Close();

            return taskListVms;
        }

        public static void UpdateCompletion(Guid taskId)
        {
           
            using (SqlConnection connection = new SqlConnection(ConnectionString)) //Connect to DB
            {
                String query =
                    "UPDATE Task SET Completion = 1 WHERE TaskId = @taskId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@taskId", taskId );

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

        }


}
}






