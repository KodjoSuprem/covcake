<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


    <div class="simplebox">
        <%= Html.ActionLink("Messages Re�us", "Index") %> | 
        <%= Html.ActionLink("Messages Envoy�s", "Index", new { folder = "1" }) %> | 
        <%= Html.ActionLink("Corbeille", "Index", new { folder = "2"}) %>
    </div>
    