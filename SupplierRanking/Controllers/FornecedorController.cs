﻿using System;
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
        public ActionResult Login()  //AARUMAR
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
        public ActionResult CadastroFuncionario() //FEITO 
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroFuncionario(string cnpj, string senha, string nome)
        {
            Fornecedor f = new Fornecedor();
            f.Cnpj = cnpj;
            f.Senha = senha;
            f.Nome = nome;

            if (f.CadastroFuncionario(cnpj, senha, nome))
            {
                ViewBag.Message = "Cadastro realizado com sucesso!";
                ViewBag.cssClass = "col-8 alert-msg alert-danger text-center p-2 mt-3 mb-5";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.Message = "Preencha o(s) campo(s) corretamente!";
                ViewBag.cssClass = "col-8 alert-msg alert-danger text-center p-2 mt-3 mb-5";
            }

            //TempData["Msg"] = f.CadastroFuncionario(cnpj, senha,  nome);
            return View();
        }


        /*================================================================================================================================================================================*/

        /*==============================================================================CADASTRO FORNECEDOR===============================================================================*/
        public ActionResult CadastroFornecedor()  //FEITO
        {
            Categorias c = new Categorias();
            ViewBag.ListaCategorias = c.ListaCategorias();

            return View();
        }
       
        [HttpPost]
        public ActionResult CadastroFornecedor(string cnpj, string nome_empresa, string email, string telefone, string celular, string endereco, string bairro, 
            string cidade, string uf, string cep, string senha, string slogan, string descricao, string plano, string nome_categoria, string confirmarSenha)
        {
            Fornecedor f = new Fornecedor();
            List<byte[]> listGaleriaFotos = new List<byte[]>();

            f.Cnpj = cnpj;
            f.Nome_empresa = nome_empresa;
            f.Email = email;
            f.Telefone = telefone;
            f.Celular = celular;
            f.Endereco = endereco;
            f.Bairro = bairro;
            f.Cidade = cidade;
            f.Uf = uf;
            f.Cep = cep;
            if(senha == confirmarSenha)
                f.Senha = senha;
            f.Slogan = slogan;
            f.Descricao = descricao;
            f.Media = 0;
            f.Plano = plano;
            f.Nome_categoria = nome_categoria;
            //f.Nome_categoria = "Informática";
            f.Media_qualidade = 0;
            f.Media_atendimento = 0;
            f.Media_entrega = 0;
            f.Media_preco = 0;
            f.Media_satisfacao = 0;

            //PROCURAR ARQUIVOS NO CADASTRO
            foreach (string arquivo in Request.Files)
            {
                HttpPostedFileBase arqPostado = Request.Files[arquivo];
                int tamConteudo = arqPostado.ContentLength; //PEGA O TAMANHO DO CONTEÚDO
                string tipoArq = arqPostado.ContentType; //PEGA O TIPO DO CONTEÚDO

                if (arquivo.Equals("imagem")) //SE FOR IGUAL A IMAGEM DE PERFIL
                {
                    if (tipoArq.IndexOf("jpeg") > 0)
                    {
                        //CONVERTER PARA BYTES
                        byte[] imagemBytes = new byte[tamConteudo];
                        arqPostado.InputStream.Read(imagemBytes, 0, tamConteudo);
                        f.Imagem = imagemBytes;
                    }
                    else
                    {
                        f.Imagem = new byte[] { };
                    }
                }
                else if (arquivo.Equals("galeriaFotos")) //SE FOR IGUAL A GALERIA DE FOTOS
                {
                    if (tipoArq.IndexOf("jpeg") > 0)
                    {
                        //CONVERTER PARA BYTES
                        byte[] galeriaFotosBytes = new byte[tamConteudo];
                        arqPostado.InputStream.Read(galeriaFotosBytes, 0, tamConteudo);
                        listGaleriaFotos.Add(galeriaFotosBytes);
                    }
                    else
                    {
                        listGaleriaFotos = null;
                    }
                }
                else if (arquivo.Equals("pdf")) //SE FOR IGUAL A PDF
                {
                    if (tipoArq.IndexOf("pdf") > 0)
                    {
                        //CONVERTER PARA BYTES
                        byte[] pdfBytes = new byte[tamConteudo];
                        arqPostado.InputStream.Read(pdfBytes, 0, tamConteudo);
                        f.Pdf = pdfBytes;
                    }
                    else
                    {
                        f.Pdf = new byte[] { };
                    }
                }
            }





            if (f.CadastroFornecedor())
            {
                if (listGaleriaFotos != null)
                    f.CadastrarGaleriaFotos(cnpj, listGaleriaFotos);
                return RedirectToAction("Login", "Login");
            }
            else { return RedirectToAction("CadastroFornecedor"); }
        }

        /*================================================================================================================================================================================*/

        /*=============================================================================EXCLUIR FUNCIONARIO================================================================================*/
      
        public ActionResult ExcluirFuncionario(int codigo) //feito
        {
        
            Fornecedor f = new Fornecedor();  
       
            f.Codigo = codigo;

            f.ExcluirFuncionario(codigo);

            return RedirectToAction("ListaFuncionario");

        }
        /*================================================================================================================================================================================*/

        public ActionResult ExcluirContaFornecedor()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult ExcluirContaFornecedor(string confirmaSenha)
        {          
            Fornecedor excluir = new Fornecedor();  

            excluir.Cnpj = Session["UserFornecedor"].ToString();

            if (excluir.ExcluirContaFornecedor(confirmaSenha))
            {
                Session["UserFornecedor"] = null;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("ExcluirContaFornecedor");
            }

            
        }

        /*==============================================================================ENVIO DE EMAIL====================================================================================*/
        public ActionResult EsqueceuSuaSenha() //FEITO
        {
            return View();
        }

        [HttpPost]
        public ActionResult EsqueceuSuaSenha(string cnpj, string email)
        {
            Fornecedor f = new Fornecedor();
            if (f.EsqueceuSuaSenha(cnpj, email))
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
            //return RedirectToAction("EsqueceuSuaSenha");
        }

        /*================================================================================================================================================================================*/

        /*==============================================================================PESQUISA FUNCIONARIO==============================================================================*/

        public ActionResult ListaFuncionario() //TRAVADO PELA HOME LOGADA
        { 
            //nome da action result / nome do model /  nome do metodo
            return View("ListaFuncionario", Fornecedor.ListaFuncionario());
        }

        /*================================================================================================================================================================================*/

        /*==============================================================================PESQUISA FUNCIONARIO==============================================================================*/
       
            //CASO PRECISE ESTA FUNCIONANDO SÓ CRIAR A VIEW
         public ActionResult listaFornecedor() //EXCLUIR MÉTODO APÓS HOME LOGADA
        {
            //nome da action result / nome do model /  nome do metodo
            return View("listaFornecedor", Fornecedor.ListaFornecedor());
        }

        /*================================================================================================================================================================================*/

        /*==============================================================================UPDATE DE SENHA===================================================================================*/
        public ActionResult UpdateSenha() //FEITO
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateSenha(string senha, string novaSenha, string senhaConfirma)
        {
            Fornecedor senhaUp = new Fornecedor();

            senhaUp.Senha = senha;

            string cnpj=Session["UserFornecedor"].ToString();
            senhaUp.Cnpj=cnpj;
            
                bool res = senhaUp.UpdateSenha(senha, novaSenha, senhaConfirma);

                if (res)  //RETORNAR NA VIEW DE UPDATE DE SENHA
                return RedirectToAction("Login","Login");

               
                return View();
        }
        /*================================================================================================================================================================================*/

        /*================================================================================RESTAURAR SENHA=================================================================================*/
        public ActionResult NovaSenha() //feito 
        {
            return View();
        }

        [HttpPost]
        public ActionResult NovaSenha(string senhaAtual, string novaSenha, string senhaConfirma)
        {
            Fornecedor f = new Fornecedor();
            f.Senha = senhaAtual;

            string cnpj = Session["UserFornecedor"].ToString();
            f.Cnpj = cnpj;

            if (f.UpdateSenha(senhaAtual, novaSenha, senhaConfirma))
            {
                ViewBag.Message = "Nova Senha alterada com sucesso!";
                ViewBag.cssClass = "col- alert-msg alert-info text-center p-2 mb-5";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.Message = "Preencha o(s) campo(s) corretamente!";
                ViewBag.cssClass = "col-6 alert-msg alert-danger text-center p-2 mb-5";
            }

            return View();
        }

        /*================================================================================================================================================================================*/

        /*==============================================================================UPDATE DE CADASTRO================================================================================*/
        public ActionResult UpdateFornecedor(string cnpj) //FEITO
        {
            
            Fornecedor c = Fornecedor.Perfil(cnpj);

            if (c == null)
            {
                TempData["Msg"] = "Erro ao encontrar dados";
                return RedirectToAction("RankingGeral", "HomeLogada");//ver se o redrect esta certo!!
            }
            return View(c);
        }
      

        [HttpPost]
        public ActionResult UpdateFornecedor(string cnpj, string nome_empresa, string email, string telefone, string bairro,string cidade, string endereco, string uf,
            string celular, string descricao, string cep, string slogan, string nome_categoria)
        {

            Fornecedor f = new Fornecedor();
            f.Cnpj = cnpj;
            f.Nome_empresa = nome_empresa;
            f.Email = email;
            f.Telefone = telefone;
            f.Bairro = bairro;
            f.Cidade = cidade;
            f.Endereco = endereco;
            f.Uf = uf;       
            f.Celular = celular;
            f.Descricao = descricao;          
            f.Cep = cep;
            f.Slogan = slogan;           
            f.Nome_categoria = nome_categoria;
            
            
                        
            foreach (string imagem in Request.Files)
            {           
                HttpPostedFileBase arqPostado = Request.Files[imagem];
                int tamConteudo = arqPostado.ContentLength; //PEGA O TAMANHO DO CONTEÚDO
                string tipoArq = arqPostado.ContentType; //PEGA O TIPO DO CONTEÚDO

                if (tamConteudo == 0)
                {

                }

                //TESTAR SE A IMAGEM É JPEG
                if (tipoArq.IndexOf("jpeg") > 0)
                {
                    //CONVERTER PARA BYTES
                    byte[] imgBytes = new byte[tamConteudo];
                    arqPostado.InputStream.Read(imgBytes, 0, tamConteudo);
                    f.Imagem = imgBytes;
                }
            }


            if (f.UpdateFornecedor(cnpj, nome_empresa, email, telefone, bairro, cidade, endereco, uf,
                 celular, descricao, cep, slogan, nome_categoria /*confirmaSenha*/))
            {
                TempData["Msg"] = "Alterações Efetuadas com sucesso!";
            }
            else
                TempData["Msg"] = "Informações Incorretas";


                return RedirectToAction("RankingGeral", "HomeLogada");


        }
        /*==============================================================================================================================================================================*/


        public ActionResult UpdateFuncionarioFornecedor(/*int codigo*/)  //FEITO
        {
            Fornecedor upFun = Fornecedor.PerfilFuncionario(/*codigo*/1);

            if (upFun == null)
            {
                TempData["Msg"] = "Erro ao encontrar dados";
                //return View();
                return RedirectToAction("UpdateFuncionarioFornecedor");
            }
            else
            {
                return View(upFun);
            }
        }


        [HttpPost]
        public ActionResult UpdateFuncionarioFornecedor(int codigo, string nome, string senha)
        {
            Fornecedor f = new Fornecedor();
            f.Nome = nome;
            f.Senha = senha;      

            if (f.UpdateFuncionarioFornecedor(codigo, nome, senha))
            {
                TempData["Msg"] = "Alterações Efetuadas com sucesso!";
            }
            else
                TempData["Msg"] = "Informações Incorretas";


            return RedirectToAction("UpdateFuncionarioFornecedor");

           
        }


        public ActionResult EsqueceuSenhaFornecedor()
        {
            return View();
        }











        public ActionResult Homepage()
        {
            return View();
        }





    }

}