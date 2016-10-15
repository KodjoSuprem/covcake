<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ErrorRedirect.aspx.cs" Inherits="covCake.Views.Shared.ErrorRedirect" %>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">

 <%= Html.TimerRedirect(ViewData.Model.RedirectUrl, ViewData.Model.DelaySeconds)%>
 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   
     <h2>
        Zut! Une erreur... :( 
    </h2>
    <%var errorMsg = "Une erreur est survenue lors du traitement de votre requête."; %>
     <% if (ViewData.ContainsKey("ErrorMsg") && !ViewData["ErrorMsg"].ToString().IsNullOrEmpty())
        {
            errorMsg = ViewData["ErrorMsg"] as string;// pas de html encode!!!
        } %>
      
    <table>
    <tr>
    <td>
        <img alt="error" src="/Content/error.png" />
    </td>
    <td>
        <p style="font-weight:bold; font-size: 130%;">  <%= errorMsg %> </p>
    </td>
    
    </tr>
    </table>
    <p style="text-align: center;">
      <span class="info" >Vous allez être redirigé automatiquement dans quelques instants.</span> 
    </p>
    <br />
    <p style="text-align: center;">
        Si le problème persiste contactez <%= Html.MailTo(CovCakeConfiguration.SiteAdminEmail, "", "Erreur technique sur Coyoyage.net: ")%>.
    </p>

    <% Html.RenderPartial("BackToHomeLink"); %>
    
</asp:Content>
