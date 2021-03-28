<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AgenciaNoticia.Models.Usuario>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">

	Alterar: <%=Html.Encode(Model.Nome)%>

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">

    function Limpa() 
    {
        var input = document.getElementsByTagName('input')

        for (var i = 0; i < input.length; i++) 
        {
            if (input[i].type == 'text')
            input[i].value = '';
        }
    }
        window.onload = function() 
        {
            document.getElementById('limpa').onclick = Limpa;
        }       

 </script>
    <h2>Alterar Usuário</h2>


    <%= Html.ValidationSummary("Alteração não teve sucesso. Corrija erros e tente de novo.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>           
            <p>
                <label for="Nome">Nome:</label>
                <%= Html.TextBox("Nome") %>
                <%= Html.ValidationMessage("Nome", "*") %>
            </p>          
            <p>
                <label for="Perfil">Perfil:</label>
                <%= Html.TextBox("Perfil") %>
                <%= Html.ValidationMessage("Perfil", "*") %>
            </p>
            <p>
                <label for="Senha">Senha:</label>
                <%= Html.TextBox("Senha") %>
                <%= Html.ValidationMessage("Senha", "*") %>
            </p>
            <p>
                <label for="Cpf">CPF:</label>
                <%= Html.TextBox("Cpf") %>
                <%= Html.ValidationMessage("Cpf", "*") %>
            </p>
            <p>
                <label for="Rg">RG:</label>
                <%= Html.TextBox("Rg") %>
                <%= Html.ValidationMessage("Rg", "*") %>
            </p>
            <p>
                <label for="Email">e-mail:</label>
                <%= Html.TextBox("Email") %>
                <%= Html.ValidationMessage("Email", "*") %>
            </p>
           
            
            <p>
                <input type="submit" value="Salvar" />
                <input type="button" id="limpa" value="Limpar campos" />          
                <input type="button" value="Voltar" onclick="history.go(-1)"/> 
            </p>
               
        </fieldset>

    <% } %>
 

</asp:Content>

