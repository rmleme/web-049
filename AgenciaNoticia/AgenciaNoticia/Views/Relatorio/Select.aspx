<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AgenciaNoticia.Models.Relatorio>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Relatórios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Relatórios</h2>

    <% if (Context.User.IsInRole("Gestor") || Context.User.IsInRole("Administrador")) { %>
    <p>
        <%= Html.ActionLink("Contábil", "IndexFinance")%>
    </p>
    <% } %>

    <% if (Context.User.IsInRole("Comprador") || Context.User.IsInRole("Jornalista")) { %>
    <p>
        <%= Html.ActionLink("Notícias Compradas/Visualizadas", "IndexNewsBought")%>
    </p>
    <% } %>

    <% if (Context.User.IsInRole("Comprador") || Context.User.IsInRole("Jornalista")) { %>
    <p>
        <%= Html.ActionLink("Ranking de Notícias", "IndexNewsRank")%>
    </p>
    <% } %>
</asp:Content>