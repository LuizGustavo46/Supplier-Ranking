﻿using SupplierRanking.Models;
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
            Categorias c = new Categorias();
            ViewBag.ListaCategorias = c.ListaCategorias();

            return View();
        }

        [HttpPost]
        public ActionResult CadastroPessoaFisica(string cpf, string nome, string sobrenome, string email, string senha,
            string confirmarSenha, string uf, string telefone, string celular)
        {
            //string[] checks = form["check"].Split(',');
            //FormCollection form/*List<Categorias> listaCategorias*/

            int cont = 0;
            List<Categorias> lista = new List<Categorias>();
            foreach (string item in Request.Form.AllKeys)
            {
                if (cont >= 9)
                {
                    Categorias cat = new Categorias();
                    cat.Categoria = item;
                    lista.Add(cat);
                }
                cont++;
            }


            Comprador c = new Comprador();
            c.Cpf = cpf;
            c.Nome = nome;
            c.Sobrenome = sobrenome;
            c.Email = email;
            if (senha == confirmarSenha)
                c.Senha = senha;
            else
                ViewBag.Message = "As senhas não correspondem";
            c.Tipo_pessoa = "F";  //F DE PESSOA FISICA, NÃO PRECISA SER PASSADO ATRAVÉS DA VIEW
            c.Cnpj = "";   //NÃO VAI SER USADO
            c.Nome_empresa = "";   //NÃO VAI SER USADO
            c.Uf = uf;
            c.Telefone = telefone;
            c.Celular = celular;

            if (c.CadastroPessoaFisica() && c.CadastrarInteresses(lista))
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

        public ActionResult UpdatePessoaFisica(int codigo)
        {
            Comprador c = Comprador.BuscaPessoa(codigo);
                if (c == null)
                {
                    TempData["Msg"] = "Erro ao encontrar dados";
                    return RedirectToAction("UpdatePessoaFisica");
                }
                return View(c);          
        }

        [HttpPost]
        public ActionResult UpdatePessoaFisica(string codigo, string cpf, string nome, string sobrenome,
            string email, string uf, string telefone, string celular)
        {
            Comprador c = new Comprador();
            c.Codigo = int.Parse(codigo);
            c.Cpf = cpf;
            c.Nome = nome;
            c.Sobrenome = sobrenome;
            c.Email = email;
            c.Uf = uf;
            c.Telefone = telefone;
            c.Celular = celular;

            if (c.UpdatePessoaFisica()) { 
            TempData["Msg"] = "Alterações Realizadas";
            return RedirectToAction("RankingGeral", "HomeLogada");
        }
            else{
                TempData["Msg"] = "Informações Inválidas";

            return RedirectToAction("UpdatePessoaFisica");
        }

        }

        public ActionResult CadastroPessoaJuridica()
        {
            Categorias c = new Categorias();
            ViewBag.ListaCategorias = c.ListaCategorias();
            return View();
        }

        [HttpPost]
        public ActionResult CadastroPessoaJuridica(string cnpj, string nome_empresa, string email, string senha,
            string confirmarSenha,string uf, string telefone, string celular)
        {
            int cont = 0;
            List<Categorias> lista = new List<Categorias>();
            foreach (string item in Request.Form.AllKeys)
            {
                if (cont >= 8)
                {
                    Categorias cat = new Categorias();
                    cat.Categoria = item;
                    lista.Add(cat);
                }
                cont++;
            }

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
            c.Uf            = uf;
            c.Telefone      = telefone;
            c.Celular       = celular;

            if (c.CadastroPessoaJuridica() && c.CadastrarInteresses(lista))
            {
                ViewBag.Message = "Cadastro Realizado";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.Message = "Informações Inválidas";
            }

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
        public ActionResult UpdatePessoaJuridica(string codigo, string nome_empresa, string email, string uf, string telefone, string celular)
        {
            Comprador c = new Comprador();
            c.Codigo        = int.Parse(codigo);         
            c.Email         = email;
            c.Nome_empresa  = nome_empresa;
            c.Uf            = uf;
            c.Telefone      = telefone;
            c.Celular       = celular;

            if (c.UpdatePessoaJuridica())
                TempData["Msg"] = "Alterações Realizadas";
            else
                TempData["Msg"] = "Informações Inválidas";

            return RedirectToAction("UpdatePessoaJuridica");
        }

        public ActionResult UpdateSenha(/*int codigo*/)
        {
            //Session["Codigo"] = codigo;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateSenha(string senhaAtual, string senhaNova, string confirmaSenhaNova)
        {
            Comprador c = new Comprador();
            c.Codigo = int.Parse(Session["CodigoUsuario"].ToString());
            
            if (c.UpdateSenha(senhaAtual, senhaNova, confirmaSenhaNova))
            {
                ViewBag.Message = "Nova Senha alterada com sucesso!";
                ViewBag.cssClass = "col-8 alert-msg alert-info text-center p-2 mb-5";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.Message = "Preencha o(s) campo(s) corretamente!";
                ViewBag.cssClass = "col-8 alert-msg alert-danger text-center p-2 mb-5";
            }

            return View("Comprador","UpdateSenha");
        }

        public ActionResult EsqueceuSuaSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EsqueceuSuaSenha(string cnpj, string cpf, string email)
        {
            Comprador c = new Comprador();
            if(c.EsqueceuSuaSenha(cnpj, cpf, email))
            {
                ViewBag.Message = "Confirmação enviada para o e-mail cadastrado!";
                ViewBag.cssClass = "col-8 alert-msg alert-info text-center p-2 mt-3 mb-5";
            }
            else
            {
                ViewBag.Message = "Preencha o(s) campo(s) corretamente!";
                ViewBag.cssClass = "col-8 alert-msg alert-danger text-center p-2 mt-3 mb-5";
            }

            return View();
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
        public ActionResult ExcluirConta(string confirmaSenha)
        {
            Comprador c = new Comprador();
            int codigo = 0;
            codigo = int.Parse(Session["CodigoUsuario"].ToString());
       

            if(c.ExcluirConta(codigo, confirmaSenha))
            {
                Session["UserPessoaFisica"] = null;
                Session["UserPessoaJuridica"] = null;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("ExcluirConta");
            }        
        }
















    }//FIM DO COMPRADOR CONTROLLER
}//FIM DO CONTROLLER