using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AgenciaNoticia.Models;

namespace AgenciaNoticia.Controllers
{
    public class RelatorioController : Controller
    {
        RelatorioRepository relatorioRepository = new RelatorioRepository();

        //método responsável por retornar a view que permite escolher o relatório a visualizar
        public ActionResult Select()
        {
            return View("Select");
        }

        //método responsável por retornar a view com o relatório contábil
        [Authorize(Roles = "Gestor, Administrador")]
        public ActionResult IndexFinance()
        {
            var relatorios = relatorioRepository.FindUpRelatorios().ToList();
            return View("IndexFinance", relatorios);
        }

        //método responsável por retornar a view com o relatório de notícias compradas/visualizadas
        [Authorize(Roles = "Comprador, Jornalista")]
        public ActionResult IndexNewsBought()
        {
            var relatorios = relatorioRepository.FindUpRelatorios().ToList();
            return View("IndexNewsBought", relatorios);
        }

        //método responsável por retornar a view com o relatório de ranking de notícias
        [Authorize(Roles = "Comprador, Jornalista")]
        public ActionResult IndexNewsRank()
        {
            NoticiaRepository noticiaRepository = new NoticiaRepository();
            var noticias = noticiaRepository.FindAllNoticiasByRank().ToList();
            return View("IndexNewsRank", noticias);
        }

        //
        // GET: /Relatorio/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Relatorio/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Relatorio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Relatorio/Edit/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}