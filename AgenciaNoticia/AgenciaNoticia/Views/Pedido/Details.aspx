<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AgenciaNoticia.Models.Pedido>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Meu carrinho
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Meu carrinho</h2>

    <% using (Html.BeginForm()) {%>
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
                    Resumo
                </th>
                <th></th>
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
                    <%= Html.Encode(item.Texto.Substring(0, Math.Min(50, item.Texto.Length)) + (item.Texto.Length < 50 ? "" : "...")) %>
                </td>
                <td>
                    <%= Html.ActionLink("Remover", "Remover", new { id=item.NoticiaID })%>
                </td>
            </tr>
        <% } %>
        </table>

        <p>
            <%= Html.ActionLink("Cancelar Compra", "Cancelar")%>
        </p>
        <p>
            <%= Html.ActionLink("Finalizar Compra", "Finalizar")%>
        </p>
        <p>
            <input type="button" value="Voltar" onclick="history.go(-1)"/> 
        </p>
    <% } %>
</asp:Content>