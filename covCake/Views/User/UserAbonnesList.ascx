<%@ Control Language="C#"  Inherits="System.Web.Mvc.ViewUserControl<covCake.Models.MonCompteViewData>" %>

<h2>Mes participants</h2>

<div id="projetsListe">
<% if(this.Model.UserProfile.Projets.UserAbonnes().Count() < 1)
   { %>
<div>
Personne ne vous accompagne pour l'instant. Voici quelques voyages qui pourraient vous intérésser ... <% //TODO: mettre la liste des voyages relatifs%>
</div>
<%}
   else
   {%>
<%foreach(IProjet proj in this.Model.UserProjets)
  { 
      if(proj.UserAbonnes.Count() < 1) continue;
%>
  
<div id="projetitem" class="subsclist">
    <div id="projettitle">
            <div class="projettitle">
		        <h2>
		           <%= proj.VilleArrive%> - <%= proj.PaysArrive.LibellePays %>
		        </h2>
		    </div>
	</div>
	
	<div class="clear"> </div>
	
	<div id="projetItemInfos">
	
		<div id="dates"><!-- Dates -->
		
		
		</div>
		
		<div id="details"><!-- Infos -->
		    <ul>
            <%foreach (var abonne in proj.UserAbonnes)
              { %>
                <li>
                    <%= Html.UserSexeIcon(abonne.UserProfile)%>  <%= Html.UserProfileLink(abonne.UserProfile)%> - <small><%= abonne.DateAbonnement %></small>
                </li>
                
            <%} %>
            </ul>
		</div>
		
		<div id="actions"><!-- subcribment -->
		    <%= Html.IconImage("email_go.png","envoyer un message") %>   <%=Html.ActionLink("Envoyer un mesage à tous!", "SendMessage", "Messages", new { ProjetId = proj.IdProjet }, new { rel = "facebox" })%>

        </div>
     </div>
    <div class="clear">  </div>
        <div  id="stats-projet">
          <small>Vu <%= proj.Visites %> fois.</small> - 
          <small>Publié le: <%= proj.DateCreation.ToShortDateString()%></small> - 
          <small>Modifié le: <%= proj.DateModification.ToShortDateString()%></small> -
        
        </div>
    </div>
 <% } %>
<%} %>
</div>