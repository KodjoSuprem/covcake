<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="PendingRegistration.aspx.cs" Inherits="covCake.Views.Account.PendingRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<h1>Félicitation <%= ViewData.Model.DisplayName %> !! </h1>
<br />
<p>
Un email vous a été envoyé à l'addresse <b><%= ViewData["email"] %></b> afin de confirmer votre adresse email.
</p>
<br />
<p>
Cliquez sur le lien d'activation présent dans ce mail pour terminer votre inscription.
</p>
<% Html.RenderPartial("BackToHomeLink"); %>
</asp:Content>
