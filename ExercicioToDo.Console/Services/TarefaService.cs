using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExercicioToDo.Core.Database;
using ExercicioToDo.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace ExercicioToDo.Console.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ExercicioToDoDbContext _dbContext;

        public TarefaService(ExercicioToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ToDoItem> DeleteAsync(int id)
        {
            var tarefa = await _dbContext.Todos.FindAsync(id);
            _dbContext.Todos.Remove(tarefa);
            await _dbContext.SaveChangesAsync();
            return tarefa;        }

        public async Task<List<ToDoItem>> GetAllAsync()
        {
             return await _dbContext.Todos.ToListAsync();
        }

        public async Task<ToDoItem> GetByIdAsync(int id)
        {
             return await _dbContext.Todos.FindAsync(id);
        }

        public async Task<ToDoItem> SaveNewAsync(string descricao)
        {
                var novaTarefa = new ToDoItem(descricao);
                _dbContext.Todos.Add(novaTarefa);
                await _dbContext.SaveChangesAsync(); // Isso vai gerar automaticamente o Id
                return novaTarefa;
        }

        public async Task<ToDoItem> UpdateAsync(int id, string descricao, bool isComplete)
        {
            var tarefa = await _dbContext.Todos.FindAsync(id);
            if (tarefa == null)
            {
                return null; // Ou lançar uma exceção informando que a tarefa não foi encontrada
            }
            if(!string.IsNullOrWhiteSpace(descricao)){
                tarefa.Descricao = descricao;
            }
            if(isComplete){
                tarefa.IsComplete = isComplete;
            }

            await _dbContext.SaveChangesAsync();
            return tarefa;
        }
    }
}