using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupplierRanking.Models;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SupplierRanking.Controllers
{
    public class HomeLogadaController : Controller
    {
        // GET: HomeLogada
        public ActionResult Pesquisa()
        {
            if (Session["UserFornecedor"] == null && Session["UserPessoaFisica"] == null && Session["UserPessoaJuridica"] == null && Session["UserFuncionario"] == null)
                return RedirectToAction("Index", "Home");
            else
                return View("Pesquisa", HomeLogada.RankingGeral());
        }

        [HttpPost]
        public ActionResult Pesquisa(string pesquisa)
        {
            List<Fornecedor> lista = new List<Fornecedor>();

            if (lista.Count == 0)
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
            if (Session["UserFornecedor"] == null && Session["UserPessoaFisica"] == null && Session["UserPessoaJuridica"] == null && Session["UserFuncionario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ListaFotos = h.GaleriaFotos(cnpj);
                ViewBag.Comentarios = h.Comentarios(cnpj);
                return View("Perfil", HomeLogada.Perfil(cnpj));
            }
        }


        public ActionResult RankingGeral()
        {
            if(Session["UserFornecedor"] == null && Session["UserPessoaFisica"] == null && Session["UserPessoaJuridica"] == null && Session["UserFuncionario"] == null)
                return RedirectToAction("Index","Home");
            else
                return View("RankingGeral", HomeLogada.RankingGeral());
        }
    

        public ActionResult RankingPremium()
        {
            if (Session["UserFornecedor"] == null && Session["UserPessoaFisica"] == null && Session["UserPessoaJuridica"] == null && Session["UserFuncionario"] == null)
                return RedirectToAction("Index", "Home");
            else
                return View("RankingPremium", HomeLogada.RankingPremium());
        }

        public ActionResult RankingInteresses()
        {
            if (Session["UserPessoaFisica"] == null && Session["UserPessoaJuridica"] == null)
                return RedirectToAction("Index", "Home");
            else

                return View("RankingInteresses", HomeLogada.RankingInteresses(int.Parse(Session["CodigoUsuario"].ToString())));
        }

        public ActionResult Suporte()
        {
            if (Session["UserFornecedor"] == null && Session["UserPessoaFisica"] == null && Session["UserPessoaJuridica"] == null && Session["UserFuncionario"] == null)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }

        public string CarregaFoto()
        {
            try
            {
                Fornecedor usuario = HomeLogada.Perfil(Session["UserFornecedor"].ToString());

                if (usuario.Imagem != null)
                {
                    Bitmap imagem = new Bitmap(Image.FromStream(new MemoryStream(usuario.Imagem)));
                    int dimensaoMaior = Math.Max(imagem.Width, imagem.Height);
                    Bitmap imagemRedimencionada = ResizeImage(imagem, dimensaoMaior, dimensaoMaior); // Redimensionamento da imagem para ser sempre quadrada, para não sair toda cagada no layout da tela

                    MemoryStream ms = new MemoryStream();
                    imagemRedimencionada.Save(ms, ImageFormat.Png);

                    string foto = Convert.ToBase64String(ms.ToArray());
                    return string.Format("data:image/jpeg;base64, {0}", foto);
                }
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
                return destImage;
            }
        }
    }
}





