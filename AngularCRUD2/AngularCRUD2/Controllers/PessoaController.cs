using AngularCRUD2.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using AngularCRUD2.Models.ModelViews;
using System.Collections;

namespace AngularCRUD2.Areas.Controller
{
    public class PessoaController : ApiController
    {


        #region Buscar

        [HttpGet]
        public object BuscarPessoa()
        {
            using (BancoDadosEntities Obj = new BancoDadosEntities())
            {
                var ListaPessoa = (from i in Obj.Pessoa
                                   select new PessoaModelView()
                                   {
                                       IdPessoa = i.IdPessoa,
                                       Nome = i.Nome,
                                       Telefone = i.Telefone,
                                       Email = i.Email,
                                       Idade = i.Idade ?? 0,
                                       Skype = i.Skype,
                                       Cidade = i.Cidade,
                                       Estado = i.Estado,
                                       Portfolio = i.Portfolio,
                                       NomeBanco = i.NomeBanco,
                                       CPF = i.CPF,
                                       NomePessoaBanco = i.NomePessoaBanco,
                                       TipoConta = i.TipoConta,
                                       Agencia = i.Agencia,
                                       NrConta = i.NrConta,
                                       OPConta = i.OPConta,
                                       Disponibilidade = i.Disponibilidade,
                                       Horario = i.Horario,
                                       Experiencias = (from t in Obj.Experiencia
                                                       where t.IdPessoa == i.IdPessoa
                                                       select new ExperienciaModelView
                                                       {
                                                           IdExperiencia = t.IdExperiencia,
                                                           Nome = t.Nome,
                                                           Nivel = t.Nivel ?? 0,
                                                       }).ToList<ExperienciaModelView>()
                                   }).ToList();
                return Json(ListaPessoa);
            }
        }
        #endregion

        #region Gravar
        [HttpPost]
        public string GravarPessoa(PessoaModelView Pessoa)
        {

            #region validações
            if (Pessoa.Email == "" || Pessoa.Email == null)
                return "Por favor informe o email";
            if (Pessoa.Telefone == "" || Pessoa.Telefone == null)
                return "Por favor informe o telefone";
            if (Pessoa.Nome == "" || Pessoa.Nome == null)
                return "Por favor informe o nome";
            if (Pessoa.Idade == 0)
                return "Por favor informe a idade";
            #endregion

            if (Pessoa != null)
            {
                using (BancoDadosEntities Obj = new BancoDadosEntities())
                {
                    Pessoa ObjPessoa = new Pessoa();
                    ObjPessoa.Nome = Pessoa.Nome;
                    ObjPessoa.Telefone = Pessoa.Telefone;
                    ObjPessoa.Email = Pessoa.Email;
                    ObjPessoa.Idade = Pessoa.Idade;
                    ObjPessoa.Skype = Pessoa.Skype;
                    ObjPessoa.Cidade = Pessoa.Cidade;
                    ObjPessoa.Estado = Pessoa.Estado;
                    ObjPessoa.Portfolio = Pessoa.Portfolio;
                    ObjPessoa.NomeBanco = Pessoa.NomeBanco;
                    ObjPessoa.CPF = Pessoa.CPF;
                    ObjPessoa.NomePessoaBanco = Pessoa.NomePessoaBanco;
                    ObjPessoa.TipoConta = Pessoa.TipoConta;
                    ObjPessoa.Agencia = Pessoa.Agencia;
                    ObjPessoa.NrConta = Pessoa.NrConta;
                    ObjPessoa.OPConta = Pessoa.OPConta;
                    ObjPessoa.Disponibilidade = Pessoa.Disponibilidade;
                    ObjPessoa.Horario = Pessoa.Horario;
                    Obj.Pessoa.Add(ObjPessoa);
                    Obj.SaveChanges();

                    if (Pessoa.Experiencias != null)
                    {
                        foreach (var exp in Pessoa.Experiencias)
                        {
                            Experiencia objExp = new Experiencia();
                            objExp.IdPessoa = ObjPessoa.IdPessoa;
                            objExp.Nivel = exp.Nivel;
                            objExp.Nome = exp.Nome;
                            Obj.Experiencia.Add(objExp);
                        }

                        Obj.SaveChanges();

                    }
                    return "Pessoa adicionada com sucesso";
                }
            }
            else
            {
                return "Não foi possivel cadastrar está pessoa";
            }
        }
        #endregion

        #region Alterar
        [HttpPost]
        public string AlterarPessoa(PessoaModelView Pessoa)
        {
            #region validações
            if (Pessoa.Email == "" || Pessoa.Email == null)
                return "Por favor informe o email";
            if (Pessoa.Telefone == "" || Pessoa.Telefone == null)
                return "Por favor informe o telefone";
            if (Pessoa.Nome == "" || Pessoa.Nome == null)
                return "Por favor informe o nome";
            if (Pessoa.Idade == 0)
                return "Por favor informe a idade";
            #endregion

            if (Pessoa != null)
            {
                using (BancoDadosEntities Con = new BancoDadosEntities())
                {
              
                    Pessoa Obj = Con.Pessoa.Where(x => x.IdPessoa == Pessoa.IdPessoa).FirstOrDefault();
                    if (Obj == null)
                        return "Está Pessoa não foi encontrada em nosso banco de dados, por favor contate o suporte.";

                    var listaExperencia = (from i in Con.Experiencia where i.IdPessoa == Pessoa.IdPessoa select i).ToList();
                    Con.Experiencia.RemoveRange(listaExperencia);

                    Obj.Email = Pessoa.Email;
                    Obj.Telefone = Pessoa.Telefone;
                    Obj.Nome = Pessoa.Nome;
                    Obj.Idade = Pessoa.Idade;
                    Obj.Skype = Pessoa.Skype;
                    Obj.Cidade = Pessoa.Cidade;
                    Obj.Portfolio = Pessoa.Portfolio;
                    Obj.NomeBanco = Pessoa.NomeBanco;
                    Obj.CPF = Pessoa.CPF;
                    Obj.NomePessoaBanco = Pessoa.NomePessoaBanco;
                    Obj.TipoConta = Pessoa.TipoConta;
                    Obj.Agencia = Pessoa.Agencia;
                    Obj.NrConta = Pessoa.NrConta;
                    Obj.OPConta = Pessoa.OPConta;
                    Obj.Disponibilidade = Pessoa.Disponibilidade;
                    Obj.Horario = Pessoa.Horario;
                    Con.SaveChanges();


                    if (Pessoa.Experiencias != null)
                    {
                        foreach (var exp in Pessoa.Experiencias)
                        {
                            Experiencia objExp = new Experiencia();
                            objExp.IdPessoa = Obj.IdPessoa;
                            objExp.Nivel = exp.Nivel;
                            objExp.Nome = exp.Nome;
                            Obj.Experiencia.Add(objExp);
                        }

                        Con.SaveChanges();

                    }

                    return "Pessoa atualizada com sucesso!";
                }
            }
            else
            {
                return "Pessoa não atualizada";
            }
        }
        #endregion

        #region Deletar
        [HttpPost]
        public string DeletePessoa(Pessoa Pessoa)
        {
            if (Pessoa != null)
            {
                using (BancoDadosEntities Con = new BancoDadosEntities())
                {
                    var listaExperiencia = (from xp in Con.Experiencia where Pessoa.IdPessoa == xp.IdPessoa select xp).ToList();
                    if (listaExperiencia.Count > 0 && listaExperiencia != null)
                        Con.Experiencia.RemoveRange(listaExperiencia);

                    var obj = (from i in Con.Pessoa where Pessoa.IdPessoa == i.IdPessoa select i).FirstOrDefault();
                    if(obj != null)
                        Con.Pessoa.Remove(obj);
                    
                    Con.SaveChanges();
                    return "Pessoa deletada com sucesso";
                }
            }
            else
            {
                return "Pessoa não deletada";
            }
        }
        #endregion

    }
}