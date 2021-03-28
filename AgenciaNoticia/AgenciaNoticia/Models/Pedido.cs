using System;
using System.Globalization;
using System.Text;
using System.Xml;

namespace AgenciaNoticia.Models
{
    public partial class Pedido
    {
        public void RemoverNoticia(int NoticiaID)
        {
            foreach (Noticia noticia in this.Noticias)
            {
                if (noticia.NoticiaID == NoticiaID)
                {
                    this.Noticias.Remove(noticia);
                    break;
                }
            }
        }

        public void CancelarCompra()
        {
            this.Noticias.Clear();
        }

        public String serialize()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = new UTF8Encoding();
            settings.IndentChars = ("    ");
            StringBuilder output = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(output, settings);

            writer.WriteStartElement("n", "Noticias", "http://ipt.br/agencianoticia");
            foreach (Noticia noticia in this.Noticias)
            {
                writer.WriteStartElement("Noticia", "http://ipt.br/agencianoticia");

                writer.WriteElementString("Categoria", "http://ipt.br/agencianoticia", noticia.Categoria);

                writer.WriteStartElement("Preco", "http://ipt.br/agencianoticia");
                writer.WriteValue(noticia.Preco);
                writer.WriteEndElement();

                writer.WriteStartElement("Vigencia", "http://ipt.br/agencianoticia");
                writer.WriteValue(DateTime.Parse(noticia.Vigencia, CultureInfo.CreateSpecificCulture("pt-BR")));
                writer.WriteEndElement();

                writer.WriteElementString("Texto", "http://ipt.br/agencianoticia", noticia.Texto);

                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.Flush();
            writer.Close();

            output.Replace("utf-16", "utf-8");
            return output.ToString();
        }
    }
}