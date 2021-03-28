<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AgenciaNoticia.Models.Usuario>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Usuario: <%=Html.Encode(Model.Nome) %>
</asp:Content>


<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Html.Encode(Model.Nome) %></h2>
    <p>
        <strong>ID:</strong> 
        <%=Model.UsuarioID %> 
    </p>
    <p>
        <strong>Perfil:</strong> 
        <%=Html.Encode(Model.Perfil) %>        
    </p>
     <p>
        <strong>Senha:</strong> 
        <%=Html.Encode(Model.Senha) %>
    </p> 
    <p>
        <strong>CPF:</strong> 
        <%=Html.Encode(Model.Cpf) %>
    </p>
    <p>
        <strong>RG:</strong> 
        <%=Html.Encode(Model.Rg) %>
    </p>
    <p>
        <strong>Email:</strong> 
        <%=Html.Encode(Model.Email) %>
    </p>
    
       
    
    <%= Html.ActionLink("Alterar Usuário", "Edit", new { id=Model.UsuarioID })%> |
    <%= Html.ActionLink("Deletar Usuário","Delete", new { id=Model.UsuarioID})%> |  
    <a href="/usuario">Lista de usuários</a> 
</asp:Content> 


