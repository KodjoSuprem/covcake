<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConnexionBlock.ascx.cs" Inherits="covCake.Views.Shared.ConnexionBlock" %>
<%@ Import Namespace="covCake.Helpers" %>


<%  if (Request.IsAuthenticated && this.CurrentUser != null) {
        var newMsgCount = this.CurrentUser.GetNewMessageCount();
%>
       <span><%= Html.MonCompteUserLink(this.CurrentUser.DisplayName,  new { style = "color: #ff6d00;" })%></span>  - 
       <%= "(" + newMsgCount + ") "%> <a href="<%=Url.RouteUrl(CovCake.Routes.MESSAGESINDEX)%>" class="newmessages"></a> -
       <%= Html.ActionLink("Deconnexion", "Logout", "Account") %> 
<%    } else {%>          

        <%= Html.ActionLink("S'inscrire", "Inscription", "Account") %> - 
        <%= Html.ActionLink("Connexion", "Login", "Account", new { fb = "true" /*, returnUrl = this.Request.Url.PathAndQuery */}, new { rel = "facebox" })%> 

<%    }%>



