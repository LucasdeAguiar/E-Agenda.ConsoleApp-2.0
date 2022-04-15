using E_Agenda.ConsoleApp.Compartilhado;
using E_Agenda.ConsoleApp.Interfaces;
using E_Agenda.ConsoleApp.ModuloTarefa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.ConsoleApp.ModuloItem
{
    public class TelaCadastroItem : TelaBase, ICadastroBasico
    {
        private readonly Notificador notificador;
        private readonly RepositorioItem repositorioItem;

        public TelaCadastroItem(RepositorioItem repositorioItem, Notificador notificador) : base("Cadastro de Itens")
        {
            this.repositorioItem = repositorioItem;
            this.notificador = notificador;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);


            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");


            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;

            
        }
        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo novo Item");
            Item item = ObterItem();

            repositorioItem.Inserir(item);
        }
        public void EditarRegistro()
        {
            MostrarTitulo("Editando Item");

            int numeroItem = ObterNumeroItem();

            List<EntidadeBase> itens = repositorioItem.SelecionarTodos();

            foreach (Item item in itens)
            {
                if (item.numero == numeroItem)
                {
                    item.Pendente = false;
                }
            }

            notificador.apresentarMensagem("Item editado com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
           
        }
 
        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Itens");

            List<EntidadeBase> itens = repositorioItem.SelecionarTodos();

            if (itens.Count == 0)
                return false;

            for (int i = 0; i < itens.Count; i++)
            {
                Item item = (Item)itens[i];


                Console.WriteLine(item.ToString());


                Console.WriteLine("\n");

            }
            Console.ReadLine();
            return true;
        }

        //Métodos Privados
        private Item ObterItem()
        {
            Console.Write("Digite a descrição do item: ");
            string descricao = Console.ReadLine();

            Item item = new Item(descricao);


            return item;
        }

        private int ObterNumeroItem()
        {
            int numeroItem;
            bool numeroItemEncontrado;

            do
            {
                List<EntidadeBase> itens = repositorioItem.SelecionarTodos();



                 
                for (int i = 0; i < itens.Count; i++)
                {
                    Item item = (Item)itens[i];

                    Console.WriteLine("Descrição: " + item.Descricao);
                    Console.WriteLine("Está Pendente?: " + item.Pendente);
                    

                    Console.WriteLine("\n");

                }


                Console.Write("Digite o número do item que deseja selecionar: ");
                numeroItem = Convert.ToInt32(Console.ReadLine());

                numeroItemEncontrado = repositorioItem.ExisteRegistro(numeroItem);

                if (numeroItemEncontrado == false)
                    notificador.apresentarMensagem("Número do item não encontrado, tente novamente..", TipoMensagem.Atencao);

            } while (numeroItemEncontrado == false);

            return numeroItem;
        }
    }
}
