<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div style="text-align: center;">
    <span class="backhome" >
        <%= Html.RouteLink("Retour � l'accueil", CovCake.Routes.HOMEINDEX, null) %>
    </span>
</div>