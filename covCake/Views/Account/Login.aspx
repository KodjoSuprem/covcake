<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="covCake.Views.Account.Login" %>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">

<% Html.RenderPartial("LoginForm"); %>

</asp:Content>
