<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AgenciaNoticia.Models.Noticia>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Inserir notícia
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Inserir notícia</h2>

    <%= Html.ValidationSummary("Corrija os erros e tente novamente.") %>

    <% using (Html.BeginForm("Create", "Noticia", FormMethod.Post, new { enctype = "multipart/form-data" })) {%>
        <fieldset>            
            <p>               
                Faça o upload da imagem:
                <input type="file" name="Image" /> 
            </p>            
            <p>
                <label for="Categoria">Categoria (Esporte, Política, Economia, Cotidiano, Lazer ou Ciência):</label>
                <%= Html.TextBox("Categoria") %>
                <%= Html.ValidationMessage("Categoria", "*") %>
            </p>
            <p>
                <label for="Preco">Preço(R$):</label>
                <%= Html.TextBox("Preco") %>
                <%= Html.ValidationMessage("Preço", "*") %>
            </p>
            <p>
                <label for="Vigencia">Vigência (insira uma data no formato dd/mm/aaaa):</label>
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