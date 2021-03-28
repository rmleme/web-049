<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AgenciaNoticia.Models.Pedido>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Compra Finalizada
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Compra Finalizada</h2>

    <table>
        <tr>                        
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
                Texto
            </th>
        </tr>
    <% foreach (var item in Model.Noticias) { %>
        <tr>
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
                <%= Html.Encode(item.Texto) %>
            </td>
        </tr>
    <% } %>
    </table>
</asp:Content>