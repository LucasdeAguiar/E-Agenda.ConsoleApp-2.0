using E_Agenda.ConsoleApp.Compartilhado;
using E_Agenda.ConsoleApp.ModuloContato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace E_Agenda.ConsoleApp.ModuloCompromisso
{
    public class Compromisso : EntidadeBase
    {
        private readonly string assunto;
        private readonly string local;
        private DateTime data;
        private int horaInicio;
        private int horaTermino;
        private Contato contato;
        private bool jaOcorreu;

        public Compromisso(string assunto, string local, DateTime data, int horaInicio, int horaTermino, Contato contato)
        {
            this.assunto = assunto;
            this.local = local;
            this.data = data;
            this.horaInicio = horaInicio;
            this.horaTermino = horaTermino;
            this.contato = contato;
            this.jaOcorreu = false;
        }

        public string Assunto => assunto;
        public string Local => local;
        public DateTime Data { get => data; set => data = value; }
        public int HoraInicio { get => horaInicio; set => horaInicio = value; }
        public int HoraTermino { get => horaTermino; set => horaTermino = value; }
        public Contato Contato { get => contato; set => contato = value; }
        public bool JaOcorreu { get => jaOcorreu; set => jaOcorreu = value; }

        public override string ToString()
        {

            return "Número: " + numero + Environment.NewLine +
            "Assunto: " + Assunto + Environment.NewLine +
            "Local: " + Local + Environment.NewLine +
            "Data: " + Data + Environment.NewLine +
            "Hora início:  " + horaInicio + ":00 hrs" + Environment.NewLine + 
            "Hora Término: " + horaTermino + ":00 hrs" + Environment.NewLine + 
            "Contato: " + contato.Nome + Environment.NewLine + 
            "Já ocorreu? " + jaOcorreu + Environment.NewLine;
        }

        public void verificaSeOcorreu()
        {
            if (this.Data <= DateTime.Today)
            {
                jaOcorreu = true;
            }
        }
    }
}
