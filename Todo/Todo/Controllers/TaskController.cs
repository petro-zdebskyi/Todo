using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using Todo.Repositories;
using Todo.Entities;

namespace Todo.Controllers
{
    public class TaskController : Controller
    {
        #region Fields

        private readonly ITaskRepository _taskRepository;

        #endregion

        public TaskController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Todo"].ConnectionString;
            _taskRepository = new SqlTaskRepository(connectionString);
        }

        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetTasks()
        {
            List<TaskEntity> tasksList = _taskRepository.GetAll();
            return Json(tasksList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int AddTask(string name, DateTime dueDate, byte priority, string comment)
        {
            int result = _taskRepository.Add(new TaskEntity()
            {
                Name = name,
                DueDate = dueDate,
                Priority = priority,
                Comment = comment
            });

            return result;
        }

        [HttpPost]
        public int UpdateTask(int id, string name, DateTime dueDate, byte priority, string comment)
        {
            int result = _taskRepository.Update(new TaskEntity()
            {
                Id = id,
                Name = name,
                DueDate = dueDate,
                Priority = priority,
                Comment = comment
            });

            return result;
        }

        [HttpPost]
        public int CompleteTask(int taskId)
        {
            int result = _taskRepository.Complete(taskId);
            return result;
        }
    }
}