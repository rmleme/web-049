<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AgenciaNoticia.Models.Usuario>" %>

 
<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Usuário excluído
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Usuário excluído</h2>

    <div>
        <p>O usuário foi excluído com sucesso.</p>
    </div>
    
    <div>
        <p><a href="/usuario">Lista de usuários</a></p>
    </div>
    
</asp:Content>
 

