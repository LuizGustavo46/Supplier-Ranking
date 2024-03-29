﻿using SupplierRanking.Models;
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

        //public ActionResult CadastrarAvaliacao(string cnpj_fornecedor)
        //{

        //    Avaliacao a = new Avaliacao();
        //    a.Cnpj_fornecedor = cnpj_fornecedor;
        //    a.Codigo_comprador = int.Parse(Session["CodigoUsuario"].ToString());

        //    if (a.VerificarSeteDias())
        //        return View();
        //    else
        //        return RedirectToAction("UpdateComentario");
        //}

        [HttpPost]
        public ActionResult CadastrarAvaliacao(int qualidade, int atendimento, int entrega, int preco, int satisfacao, 
            string comentario/*, string data_avaliacao*/, string cnpj_fornecedor)
        {
            Avaliacao av = new Avaliacao();

            av.Cnpj_fornecedor = cnpj_fornecedor;
            av.Codigo_comprador = int.Parse(Session["CodigoUsuario"].ToString());

            if (av.VerificarSeteDias())
            {
                av.Qualidade = qualidade;
                av.Atendimento = atendimento;
                av.Entrega = entrega;
                av.Preco = preco;
                av.Satisfacao = satisfacao;
                av.Comentario = comentario;
                //av.Data_avaliacao = data_avaliacao;
                av.Cnpj_fornecedor = /*Session["PassarCnpj"].ToString();*/ cnpj_fornecedor;
                av.Codigo_comprador = int.Parse(Session["CodigoUsuario"].ToString());

                if (av.CadastrarAvaliacao())
                {
                    ViewBag.Message = "Avaliação realizada!";
                    return RedirectToAction("RankingGeral", "HomeLogada");
                }
                else
                {
                    ViewBag.Message = "Deseja atualizar seu comentário?";
                    return RedirectToAction("UpdateComentario");
                }
            }
            else
            {
                Session["PassarCnpj"] = cnpj_fornecedor;
                return RedirectToAction("UpdateComentario");
            }
        }
    
        /************************************************************ UPDATE AVALIAÇÃO ******************************************************/

        public ActionResult UpdateComentario()
        {
            string cnpj_fornecedor = Session["PassarCnpj"].ToString();
            int codigo_comprador = int.Parse(Session["CodigoUsuario"].ToString());
            Avaliacao a = Avaliacao.ReturnUpdateComentario(cnpj_fornecedor, codigo_comprador);
            if (a == null)
            {
                ViewBag.Message = "Avaliação não encontrada.";
                return RedirectToAction("RankingGeral", "HomeLogada"); // VERIFICAR A VIEW QUE VAI RETORNAR 
            }
            return View(a);
        }

        [HttpPost]
        public ActionResult UpdateComentario(string comentario)
        {
            Avaliacao avUP = new Avaliacao();

            avUP.Comentario = comentario;
            avUP.Cnpj_fornecedor = Session["PassarCnpj"].ToString(); ;
            avUP.Codigo_comprador = int.Parse(Session["CodigoUsuario"].ToString());

            if (avUP.UpdateComentario()) //SE CONSEGUIR ATUALIZAR
            {
                ViewBag.Message = "Comentário atualizado.";
                return RedirectToAction("RankingGeral", "HomeLogada");
            }
            else //SENÃO CONSEGUIR
            {
                ViewBag.Message = "Comentário não apropriado.";
                return RedirectToAction("UpdateComentario");
            }
        }


        // GET: Avaliacao
        public ActionResult Index()
        {
            return View();
        }
    }
}