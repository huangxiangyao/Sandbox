using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myvas.AspNetCore.TodoApi.Models
{
    public class TodoInMemoryRepository : ITodoRepository
    {
        private static ConcurrentDictionary<string, TodoItem> _todos =
              new ConcurrentDictionary<string, TodoItem>();

        public TodoInMemoryRepository()
        {
            Add(new TodoItem { Name = "Item1" });
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _todos.Values;
        }

        public void Add(TodoItem item)
        {
            item.Id = Guid.NewGuid().ToString();
            _todos[item.Id] = item;
        }

        public TodoItem Find(string id)
        {
            TodoItem item;
            _todos.TryGetValue(id, out item);
            return item;
        }

        public TodoItem Remove(TodoItem item)
        {
            TodoItem removedItem;
            _todos.TryRemove(item.Id, out removedItem);
            return removedItem;
        }

        public void Update(TodoItem item, TodoItem oldItem)
        {
            _todos[item.Id] = item;
        }
    }
}
