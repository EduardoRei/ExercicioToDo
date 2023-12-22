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
            return services.AddDbContext<ExercicioToDoDbContext>(options => options.UseSqlite(connectionString));
        }
    }
}