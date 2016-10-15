<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ChangePhoto.aspx.cs" Inherits="covCake.Views.User.ChangePhoto" %>
<%@ Import Namespace="covCake" %>
<%@ Import Namespace="covCake.Helpers" %>
<%@ Import Namespace="covCake.DataAccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%using(Html.BeginForm())
  { %>
<div>
 <%= Html.Image(this.UserPhotoUrl, "Ma photo"/*, new { @id = "userphoto" }*/)%>

</div>
<input type="file" name="userImage" />
<input type="submit" value="check" />


<%} %>
</asp:Content>
