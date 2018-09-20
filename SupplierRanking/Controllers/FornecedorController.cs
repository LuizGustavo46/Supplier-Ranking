using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupplierRanking.Models;

namespace SupplierRanking.Controllers
{
    public class FornecedorController : Controller
    {
        // GET: Fornecedor
        public ActionResult Index()
        {
            return View();
        }
    
         /*===LOGIN FORNECEDOR===*/
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
        /*======================*/


        /*===CADASTRO FUNCIONARIO - FORNECEDOR===*/
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
        /*======================*/



        /*===CADASTRO FORNECEDOR===*/
        public ActionResult CadastroPessoaJuridica()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult CadastroPessoaJuridica(string cnpj, string nome_empresa, string telefone, string celular, string endereco, string bairro, 
            string cidade, string uf, string cep, string senha, string slogan, string descrição, string plano, string imagem, string nome_categoria)
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
            f.Descricao = descrição;
            f.Media = 0;
            f.Plano = plano;
            f.Imagem = imagem;
            f.Nome_categoria = nome_categoria;

            return RedirectToAction("CadastroPessoaJuridica");
        }

        /*======================*/





        public ActionResult Homepage()
    {
        return View();
    }


    }

}