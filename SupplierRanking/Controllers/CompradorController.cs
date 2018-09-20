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
            login.Cpf = cpf;
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
            login.Cnpj = cnpj;
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
            Comprador c = Comprador.TelaCadastroComprador();
            return View(c);
        }

        [HttpPost]
        public ActionResult CadastroPessoaFisica(string cpf, string nome, string sobrenome, string email, string senha, string tipo_pessoa, string uf, string telefone, string celular)
        {
            Comprador c = new Comprador();
            c.Cpf = cpf;
            c.Nome = nome;
            c.Sobrenome = sobrenome;
            c.Email = email;
            c.Senha = senha;
            c.Tipo_pessoa = tipo_pessoa;
            c.Cnpj = ""; //NÃO VAI SER USADO
            c.Nome_empresa = ""; //NÃO VAI SER USADO
            c.Endereço = ""; //NÃO VAI SER USADO
            c.Bairro = ""; //NÃO VAI SER USADO
            c.Cidade = ""; //NÃO VAI SER USADO
            c.Uf = uf;
            c.Cep = ""; //NÃO VAI SER USADO
            c.Telefone = telefone;
            c.Celular = celular;

            if (c.CadastroPessoaFisica())
                TempData["Msg"] = "Cadastro Realizado";
            else
                TempData["Msg"] = "Informações Inválidas";

            return RedirectToAction("CadastroPessoaFisica");
        }

        public ActionResult UpdatePessoaFisica(int codigo)
        {
            Comprador c = Comprador.BuscaPessoa(codigo);

            if (c == null)
            { 
                TempData["Msg"] = "Erro ao encontrar dados";
                return RedirectToAction("Index");
            }
            return View(c);
        }

        [HttpPost]
        public ActionResult UpdatePessoaFisica(string cpf, string nome, string sobrenome, string email, string senha, string uf, string telefone, string celular)
        {
            Comprador c = new Comprador();
            c.Cpf = cpf;
            c.Nome = nome;
            c.Sobrenome = sobrenome;
            c.Email = email;
            c.Senha = ""; //A SENHA NÃO VAI SER ALTERADA POR AQUI
            c.Tipo_pessoa = ""; //NÃO VAI SER USADO
            c.Cnpj = ""; //NÃO VAI SER USADO
            c.Nome_empresa = ""; //NÃO VAI SER USADO
            c.Endereço = ""; //NÃO VAI SER USADO
            c.Bairro = ""; //NÃO VAI SER USADO
            c.Cidade = ""; //NÃO VAI SER USADO
            c.Uf = uf;
            c.Cep = ""; //NÃO VAI SER USADO
            c.Telefone = telefone;
            c.Celular = celular;

            if (c.UpdatePessoaFisica())
                TempData["Msg"] = "Alterações Realizadas";
            else
                TempData["Msg"] = "Informações Inválidas";

            return RedirectToAction("UpdatePessoaFisica");
        }

        public ActionResult CadastroPessoaJuridica()
        {
            Comprador c = Comprador.TelaCadastroComprador();
            return View(c);
        }

        [HttpPost]
        public ActionResult CadastroPessoaJuridica(string tipo_pessoa, string cnpj, string nome_empresa, string email, string senha, string endereco, string bairro, string cidade, string cep, string uf, string telefone, string celular)
        {
            Comprador c = new Comprador();
            c.Cpf = ""; //NÃO VAI SER USADO
            c.Nome = ""; //NÃO VAI SER USADO
            c.Sobrenome = ""; //NÃO VAI SER USADO
            c.Email = email;
            c.Senha = senha;
            c.Tipo_pessoa = tipo_pessoa;
            c.Cnpj = cnpj;
            c.Nome_empresa = nome_empresa; 
            c.Endereço = endereco; 
            c.Bairro = bairro; 
            c.Cidade = cidade; 
            c.Uf = uf;
            c.Cep = cep;
            c.Telefone = telefone;
            c.Celular = celular;

            if (c.CadastroPessoaJuridica())
                TempData["Msg"] = "Cadastro Realizado";
            else
                TempData["Msg"] = "Informações Inválidas";

            return RedirectToAction("CadastroPessoaJuridica");
        }

        public ActionResult UpdatePessoaJuridica(int codigo)
        {
            Comprador c = Comprador.BuscaPessoa(codigo);

            if (c == null)
            {
                TempData["Msg"] = "Erro ao encontrar dados";
                return RedirectToAction("Index");
            }
            return View(c);
        }

        [HttpPost]
        public ActionResult UpdatePessoaJuridica(string nome_empresa, string email, string endereco, string bairro, string cidade, string uf, string cep, string telefone, string celular)
        {
            Comprador c = new Comprador();
            c.Cpf = ""; //NÃO VAI SER USADO
            c.Nome = ""; //NÃO VAI SER USADO
            c.Sobrenome = ""; //NÃO VAI SER USADO
            c.Email = email;
            c.Senha = ""; //A SENHA NÃO VAI SER ALTERADA POR AQUI --- UpdateSenha
            c.Tipo_pessoa = ""; //NÃO VAI SER USADO
            c.Cnpj = ""; //NÃO VAI SER USADO
            c.Nome_empresa = nome_empresa;
            c.Endereço = endereco;
            c.Bairro = bairro;
            c.Cidade = cidade;
            c.Uf = uf;
            c.Cep = cep; 
            c.Telefone = telefone;
            c.Celular = celular;

            if (c.UpdatePessoaFisica())
                TempData["Msg"] = "Alterações Realizadas";
            else
                TempData["Msg"] = "Informações Inválidas";

            return RedirectToAction("UpdatePessoaJuridica");
        }














    }//FIM DO COMPRADOR CONTROLLER
}//FIM DO CONTROLLER