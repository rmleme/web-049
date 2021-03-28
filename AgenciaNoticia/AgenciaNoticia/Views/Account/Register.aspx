<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Cadastro
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Criar uma nova conta</h2>
    <p>
        Use o formulário abaixo para criar uma nova conta. 
    </p>
    <p>
        Senhas com no mínimo <%=Html.Encode(ViewData["PasswordLength"])%> caracteres.
    </p>
    <%= Html.ValidationSummary("Cadastro sem sucesso. Corrija os erros e tente novamente.") %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend>Informações da conta:</legend>
                <p>
                    <label for="username">Nome:</label>
                    <%= Html.TextBox("username") %>
                    <%= Html.ValidationMessage("username") %>
                </p>
                <p>
                    <label for="email">E-mail:</label>
                    <%= Html.TextBox("email") %>
                    <%= Html.ValidationMessage("email") %>
                </p>
                 <p>
                    <label for="perfil">Perfil (Administrador, Gestor, Jornalista ou Comprador) :</label>
                    <%= Html.TextBox("perfil") %>
                    <%= Html.ValidationMessage("perfil") %>
                </p>
                <p>
                    <label for="cpf">CPF:</label>
                    <%= Html.TextBox("cpf") %>
                    <%= Html.ValidationMessage("cpf") %>
                </p>
                <p>
                    <label for="perfil">RG:</label>
                    <%= Html.TextBox("rg") %>
                    <%= Html.ValidationMessage("rg") %>
                </p>
                <p>
                    <label for="password">Senha:</label>
                    <%= Html.Password("password") %>
                    <%= Html.ValidationMessage("password") %>
                </p>
                <p>
                    <label for="confirmPassword">Confirme a senha:</label>
                    <%= Html.Password("confirmPassword") %>
                    <%= Html.ValidationMessage("confirmPassword") %>
                </p>
                <p>
                    <input type="submit" value="Cadastrar" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
