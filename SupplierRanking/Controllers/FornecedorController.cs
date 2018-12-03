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
                               

            TempData["Msg"] = f.CadastroFuncionario(cnpj, senha,  nome);
            return RedirectToAction("CadastroFuncionario");
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
            string cidade, string uf, string cep, string senha, string slogan, string descricao, /*string planostring nome_categoria,*/  string confirmarSenha)
        {
            Fornecedor f = new Fornecedor();

            int cont = 0;
            foreach (string item in Request.Form.AllKeys)
            {
                if (cont >= 14)
                {
                    Categorias cat = new Categorias();
                    cat.Categoria = item;
                    f.Nome_categoria = cat.Categoria;
                }
                cont++;
            }

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
            f.Plano = "F";
            //f.Nome_categoria = nome_empresa;
            f.Media_qualidade = 0;
            f.Media_atendimento = 0;
            f.Media_entrega = 0;
            f.Media_preco = 0;
            f.Media_satisfacao = 0;

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
                else
                {
                    f.Imagem = new byte[] { };
                }
            }

            TempData["Msg"] = f.CadastroFornecedor();
            return RedirectToAction("CadastroFornecedor");
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

        public ActionResult ExcluirContaFornecedor(string cnpj)/*45.997.418/0001-53*/
        {          
            Fornecedor excluir = new Fornecedor();  //TRAVADO PELA HOME LOGADA

            excluir.Cnpj = cnpj;
            excluir.ExcluirContaFornecedor(cnpj);

            return RedirectToAction("listaFornecedor");
        }

        /*==============================================================================ENVIO DE EMAIL====================================================================================*/
        public ActionResult EsqueceuSuaSenha() //FEITO
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult EsqueceuSuaSenha(string cnpj, string email)
        {
            Fornecedor enviaEmail = new Fornecedor();

            enviaEmail.EsqueceuSuaSenha(cnpj, email); 


            return RedirectToAction("EsqueceuSuaSenha");
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
        public ActionResult UpdateSenha(string senha, string novaSenha, string senhaConfirma, string cnpj)
        {
            Fornecedor senhaUp = new Fornecedor();

            senhaUp.Senha = senha;
            

            
                bool res = senhaUp.UpdateSenha(senha, novaSenha, senhaConfirma, cnpj);

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
        public ActionResult NovaSenha(string senha, string novaSenha, string senhaConfirma, string cnpj)
        {
            Fornecedor senhaRe = new Fornecedor();

            senhaRe.Senha = senha;



            bool res = senhaRe.UpdateSenha(senha, novaSenha, senhaConfirma, cnpj);

            if (res)  //RETORNAR NA VIEW DE RESTAURAR DE SENHA
                return RedirectToAction("NovaSenha");

            return View("NovaSenha");
        }

        /*================================================================================================================================================================================*/

        /*==============================================================================UPDATE DE CADASTRO================================================================================*/
        public ActionResult UpdateFornecedor(string cnpj) //FEITO
        {
            
            Fornecedor c = Fornecedor.Perfil(/*cnpj*/"45.997.418/0001-53");

            if (c == null)
            {
                TempData["Msg"] = "Erro ao encontrar dados";
                return RedirectToAction("UpdateSenha");//ver se o redrect esta certo!!
            }
            return View(c);
        }
      

        [HttpPost]
        public ActionResult UpdateFornecedor(string cnpj, string nome_empresa, string email, string telefone, string bairro,string cidade, string endereco, string uf,
            string celular, string descricao, string cep, string slogan, string nome_categoria /*string confirmaSenha*/)
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


                return RedirectToAction("UpdateFornecedor");


        }
        /*==============================================================================================================================================================================*/


        public ActionResult UpdateFuncionarioFornecedor(/*int codigo*/)  //FEITO
        {
            Fornecedor upFun = Fornecedor.PerfilFuncionario(/*codigo*/3);

            if (upFun == null)
            {
                TempData["Msg"] = "Erro ao encontrar dados";
                return RedirectToAction("UpdateFuncionarioFornecedor");
            }
            return View(upFun);
            
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