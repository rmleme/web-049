<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Troca de senha
</asp:Content>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Troca de senha</h2>
    <p>
        Use o formulário a seguir pra trocar de senha.
    </p>
    <p>
        Novas senhas precisam ter um mínimo de <%=Html.Encode(ViewData["PasswordLength"])%> caracteres.
    </p>
    <%= Html.ValidationSummary("Troca de senha sem sucesso. Tente novamente.")%>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend>Informações da conta</legend>
                <p>
                    <label for="currentPassword">Senha atual:</label>
                    <%= Html.Password("currentPassword") %>
                    <%= Html.ValidationMessage("currentPassword") %>
                </p>
                <p>
                    <label for="newPassword">Nova senha:</label>
                    <%= Html.Password("newPassword") %>
                    <%= Html.ValidationMessage("newPassword") %>
                </p>
                <p>
                    <label for="confirmPassword">Confirmar a nova senha:</label>
                    <%= Html.Password("confirmPassword") %>
                    <%= Html.ValidationMessage("confirmPassword") %>
                </p>
                <p>
                    <input type="submit" value="Trocar senha" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
