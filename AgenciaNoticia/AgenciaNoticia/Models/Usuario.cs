using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

namespace AgenciaNoticia.Models
{
    public partial class Usuario
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Nome))
                yield return new RuleViolation("Nome requerido", "Nome");

            if (String.IsNullOrEmpty(UsuarioID.ToString()))
                yield return new RuleViolation("ID requerido", "UsuarioID");

            if (String.IsNullOrEmpty(Perfil))
                yield return new RuleViolation("Perfil requerido", "Perfil");

            if (!Perfil.ToLower().Equals("administrador") & !Perfil.ToLower().Equals("jornalista") & !Perfil.ToLower().Equals("comprador") & !Perfil.ToLower().Equals("gestor") & (!String.IsNullOrEmpty(Perfil)))
                yield return new RuleViolation("Perfil inválido. Perfis válidos: administrador, gestor, jornalista ou comprador", "Perfil");

            if (String.IsNullOrEmpty(Senha))
                yield return new RuleViolation("Senha requerida", "Senha");

            if (Senha.Length < 6)
                yield return new RuleViolation("Senha requerida com no mínimo 6 caracteres.", "Senha");

            if (String.IsNullOrEmpty(Cpf))
                yield return new RuleViolation("CPF requerido", "Cpf");

            if ((!Cpf.Contains("0")) & (!Cpf.Contains("1")) & (!Cpf.Contains("2")) & (!Cpf.Contains("3")) & (!Cpf.Contains("4")) & (!Cpf.Contains("5")) & (!Cpf.Contains("6")) & (!Cpf.Contains("7")) & (!Cpf.Contains("8")) & (!Cpf.Contains("9")) & (!String.IsNullOrEmpty(Cpf)))
                yield return new RuleViolation("Entre com um CPF válido", "Cpf");

            if (String.IsNullOrEmpty(Rg))
                yield return new RuleViolation("RG requerido", "Rg");

            if ((!Rg.Contains("0")) & (!Rg.Contains("1")) & (!Rg.Contains("2")) & (!Rg.Contains("3")) & (!Rg.Contains("4")) & (!Rg.Contains("5")) & (!Rg.Contains("6")) & (!Rg.Contains("7")) & (!Rg.Contains("8")) & (!Rg.Contains("9")) & (!String.IsNullOrEmpty(Rg)))
                yield return new RuleViolation("Entre com um RG válido", "Rg");

            if (String.IsNullOrEmpty(Email))
                yield return new RuleViolation("e-mail requerido", "Email");

            if ((!Email.Contains("@")) || (!Email.Contains(".")) || (Email.IndexOf("@") > Email.LastIndexOf(".")) || (Email.StartsWith(".")) || (Email.StartsWith("@")) || (Email.EndsWith(".")) || (Email.EndsWith("@")))
            {
                yield return new RuleViolation("Você precisa especificar um e-mail válido.", "Email");
            }

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Violação de regras não permitiram salvar");
        }
    }

    public class RuleViolation
    {
        public string ErrorMessage { get; private set; }
        public string PropertyName { get; private set; }

        public RuleViolation(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }
    }
}