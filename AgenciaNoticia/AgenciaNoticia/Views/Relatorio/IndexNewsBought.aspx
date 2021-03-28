<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AgenciaNoticia.Models.Relatorio>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Relat�rio de Not�cias Compradas / Visualizadas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Relat�rio de Not�cias Compradas / Visualizadas</h2>

    <table>
        <tr>            
            <th>
                Data do relat�rio
            </th>
            <th>
                Total de Not�cias Visualizadas
            </th>
            <th>
                Total de Not�cias Compradas
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