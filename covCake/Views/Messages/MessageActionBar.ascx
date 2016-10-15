<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


    <div class="simplebox">
        <%= Html.ActionLink("Messages Reçus", "Index") %> | 
        <%= Html.ActionLink("Messages Envoyés", "Index", new { folder = "1" }) %> | 
        <%= Html.ActionLink("Corbeille", "Index", new { folder = "2"}) %>
    </div>
    