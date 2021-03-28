<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AgenciaNoticia.Models.Usuario>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Lista de usu�rios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Lista de usu�rios</h2>

    <table>
        <tr>                        
            <th>
                Nome
            </th>
            <th>
                ID do usu�rio 
            </th>            
            <th></th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>                 
            <td>
                <%= Html.Encode(item.Nome) %>
            </td>
            <td>
                <%= Html.Encode(item.UsuarioID) %>
            </td>   
            <td>                 
                <%= Html.ActionLink("Consultar", "Details", new { id=item.UsuarioID })%>|
                <%= Html.ActionLink("Alterar", "Edit", new { id=item.UsuarioID }) %>|
                <%= Html.ActionLink("Excluir", "Delete", new { id=item.UsuarioID }) %>
            </td>          
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Criar usu�rio", "Create") %>
    </p>

</asp:Content>

