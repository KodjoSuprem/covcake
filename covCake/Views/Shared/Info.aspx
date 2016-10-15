<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= ViewData["InfoMsgTitle"] %></h2>

 <table width="100%">
    <tr>
    <td>
        <img alt="info" src="/Content/info.png" />
    </td>
    <td style="text-align: center;">
        <p class="info" style="font-weight:bold; font-size: 130%;">  <%= ViewData["InfoMsg"] %> </p>
    </td>
    
    </tr>
    </table>

    <% Html.RenderPartial("BackToHomeLink"); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="DevStyleCSS" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="AfterBodySection" runat="server">
</asp:Content>
