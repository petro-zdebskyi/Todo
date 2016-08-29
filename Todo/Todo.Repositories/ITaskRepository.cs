using System.Collections.Generic;
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
