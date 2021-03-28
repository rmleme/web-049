<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AgenciaNoticia.Models.Relatorio>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Relatório de Notícias Compradas / Visualizadas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Relatório de Notícias Compradas / Visualizadas</h2>

    <table>
        <tr>            
            <th>
                Data do relatório
            </th>
            <th>
                Total de Notícias Visualizadas
            </th>
            <th>
                Total de Notícias Compradas
            </th>
        </tr>

    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%= Html.Encode(String.Format("{0:dd/MM/yyyy}", item.DataRelatorio)) %>
            </td>
            <td>
                <%= item.TotalDeNoticiasAcessadas %>
            </td>
            <td>
                <%= item.NumeroNoticiasVendidas %>
            </td>
        </tr>
    <% } %>
    </table>

    <p>
        <%= Html.ActionLink("Voltar", "Select") %>
    </p>
</asp:Content>