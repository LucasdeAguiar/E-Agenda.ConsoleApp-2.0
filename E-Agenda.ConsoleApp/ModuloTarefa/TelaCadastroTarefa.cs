using E_Agenda.ConsoleApp.Compartilhado;
using E_Agenda.ConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Agenda.ConsoleApp.ModuloTarefa
{
    public class TelaCadastroTarefa : TelaBase , ICadastroBasico
    {
        private readonly Notificador notificador;
        private readonly RepositorioTarefa repositorioTarefa;


        public TelaCadastroTarefa(RepositorioTarefa repositorioTarefa, Notificador notificador) : base("Cadastro de Tarefas")
        {
            this.repositorioTarefa = repositorioTarefa;
            this.notificador = notificador;
        }



        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);


            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Fechar itens:");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;


        }

        
        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo nova Tarefa");
            Tarefa tarefa = obterTarefa();

            repositorioTarefa.Inserir(tarefa);


        }
        
        public void EditarRegistro()
        {
            MostrarTitulo("Editando Tarefa");

            int numeroTarefa = ObterNumeroTarefa();

            Tarefa tarefaAtualizada = editarTarefa();

            repositorioTarefa.Editar(numeroTarefa, tarefaAtualizada);

            notificador.apresentarMensagem("Tarefa editada com sucesso", TipoMensagem.Sucesso);

        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Tarefa");

            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando:");

            if (temTarefasCadastradas == false)
            {
                notificador.apresentarMensagem("Nenhuma tarefa cadastrada..", TipoMensagem.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroTarefa();

            repositorioTarefa.Excluir(numeroTarefa);

            notificador.apresentarMensagem("Tarefa excluída com sucesso", TipoMensagem.Sucesso);

        }

         public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Tarefas");

            List<EntidadeBase> tarefas = repositorioTarefa.SelecionarTodos();

            List<Tarefa> tarefasNivel1 = new List<Tarefa>();
            List<Tarefa> tarefasNivel2 = new List<Tarefa>();
            List<Tarefa> tarefasNivel3 = new List<Tarefa>();

            if (tarefas.Count == 0)
                return false;

            for (int i = 0; i < tarefas.Count; i++)
            {
                Tarefa tarefa = (Tarefa)tarefas[i];
                if (tarefa.Prioridade == 3)
                {
                    tarefasNivel3.Add(tarefa);

                }else if (tarefa.Prioridade == 2)
                {
                    tarefasNivel2.Add(tarefa);

                }else if (tarefa.Prioridade == 1)
                {
                    tarefasNivel1.Add(tarefa);
                }

            }

            for (int i = 0;i < tarefasNivel3.Count; i++)
            {
                Tarefa tarefa = (Tarefa)tarefasNivel3[i];

                Console.WriteLine(tarefa.ToString());
                Console.WriteLine("\n");
            }

            for (int i = 0; i < tarefasNivel2.Count; i++)
            {
                Tarefa tarefa = (Tarefa)tarefasNivel2[i];

                Console.WriteLine(tarefa.ToString());
                Console.WriteLine("\n");
            }

            for (int i = 0; i < tarefasNivel1.Count; i++)
            {
                Tarefa tarefa = (Tarefa)tarefasNivel1[i];

                Console.WriteLine(tarefa.ToString());
                Console.WriteLine("\n");
            }
            
            Console.ReadLine();
            return true;
        }


        private Tarefa obterTarefa()
        {
            Console.WriteLine("Digite o título da tarefa:");
            string titulo = Console.ReadLine();

            Console.WriteLine("Digite o grau de prioridade da tarefa: (1 - Baixa),(2 - Normal),(3 - Alta)");
            int prioridade = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite a data de criação da tarefa: ");
            DateTime dataCriacao = Convert.ToDateTime(Console.ReadLine());

        
            DateTime dataConclusao;

            do {

                Console.WriteLine("Digite a data de conclusão da tarefa: ");
                  dataConclusao = Convert.ToDateTime(Console.ReadLine());

                if (dataConclusao < dataCriacao)
                {
                    notificador.apresentarMensagem("Data inválida..", TipoMensagem.Atencao);
                }
            }while(dataConclusao < dataCriacao);

            Console.WriteLine("Quantos itens será adicionado na tarefa?");
             int qtItem = Convert.ToInt32(Console.ReadLine());

            

            List<Item> itens = new List<Item>();

            for (int i = 0;i < qtItem; i++)
            {
                Console.WriteLine("Digite a descrição do item:");
                 string descricao = Console.ReadLine();

                Item item = new Item(descricao);

                


                itens.Add(item);
            }


            Tarefa t = new Tarefa(titulo, prioridade , dataCriacao, dataConclusao, itens);

            

             

            return t;
        }

        public void FecharItem(List<Item> itens , Tarefa tarefa)
        {
            foreach (Item item in itens)
            {
                Console.WriteLine(item.ToString());
            }

            int numeroItem = ObterNumeroRegistro();

            foreach (Item item in itens)
            {
                if (item.numero == numeroItem)
                {
                    item.itemConcluido();
                    tarefa.FecharItemDaTarefa();
                }
            }

        }

        private Tarefa editarTarefa()
        {
            Console.WriteLine("Digite o título da tarefa:");
            string titulo = Console.ReadLine();

            Console.WriteLine("Digite o grau de prioridade da tarefa: (1 - Baixa),(2 - Normal),(3 - Alta)");
            int prioridade = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite a data de criação da tarefa: ");
            DateTime dataCriacao = Convert.ToDateTime(Console.ReadLine());
     

            Console.WriteLine("Quantos itens será adicionado na tarefa?");
            int qtItem = Convert.ToInt32(Console.ReadLine());



            List<Item> itens = new List<Item>();

            for (int i = 0; i < qtItem; i++)
            {
                Console.WriteLine("Digite a descrição do item:");
                string descricao = Console.ReadLine();

                Item item = new Item(descricao);




                itens.Add(item);
            }


            Tarefa t = new Tarefa(titulo, prioridade, dataCriacao, itens);





            return t;
        }


        private int ObterNumeroTarefa()
        {
            int numeroTarefa;
            bool numeroTarefaEncontrado;

            do
            {
                List<EntidadeBase> tarefas = repositorioTarefa.SelecionarTodos();




                for (int i = 0; i < tarefas.Count; i++)
                {
                    Tarefa tarefa = (Tarefa)tarefas[i];

                    Console.WriteLine("Número: " + tarefa.numero);
                    Console.WriteLine("Título: " + tarefa.Titulo);
                    Console.WriteLine("Data de criação: " + tarefa.DataCriacao);
                    Console.WriteLine("Data de conclusão: " + tarefa.DataConclusao);
                    
                 
                    Console.WriteLine("\n");

                }


                Console.Write("Digite o número da tarefa que deseja selecionar: ");
                numeroTarefa = Convert.ToInt32(Console.ReadLine());

                numeroTarefaEncontrado = repositorioTarefa.ExisteRegistro(numeroTarefa);

                if (numeroTarefaEncontrado == false)
                    notificador.apresentarMensagem("Número da tarefa não encontrado, tente novamente..", TipoMensagem.Atencao);

            } while (numeroTarefaEncontrado == false);

            return numeroTarefa;
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da tarefa que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado =  repositorioTarefa.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                     notificador.apresentarMensagem("ID da tarefa não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

    }
}
