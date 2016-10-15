<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlockProjet.ascx.cs" Inherits="covCake.Views.Shared.BlockProjet" %>
<%@ Import Namespace="covCake.Helpers" %>
<%@ Import Namespace="covCake.DataAccess" %>
 <% IProjet proj = ViewData.Model;
    IUserProfile user = proj.OwnerUserProfile;%>

<br />
 
 <fieldset class="detailsProjet">
<legend align="top"> Destination : <%=proj.PaysArrive.LibellePays %> </legend>
<%if (proj.Realise)
  {%><p>  <%=user.UserName%> a déjà réaliser son projet de voyage!! <%= Html.Link("voir son album photo", "#")%> </p> 
  <% }
  %>
<ul>  
<% if(!proj.Incertain) { %>
<li> Date de Départ : <%= proj.DateDebut.Value.ToLongDateString()%> depuis : <%= proj.VilleDepart%>  </li> 
<li> Date du Retour : <%= proj.DateFin.Value.ToLongDateString()%> depuis : <%= proj.VilleArrive%> </li> 
<%}else{ %>
<li><span>Voyage Incertain</span></li>
<%} %>
<li> Duree du séjour : <%= proj.GetDuree().ToString() %></li>
</ul>
<br />
<div class="commentairesProjet">
<div class="title">Commentaires</div>
     <%= proj.Commentaires %>
</div>
<%  Html.RenderPartial("SendMessageControl",proj); %>
</fieldset>