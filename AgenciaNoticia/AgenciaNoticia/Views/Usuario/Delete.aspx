<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AgenciaNoticia.Models.Usuario>" %>

 
<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Delete Confirmation:  <%=Html.Encode(Model.Nome) %>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        Confirmação de exclusão de usuário
    </h2>

    <div>
        <p>Confirma a exclusão de: 
           <i> <%=Html.Encode(Model.Nome) %>? </i> 
        </p>
    </div>
    
    <% using (Html.BeginForm()) {  %>
        <input name="confirmButton" type="submit" value="Excluir" /> 
        <input type="button" value="Voltar" onclick="history.go(-1)"/>       
    <% } %>
     
</asp:Content>
 
