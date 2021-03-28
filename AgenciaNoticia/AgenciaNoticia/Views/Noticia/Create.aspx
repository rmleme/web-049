<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AgenciaNoticia.Models.Noticia>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Inserir not�cia
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Inserir not�cia</h2>

    <%= Html.ValidationSummary("Corrija os erros e tente novamente.") %>

    <% using (Html.BeginForm("Create", "Noticia", FormMethod.Post, new { enctype = "multipart/form-data" })) {%>
        <fieldset>            
            <p>               
                Fa�a o upload da imagem:
                <input type="file" name="Image" /> 
            </p>            
            <p>
                <label for="Categoria">Categoria (Esporte, Pol�tica, Economia, Cotidiano, Lazer ou Ci�ncia):</label>
                <%= Html.TextBox("Categoria") %>
                <%= Html.ValidationMessage("Categoria", "*") %>
            </p>
            <p>
                <label for="Preco">Pre�o(R$):</label>
                <%= Html.TextBox("Preco") %>
                <%= Html.ValidationMessage("Pre�o", "*") %>
            </p>
            <p>
                <label for="Vigencia">Vig�ncia (insira uma data no formato dd/mm/aaaa):</label>
                <%= Html.TextBox("Vigencia") %>
                <%= Html.ValidationMessage("Vigencia", "*") %>
            </p>
            <p>
                <label for="Texto">Texto:</label>
                <%= Html.TextArea("Texto") %>
                <%= Html.ValidationMessage("Texto", "*") %>
            </p>
            <p>
                <input type="submit" value="Inserir" style="height: 26px" />
            </p>
        </fieldset>
    <% } %>
</asp:Content>