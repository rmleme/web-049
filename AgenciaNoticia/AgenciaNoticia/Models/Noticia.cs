using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Text.RegularExpressions;

namespace AgenciaNoticia.Models
{
    public partial class Noticia
    {
        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Noticia n = obj as Noticia;
            if ((Object) n == null)
            {
                return false;
            }

            return (this.NoticiaID == n.NoticiaID);
        }

        public override int GetHashCode()
        {
            return NoticiaID;
        }

        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Preco.ToString()))
                yield return new RuleViolation("Preço requerido", "Preço");

            //retorna mensagem de validação se preço é negativo
            if (Preco < 0)  
                yield return new RuleViolation("Preço inválido", "Preço");            

            if (String.IsNullOrEmpty(Vigencia.ToString()))
                yield return new RuleViolation("Vigência requerida. Formato dia/mês/ano (DD/MM/YYYY).", "Vigencia");

            //retorna mensagem de validação se formato da data não é dd/mm/aaaa
            if (!DateValidator.IsValidFormat(Vigencia, "BR"))
                yield return new RuleViolation("Data com formato inválido. Inserir data no formato dia/mês/ano (dd/mm/aaaa).", "Vigencia");

            //retorna mensagem de validação se data é futura
            if (!DateValidator.IsValidDate(Vigencia, "BR") & DateValidator.IsValidFormat(Vigencia, "BR"))
                yield return new RuleViolation("Data futura. Inserir data válida", "Vigencia");

            if (String.IsNullOrEmpty(Texto))
                yield return new RuleViolation("Texto da notícia requerido", "Texto");

            if (!Categoria.ToLower().Equals("esporte") & !Categoria.ToLower().Equals("política") & !Categoria.ToLower().Equals("economia") & !Categoria.ToLower().Equals("cotidiano") & !Categoria.ToLower().Equals("lazer") & !Categoria.ToLower().Equals("ciência"))
                yield return new RuleViolation("Categorias válidas: esporte, política, economia, cotidiano, lazer ou ciência", "Categoria");

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Violação de regras não permitiram salvar");
        }
    }

    public class RuleViolationNoticia
    {
        public string ErrorMessage { get; private set; }
        public string PropertyName { get; private set; }

        public RuleViolationNoticia(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }
    }

    //
    // método que faz a validação da data
    public class DateValidator
    {
        static IDictionary<string, Regex> countryRegex = new Dictionary<string, Regex>() {
           //{ "BR", new Regex("^\\d{2}/\\d{2}/\\d{4}$")},
           { "BR", new Regex("^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9][0-9]$")},
            };

        //verifica formato dd/mm/yyyy da data 
        public static bool IsValidFormat(string date, string country)
        {
            if (country != null && countryRegex.ContainsKey(country))
                return countryRegex[country].IsMatch(date);
            else
                return false;
        }

        public static IEnumerable<string> Countries
        {
            get
            {
                return countryRegex.Keys;
            }
        }

        //compara se data da notícia é maior que data atual 
        public static bool IsValidDate(string date, string country)
        {
            if (IsValidFormat(date, country))
            {
                if ((Convert.ToInt64(date.Substring(6, 4)) == DateTime.Now.Year) & (Convert.ToInt64(date.Substring(3, 2)) > DateTime.Now.Month))
                {
                    return false;
                }

                if ((Convert.ToInt64(date.Substring(6, 4)) == DateTime.Now.Year) & (Convert.ToInt64(date.Substring(3, 2)) == DateTime.Now.Month)
                    & (Convert.ToInt64(date.Substring(0, 2))) > DateTime.Now.Day)
                {
                    return false;
                }

                if ((Convert.ToInt64(date.Substring(6, 4)) > DateTime.Now.Year))
                {
                    return false;
                }

                else
                    return true;
            }
            else
                return false;
        }
    }
}