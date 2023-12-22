using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExercicioToDo.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace ExercicioToDo.Core.Database
{
    public class ExercicioToDoDbContext : DbContext
    {
        public ExercicioToDoDbContext() {}
        public ExercicioToDoDbContext(DbContextOptions<ExercicioToDoDbContext> options) : base(options) {}
        public DbSet<ToDoItem>? Todos {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            // Construir o caminho completo para o arquivo do banco de dados
            var dbPath = Path.Combine(basePath, "ExercicioToDo.Core", "Todos.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}