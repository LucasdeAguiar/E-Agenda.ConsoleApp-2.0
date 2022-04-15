using E_Agenda.ConsoleApp.Compartilhado;
using E_Agenda.ConsoleApp.Interfaces;
using E_Agenda.ConsoleApp.ModuloContato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.ConsoleApp.ModuloCompromisso
{
    public class TelaCadastroCompromisso : TelaBase, ICadastroBasico
    {
        private readonly Notificador notificador;
        private readonly RepositorioCompromisso repositorioCompromisso;
        private readonly TelaCadastroContato telaCadastroContato;
        private readonly RepositorioContato repositorioContato;

        public TelaCadastroCompromisso(TelaCadastroContato telaCadastroContato, RepositorioContato repositorioContato, RepositorioCompromisso repositorioCompromisso, Notificador notificador) : base("Cadastro de Compromissos")
        {
            this.telaCadastroContato = telaCadastroContato;
            this.repositorioContato = repositorioContato;
            this.repositorioCompromisso = repositorioCompromisso;
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
            MostrarTitulo("Inserindo novo Compromisso");

            Contato contatoSelecionado = ObtemContato();



            if (contatoSelecionado == null)
            {
                notificador.apresentarMensagem("Cadastre um contato antes de cadastrar compromissos!", TipoMensagem.Atencao);
                return;
            }


            Compromisso novoCompromisso = ObterCompromisso(contatoSelecionado);

            repositorioCompromisso.Inserir(novoCompromisso);

           
            notificador.apresentarMensagem("Compromisso inserido com sucesso!", TipoMensagem.Sucesso);
            
               
        }
        public void EditarRegistro()
        {
            MostrarTitulo("Editando Compromisso");

            int numeroCompromisso = ObterNumeroCompromisso();

            Contato contatoSelecionado = ObtemContato();

            Compromisso compromissoAtualizado = editarCompromisso(contatoSelecionado);

            repositorioCompromisso.Editar(numeroCompromisso, compromissoAtualizado);

            notificador.apresentarMensagem("Compromisso editado com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Compromisso");

            bool temCompromissosCadastrados = VisualizarRegistros("Pesquisando:");

            if (temCompromissosCadastrados == false)
            {
                notificador.apresentarMensagem("Nenhum compromisso cadastrado..", TipoMensagem.Atencao);
                return;
            }

            int numeroCompromisso = ObterNumeroCompromisso();

            repositorioCompromisso.Excluir(numeroCompromisso);

            notificador.apresentarMensagem("Compromisso excluído com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Compromissos");

            List<EntidadeBase> compromissos = repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
                return false;

            for (int i = 0; i < compromissos.Count; i++)
            {
                Compromisso compromisso = (Compromisso)compromissos[i];


                Console.WriteLine(compromisso.ToString());


                Console.WriteLine("\n");

            }
            Console.ReadLine();
            return true;
        }



        //Métodos Privados

        private Contato ObtemContato()
        {
            bool temContatosDisponiveis = telaCadastroContato.VisualizarRegistros("");

            if (!temContatosDisponiveis)
            {
                 notificador.apresentarMensagem("Você precisa cadastrar um contato antes de um compromisso!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do contato: ");
            int numContatoSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Contato contatoSelecionado = (Contato)repositorioContato.SelecionarRegistro(numContatoSelecionado);

            return contatoSelecionado;
        }

        private Compromisso ObterCompromisso(Contato contatoSelecionado)
        {
            Console.WriteLine("Digite o asssunto do compromisso:");
            string assunto = Console.ReadLine();

            Console.WriteLine("Digite o local do compromisso:");
             string local = Console.ReadLine();

            Console.WriteLine("Digite a data do compromisso: ");
            DateTime data = Convert.ToDateTime(Console.ReadLine());

            if (data <= DateTime.Today)
            {
                notificador.apresentarMensagem("Você precisa registrar uma data válida!", TipoMensagem.Atencao);
                return null;
            }

            Console.WriteLine("Digite a hora de início do compromisso:");
             int horaInicio = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite a hora de término do compromisso:");
            int horaTermino = Convert.ToInt32(Console.ReadLine());

            Compromisso novoCompromisso = new Compromisso(assunto, local, data, horaInicio, horaTermino, contatoSelecionado);


            return novoCompromisso;


        }

        private int ObterNumeroCompromisso()
        {
            int numeroCompromisso;
            bool numeroCompromissoEncontrado;

            do
            {
                List<EntidadeBase> compromissos = repositorioCompromisso.SelecionarTodos();




                for (int i = 0; i < compromissos.Count; i++)
                {
                    Compromisso compromisso = (Compromisso)compromissos[i];

                    Console.WriteLine("Número: " + compromisso.numero);
                    Console.WriteLine("Assunto: " + compromisso.Assunto);
                    Console.WriteLine("Data: " + compromisso.Data);
                    Console.WriteLine("Hora Início: " + compromisso.HoraInicio);
                    Console.WriteLine("Hora Término: " + compromisso.HoraTermino);

                    Console.WriteLine("\n");

                }


                Console.Write("Digite o número do compromisso que deseja selecionar: ");
                numeroCompromisso = Convert.ToInt32(Console.ReadLine());

                numeroCompromissoEncontrado = repositorioCompromisso.ExisteRegistro(numeroCompromisso);

                if (numeroCompromissoEncontrado == false)
                    notificador.apresentarMensagem("Número do compromisso não encontrado, tente novamente..", TipoMensagem.Atencao);

            } while (numeroCompromissoEncontrado == false);

            return numeroCompromisso;
        }

        private Compromisso editarCompromisso(Contato contatoSelecionado)
        {
            Console.WriteLine("Digite o assunto do compromisso:");
            string assunto = Console.ReadLine();

            Console.WriteLine("Digite o local do compromisso");
            string local = Console.ReadLine();

            Console.WriteLine("Digite a data do compromisso: ");
            DateTime data = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Digite a hora de início do compromisso: ");
            int horaInicio = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite a hora de término do compromisso: ");
            int horaTermino = Convert.ToInt32(Console.ReadLine());







            Compromisso c = new Compromisso(assunto, local, data, horaInicio, horaTermino, contatoSelecionado);





            return c;
        }
    }
}
