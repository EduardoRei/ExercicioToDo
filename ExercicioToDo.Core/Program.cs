using ExercicioToDo.Core.Database;
using ExercicioToDo.Core.Microsoft.Extensions.DependencyInjection;
using ExercicioToDo.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
                .AddCoreDbContext("Data Source=Todos.db")
                .BuildServiceProvider();

         using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ExercicioToDoDbContext>();

                // Garante que o banco de dados está criado e as migrações foram aplicadas
                dbContext.Database.Migrate();

            }