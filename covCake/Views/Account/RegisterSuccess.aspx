<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="RegisterSuccess.aspx.cs" Inherits="covCake.Views.Account.RegisterSuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<h1>Bienvenue sur Covoyage !!</h1>

<p> Fécilitation, votre inscription à réussis! Commencer à utiliser le site dès à présent</p>

<%= Html.ActionLink("Recherche","Liste","Projet") %>

</asp:Content>
