using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupplierRanking.Models;

namespace SupplierRanking.Controllers
{
    public class LoginController : Controller
    {
        // chama tela de login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string cpf, string cnpj, string senha, string inputHidden)
        {
            if (inputHidden == "0") // 0 é Comprador Pessoa Física
            {
                Comprador cf = new Comprador();
                cf.Cpf = cpf;
                cf.Senha = senha;
                if (cf.LoginPessoaFisica())
                {
                    Session["User"] = cf;
                    TempData["Msg"] = "Bem-vindo";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Msg"] = "CPF ou SENHA incorretos";
                }
            }

            if (inputHidden == "1") // 1 é Comprador Pessoa Juridica
            {
                Comprador cj = new Comprador();
                cj.Cpf = cpf;
                cj.Senha = senha;
                if (cj.LoginPessoaJuridica())
                {
                    Session["User"] = cj;
                    TempData["Msg"] = "Bem-vindo";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Msg"] = "CNPJ ou SENHA incorretos";
                }
            }

            if (inputHidden == "2") // 2 é Fornecedor
            {
                Fornecedor f = new Fornecedor();
                f.Cnpj = cnpj;
                f.Senha = senha;
                if (f.Login())
                {
                    Session["User"] = f;
                    TempData["Msg"] = "Bem-vindo";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Msg"] = "CNPJ ou SENHA incorretos";
                }
            }

            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }
    }
}