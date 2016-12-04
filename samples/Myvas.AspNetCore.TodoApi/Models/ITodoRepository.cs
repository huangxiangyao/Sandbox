using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myvas.AspNetCore.TodoApi.Models
{
    public interface ITodoRepository
    {
        void Add(TodoItem item);
        IEnumerable<TodoItem> GetAll();
        TodoItem Find(string id);
        TodoItem Remove(TodoItem item);
        void Update(TodoItem item, TodoItem oldItem);
    }
}
