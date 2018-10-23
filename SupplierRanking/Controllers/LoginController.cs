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
        public ActionResult Login(string codigoFuncionario, string cpfComprador, string cnpjComprador, string cnpjFornecedor, string senha, string inputHidden)
        {
            if (inputHidden == "0") // 0 é Comprador Pessoa Física
            {
                Comprador cf = new Comprador();
                cf.Cpf = cpfComprador;
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
                cj.Cpf = cnpjComprador;
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
                f.Cnpj = cnpjFornecedor;
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

            if (inputHidden == "3") // 3 é funcionario
            {
                Fornecedor fu = new Fornecedor();
                fu.Codigo = codigoFuncionario;
                fu.Cnpj = cnpjFornecedor;          
                fu.Senha = senha;
                if (fu.LoginFuncionario())
                {
                    Session["User"] = fu;
                    TempData["Msg"] = "Bem-vindo";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Msg"] = "Preencha corretamente os dados pedidos para efetuar o LOGIN";
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