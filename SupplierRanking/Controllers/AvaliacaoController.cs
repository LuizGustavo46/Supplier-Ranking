using SupplierRanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupplierRanking.Controllers
{
    public class AvaliacaoController : Controller
    {


        /*==============================================================================Cadastrar avaliação=================================================================================*/
        public ActionResult CadastrarAvaliacao()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CadastrarAvaliacao(int qualidade, int atendimento, int entrega, int preco, int satisfacao, String comentario, String data_avaliacao, String cnpj_fornecedor, int codigo_comprador)
        {
            Avaliacao av = new Avaliacao();

            av.Qualidade = qualidade;
            av.Atendimento = atendimento;
            av.Entrega = entrega;
            av.Preco = preco;
            av.Satisfacao = satisfacao;
            av.Comentario = comentario;
            av.Data_avaliacao = data_avaliacao;
            av.Cnpj_fornecedor = cnpj_fornecedor;
            av.Codigo_comprador = codigo_comprador;


            return RedirectToAction("CadastrarAvaliacao");
        }

        /*================================================================================================================================================================================*/

        /*==============================================================================Update avaliação==================================================================================*/

        public ActionResult UpdateAvaliacao()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateAvaliacao(string comentario, string cnpj_fornecedor, int codigo_comprador)
        {
            Avaliacao avUP = new Avaliacao();

            avUP.Comentario = comentario;
            avUP.Cnpj_fornecedor = cnpj_fornecedor;
            avUP.Codigo_comprador = codigo_comprador;

            return View("Avaliacao");
        }
        /*================================================================================================================================================================================*/

        /*==============================================================================Listar============================================================================================*/

        //TALVEZ NAO SEJA NECESSARIO ESTE MÉTODO NO CONTROLER
            public ActionResult RankingGeral()
        {
            return View();
        }

        /*[HttpPost]
        public ActionResult RankingGeral(string categoria)
        {   
                                
        }*/










        // GET: Avaliacao
        public ActionResult Index()
        {
            return View();
        }
    }
}