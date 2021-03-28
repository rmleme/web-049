<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AgenciaNoticia.Models.Relatorio>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Relatório Contábil
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Relatório Contábil</h2>

    <table>
        <tr>            
            <th>
                Data do relatório
            </th>
            <th>
                Total de Vendas (R$)
            </th>
        </tr>

    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%= Html.Encode(String.Format("{0:dd/MM/yyyy}", item.DataRelatorio)) %>
            </td>
            <td>
                <%= item.TotalDeVendas %>
            </td>
        </tr>
    <% } %>
    </table>

    <p>
        <%= Html.ActionLink("Voltar", "Select") %>
    </p>
</asp:Content>