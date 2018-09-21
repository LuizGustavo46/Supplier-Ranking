using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupplierRanking.Models;
using System.Net.Mail;

namespace SupplierRanking.Controllers
{
    public class FornecedorController : Controller
    {
        // GET: Fornecedor
        public ActionResult Index()
        {
            return View();
        }

        /*==============================================================================LOGIN FORNECEDOR=================================================================================*/
        public ActionResult Login()
         {
            return View();
         }

         [HttpPost]
         public ActionResult Login(string cnpj, string senha)
         {
        Fornecedor f = new Fornecedor();
        f.Cnpj = cnpj;
        f.Senha = senha;         
         
        return View();
         }
        /*================================================================================================================================================================================*/

        /*==============================================================================CADASTRO FUNCIONARIO==============================================================================*/
        public ActionResult CadastroFuncionario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroFuncionario(string cnpj, string senha, string nome, int codigo)
        {
            Fornecedor f = new Fornecedor();
            f.Cnpj = cnpj;
            f.Senha = senha;
            f.Nome = nome;
            f.Codigo = codigo.ToString();           

            TempData["Msg"] = f.CadastroFuncionario(cnpj, senha,  nome, codigo);
            return RedirectToAction("CadastrarFuncionario");
        }
        /*================================================================================================================================================================================*/

        /*==============================================================================CADASTRO FORNECEDOR===============================================================================*/
        public ActionResult CadastroPessoaJuridica()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult CadastroPessoaJuridica(string cnpj, string nome_empresa, string telefone, string celular, string endereco, string bairro, 
            string cidade, string uf, string cep, string senha, string slogan, string descricao, string plano, string imagem, string nome_categoria)
        {

            Fornecedor f = new Fornecedor();

            f.Cnpj = cnpj;
            f.Nome_empresa = nome_empresa;
            f.Telefone = telefone;
            f.Celular = celular;
            f.Endereco = endereco;
            f.Bairro = bairro;
            f.Cidade = cidade;
            f.Uf = uf;
            f.Cep = cep;
            f.Senha = senha;
            f.Slogan = slogan;
            f.Descricao = descricao;
            f.Media = 0;
            f.Plano = plano;
            f.Imagem = imagem;
            f.Nome_categoria = nome_categoria;

            return RedirectToAction("CadastroPessoaJuridica");
        }

        /*================================================================================================================================================================================*/

        /*=============================================================================EXCLUIR FUNCIONARIO================================================================================*/
        [HttpPost]
        public ActionResult ExcluirFuncionario(string nome, int codigo, string nomeDigitado, int codigoDigitado)
        {
        
            Fornecedor f = new Fornecedor();
            f.Nome = nome;
            f.Codigo = codigo.ToString();

            f.ExcluirFuncionario(nome, codigo, nomeDigitado, codigoDigitado);

            return RedirectToAction("Listar");

        }
        /*================================================================================================================================================================================*/

        /*==============================================================================ENVIO DE EMAIL====================================================================================*/
        public ActionResult EnviarEmail(string login)
        {
            //CRIAR A VIEW PARA RECUPERAR A SENHA
            return View();
        }

        [HttpPost]
        public ActionResult EnviarEmail(string nome, string email, string mensagem)
        {
            try
            {
                //Configurando a mensagem
                MailMessage mail = new MailMessage();
                //Origem
                mail.From = new MailAddress("suportsupplierranking@hotmail.com@hotmail.com");
                //Destinatário
                mail.To.Add(email);
                //Assunto
                mail.Subject = nome + "REDEFINIÇÃO DE SENHA - Supplier Ranking";
                //Corpo do e-mail
                mail.Body = "NADA";


                //Configurar o smtp
                SmtpClient smtpServer = new SmtpClient("smtp.live.com");
                //configurou porta
                smtpServer.Port = 25;
                //Habilitou o TLS
                smtpServer.EnableSsl = true;
                //Configurou usuario e senha p/ logar
                smtpServer.Credentials = new System.Net.NetworkCredential("suportsupplierranking@hotmail.com", "Senai1234");
                //Envia
                smtpServer.Send(mail);
                TempData["Msg"] = "Enviado com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Erro ao enviar";
            }

            return RedirectToAction("Listar");
        }

        /*================================================================================================================================================================================*/

        /*==============================================================================PESQUISA FUNCIONARIO==============================================================================*/
        public ActionResult PesquisaFuncionario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PesquisarFuncionario(string pesquisa)
        {
            List<Fornecedor> u = new List<Fornecedor>();
            if (pesquisa == null || pesquisa == "")
            {
                TempData["Msg"] = "Digite LOGIN ou NOME do funcionário.";
                return View(u);
            }
            else
            {
                u = Fornecedor.PesquisaFornecedor(pesquisa);
                if (u.Count == 0)
                    TempData["Msg"] = "Erro ao encontrar Fornecedor";
                return View(u);
            }

        }

        /*================================================================================================================================================================================*/

        /*==============================================================================UPDATE DE SENHA===================================================================================*/
        public ActionResult UpdateSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateSenha(string senha, string novaSenha, string senhaDigitada)
        {
            Fornecedor senhaUp = new Fornecedor();

            senhaUp.Senha = senha;

            bool res = senhaUp.UpdateSenha(senha, novaSenha, senhaDigitada);

            if (res)
                //RETORNAR NA VIEW DE UPDATE DE SENHA
                return RedirectToAction("Logar");
            else
                return View();
        }
        /*================================================================================================================================================================================*/

        /*==============================================================================UPDATE DE CADASTRO================================================================================*/
        public ActionResult UpdateCadastroPessoaJuridica(string login)
        {
         
            return View();
        }
      

        [HttpPost]
        public ActionResult UpdateCadastroPessoaJuridica(string cnpj, string nome_empresa, string email, string telefone, string bairro,string cidade, string rua, string uf,
            string imagem, string senha, string celular, string endereco, string posicao, string descricao, string cep, float media, string slogan, string plano, string nome_categoria)
        {
            Fornecedor f = new Fornecedor();
            f.Cnpj = cnpj;
            f.Nome_empresa = nome_empresa;
            f.Email = email;
            f.Telefone = telefone;
            f.Bairro = bairro;
            f.Cidade = cidade;
            f.Rua = rua;
            f.Uf = uf;
            f.Imagem = imagem;
            f.Senha = senha;
            f.Celular = celular;
            f.Endereco = endereco;
            f.Posicao = posicao;
            f.Descricao = descricao;
            f.Cep = cep;
            f.Media = 0;
            f.Slogan = slogan;
            f.Plano = plano;
            f.Nome_categoria = nome_categoria;

           

            string res = f.UpdateCadastroPessoaJuridica( cnpj,  nome_empresa,  email,  telefone,  bairro,  cidade,  rua,  uf,
             imagem,  senha,  celular,  endereco,  posicao,  descricao,  cep,  media,  slogan,  plano,  nome_categoria);
            TempData["Msg"] = res;
            if (res == "Salvo com sucesso!")
                return RedirectToAction("Listar");
            else
                return View();
        }
        /*==============================================================================================================================================================================*/

        public ActionResult Homepage()
        {
            return View();
        }
    }

}