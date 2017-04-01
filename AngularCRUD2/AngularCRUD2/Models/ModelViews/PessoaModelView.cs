using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularCRUD2.Models.ModelViews
{
    public class PessoaModelView
    {
        public int IdPessoa { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }
        public string Skype { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Portfolio { get; set; }
        public string NomeBanco { get; set; }
        public string CPF { get; set; }
        public string NomePessoaBanco { get; set; }
        public string TipoConta { get; set; }
        public string Agencia { get; set; }
        public string NrConta { get; set; }
        public string OPConta { get; set; }
        public string Disponibilidade { get; set; }
        public string Horario { get; set; }
        public List<ExperienciaModelView> Experiencias = new List<ExperienciaModelView>();
    }
}

