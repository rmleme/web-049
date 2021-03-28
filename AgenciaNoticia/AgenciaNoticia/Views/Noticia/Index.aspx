<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AgenciaNoticia.Models.Noticia>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Lista de notícias
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Lista de notícias</h2>

    <table>
        <tr> 
            <th>
                Imagem
            </th>                       
            <th>
                Categoria
            </th>
            <th>
                Preço
            </th>
            <th>
                Vigência
            </th>
            <th>
                Resumo
            </th>
            <% if (Context.User.IsInRole("Comprador")) { %>
            <th></th>
            <% } %>
        </tr>
    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <img src='/Noticia/GetImage/<%= Html.Encode(item.NoticiaID) %>' alt="" />
            </td>
            <td>
                <%= Html.Encode(item.Categoria) %>
            </td>
            <td>
                <%= Html.Encode(item.Preco) %>
            </td>
            <td>
                <%= Html.Encode(item.Vigencia) %>
            </td>
            <td>
                <%= Html.Encode(item.Texto.Substring(0, Math.Min(50, item.Texto.Length)) + (item.Texto.Length < 50 ? "" : "...")) %>
            </td>
            <% if (Context.User.IsInRole("Comprador")) { %>
            <td>
                <%= Html.ActionLink("Comprar", "Comprar", new { id=item.NoticiaID })%>
            </td>
            <% } %>
        </tr>
    <% } %>
    </table>
</asp:Content>