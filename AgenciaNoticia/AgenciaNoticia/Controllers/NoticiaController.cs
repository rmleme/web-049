using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AgenciaNoticia.Models;
using System.IO;
using System.Data.SqlClient;

namespace AgenciaNoticia.Controllers
{
    public class NoticiaController : Controller
    {
        NoticiaRepository noticiaRepository = new NoticiaRepository();

        //
        // Método responsável por retornar a view com a consulta de todas as notícias
        public ActionResult Index()
        {
            var noticias = noticiaRepository.FindAllNoticias().ToList();

            // Caso o usuário seja um comprador, remove do result set as notícias já existentes na sessão
            if (User.IsInRole("Comprador") && Session["Pedido"] != null)
            {
                Pedido pedido = (Pedido) Session["Pedido"];
                foreach (Noticia n in pedido.Noticias)
                    if (noticias.Contains(n))
                        noticias.Remove(n);
            }

            return View("Index", noticias);
        }

        //
        // Método responsável por retornar a view que permite comprar uma notícia
        [Authorize(Roles = "Comprador")]
        public ActionResult Comprar(int id)
        {
            Noticia noticia = noticiaRepository.GetNoticia(id);
            if (noticia == null)
                return View("NotFound");
            else
            {
                this.atualizarEstatisticas(noticia);

                var noticias = noticiaRepository.FindAllNoticias().ToList();

                if (Session["Pedido"] == null)
                    Session["Pedido"] = new Pedido();
                Pedido pedido = (Pedido) Session["Pedido"];
                pedido.Noticias.Add(noticia);

                // Remove do result set as notícias já existentes na sessão daquele usuário
                foreach (Noticia n in pedido.Noticias)
                    if (noticias.Contains(n))
                        noticias.Remove(n);

                return View("Index", noticias);
            }
        }

        //
        // Método responsável por retornar a view com detalhes de uma notícia inserida 
        public ActionResult Details(int id)
        {
            Noticia noticia = noticiaRepository.GetNoticia(id);
            if (noticia == null)
                return View("NotFound");
            else
                return View("Details", noticia);
        }

        //
        // Método responsável por retornar a view para inserir uma notícia
        [Authorize(Roles = "Jornalista")]
        public ActionResult Create()
        {
            Noticia noticia = new Noticia();
            return View("Create", noticia);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Noticia noticia, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                        string categoria = noticia.Categoria;
                        double preco = (double)noticia.Preco;
                        string vigencia = noticia.Vigencia;
                        string texto = noticia.Texto;

                        if (image != null)
                        {

                            foreach (string upload in Request.Files)
                            {
                                string mimeType = Request.Files[upload].ContentType;
                                Stream fileStream = Request.Files[upload].InputStream;
                                string fileName = Path.GetFileName(Request.Files[upload].FileName);
                                int fileLength = Request.Files[upload].ContentLength;
                                byte[] fileData = new byte[fileLength];
                                fileStream.Read(fileData, 0, fileLength);
                                noticia.MimeType = mimeType;
                                noticia.FileName = fileName;
                                noticia.Image = fileData;
                            }
                        }
                        noticiaRepository.Add(noticia);
                        noticiaRepository.Save();
                        return RedirectToAction("Details", new { id = noticia.NoticiaID });
                    
                }
                catch
                {
                    foreach (var issue in noticia.GetRuleViolations())
                    {
                        ModelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
                    }

                }
            }

            return View("Create", noticia);
        }


        public FileContentResult GetImage(int id)
        {
            SqlDataReader rdr; byte[] fileContent = null;
            string mimeType = ""; string fileName = "";
            const string connect = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\AgenciaNoticia.mdf;Integrated Security=True;User Instance=True";

            using (var conn = new SqlConnection(connect))
            {
                var qry = "SELECT Image, MimeType, FileName FROM Noticia WHERE NoticiaID = @ID";
                var cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    fileContent = (byte[])rdr["Image"];
                    mimeType = rdr["MimeType"].ToString();
                    fileName = rdr["FileName"].ToString();
                }
            }
            return File(fileContent, mimeType, fileName);
        }

        //
        // GET: /Noticia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Noticia/Edit/5
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

        public void atualizarEstatisticas(Noticia noticia)
        {
            if (noticia == null)
                return;

            DateTime hoje = DateTime.Now;
            RelatorioRepository relatorioRepository = new RelatorioRepository();
            Relatorio relatorio = relatorioRepository.GetRelatorio(hoje);
            if (relatorio == null)
            {
                relatorio = new Relatorio();
                relatorio.DataRelatorio = new DateTime(hoje.Year, hoje.Month, hoje.Day);
                relatorio.TotalDeNoticiasAcessadas = 0;
                relatorio.NumeroNoticiasVendidas = 0;
                relatorio.TotalDeVendas = 0;
                relatorioRepository.Add(relatorio);
            }

            noticia.NumeroDeAcessos++;
            noticiaRepository.Save();

            relatorio.TotalDeNoticiasAcessadas++;
            relatorioRepository.Save();
        }
    }
}