using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoTutorial.Models;

namespace ToDoTutorial.Data
{
    public class TodoService : ITodoService
    {
        private string todoFile = "todos.json";
        private IList<Todo> todos;

        public TodoService()
        {
            if (!File.Exists(todoFile))
            {
                Seed();
                WriteTodosToFile();
            }
            else
            {
                string content = File.ReadAllText(todoFile);
                todos = JsonSerializer.Deserialize<List<Todo>>(content);
            }
        }

        private void Seed()
        {
            Todo[] ts = {
                new Todo {
                    UserId = 1,
                    TodoId = 1,
                    Title = "Do dishes",
                    IsCompleted = false
                },
                new Todo {
                    UserId = 1,
                    TodoId = 2,
                    Title = "Walk the dog",
                    IsCompleted = false
                },
                new Todo {
                    UserId = 2,
                    TodoId = 3,
                    Title = "Do DNP homework",
                    IsCompleted = true
                },
                new Todo {
                    UserId = 3,
                    TodoId = 4,
                    Title = "Eat breakfast",
                    IsCompleted = false
                },
                new Todo {
                    UserId = 4,
                    TodoId = 5,
                    Title = "Mow lawn",
                    IsCompleted = true
                },
            };
            todos = ts.ToList();
        }

        public async Task<IList<Todo>> GetTodosAsync(int id, string completed)
        {
            List<Todo> tmp = new List<Todo>(todos);
            List<Todo> result = new List<Todo>();
            foreach (var todo in tmp)
            {
                if (todo.UserId == id && todo.IsCompleted == Convert.ToBoolean(completed))
                {
                    result.Add(todo);
                }
            }
            return result;
        }

        public async Task AddTodoAsync(Todo todo)
        {
            int max = todos.Max(todo => todo.TodoId);
            todo.TodoId = (++max);
            todos.Add(todo);
            WriteTodosToFile();
        }

        public async Task RemoveTodoAsync(int todoId)
        {
            Todo toRemove = todos.First(t => t.TodoId == todoId);
            todos.Remove(toRemove);
            WriteTodosToFile();
        }

        public async Task UpdateTodoAsync(Todo todo)
        {
            Todo toUpdate = todos.First(t => t.TodoId == todo.TodoId);
            toUpdate.IsCompleted = todo.IsCompleted;
            WriteTodosToFile();
        }

        private void WriteTodosToFile()
        {
            string productsAsJson = JsonSerializer.Serialize(todos);
            File.WriteAllText(todoFile, productsAsJson);
        }
    }
}
