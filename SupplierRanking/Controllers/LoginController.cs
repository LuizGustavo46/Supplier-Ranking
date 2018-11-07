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
        public ActionResult Login(string codigoFuncionario, string cpf, string cnpj, string senha, string inputHidden)
        {
            if (inputHidden == "0") // 0 é Comprador Pessoa Física
            {
                Comprador cf = new Comprador();
                cf.Cpf = cpf;
                cf.Senha = senha;
                if (cf.LoginPessoaFisica())
                {
                    Session["User"] = cf;
                    ViewBag.Message = "Bem-vindo";
                    return RedirectToAction("UpdatePessoaFisica", "Comprador");
                }
                else
                {
                    ViewBag.Message = "CPF ou SENHA incorretos";
                }
            }

            if (inputHidden == "1") // 1 é Comprador Pessoa Juridica
            {
                Comprador cj = new Comprador();
                cj.Cnpj = cnpj;
                cj.Senha = senha;
                if (cj.LoginPessoaJuridica())
                {
                    Session["User"] = cj;
                    ViewBag.Message = "Bem-vindo";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "CNPJ ou SENHA incorretos";
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
                    ViewBag.Message = "Bem-vindo";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "CNPJ ou SENHA incorretos";
                }
            }

            if (inputHidden == "3") // 3 é funcionario
            {
                Fornecedor fu = new Fornecedor();
                fu.Codigo = int.Parse(codigoFuncionario);
                fu.Cnpj = cnpj;          
                fu.Senha = senha;
                if (fu.LoginFuncionario())
                {
                    Session["User"] = fu;
                    ViewBag.Message = "Bem-vindo";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Preencha corretamente os dados pedidos para efetuar o LOGIN";
                }
            }
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

    }
}