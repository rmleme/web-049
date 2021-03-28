using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AgenciaNoticia.Models;

namespace AgenciaNoticia.Controllers
{
    public class PedidoController : Controller
    {
        PedidoRepository pedidoRepository = new PedidoRepository();

        //
        // GET: /Pedido/
        [Authorize(Roles = "Comprador")]
        public ActionResult Index()
        {
            return View();
        }

        //
        // Método responsável por retornar a view com os pedidos daquele usuário
        [Authorize(Roles = "Comprador")]
        public ActionResult Details()
        {
            if (Session["Pedido"] == null)
                Session["Pedido"] = new Pedido();
            Pedido pedido = (Pedido) Session["Pedido"];
            return View("Details", pedido);
        }

        //
        // Método responsável por remover a notícia do pedido
        [Authorize(Roles = "Comprador")]
        public ActionResult Remover(int id)
        {
            if (Session["Pedido"] == null)
                Session["Pedido"] = new Pedido();
            Pedido pedido = (Pedido) Session["Pedido"];
            pedido.RemoverNoticia(id);
            return View("Details", pedido);
        }

        //
        // Método responsável por cancelar a compra
        [Authorize(Roles = "Comprador")]
        public ActionResult Cancelar()
        {
            if (Session["Pedido"] == null)
                Session["Pedido"] = new Pedido();
            Pedido pedido = (Pedido) Session["Pedido"];
            pedido.CancelarCompra();
            return RedirectToAction("Index", "Noticia");
        }

        //
        // Método responsável por finalizar a compra
        [Authorize(Roles = "Comprador")]
        public ActionResult Finalizar()
        {
            if (Session["Pedido"] == null)
                Session["Pedido"] = new Pedido();
            Pedido pedido = (Pedido) Session["Pedido"];
            return View("Commit", pedido);
        }

        //
        // Método responsável por efetuar o pagamento e liberar as notícias, como XML
        [Authorize(Roles = "Comprador")]
        public ActionResult PagarXml()
        {
            if (Session["Pedido"] == null)
                Session["Pedido"] = new Pedido();
            Pedido pedido = (Pedido) Session["Pedido"];

            //pedidoRepository.Add(pedido);
            //pedidoRepository.Save();

            this.atualizarEstatisticas(pedido.Noticias);

            ContentResult result = new ContentResult();
            result.ContentType = "text/xml";
            result.ContentEncoding = new UTF8Encoding();
            result.Content = pedido.serialize();

            Session.Remove("Pedido");

            return result;
        }

        //
        // Método responsável por efetuar o pagamento e liberar as notícias, como HTML
        [Authorize(Roles = "Comprador")]
        public ActionResult PagarHtml()
        {
            if (Session["Pedido"] == null)
                Session["Pedido"] = new Pedido();
            Pedido pedido = (Pedido) Session["Pedido"];

            //pedidoRepository.Add(pedido);
            //pedidoRepository.Save();

            this.atualizarEstatisticas(pedido.Noticias);

            Session.Remove("Pedido");

            return View("Generated", pedido);
        }

        public void atualizarEstatisticas(EntitySet<Noticia> noticias)
        {
            if (noticias == null || noticias.Count == 0)
                return;

            DateTime hoje = DateTime.Now;
            RelatorioRepository relatorioRepository = new RelatorioRepository();
            Relatorio relatorio = relatorioRepository.GetRelatorio(DateTime.Now);
            if (relatorio == null)
            {
                relatorio = new Relatorio();
                relatorio.DataRelatorio = new DateTime(hoje.Year, hoje.Month, hoje.Day);
                relatorio.TotalDeNoticiasAcessadas = 0;
                relatorio.NumeroNoticiasVendidas = 0;
                relatorio.TotalDeVendas = 0;
                relatorioRepository.Add(relatorio);
            }

            foreach (Noticia noticia in noticias)
            {
                noticia.NumeroDeVendas++;
                relatorio.NumeroNoticiasVendidas++;
                relatorio.TotalDeVendas += noticia.Preco;
            }

            relatorioRepository.Save();
        }
    }
}
