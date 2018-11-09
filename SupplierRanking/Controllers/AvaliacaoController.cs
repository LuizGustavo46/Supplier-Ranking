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
        /************************************************************ CADASTRAR AVALIAÇÃO ******************************************************/

        public ActionResult CadastrarAvaliacao()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarAvaliacao(int qualidade, int atendimento, int entrega, int preco, int satisfacao, 
            string comentario/*, string data_avaliacao*/, string cnpj_fornecedor, int codigo_comprador)
        {
            Avaliacao av = new Avaliacao();

            av.Qualidade = qualidade;
            av.Atendimento = atendimento;
            av.Entrega = entrega;
            av.Preco = preco;
            av.Satisfacao = satisfacao;
            av.Comentario = comentario;
            //av.Data_avaliacao = data_avaliacao;
            av.Cnpj_fornecedor = cnpj_fornecedor;
            av.Codigo_comprador = codigo_comprador;

            if (av.CadastrarAvaliacao())
            {
                ViewBag.Message = "";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Deseja atualizar seu comentário ?";
                return RedirectToAction("UpdateComentario");
            }


            return RedirectToAction("CadastrarAvaliacao");
        }
    
        /************************************************************ UPDATE AVALIAÇÃO ******************************************************/

        public ActionResult UpdateComentario(string cnpj_fornecedor, int codigo_comprador)
        {
            Avaliacao a = Avaliacao.ReturnUpdateComentario(cnpj_fornecedor, codigo_comprador);
            if (a == null)
            {
                TempData["Msg"] = "Avaliação não encontrada.";
                return RedirectToAction("RankingGeral"); // VERIFICAR A VIEW QUE VAI RETORNAR 
            }
            return View(a);
        }

        [HttpPost]
        public ActionResult UpdateComentario(string comentario, string cnpj_fornecedor, int codigo_comprador)
        {
            Avaliacao avUP = new Avaliacao();

            avUP.Comentario = comentario;
            avUP.Cnpj_fornecedor = cnpj_fornecedor;
            avUP.Codigo_comprador = codigo_comprador;

            if (avUP.UpdateComentario()) //SE CONSEGUIR ATUALIZAR
            {
                TempData["Msg"] = "Comentário atualizado.";
            }
            else //SENÃO CONSEGUIR
            {
                TempData["Msg"] = "Comentário não apropriado.";
                return RedirectToAction("UpdateAvaliacao");
            }

            return View("UpdateAvaliacao");
        }

        /*********************************************************** RANKING POR CATEGORIA *****************************************************/

        public ActionResult RankingLista(string categoria)
        {
            return View("RankingLista", Avaliacao.RankingLista(/*categoria*/"pesca"));
        }

        /*************************************************************** RANING GERAL *********************************************************/

        public ActionResult RankingGeral()
        {
            return View("RankingGeral", Avaliacao.RankingGeral());
        }

        /*************************************************************** FINAL INDEX ***********************************************************/
        // GET: Avaliacao
        public ActionResult Index()
        {
            return View();
        }
    }
}