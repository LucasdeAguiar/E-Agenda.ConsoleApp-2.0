using E_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.ConsoleApp.ModuloTarefa
{
    public class Item : EntidadeBase
    {
        private readonly String descricao;
        private Boolean pendente = true;

        public Item(string descricao)
        {
            this.descricao = descricao;
            this.pendente = true;
        }

        public string Descricao => descricao;

        public bool Pendente { get => pendente; set => pendente = value; }

        public void itemConcluido()
        {
            this.pendente = false;
        }

        public void mostraEntidade()
        {
            Console.WriteLine("Descrição: " + descricao);
            Console.WriteLine("Está pendente? " + pendente);
        }
    }
}
