using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupplierRanking.Models;

namespace SupplierRanking.Controllers
{
    public class HomeLogadaController : Controller
    {
        // GET: HomeLogada
        public ActionResult Pesquisa()
        {
            return View("Pesquisa", HomeLogada.RankingGeral());
        }

        [HttpPost]
        public ActionResult Pesquisa(string pesquisa)
        {
            List<Fornecedor> lista = new List<Fornecedor>();

            if(lista.Count == 0)
                lista = HomeLogada.PesquisaFornecedor(pesquisa);
            if (lista.Count == 0)
                lista = HomeLogada.RankingCategoria(pesquisa);
            if (lista.Count == 0)
                lista = HomeLogada.RankingFiltro(pesquisa);

            return View(lista);
        }


        public ActionResult Perfil(string cnpj)
        {
            HomeLogada h = new HomeLogada();
            ViewBag.Comentarios = h.Comentarios(cnpj);
            return View("Perfil", HomeLogada.Perfil(cnpj));
        }


        public ActionResult RankingGeral()
        {
            return View("RankingGeral", HomeLogada.RankingGeral());
        }


        public ActionResult RankingPremium()
        {
            return View("RankingPremium", HomeLogada.RankingPremium());
        }

        public ActionResult Suporte()
        {
            return View();
        }


    }
}