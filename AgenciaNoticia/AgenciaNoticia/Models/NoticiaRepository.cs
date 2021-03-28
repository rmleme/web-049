using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgenciaNoticia.Models
{
    public class NoticiaRepository
    {
        private AgenciaNoticiaDataContext db = new AgenciaNoticiaDataContext();

        //
        // Query Methods

        public IQueryable<Noticia> FindAllNoticias()
        {
            return from noticia in db.Noticias
                   orderby noticia.Vigencia.Substring(6, 4) + noticia.Vigencia.Substring(3, 2) + noticia.Vigencia.Substring(0, 2)
                   select noticia;
        }

        public IQueryable<Noticia> FindAllNoticiasByRank()
        {
            return from noticia in db.Noticias
                   orderby noticia.NumeroDeAcessos
                   select noticia;
        }

        public Noticia GetNoticia(int id)
        {
            return db.Noticias.SingleOrDefault(d => d.NoticiaID == id);
        }
        //
        // Insert/Delete Methods

        public void Add(Noticia noticia)
        {
            db.Noticias.InsertOnSubmit(noticia);
        }

        //
        // Persistence 

        public void Save()
        {
            db.SubmitChanges();
        }
    }
}