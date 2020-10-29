using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoTutorial.Models;

namespace ToDoTutorial.Data
{
    public class CloudTodoService : ITodoService
    {
        private string uri = "https://localhost:44334";
        private string uri1 = "http://jsonplaceholder.typicode.com";
        private readonly HttpClient client;

        public CloudTodoService()
        {
            client = new HttpClient();
        }
        public async Task<IList<Todo>> GetTodosAsync(int id, string completed)
        {
            string message = "";
            if (completed == null || completed.Equals("Both"))
            {
                message = await client.GetStringAsync(uri + "/todos?userId=" + id);
            }
            if (id < 1)
            {
                message = await client.GetStringAsync(uri + "/todos");
            }
            if (id > 1 && completed != null && !completed.Equals("Both"))
            {
                message = await client.GetStringAsync(uri + "/todos?userId=" + id + "&completed=" + completed);
            }
            if (id > 1 && completed != null && completed.Equals("Both"))
            {
                message = await client.GetStringAsync(uri + "/todos?userId=" + id);
            }
            else 
            {
                message = await client.GetStringAsync(uri + "/todos?userId=" + id + "&completed=" + completed);
            }
            List<Todo> result = JsonSerializer.Deserialize<List<Todo>>(message);
            return result;
        }
        public async Task AddTodoAsync(Todo todo)
        {
            string todoAsJson = JsonSerializer.Serialize(todo);
            HttpContent content = new StringContent(todoAsJson,
                Encoding.UTF8,
                "application/json");
            await client.PostAsync(uri1 + "/todos", content);
        }
        public async Task RemoveTodoAsync(int todoId)
        {
            await client.DeleteAsync($"{uri1}/todos/{todoId}");
        }

        public async Task UpdateTodoAsync(Todo todo)
        {
            string todoAsJson = JsonSerializer.Serialize(todo);
            HttpContent content = new StringContent(todoAsJson,
                Encoding.UTF8,
                "application/json");
            await client.PatchAsync($"{uri1}/todos/{todo.TodoId}", content);
        }
    }
}
