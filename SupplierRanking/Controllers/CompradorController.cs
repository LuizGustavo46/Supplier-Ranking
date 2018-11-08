using SupplierRanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupplierRanking.Controllers
{
    public class CompradorController : Controller
    {
        // GET: Comprador
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginPessoaFisica()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginPessoaFisica(string cpf, string senha)
        {
            Comprador login = new Comprador();
            login.Cpf   = cpf;
            login.Senha = senha;
            if (login.LoginPessoaFisica())
            {
                Session["User"] = login;
                TempData["Msg"] = "Bem-vindo";
                return RedirectToAction("Index", "Comprador");
            }
            else
                TempData["Msg"] = "CPF ou SENHA incorretos";
            return View();
        }

        public ActionResult LoginPessoaJuridica()
        {
            return View();
        }

        public ActionResult LoginPessoaJuridica(string cnpj, string senha)
        {
            Comprador login = new Comprador();
            login.Cnpj  = cnpj;
            login.Senha = senha;
            if (login.LoginPessoaJuridica())
            {
                Session["User"] = login;
                TempData["Msg"] = "Bem-vindo";
                return RedirectToAction("Index", "Comprador");
            }
            else
                TempData["Msg"] = "CNPJ ou SENHA incorretos";
            return View();
        }

        public ActionResult CadastroPessoaFisica()
        {
            Categorias c = new Categorias();
            ViewBag.ListaCategorias = c.ListaCategorias();

            return View();
        }

        [HttpPost]
        public ActionResult CadastroPessoaFisica(string cpf, string nome, string sobrenome, string email, string senha,string confirmarSenha, string uf, string telefone, string celular)
        {
            Comprador c = new Comprador();
            c.Cpf           = cpf;
            c.Nome          = nome;
            c.Sobrenome     = sobrenome;
            c.Email         = email;
            if(senha == confirmarSenha)
                c.Senha     = senha;
            c.Tipo_pessoa   = "F";  //F DE PESSOA FISICA, NÃO PRECISA SER PASSADO ATRAVÉS DA VIEW
            c.Cnpj          = "";   //NÃO VAI SER USADO
            c.Nome_empresa  = "";   //NÃO VAI SER USADO
            c.Endereco      = "";   //NÃO VAI SER USADO
            c.Bairro        = "";   //NÃO VAI SER USADO
            c.Cidade        = "";   //NÃO VAI SER USADO
            c.Uf            = uf;
            c.Cep           = "";   //NÃO VAI SER USADO
            c.Telefone      = telefone;
            c.Celular       = celular;

            if (c.CadastroPessoaFisica())
            {
                ViewBag.Message = "Cadastro Realizado";
                ViewBag.cssClass = "col-8 error-msg alert-danger text-center p-2 mt-3 mb-4";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.Message = "Informações Inválidas";
                ViewBag.cssClass = "col-8 error-msg alert-danger text-center p-2 mt-3 mb-4";
            }

            return RedirectToAction("CadastroPessoaFisica");
        }

        public ActionResult UpdatePessoaFisica(/*int codigo*/)
        {
            Comprador c = Comprador.BuscaPessoa(/*codigo*/1);

            if (c == null)
            {
                TempData["Msg"] = "Erro ao encontrar dados";
                return RedirectToAction("UpdatePessoaFisica");
            }
            return View(c);
        }

        [HttpPost]
        public ActionResult UpdatePessoaFisica(string codigo, string cpf, string nome, string sobrenome, string email, string uf, string telefone, string celular)
        {
            Comprador c = new Comprador();
            c.Codigo        = int.Parse(codigo);
            c.Cpf           = cpf;
            c.Nome          = nome;
            c.Sobrenome     = sobrenome;
            c.Email         = email;
            c.Uf            = uf;
            c.Telefone      = telefone;
            c.Celular       = celular;

            if (c.UpdatePessoaFisica())
                TempData["Msg"] = "Alterações Realizadas";
            else
                TempData["Msg"] = "Informações Inválidas";

            return RedirectToAction("UpdatePessoaFisica");
        }

        public ActionResult CadastroPessoaJuridica()
        {
            //Comprador c = Comprador.TelaCadastroComprador();
            //return View(c);
            return View();
        }

        [HttpPost]
        public ActionResult CadastroPessoaJuridica(string cnpj, string nome_empresa, string email, string senha, string confirmarSenha, string endereco, string bairro, string cidade, string cep, string uf, string telefone, string celular)
        {
            Comprador c = new Comprador();
            c.Cpf           = ""; //NÃO VAI SER USADO
            c.Nome          = ""; //NÃO VAI SER USADO
            c.Sobrenome     = ""; //NÃO VAI SER USADO
            c.Email         = email;
            if (senha == confirmarSenha)
                c.Senha     = senha;
            else
                ViewBag.Message = "As senhas não correspondem";
            c.Tipo_pessoa   = "J"; //J DE PESSOA JURIDICA, NÃO PRECISA SER PASSADO ATRAVÉS DA VIEW
            c.Cnpj          = cnpj;
            c.Nome_empresa  = nome_empresa;
            c.Endereco      = endereco;
            c.Bairro        = bairro;
            c.Cidade        = cidade;
            c.Uf            = uf;
            c.Cep           = cep;
            c.Telefone      = telefone;
            c.Celular       = celular;

            if (c.CadastroPessoaJuridica())
                //TempData["Msg"] = "Cadastro Realizado";
                ViewBag.Message = "Cadastro Realizado";
            else
                //TempData["Msg"] = "Informações Inválidas";
                ViewBag.Message = "Informações Inválidas";

            return RedirectToAction("CadastroPessoaJuridica");
        }

        public ActionResult UpdatePessoaJuridica(/*int codigo*/)
        {
            Comprador c = Comprador.BuscaPessoa(/*codigo*/5);

            if (c == null)
            {
                TempData["Msg"] = "Erro ao encontrar dados";
                return RedirectToAction("Index");
            }
            return View(c);
        }

        [HttpPost]
        public ActionResult UpdatePessoaJuridica(string codigo, string nome_empresa, string email, string endereco, string bairro, string cidade, string uf, string cep, string telefone, string celular)
        {
            Comprador c = new Comprador();
            c.Codigo        = int.Parse(codigo);         
            c.Email         = email;
            c.Nome_empresa  = nome_empresa;
            c.Endereco      = endereco;
            c.Bairro        = bairro;
            c.Cidade        = cidade;
            c.Uf            = uf;
            c.Cep           = cep;
            c.Telefone      = telefone;
            c.Celular       = celular;

            if (c.UpdatePessoaJuridica())
                TempData["Msg"] = "Alterações Realizadas";
            else
                TempData["Msg"] = "Informações Inválidas";

            return RedirectToAction("UpdatePessoaJuridica");
        }

        public ActionResult UpdateSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateSenha(/*int codigo ,*/string senhaAntiga, string senhaNova, string confirmaSenhaNova)
        {
            Comprador c = new Comprador();
            c.Codigo = /*codigo*/1;
            if (c.UpdateSenha(senhaAntiga, senhaNova, confirmaSenhaNova))
            {
                TempData["Msg"] = "Senha Alterada";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                TempData["Msg"] = "Senhas não correspondem";
                return RedirectToAction("");
            }

            return RedirectToAction("UpdateSenha");
        }

        public ActionResult RestaurarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RestaurarSenha(string cnpj, string cpf, string email)
        {
            Comprador c = new Comprador();
            if(c.RestaurarSenha(cnpj, cpf, email))
                TempData["Msg"] = "Confirmação enviada para o e-mail cadastrado";
            else
                TempData["Msg"] = "Informação Inválida";

            return RedirectToAction("RestaurarSenha");
        }

        public ActionResult NovaSenha()
        {
            return View();
        }

        public ActionResult ExcluirConta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ExcluirConta(int codigo, string senha)
        {
            Comprador c = new Comprador();

            if(c.ExcluirConta(codigo, senha))
                TempData["Msg"] = "Sua conta foi excluída";
            else
                TempData["Msg"] = "Senha incorreta";

            return RedirectToAction("Index","Home");
        }
















    }//FIM DO COMPRADOR CONTROLLER
}//FIM DO CONTROLLER