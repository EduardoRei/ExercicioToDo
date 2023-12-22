using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExercicioToDo.Core.Model;


namespace ExercicioToDo.Console.Services
{
    public interface ITarefaService
    {
        Task<List<ToDoItem>> GetAllAsync();
        Task<ToDoItem> GetByIdAsync(int id);
        Task<ToDoItem> SaveNewAsync(string descricao);
        Task<ToDoItem> UpdateAsync(int id, string descricao, bool isComplete);
        Task<ToDoItem> DeleteAsync(int id);
        
    }
}