using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgenciaNoticia.Models
{
    public class RelatorioRepository
    {
        private AgenciaNoticiaDataContext db = new AgenciaNoticiaDataContext();

        //
        // Query Methods
        public IQueryable<Relatorio> FindUpRelatorios()
        {
            return from relatorio in db.Relatorios
                   orderby relatorio.DataRelatorio descending
                   select relatorio;
        }

        public Relatorio GetRelatorio(DateTime data)
        {
            DateTime dataRelatorio = new DateTime(data.Year, data.Month, data.Day);
            return db.Relatorios.SingleOrDefault(d => d.DataRelatorio == dataRelatorio);
        }

        //
        // Insert/Delete Methods
        public void Add(Relatorio relatorio)
        {
            db.Relatorios.InsertOnSubmit(relatorio);
        }

        public void Delete(Relatorio relatorio)
        {
            db.Relatorios.DeleteOnSubmit(relatorio);
        }

        //
        // Persistence 
        public void Save()
        {
            db.SubmitChanges();
        }
    }
}