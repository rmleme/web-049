<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AgenciaNoticia.Models.Noticia>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Relat�rio - Ranking de Not�cias
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Relat�rio - Ranking de Not�cias</h2>

    <table>
        <tr>            
            <th>
                Not�cia (Resumo)
            </th>
            <th>
                Vig�ncia
            </th>
            <th>
                N�mero de Acessos
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