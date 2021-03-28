using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgenciaNoticia.Models
{
    public class PedidoRepository
    {
        private AgenciaNoticiaDataContext db = new AgenciaNoticiaDataContext();

        //
        // Query Methods
        public IQueryable<Pedido> FindAllPedidos()
        {
            return db.Pedidos;
        }

        public Pedido GetPedido(int id)
        {
            return db.Pedidos.SingleOrDefault(d => d.PedidoId == id);
        }

        //
        // Insert/Delete Methods
        public void Add(Pedido pedido)
        {
            db.Pedidos.InsertOnSubmit(pedido);
        }

        public void Delete(Pedido pedido)
        {
            db.Pedidos.DeleteOnSubmit(pedido);
        }

        //
        // Persistence 
        public void Save()
        {
            db.SubmitChanges();
        }
    }
}