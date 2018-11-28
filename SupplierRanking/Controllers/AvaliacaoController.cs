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

        public ActionResult CadastrarAvaliacao(/*string cnpj_fornecedor, int codigo_comprador*/)
        {

            Avaliacao a = new Avaliacao();
            a.Cnpj_fornecedor = /*cnpj_fornecedor*/"45.997.418/0001-53";
            a.Codigo_comprador = /*codigo_comprador*/1;

            if (a.VerificarSeteDias())
                return View();
            else
                return RedirectToAction("UpdateComentario");
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

        public ActionResult UpdateComentario(/*string cnpj_fornecedor, int codigo_comprador*/)
        {
            Avaliacao a = Avaliacao.ReturnUpdateComentario(/*cnpj_fornecedor, codigo_comprador*/"45.997.418/0001-53", 2);
            if (a == null)
            {
                ViewBag.Message = "Avaliação não encontrada.";
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
                ViewBag.Message = "Comentário atualizado.";
                return RedirectToAction("UpdateComentario");
            }
            else //SENÃO CONSEGUIR
            {
                ViewBag.Message = "Comentário não apropriado.";
                return RedirectToAction("UpdateComentario");
            }

            return View("UpdateComentario");
        }


        // GET: Avaliacao
        public ActionResult Index()
        {
            return View();
        }
    }
}