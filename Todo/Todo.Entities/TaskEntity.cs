using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Entities
{
    public class TaskEntity
    {
        public int Id;
        public string Name;
        public DateTime DueDate;
        public byte Priority;
        public string Comment;
    }
}
