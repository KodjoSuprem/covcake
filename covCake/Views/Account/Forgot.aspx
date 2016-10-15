<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Forgot.aspx.cs" Inherits="covCake.Views.Account.Forgot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<h2>Récupération du mot de passe</h2>
<% if(ViewData.Model.Succeed == false)
   { %>     
   
   
   <%= Html.ValidationSummary() %>
   
        <p>Un email vous sera envoyé avec votre nouveau mot de passe.</p>
        <%using(Html.BeginForm())
          { %>
            <table width="100%">
            <tr>
            <td>Saisissez l'adresse Email que vous avez utilisée pour vous inscrire.</td>
            <td><%= Html.cTextBox("email")%> </td>

            </tr>
            <tr>
            <td></td>
            <td> <br />
                <%= Html.cSubmit("Changer mon mot de passe") %>
            </td>
            </tr>
            </table>
        <%} %>
<% }
   else{ %>
   <div >
    <p>Un email a été envoyé à l'adresse suivante: <%=ViewData.Model.Email %> avec vos nouveaux identifiants.</p>
   </div>
<%} %>
</asp:Content>

<asp:Content ContentPlaceHolderID="DevStyleCSS" runat="server">
<style type="text/css">

#email { width: 280px; font-size:1.5em;}   

</style>

</asp:Content>

