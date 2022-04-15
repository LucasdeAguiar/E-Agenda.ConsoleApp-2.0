using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.ConsoleApp.Compartilhado
{
    public class RepositorioBase
    {
        protected readonly List<EntidadeBase> registros;
        protected int contadorNumero = 1;

        public RepositorioBase()
        {
            registros = new List<EntidadeBase>();
        }


        public virtual void Inserir(EntidadeBase entidade)
        {
            entidade.numero = contadorNumero;
            registros.Add(entidade);
            Notificador not = new Notificador();
            not.apresentarMensagem("Adicionado com sucesso!", TipoMensagem.Sucesso);
            contadorNumero++;
        }

        public void Editar(int numeroSelecionado, EntidadeBase entidade)
        {
            for (int i = 0; i < registros.Count; i++)
            {
                if (this.registros[i].numero == numeroSelecionado)
                {
                    entidade.numero = numeroSelecionado;
                    this.registros[i] = entidade;

                    break;
                }
            }
        }

        public bool Excluir(int numeroSelecionado)
        {
            EntidadeBase entidadeSelecionada = SelecionarRegistro(numeroSelecionado);

            if (entidadeSelecionada == null)
                return false;

            registros.Remove(entidadeSelecionada);

            return true;
        }

        public EntidadeBase SelecionarRegistro(int numeroRegistro)
        {
            foreach (EntidadeBase registro in this.registros)
                if (numeroRegistro == registro.numero)
                    return registro;

            return null;
        }

        public List<EntidadeBase> SelecionarTodos()
        {
            return this.registros;

        }

        public bool ExisteRegistro(int numeroRegistro)
        {
            foreach (EntidadeBase registro in this.registros)

                if (registro.numero == numeroRegistro)
                    return true;


            return false;

        }

    }
}
