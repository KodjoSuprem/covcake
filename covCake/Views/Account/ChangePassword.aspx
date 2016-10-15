<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="covCake.Views.Account.ChangePassword" %>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
   <% Html.RenderPartial("ChangePasswordForm"); %>
</asp:Content>
