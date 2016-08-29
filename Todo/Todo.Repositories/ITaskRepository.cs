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
        void Complete(int taskId);
        List<TaskEntity> GetAll();
        void Update(TaskEntity task);
    }
}
