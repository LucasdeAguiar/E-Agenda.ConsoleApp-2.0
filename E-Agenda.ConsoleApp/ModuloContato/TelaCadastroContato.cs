using E_Agenda.ConsoleApp.Compartilhado;
using E_Agenda.ConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.ConsoleApp.ModuloContato
{
    public class TelaCadastroContato : TelaBase, ICadastroBasico
    {
        private readonly Notificador notificador;
        private readonly RepositorioContato repositorioContato;

        public TelaCadastroContato(RepositorioContato repositorioContato, Notificador notificador) : base("Cadastro de Contatos")
        {
            this.repositorioContato = repositorioContato;
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
            MostrarTitulo("Inserindo novo Contato");
            Contato contato = ObterContato();

            repositorioContato.Inserir(contato);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Contato");

            int numeroContato = ObterNumeroContato();

            Contato contatoAtualizado = EditarContato();

            repositorioContato.Editar(numeroContato, contatoAtualizado);

            notificador.apresentarMensagem("Contato editado com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Contato");

            bool temContatosCadastrados = VisualizarRegistros("Pesquisando:");

            if (temContatosCadastrados == false)
            {
                notificador.apresentarMensagem("Nenhum contato cadastrado..", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroContato();

            repositorioContato.Excluir(numeroContato);

            notificador.apresentarMensagem("Contato excluído com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Contatos");

            List<EntidadeBase> contatos = repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
                return false;

           

            for (int i = 0; i < contatos.Count; i++)
            {
                Contato contato = (Contato)contatos[i];
                

                Console.WriteLine(contato.ToString());


                Console.WriteLine("\n");

            }
            Console.ReadLine();
            return true;
        }

        //Métodos privados

        private Contato ObterContato()
        {
            Console.Write("Digite o nome do contato: ");
            string nome = Console.ReadLine();

            string email;
            bool temArroba;
            do
            {
                Console.Write("Digite o nome o email do contato: ");
                email = Console.ReadLine();

                if (email.Contains("@"))
                {
                    temArroba = true;
                }
                else
                {
                    temArroba = false;
                    notificador.apresentarMensagem("Email inválido..", TipoMensagem.Atencao);
                }
            } while (temArroba == false);

            bool numeroVerificado;
            string telefone;

            do {
                Console.Write("Digite o número do telefone do contato: ");
                 telefone = Console.ReadLine();

                 numeroVerificado = verificaTelefone(telefone);

                if (numeroVerificado == false)
                {
                    notificador.apresentarMensagem("Número inválido..", TipoMensagem.Atencao);

                }
            } while (numeroVerificado == false);

            Console.Write("Digite onde o nome da empresa do " + nome + ":");
            string empresa = Console.ReadLine();

            Console.WriteLine("Digite o cargo do " + nome + "na " + empresa + ":");
            string cargo = Console.ReadLine();

            Contato contato = new Contato(nome, email, telefone, empresa, cargo);

            return contato;
        }

        private int ObterNumeroContato()
        {
            int numeroContato;
            bool numeroContatoEncontrado;

            do
            {
                List<EntidadeBase> contatos = repositorioContato.SelecionarTodos();




                for (int i = 0; i < contatos.Count; i++)
                {
                    Contato contato = (Contato)contatos[i];

                    Console.WriteLine("Nome: " + contato.Nome);
                    Console.WriteLine("Email: " + contato.Email);
                    Console.WriteLine("Telefone: " + contato.Telefone);
                    Console.WriteLine("Empresa: " + contato.Empresa);
                    Console.WriteLine("Cargo: " + contato.Cargo);


                    Console.WriteLine("\n");

                }


                Console.Write("Digite o número do contato que deseja selecionar: ");
                numeroContato = Convert.ToInt32(Console.ReadLine());

                numeroContatoEncontrado = repositorioContato.ExisteRegistro(numeroContato);

                if (numeroContatoEncontrado == false)
                    notificador.apresentarMensagem("Número do contato não encontrado, tente novamente..", TipoMensagem.Atencao);

            } while (numeroContatoEncontrado == false);

            return numeroContato;
        }

        private Contato EditarContato()
        {

            Console.Write("Digite o nome do contato: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome o email do contato: ");
            string email = Console.ReadLine();

            Console.Write("Digite o número do telefone do contato: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite onde o nome da empresa do " + nome + ":");
            string empresa = Console.ReadLine();

            Console.WriteLine("Digite o cargo do " + nome + "na " + empresa + ":");
            string cargo = Console.ReadLine();

            Contato contato = new Contato(nome, email, telefone, empresa, cargo);


            return contato;
        }

        private bool verificaTelefone(string telefone)
        {
            if (telefone.Length == 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
