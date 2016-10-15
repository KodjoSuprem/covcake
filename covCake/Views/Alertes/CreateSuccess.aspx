<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="covCake.BaseViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Votre alerte a bien été créée !</h2>

<div>
A partir de maintenant, vous recevrez automatiquement dans votre boîte email (<b><%= this.CurrentUser.Email %></b>)
les voyages qui correspondent aux critères saisis.

</div>

<%= Html.ActionLink("Consulter votre liste d'alertes email","Index") %>
<%= Html.ActionLink("Ajouter une nouvelle alerte","Create") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="DevStyleCSS" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="AfterBodySection" runat="server">
</asp:Content>
