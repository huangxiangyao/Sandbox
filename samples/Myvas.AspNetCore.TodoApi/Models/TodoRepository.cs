using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myvas.AspNetCore.TodoApi.Models
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _db;

        public TodoRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _db.TodoItems.AsEnumerable();
        }

        public void Add(TodoItem item)
        {
            item.Id = Guid.NewGuid().ToString();
            _db.TodoItems.Add(item);
            _db.SaveChanges();
        }

        public TodoItem Find(string id)
        {
            return _db.TodoItems.Find(id);
        }

        public TodoItem Remove(TodoItem item)
        {
            var r = _db.TodoItems.Remove(item);
            _db.SaveChanges();
            return r.Entity;
        }

        public void Update(TodoItem item, TodoItem oldItem)
        {
            _db.Entry<TodoItem>(oldItem).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            _db.TodoItems.Attach(item);
            _db.SaveChanges();
        }
    }
}
