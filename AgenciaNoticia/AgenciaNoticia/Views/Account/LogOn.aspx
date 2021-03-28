<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Log On
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Log On</h2>
    <p>
        Entre com seu nome e senha. <%= Html.ActionLink("Cadastre-se", "Register") %> se você não tem uma conta.
    </p>
    <%= Html.ValidationSummary("Login sem sucesso. Corrija os erros e tente novamente.") %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend>Informações da conta</legend>
                <p>
                    <label for="username">Nome:</label>
                    <%= Html.TextBox("username") %>
                    <%= Html.ValidationMessage("username") %>
                </p>
                <p>
                    <label for="password">Senha:</label>
                    <%= Html.Password("password") %>
                    <%= Html.ValidationMessage("password") %>
                </p>
               
                <p>
                    <%= Html.CheckBox("rememberMe") %> <label class="inline" for="rememberMe">Lembrar-me?</label>
                </p>
                <p>
                    <input type="submit" value="Log On" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
