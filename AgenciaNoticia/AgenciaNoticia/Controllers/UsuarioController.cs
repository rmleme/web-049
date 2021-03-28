using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AgenciaNoticia.Models;

namespace AgenciaNoticia.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioRepository usuarioRepository = new UsuarioRepository();

        //
        // M�todo respons�vel por retornar a view com a consulta de todos usu�rios
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            var usuarios = usuarioRepository.FindUpUsuarios().ToList();
            return View("Index", usuarios);
        }

        //
        // M�todo respons�vel por retornar a view com a consulta de um usu�rio espec�fico
        public ActionResult Details(int id)
        {
            Usuario usuario = usuarioRepository.GetUsuario(id);
            if (usuario == null)
                return View("NotFound");
            else
                return View("Details", usuario);
        }

        //
        // M�todo respons�vel por retornar a view para criar um usu�rio
        public ActionResult Create()
        {
            Usuario usuario = new Usuario();
            return View("Create", usuario);
        }

        //
        // POST: /Usuario/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    usuarioRepository.Add(usuario);
                    usuarioRepository.Save();

                    AccountController login = new AccountController();
                    login.MembershipService.CreateUser(usuario.Nome, usuario.Senha, usuario.Email);

                    return RedirectToAction("Details", new { id = usuario.UsuarioID });
                }
                catch
                {
                    foreach (var issue in usuario.GetRuleViolations())
                    {
                        ModelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
                    }
                }
            }

            return View("Create", usuario);
        }

        //
        // M�todo respons�vel por retornar a view para alterar um usu�rio
        public ActionResult Edit(int id)
        {
            Usuario usuario = usuarioRepository.GetUsuario(id);
            return View("Edit", usuario);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            Usuario usuario = usuarioRepository.GetUsuario(id);
            try
            {
                UpdateModel(usuario);
                usuarioRepository.Save();
                return RedirectToAction("Details", new { id = usuario.UsuarioID });
            }
            catch
            {
                foreach (var issue in usuario.GetRuleViolations())
                {
                    ModelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
                }

                return View("Edit", usuario);
            }
        }

        //
        // M�todo respons�vel por retornar a view para excluir um usu�rio
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int id)
        {
            //encontra o usu�rio no reposit�rio conforme id
            Usuario usuario = usuarioRepository.GetUsuario(id);

            if (usuario == null)
                return View("NotFound");
            else
                return View(usuario);
        }

        [Authorize(Roles = "Administrador")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id, string confirmButton)
        {
            Usuario usuario = usuarioRepository.GetUsuario(id);

            if (usuario == null)
                return View("NotFound");

            //deleta usu�rio ap�s confirma��o
            usuarioRepository.Delete(usuario);
            usuarioRepository.Save();

            return View("Deleted");
        }
    }
}