using E_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.ConsoleApp.ModuloTarefa
{
    public class Tarefa : EntidadeBase
    {


        private string titulo;
        private int prioridade;
        private int percentualConcluido;
        private DateTime dataCriacao;
        private DateTime dataConclusao;
        private List<Item> itens;
        private bool concluido;

        public Tarefa(string titulo, int prioridade, DateTime dataCriacao, List<Item> itens)
        {
            this.titulo = titulo;
            this.prioridade = prioridade;
            this.dataCriacao = dataCriacao;
            this.itens = itens;
            this.concluido = false;
        }

        public Tarefa(string titulo, int prioridade, DateTime dataCriacao, DateTime dataConclusao, List<Item> itens)
        {
            this.titulo = titulo;
            this.prioridade = prioridade; 
            this.dataCriacao = dataCriacao;
            this.dataConclusao = dataConclusao;
            this.itens = itens;
            this.percentualConcluido = 0;
            this.concluido = false;
        }


        public string Titulo { get => titulo; set => titulo = value; }
        public int Prioridade { get => prioridade; set => prioridade = value; }
        public DateTime DataCriacao { get => dataCriacao; set => dataCriacao = value; }
        public DateTime DataConclusao { get => dataConclusao; set => dataConclusao = value; }
        public List<Item> Itens { get => itens; set => itens = value; }
        public int PercentualConcluido { get => percentualConcluido; set => percentualConcluido = value; }
        public bool Concluido { get => concluido; set => concluido = value; }

        public List<Item> SelecionarTodosItens()
        {
            return this.Itens;

        }

      

        public override string ToString()
        {
            return "Id: " + numero + Environment.NewLine +
                "Nome: " + titulo + Environment.NewLine +
                "Prioridade: " + prioridade + Environment.NewLine +
                "Percentual Concluido: " + percentualConcluido + "% " +Environment.NewLine +
                "Data de criação: " + dataCriacao.ToShortDateString() + Environment.NewLine +
                "Itens: \n" + ListarItens() + Environment.NewLine;         
            
        }

        public void FecharItemDaTarefa()
        {
            double porcentagem = 100/itens.Count;
            percentualConcluido += (int)porcentagem;
        }

        private string ListarItens()
        {
            string listaItens = "";

            foreach (Item item in itens)
            {
                listaItens += "--" + item.Descricao + "\n";
            }

            return listaItens;
        }
       
        
        public void mostraTarefaNivel3()
        {
            if (prioridade == 3)
            {
                Console.WriteLine("Id:" + numero);
                Console.WriteLine("Nome: " + titulo);
                Console.WriteLine("Prioridadae: " + prioridade);
            }
        }

        public void mostraTarefaNivel2()
        {
            if (prioridade == 2)
            {
                Console.WriteLine("Id:" + numero);
                Console.WriteLine("Nome: " + titulo);
                Console.WriteLine("Prioridadae: " + prioridade);
            }
        }

        public void mostraTarefaNivel1()
        {
            if (prioridade == 1)
            {
                Console.WriteLine("Id:" + numero);
                Console.WriteLine("Nome: " + titulo);
                Console.WriteLine("Prioridadae: " + prioridade);
            }
        }
    }
}
