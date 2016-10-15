<%@ Control Language="C#" Inherits="covCake.BaseViewUserControl<covCake.Models.ListeProjetViewData>" %>

<% var listeProj = this.Model.ListeProjets;  %>
<ul>

<%foreach (IProjet proj in listeProj){ %>

<li> <%= Html.CountryFlagImg(proj.PaysArrive,CovCakeHtmlHelper.FlagSize.s16)  %> <%= Html.ProjetIndexLink(proj.GetShortDisplayName(), proj) %> <%= " - " + (1 + proj.UserAbonnes.Count()) + " " + Html.IconImage("group.png","voyageur(s)") %></li>

<%} %>
</ul>