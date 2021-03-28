using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using AgenciaNoticia.Models;

namespace AgenciaNoticia.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.
        public AccountController()
            : this(null, null)
        {
        }

        // This constructor is not used by the MVC framework but is instead provided for ease
        // of unit testing this type. See the comments at the end of this file for more
        // information.
        public AccountController(IFormsAuthentication formsAuth, IMembershipService service)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationService();
            MembershipService = service ?? new AccountMembershipService();
        }

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }

        public IMembershipService MembershipService
        {
            get;
            private set;
        }

        public ActionResult LogOn()
        {

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult LogOn(string userName, string password, bool rememberMe, string returnUrl, string perfil, string email)
        {
            if (!ValidateLogOn(userName, password, perfil, email))
            {
                return View();
            }

            FormsAuth.SignIn(userName, rememberMe);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LogOff()
        {
            Session.Remove("Pedido");
            FormsAuth.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(string userName, string email, string password, string confirmPassword, string perfil, string rg, string cpf)
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (ValidateRegistration(userName, email, password, confirmPassword, perfil, rg, cpf))
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(userName, password, email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuth.SignIn(userName, false /* createPersistentCookie */);

                    if (String.IsNullOrEmpty(perfil))
                    {
                        ModelState.AddModelError("perfil", "Você precisa especificar um perfil.");
                    }
                    if (!perfil.ToLower().Equals("administrador") & !perfil.ToLower().Equals("jornalista") & !perfil.ToLower().Equals("comprador") & !perfil.ToLower().Equals("gestor"))
                    {
                        if (!String.IsNullOrEmpty(perfil))
                        {
                            ModelState.AddModelError("perfil", "Você precisa especificar um perfil válido (Administrador, Gestor, Jornalista ou Comprador).");
                        }
                    }
                    else
                    {
                        UsuarioRepository usuarioRepository = new UsuarioRepository();
                        Usuario usuario = new Usuario();
                        usuario.Nome = userName;
                        usuario.Perfil = perfil;
                        usuario.Senha = password;
                        usuario.Cpf = cpf;
                        usuario.Rg = rg;
                        usuario.Email = email;
                        usuarioRepository.Add(usuario);
                        usuarioRepository.Save();
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "A senha não foi trocada devido a exceções.")]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (!ValidateChangePassword(currentPassword, newPassword, confirmPassword))
            {
                return View();
            }

            try
            {
                if (MembershipService.ChangePassword(User.Identity.Name, currentPassword, newPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                    return View();
                }
            }
            catch
            {
                ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                return View();
            }
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication não é suportada.");
            }
        }

        #region Validation Methods

        private bool ValidateChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (String.IsNullOrEmpty(currentPassword))
            {
                ModelState.AddModelError("currentPassword", "Você precisa especificar a senha atual.");
            }
            if (newPassword == null || newPassword.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("newPassword",
                    String.Format(CultureInfo.CurrentCulture,
                         "Você precisa especificar uma senha de {0} caracteres ou mais.",
                         MembershipService.MinPasswordLength));
            }

            if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "A nova senha e a confirmação de senha não conferem.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateLogOn(string userName, string password, string perfil, string email)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "Você precisa especificar um nome de usuário.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "Você precisa especificar uma senha.");
            }

            if (!MembershipService.ValidateUser(userName, password))
            {
                ModelState.AddModelError("_FORM", "O usuário ou senha fornecidos estão incorretos.");
            }


            return ModelState.IsValid;
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword, string perfil, string rg, string cpf)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "Você precisa especificar um nome de usuário.");
            }
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "Você precisa especificar um e-mail.");
            }
            if ((!email.Contains("@")) || (!email.Contains(".")) || (email.IndexOf("@") > email.LastIndexOf(".")) || (email.StartsWith(".")) || (email.StartsWith("@")) || (email.EndsWith(".")) || (email.EndsWith("@")))
            {
                ModelState.AddModelError("email", "Você precisa especificar um e-mail válido.");
            }

            if (String.IsNullOrEmpty(perfil))
            {
                ModelState.AddModelError("perfil", "Você precisa especificar um perfil.");
            }
            if (!perfil.ToLower().Equals("administrador") & !perfil.ToLower().Equals("jornalista") & !perfil.ToLower().Equals("comprador") & !perfil.ToLower().Equals("gestor"))
            {
                ModelState.AddModelError("perfil", "Você precisa especificar um perfil válido (administrador, gestor, jornalista ou comprador).");
            }


            if (String.IsNullOrEmpty(cpf))
            {
                ModelState.AddModelError("cpf", "CPF requerido");
            }

            if ((!cpf.Contains("0")) & (!cpf.Contains("1")) & (!cpf.Contains("2")) & (!cpf.Contains("3")) & (!cpf.Contains("4")) & (!cpf.Contains("5")) & (!cpf.Contains("6")) & (!cpf.Contains("7")) & (!cpf.Contains("8")) & (!cpf.Contains("9")) & (!String.IsNullOrEmpty(cpf)))
            {
                ModelState.AddModelError("Cpf", "Entre com um CPF válido");
            }

            if (String.IsNullOrEmpty(rg))
            {
                ModelState.AddModelError("rg", "RG requerido");

            }

            if ((!rg.Contains("0")) & (!rg.Contains("1")) & (!rg.Contains("2")) & (!rg.Contains("3")) & (!rg.Contains("4")) & (!rg.Contains("5")) & (!rg.Contains("6")) & (!rg.Contains("7")) & (!rg.Contains("8")) & (!rg.Contains("9")) & (!String.IsNullOrEmpty(rg)))
            {
                ModelState.AddModelError("rg", "Entre com um RG válido");
            }


            if (password == null || password.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "Você precisa especificar uma senha de {0} ou mais caracteres.",
                         MembershipService.MinPasswordLength));
            }
            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "A nova senha e a confirmação de senha não conferem.");
            }
            return ModelState.IsValid;
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Usuário já existe. Entre um nome diferente.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Um usuário com este e-mail já existe. Entre com um e-mail diferente.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Senha inválida. Entre com uma senha válida.";

                case MembershipCreateStatus.InvalidEmail:
                    return "E-mail inválido. Entre com um e-mail válido.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "A resposta de senha é inválida. Tente novamente.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "A pergunta de senha é inválida. Tente novamente.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Usuário inválido. Tente novamente.";

                case MembershipCreateStatus.ProviderError:
                    return "O provedor de autenticação retornoiu um erro. Tente novamente. Se o problema persistir, contate o administrador.";

                case MembershipCreateStatus.UserRejected:
                    return "A solicitação de registro de usuário foi cancelada. Tente novamente. Se o problema persistir, contate o administrador.";

                default:
                    return "Um erro desconhecido ocorreu. Tente novamente. Se o problema persistir, contate o administrador.";
            }
        }
        #endregion
    }

    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    public interface IFormsAuthentication
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthentication
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
            return currentUser.ChangePassword(oldPassword, newPassword);
        }
    }
}