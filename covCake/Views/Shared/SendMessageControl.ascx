<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendMessageControl.ascx.cs" Inherits="covCake.Views.Shared.SendMessageControl" %>
<%@ Import Namespace="covCake.Helpers" %>
<%@ Import Namespace="covCake.DataAccess" %>
<%  IProjet projet = ViewData.Model; IUserProfile user = ViewData.Model.OwnerUserProfile; %>
<%  if (!Request.IsAuthenticated) { %>
     <p><%=Html.ActionLink("Identifiez vous pour envoyer un message à "+ user.UserName , "Login","Account")%></p>
<% } else { %>
    <% using(Html.BeginForm("SendPrivateMessage", "User", "POST")){ %>
    <fieldset >
    <%= Html.ValidationMessage("message") %>
    <%= Html.TextArea("message","Entrez ici le message à envoyer à "+ user.UserName )%>
    </fieldset>
    <%= Html.Hidden("toUserid",user.UserId) %>
    <%= Html.Hidden("fromUserId", this.CurrentUser.UserId)%>
    <%= Html.Hidden("projetId",projet.IdProjet) %>
    <input type="submit" value="Envoyer" />
    <% } %>
<%  } %>