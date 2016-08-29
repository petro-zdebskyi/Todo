using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Todo.Entities;

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
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = _connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spAddTask";
                command.Parameters.AddWithValue("@name", task.Name);
                command.Parameters.AddWithValue("@dueDate", dateTime);
                command.Parameters.AddWithValue("@priority", task.Priority);
                command.Parameters.AddWithValue("@comment", task.Comment);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = (int)(decimal)reader[0];
                    }
                    _connection.Close();
                    return result;
                }
            }
        }

        public void Complete(int taskId)
        {
            _connection.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = _connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spCompleteTask";
                command.Parameters.AddWithValue("@taskId", taskId);
                command.ExecuteNonQuery();

                _connection.Close();
            }
        }

        public List<TaskEntity> GetAll()
        {
            List<TaskEntity> tasksList = new List<TaskEntity>();

            _connection.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = _connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spGetTasks";
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

                    _connection.Close();
                    return tasksList;
                }
            }
        }

        public void Update(TaskEntity task)
        {
            var dateTime = task.DueDate.ToString("yyyy-MM-dd HH:mm:ss");

            _connection.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = _connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spUpdateTask";
                command.Parameters.AddWithValue("@id", task.Id);
                command.Parameters.AddWithValue("@name", task.Name);
                command.Parameters.AddWithValue("@dueDate", dateTime);
                command.Parameters.AddWithValue("@priority", task.Priority);
                command.Parameters.AddWithValue("@comment", task.Comment);
                command.ExecuteNonQuery();

                _connection.Close();
            }
        }

        #endregion ITaskRepository
    }
}
