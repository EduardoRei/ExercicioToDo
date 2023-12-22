using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExercicioToDo.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace ExercicioToDo.Core.Microsoft.Extensions.DependencyInjection
{
    public static class ExercicioToDoDbContextExtensions
    {
        public static IServiceCollection AddCoreDbContext(this IServiceCollection services, string connectionString) 
        {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString), "A string de conexão não pode ser nula ou vazia.");
        }

        var absolutePath = AppDomain.CurrentDomain.BaseDirectory;

        services.AddDbContext<ExercicioToDoDbContext>(options =>
            options.UseSqlite($"Data Source={absolutePath}ExercicioToDo.Core/Todos.db"));

        return services;
        }
    }
}