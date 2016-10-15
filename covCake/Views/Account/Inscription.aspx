<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Inscription.aspx.cs" Inherits="covCake.Views.Account.Register" %>

<asp:Content ID="css" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" type="text/css" href="../Content/jquery.autocomplete.css" /> 
    <script type='text/javascript' src='../Scripts/jquery.autocomplete.min.js'></script> 
</asp:Content>

<asp:Content ContentPlaceHolderID="DevStyleCSS" runat="server">

</asp:Content>

<asp:Content ID="java" ContentPlaceHolderID="JavaScript" runat="server">

<script type="text/javascript">
    // scripts pour le form (check Ajax de l'email et autres...)
</script>
</asp:Content>
<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
   <% Html.RenderPartial("RegisterForm"); %>
</asp:Content>
