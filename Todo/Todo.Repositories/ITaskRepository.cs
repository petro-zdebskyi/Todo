using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Entities;

namespace Todo.Repositories
{
    public interface ITaskRepository
    {
        int Add(TaskEntity task);
        int Complete(int taskId);
        List<TaskEntity> GetAll();
        int Update(TaskEntity task);
    }
}
