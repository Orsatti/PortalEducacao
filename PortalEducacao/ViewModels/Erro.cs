using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalEducacao.ViewModels
{
    public class Erro
    {
        public List<string> listaErros { get; set; }

        public Erro()
        {
            this.listaErros = new List<string>();
        }

        public Erro(string mensagem)
        {
            this.listaErros = new List<string>();
            this.listaErros.Add(mensagem);
        }

        public void adicionaErro(string mensagem)
        {
            this.listaErros.Add(mensagem);
        }
    }
}