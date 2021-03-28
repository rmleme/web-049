using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgenciaNoticia.Models
{
    public class UsuarioRepository
    {
        private AgenciaNoticiaDataContext db = new AgenciaNoticiaDataContext();

        //
        // Query Methods
        public IQueryable<Usuario> FindAllUsers()
        {
            return db.Usuarios;
        }

        public IQueryable<Usuario> FindUpUsuarios()
        {
            return from usuario in db.Usuarios                   
                   orderby usuario.UsuarioID
                   select usuario;
        }

        public Usuario GetUsuario(int id)
        {
            return db.Usuarios.SingleOrDefault(d => d.UsuarioID == id);
        }

        //
        // Insert/Delete Methods
        public void Add(Usuario usuario)
        {
            db.Usuarios.InsertOnSubmit(usuario);
        }

        public void Delete(Usuario usuario)
        {           
            db.Usuarios.DeleteOnSubmit(usuario);
        }

        //
        // Persistence 
        public void Save()
        {
            db.SubmitChanges();
        }
    }
}
