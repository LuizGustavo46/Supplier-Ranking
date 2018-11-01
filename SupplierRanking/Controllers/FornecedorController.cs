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
        /*================================================================================================================================================================================*/

        /*==============================================================================CADASTRO FUNCIONARIO==============================================================================*/
        public ActionResult CadastroFuncionario()
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
            return RedirectToAction("CadastrarFuncionario");
        }


        /*================================================================================================================================================================================*/

        /*==============================================================================CADASTRO FORNECEDOR===============================================================================*/
        public ActionResult CadastroFornecedor()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult CadastroFornecedor(string cnpj, string nome_empresa, string email, string telefone, string celular, string endereco, string bairro, 
            string cidade, string uf, string cep, string senha, string slogan, string descricao, /*string plano, */string nome_categoria, string confirmarSenha)
        {

            Fornecedor f = new Fornecedor();

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
                else
                {
                    f.Imagem = new byte[] { };
                }
            }

            TempData["Msg"] = f.CadastroFornecedor();
            return RedirectToAction("UpdateFornecedor");
        }

        /*================================================================================================================================================================================*/

        /*=============================================================================EXCLUIR FUNCIONARIO================================================================================*/
        [HttpPost]
        public ActionResult ExcluirFuncionario(string nome, int codigo, string nomeDigitado, int codigoDigitado)
        {
        
            Fornecedor f = new Fornecedor();
            f.Nome = nome;
            f.Codigo = codigo;

            f.ExcluirFuncionario(nome, codigo, nomeDigitado, codigoDigitado);

            return RedirectToAction("Listar");

        }
        /*================================================================================================================================================================================*/

        /*==============================================================================ENVIO DE EMAIL====================================================================================*/
        public ActionResult EnviarEmail()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult EnviarEmail(string cnpj)
        {
            Fornecedor enviaEmail = new Fornecedor();

            enviaEmail.RestaurarSenha(cnpj); 


            return RedirectToAction("EnviarEmail");
        }

        /*================================================================================================================================================================================*/

        /*==============================================================================PESQUISA FUNCIONARIO==============================================================================*/
        public ActionResult PesquisaFuncionario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PesquisarFuncionario(string pesquisa)
        {
            List<Fornecedor> u = new List<Fornecedor>();
            if (pesquisa == null || pesquisa == "")
            {
                TempData["Msg"] = "Digite LOGIN ou NOME do funcionário.";
                return View(u);
            }
            else
            {
                u = Fornecedor.PesquisaFornecedor(pesquisa);
                if (u.Count == 0)
                    TempData["Msg"] = "Erro ao encontrar Fornecedor";
                return View(u);
            }

        }

        /*================================================================================================================================================================================*/

        /*==============================================================================UPDATE DE SENHA===================================================================================*/
        public ActionResult UpdateSenha()
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
                return RedirectToAction("RestaurarSenha");

               
                return View("RestaurarSenha");
        }

        public ActionResult RestaurarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RestaurarSenha(string senha, string novaSenha, string senhaConfirma, string cnpj)
        {
            Fornecedor senhaRe = new Fornecedor();

            senhaRe.Senha = senha;



            bool res = senhaRe.UpdateSenha(senha, novaSenha, senhaConfirma, cnpj);

            if (res)  //RETORNAR NA VIEW DE RESTAURAR DE SENHA
                return RedirectToAction("RestaurarSenha");

            return View("RestaurarSenha");
        }

        /*================================================================================================================================================================================*/

        /*==============================================================================UPDATE DE CADASTRO================================================================================*/
        public ActionResult UpdateFornecedor(string cnpj)
        {
            
            Fornecedor c = Fornecedor.PesquisaUpdateFornecedor(/*cnpj*/"45.997.418/0001-53");

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


        public ActionResult UpdateFuncionarioFornecedor(string codigo)
        {
            return View(Fornecedor.PerfilFuncionario(int.Parse(codigo))); //PASSAR O codgio PARA RETORNAR O PERFIL PARA PODER EDITAR
        }


        [HttpPost]
        public ActionResult UpdateFuncionarioFornecedor(string nome, string senha)
        {
            Fornecedor f = new Fornecedor();
            f.Nome = nome;
            f.Senha = senha;
      

            

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


            TempData["Msg"] = f.UpdateFuncionarioFornecedor();

            return RedirectToAction("UpdateFornecedor");
        }






    

      





        public ActionResult Homepage()
        {
            return View();
        }





    }

}