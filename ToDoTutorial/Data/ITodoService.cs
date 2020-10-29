using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoTutorial.Models;

namespace ToDoTutorial.Data
{
    public interface ITodoService
    {
        Task<IList<Todo>> GetTodosAsync(int id, string completed);
        Task AddTodoAsync(Todo todo);
        Task RemoveTodoAsync(int todoId);
        Task UpdateTodoAsync(Todo todo);
    }
    

}
