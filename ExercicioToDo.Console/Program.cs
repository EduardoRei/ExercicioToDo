using ExercicioToDo.Console.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ExercicioToDo.Core.Microsoft.Extensions.DependencyInjection;


IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        string connectionString = configuration.GetConnectionString("DatabaseConnection");
 

        var serviceCollection = new ServiceCollection().AddCoreDbContext(connectionString);
        serviceCollection.AddScoped<ITarefaService, TarefaService>();
        serviceCollection.AddScoped<MensagemService>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var mensagemService = serviceProvider.GetRequiredService<MensagemService>();

        mensagemService.ExibirMensagemBemvindo(mensagemService.DescobrirSistemaOperacional());
        mensagemService.ExibirMenu();