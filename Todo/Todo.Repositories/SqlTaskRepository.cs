using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Entities;
using Todo.Repositories;

namespace Todo.Repositories
{
    public class SqlTaskRepository : ITaskRepository
    {
        #region Private Fields

        private readonly SqlConnection _connection;

        #endregion

        #region Constructors

        public SqlTaskRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        #endregion

        #region ITaskRepository

        public int Add(TaskEntity task)
        {
            var dateTime = task.DueDate.ToString("yyyy-MM-dd HH:mm:ss");
            int result = 0;
            _connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO tblTasks (Name, DueDate, Priority, IsCompleted, Comment) VALUES ('" + task.Name + "',convert(datetime,'" + dateTime + "',120),'" + task.Priority.ToString() + "',0,'" + task.Comment + "'); SELECT SCOPE_IDENTITY();", _connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    result = (int)(decimal)reader[0];
                }
            }

            _connection.Close();

            return result;
        }

        public int Complete(int taskId)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand("UPDATE tblTasks SET IsCompleted = 1 WHERE Id = " + taskId.ToString(), _connection);
            int executeStatus = command.ExecuteNonQuery();
            _connection.Close();

            return executeStatus;
        }

        public List<TaskEntity> GetAll()
        {
            List<TaskEntity> tasksList = new List<TaskEntity>();
            _connection.Open();
            using (SqlCommand command = new SqlCommand("SELECT * FROM tblTasks WHERE IsCompleted = 0", _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasksList.Add(new TaskEntity()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            DueDate = Convert.ToDateTime(reader["DueDate"].ToString()),
                            Priority = (byte)reader["Priority"],
                            Comment = (reader["Comment"] == null) ? string.Empty : reader["Comment"].ToString()
                        });
                    }
                }
            }
            _connection.Close();

            return tasksList;
        }

        public int Update(TaskEntity task)
        {
            var dateTime = task.DueDate.ToString("yyyy-MM-dd HH:mm:ss");

            _connection.Open();
            SqlCommand command = new SqlCommand("UPDATE tblTasks SET IsCompleted=0, Name='" + task.Name + "',DueDate=convert(datetime,'" + dateTime + "',120), Priority='" + task.Priority.ToString() + "', Comment='" + task.Comment + "' WHERE Id = " + task.Id, _connection);
            int executeStatus = command.ExecuteNonQuery();
            _connection.Close();

            return executeStatus;
        }

        #endregion ITaskRepository
    }
}
