<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AgenciaNoticia.Models.Noticia>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Detalhes da not�cia
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Detalhes da not�cia</h2>

    
        <p>
            Imagem:
             <img src='/Noticia/GetImage/<%= Html.Encode(Model.NoticiaID) %>' alt="" />
        </p>
        <p>
            Categoria:
            <%= Html.Encode(Model.Categoria.ToLower()) %>
        </p>
        <p>
            Pre�o:
            R$<%= Html.Encode(Model.Preco) %>
        </p>
        <p>
            Vig�ncia:
            <%= Html.Encode(String.Format("{0:g}", Model.Vigencia)) %>
        </p>
        <p>
            Texto:
            <%= Html.Encode(Model.Texto) %>
        </p>
    
</asp:Content>