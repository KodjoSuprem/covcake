<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.ascx.cs" Inherits="covCake.Views.Shared.LoginForm" %>

<div id="modalmain">
<h2>Connexion</h2>
    <p>
        Entrez votre adresse email et votre mot de passe.<br />
        Pas de compte? Inscrivez vous en 2 min ici: <%= Html.ActionLink("S'inscrire!", "Inscription") %>.
    </p>
    <%= Html.ValidationSummary() %>

    <% using (Html.BeginForm("Login","Account")) { %>
        <div>
            <table>
                <tr>
                    <td>Email:</td>
                    <td>
                        <%= Html.cTextBox("username") %>
                        <%= Html.ValidationMessage("username") %>
                    </td>
                </tr>
                <tr>
                    <td>Mot de passe:</td>
                    <td>
                        <%= Html.cPassword("password") %>
                        <%= Html.ValidationMessage("password") %>
                    </td>
                </tr>
               
                <tr > 
                    <td colspan="2"> <%= Html.ActionLink("Mot de passe oublié","Forgot","Account") %></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: right;">
                        <%= Html.cSubmit("Connexion") %>
                    </td>
                </tr>
            </table>
                <input type="hidden" name="returnUrl" value="<%=ViewData["ReturnUrl"] %>"  />
        </div>
    <% } %>
</div>