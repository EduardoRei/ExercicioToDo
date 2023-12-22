using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ExercicioToDo.Core.Database;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace ExercicioToDo.Console.Services
{
    public class MensagemService
    {
        private ITarefaService _service;
        private ExercicioToDoDbContext _dbContext;
        public MensagemService(IServiceProvider serviceProvider){
            this._service = serviceProvider.GetRequiredService<ITarefaService>();
            this._dbContext = serviceProvider.GetRequiredService<ExercicioToDoDbContext>();
            
            ExibirMensagemBemvindo(DescobrirSistemaOperacional());
            ExibirMenu();
        }

        public void ExibirMensagemBemvindo(string sistemaOperacional){
            System.Console.WriteLine("-----Olá Seja Bem-Vindo-----");
            System.Console.WriteLine(sistemaOperacional);
        }

        public string DescobrirSistemaOperacional()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "Opa, você esta usando Windows, cuidado que o Bill gates esta de olho em você!";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "Opa, você esta usando macOS, alguem gosta de praticidade";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "Opa, você esta usando Linux, parece que você gosta de configurar seu propio sistema operacional";
            }
            else
            {
                return "Não consegui identificar o seu sistema operacional ;-; ";
            }
        }

        public void ExibirMenu(){
            bool loop = true;
            while(loop){
                Opcoes();
                int resposta = Convert.ToInt32(System.Console.ReadLine());
                if(resposta == 1){
                    AdicionarTarefa();
                }
                if(resposta == 2){
                    BuscarPorId();
                }
                if(resposta == 3){
                    VerTodasTarefas();
                }
                if(resposta == 4){
                    MarcarTarefaCompleta();
                }
                if(resposta == 5){
                    AtualizarDescricaoTarefa();
                }
                if(resposta == 6){
                    RemoverTarefa();
                } 
                if(resposta == 7){
                    loop = false;
                }
            }
        }

        public void Opcoes(){
            System.Console.WriteLine("1 - Adicionar tarefa");
            System.Console.WriteLine("2 - Buscar tarefa por id");
            System.Console.WriteLine("3 - Ver todas as tarefas");
            System.Console.WriteLine("4 - Marcar tarefa como completa");
            System.Console.WriteLine("5 - Atualizar descricao da tarefa");
            System.Console.WriteLine("6 - Remover Tarefa");
            System.Console.WriteLine("7 - Sair do programa");
        }

        public async void AdicionarTarefa(){
            System.Console.WriteLine("---Adicionar Tarefa---");
            System.Console.WriteLine("Escreva a descricao da tarefa");
            string descricao = System.Console.ReadLine();
            while(true)
            {
                    if(string.IsNullOrWhiteSpace(descricao))
                        {
                            System.Console.WriteLine("A descrição não pode estar vazia, insira um valor");
                            descricao = System.Console.ReadLine();
                        }
                    else{
                        break;
                    }
            }
            await _service.SaveNewAsync(descricao);
        }

        public async void BuscarPorId()
        {
            System.Console.WriteLine("---Buscar por Id---");
            System.Console.WriteLine("Digite um id valido:");
            int id = Convert.ToInt32(System.Console.ReadLine());
            while(true)
            {
                if(id < -1){
                    System.Console.WriteLine("O id deve ser positivo.");
                }
                var tarefaPesquisada = await _service.GetByIdAsync(id);
                if(string.IsNullOrEmpty(tarefaPesquisada.Descricao))
                {
                    System.Console.WriteLine("Não encontramos uma tarefa referente ao id digitado, insira outro id, ou digite -1 para voltar");
                } else{
                    System.Console.WriteLine(tarefaPesquisada.ToString());
                    System.Console.WriteLine();
                    break;
                }
                id = Convert.ToInt32(System.Console.ReadLine());
                if(id == -1){
                    break;
                }
            }

        }

        public async void VerTodasTarefas()
        {
            System.Console.WriteLine("---Ver todas as tarefas---");
            await _service.GetAllAsync();
            System.Console.WriteLine();
        }

        public async void MarcarTarefaCompleta()
        {
            System.Console.WriteLine("---Marcar tarefa como completa---");
            System.Console.WriteLine("Informe o id da tarefa que deseja completar: ");
            int id = Convert.ToInt32(System.Console.ReadLine());
            while(true)
            {
                if(id < -1){
                    System.Console.WriteLine("O id deve ser positivo. Caso deseje cancelar a operação digite -1");
                } else if(id == -1){
                    break;
                }
                var tarefaPesquisada = await _service.GetByIdAsync(id);
                if(string.IsNullOrEmpty(tarefaPesquisada.Descricao))
                {
                    System.Console.WriteLine("Não encontramos uma tarefa referente ao id digitado, insira outro id, ou digite -1 para voltar");
                } else{
                    await _service.UpdateAsync(id, "", true);
                    System.Console.WriteLine("Tarefa marcada como completa");
                    System.Console.WriteLine();
                    break;
                }
                id = Convert.ToInt32(System.Console.ReadLine());

            }
        }

        public async void AtualizarDescricaoTarefa()
        {
            System.Console.WriteLine("---Atualizar descricao da tarefa---");
            System.Console.WriteLine("Informe o id da tarefa que deseja alterar a descricao: ");
            int id = Convert.ToInt32(System.Console.ReadLine());
            while(true)
            {
                if(id < -1){
                    System.Console.WriteLine("O id deve ser positivo. Caso deseje cancelar a operação digite -1");
                } else if(id == -1){
                    break;
                }
                var tarefaPesquisada = await _service.GetByIdAsync(id);
                if(string.IsNullOrEmpty(tarefaPesquisada.Descricao))
                {
                    System.Console.WriteLine("Não encontramos uma tarefa referente ao id digitado, insira outro id, ou digite -1 para voltar");
                } else{
                    System.Console.WriteLine("Digite a nova descricao: ");
                    string descricao = System.Console.ReadLine();
                    await _service.UpdateAsync(id,descricao, false);
                    System.Console.WriteLine("Descricao alterada");
                    System.Console.WriteLine();
                    break;
                }
                id = Convert.ToInt32(System.Console.ReadLine());

            }
        }

        public async void RemoverTarefa()
        {
            System.Console.WriteLine("---Remover tarefa---");
            System.Console.WriteLine("Informe o id da tarefa que deseja remover: ");
            int id = Convert.ToInt32(System.Console.ReadLine());
            while(true)
            {
                if(id < -1){
                    System.Console.WriteLine("O id deve ser positivo. Caso deseje cancelar a operação digite -1");
                } else if(id == -1){
                    break;
                }
                var tarefaPesquisada = await _service.GetByIdAsync(id);
                if(string.IsNullOrEmpty(tarefaPesquisada.Descricao))
                {
                    System.Console.WriteLine("Não encontramos uma tarefa referente ao id digitado, insira outro id, ou digite -1 para voltar");
                } else{
                    await _service.DeleteAsync(id);
                    System.Console.WriteLine("Tarefa removida com sucesso");
                    System.Console.WriteLine();
                    break;
                }
                id = Convert.ToInt32(System.Console.ReadLine());

            }
        }

    }
}