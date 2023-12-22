using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExercicioToDo.Core.Model
{
    public class ToDoItem
    {
        public int Id {get; set;}
        public string Descricao {get; set;}
        public bool IsComplete {get; set;}

        public ToDoItem(string descricao, bool isComplete = false)
        {
            Descricao = descricao;
            IsComplete = isComplete;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Descricao = {Descricao}, Completa = {IsComplete}";
        }
    }
}