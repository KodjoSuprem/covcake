<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchBar.ascx.cs" Inherits="covCake.Views.Shared.SearchBar" %>


<div id="searchbar"  >
<div >
<% using(Html.BeginForm("Search", "Projets", FormMethod.Post, new { id= "searchForm" }))
   { %>
<div id="pays">

    <%= Html.Label("Pays", "paysArrive")%>
    <%= Html.cTextBox("paysArrive", "ex : Brésil", new { style = "color: #888" })%>
    <div style="float: left;">
        <%= Html.HelpBullet("N'oubliez pas les accents! ex: Brésil")%>
    </div>
    
</div>
<div id="date">
    <%= Html.Label("Date de départ", "dateDebut")%>
    <%= Html.cTextBox("dateDebut")%>
</div>
<div id="jours"> 
    <%= Html.Label("Nombre de jours", "nbJours")%>
    <%= Html.cTextBox("nbJours", "ex : 5", new { style = "color: #888", maxlength = "3" })%>
</div>

<%= Html.Submit("Rechercher", new { @class = "button" , style = "margin-left: auto;" })%>
<!--
<input class="button" type="submit" value="Rechercher" style="margin-left: auto;"/>
-->
   </div>
<div class="clear">   </div>
<%} %>

</div>

<%//fake search bar %>