<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AgenciaNoticia.Models.Usuario>" %>

 
<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Usu�rio exclu�do
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Usu�rio exclu�do</h2>

    <div>
        <p>O usu�rio foi exclu�do com sucesso.</p>
    </div>
    
    <div>
        <p><a href="/usuario">Lista de usu�rios</a></p>
    </div>
    
</asp:Content>
 

