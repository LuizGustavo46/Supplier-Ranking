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
            return View();
        }

        [HttpPost]
        public ActionResult Pesquisa(string pesquisa)
        {
            List<Fornecedor> lista = new List<Fornecedor>();

            if(lista == null)
                lista = HomeLogada.PesquisaFornecedor(pesquisa);
            else if (lista == null)
                lista = HomeLogada.RankingCategoria(pesquisa);
            else if (lista == null)
                lista = HomeLogada.RankingFiltro(pesquisa);

            return View(lista);
        }


        public ActionResult Perfil()

        {
            return View();
        }


        public ActionResult RankingGeral()
        {
            return View("RankingGeral", HomeLogada.RankingGeral());
        }


        public ActionResult RankingPremium()
        {
            return View("RankingPremium", HomeLogada.RankingPremium());
        }


    }
}