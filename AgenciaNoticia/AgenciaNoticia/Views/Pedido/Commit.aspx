<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AgenciaNoticia.Models.Pedido>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Finalização da Compra - Efetue o Pagamento
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Finalização da Compra - Efetue o Pagamento</h2>

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
            </tr>
        <% } %>
        </table>

        <p>
            <%= Html.ActionLink("Pagar (Feed)", "PagarXml")%>
        </p>
        <p>
            <%= Html.ActionLink("Pagar (Visualizar)", "PagarHtml")%>
        </p>
        <p>
            <input type="button" value="Voltar" onclick="history.go(-1)"/> 
        </p>
    <% } %>
</asp:Content>