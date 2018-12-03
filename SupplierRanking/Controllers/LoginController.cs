﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupplierRanking.Models;

namespace SupplierRanking.Controllers
{
    public class LoginController : Controller
    {
        // Chama das Tela
        public ActionResult Login()
        {
            return View();
        }

        


        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string codigoFuncionario, string cpf, string cnpj, string senha, string inputHidden)
        {
            if (inputHidden.Equals("0")) // 0 é Comprador Pessoa Física
            {
                Comprador cf = new Comprador();
                cf.Cpf = cpf;
                cf.Senha = senha;
                if (cf.LoginPessoaFisica())
                {
                    Session["User"] = cf;
                    ViewBag.Message = "Bem-vindo";
                    return RedirectToAction("RankingGeral", "HomeLogada");
                }

                else
                {
                    ViewBag.Message = "Preencha o(s) campo(s) corretamente!";
                    ViewBag.cssClass = "col-8 error-msg alert-danger text-center p-2 mt-3 mb-4";
                }
            }

            else if (inputHidden.Equals("1")) // 1 é Comprador Pessoa Juridica
            {
                Comprador cj = new Comprador();
                cj.Cnpj = cnpj;
                cj.Senha = senha;
                if (cj.LoginPessoaJuridica())
                {
                    Session["User"] = cj;
                    ViewBag.Message = "Bem-vindo";
                    return RedirectToAction("RankingGeral", "HomeLogada");
                }

                else
                {
                    ViewBag.Message = "Preencha o(s) campo(s) corretamente!";
                    ViewBag.cssClass = "col-8 error-msg alert-danger text-center p-2 mt-3 mb-4";
                }
            }

            else if (inputHidden.Equals("2")) // 2 é Fornecedor
            {
                Fornecedor f = new Fornecedor();
                f.Cnpj = cnpj;
                f.Senha = senha;
                if (f.Login())
                {
                    Session["User"] = f;
                    ViewBag.Message = "Bem-vindo";
                    return RedirectToAction("RankingGeral", "HomeLogada");
                }

                else
                {
                    ViewBag.Message = "Preencha o(s) campo(s) corretamente!";
                    ViewBag.cssClass = "col-8 error-msg alert-danger text-center p-2 mt-3 mb-4";
                }
            }

            else if (inputHidden.Equals("3")) // 3 é funcionario
            {
                Fornecedor fu = new Fornecedor();
                fu.Codigo = int.Parse(codigoFuncionario);
                fu.Cnpj = cnpj;          
                fu.Senha = senha;
                if (fu.LoginFuncionario())
                {
                    Session["User"] = fu;
                    ViewBag.Message = "Bem-vindo";
                    return RedirectToAction("RankingGeral", "HomeLogada");
                }

                else
                {
                    ViewBag.Message = "Preencha o(s) campo(s) corretamente!";
                    ViewBag.cssClass = "col-8 error-msg alert-danger text-center p-2 mt-3 mb-4";
                }
            }
     
            return View();
        }

       /* public ActionResult Sair()
        {
            Session["0"] = null;
            Session["1"] = null;
            Session["2"] = null;
            Session["3"] = null;

            return RedirectToAction("Login", "Login");
        }*/

    }
}