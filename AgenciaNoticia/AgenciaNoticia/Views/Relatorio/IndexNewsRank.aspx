<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AgenciaNoticia.Models.Noticia>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Relatório - Ranking de Notícias
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Relatório - Ranking de Notícias</h2>

    <table>
        <tr>            
            <th>
                Notícia (Resumo)
            </th>
            <th>
                Vigência
            </th>
            <th>
                Número de Acessos
            </th>
        </tr>

    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%= item.Texto.Substring(0, Math.Min(50, item.Texto.Length)) + (item.Texto.Length < 50 ? "" : "...") %>
            </td>
            <td>
                <%= item.Vigencia %>
            </td>
            <td>
                <%= item.NumeroDeAcessos == null ? 0 : item.NumeroDeAcessos %>
            </td>
        </tr>
    <% } %>
    </table>

    <p>
        <%= Html.ActionLink("Voltar", "Select") %>
    </p>
</asp:Content>