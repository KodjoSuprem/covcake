<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<covCake.Models.MonCompteViewData>" %>
<script type="text/javascript">
 $(function() {
        $("a[id*=del_]").click(function() {
            return confirm("Etes vous sur de vouloir supprimer ce voyage?");
        });
        });

</script>

<div id="userabolist">
<h2>Mes participations</h2>

<div class="pagination">
<%= Html.AjaxPaginator(this.Model.UserAbonnements.TotalPages, this.Model.UserAbonnements.PageIndex,"UserAbonnementsList", new AjaxOptions() { UpdateTargetId = "projets" , HttpMethod = "GET" })%>
</div>

<div id="projetsListe">
<% if(this.Model.UserAbonnements.Count < 1)
   { %>
<div>
Vous ne vous êtes inscrit à aucun voyage pour le moment. <%= Html.ActionLink("Consulter les voyages proposés par les membres CoVoyage.net", "Liste", "Projets")%>
</div>
<%}
   else
   {%>
<%foreach(IAbonnementProjet abo in this.Model.UserAbonnements)
  { %>
<div id="projetitem" class="subsclist">
    <div id="projettitle">
            <div class="projettitle">
		        <h2>
		            <%= abo.Projet.PaysDepart.LibellePays%> -> <%= abo.Projet.VilleArrive%> (<%=  abo.Projet.PaysArrive.LibellePays%>)
		        </h2>
		       <% if(abo.Projet.IsOutDated())
            { %>
		         <span class="info">
		           La Date de départ est dépassée!
		          </span> 
		          <%= Html.HelpBullet("Marquez ce projet comme réalisé: Vous pourrez remplir le carnet de route et commencer à ajouter les photos du voyage à votre album!!")%>
		         
		         <%} %>
		    </div>
            <div>
		        <ul class="projet-edit-menu">
		            <li>
		                 <%= Html.ImageLink( "/Content/cross.png", "Ne plus participer à ce voyage", "Delete", "Projets", new { projetId = abo.Projet.IdProjet }, new { id = "del_" + abo.Projet.IdProjet })%>
		            </li>
		            <li>
		                 <%= Html.ImageLink( "/Content/page_edit.png", "edit", "Edit", "Projets", new { projetId = abo.Projet.IdProjet })%>
		            </li>
		        </ul>
		    </div>
	</div>
	
	<div class="clear"> </div>
	
	<div id="projetItemInfos">
	
		<div id="dates"><!-- Dates -->
		<% var dateDepart = (abo.Projet.DateDebut.HasValue) ? abo.Projet.DateDebut.Value.ToShortDateString() : "Incertaine"; %>
		<% var dateRetour = (abo.Projet.DateFin.HasValue) ? abo.Projet.DateFin.Value.ToShortDateString() : "Incertaine"; %>
        Créé par : <span><%= Html.UserProfileLink(abo.Projet.OwnerUserProfile)%></span>
        <br />
		Depart : <span class="gdorange"><%= dateDepart%></span>
		<br/>
		Retour : <span class="gdorange"><%= dateRetour%></span>
		<%if(abo.Projet.Incertain)
    { %>
		<br />
		Nombre de jours: <span class="gdorange"><%= abo.Projet.NbJours%></span>
		<%} %>
		</div>
		
		<div id="details"><!-- Infos -->
		<%
    ITransport trans = abo.Projet.Transports.FirstOrDefault();
    IResidence resi = abo.Projet.Residence;

    var minTransPrix = 0d;
    if(trans == null)
        trans = ViewData.Model.FakeTransport;
    else minTransPrix = abo.Projet.Transports.Min(t => t.PrixAR);
    if(resi == null)
        resi = ViewData.Model.FakeResidence;
		 %>
		 <% //TODO: Detail lorsqu'on clique sur mode transports %>
		Par: <span class="gdorange"><%= trans.ModeTransport%></span> <br/>
		Hebergement: <span class="gdorange">(<%= resi.Type%>) <%= resi.Adresse%></span><br/>
		Coût estimé: <span class="gdorange"><%= (minTransPrix + resi.Prix)%>€ </span> (<%= minTransPrix + "+" + resi.Prix%>)
		</div>
		
		<div id="actions"><!-- subcribment -->
		<% var subsCount = abo.Projet.UserAbonnes.Count();
     var subsTextNull = "Personne ne vous accompagne pour l'instant";
     var subsTextUnique = "personne vous accompagne!";
     var subsText = "personnes vous accompagne!";
     switch(subsCount)
     {
         case 1: subsText = subsTextUnique; break;
         case 0: subsText = subsTextNull; break;
     }
        %>
		<span><%= (subsCount == 0) ? "" : subsCount.ToString()%></span> <%= subsText%>
		<br/>
		<%//TODO: Lien sur les messages privés concernant le voyage %>
		<% var relMsgCount = abo.Projet.MessagePrives.Count();  %>
		<% var msgLinklibelle = (relMsgCount > 1) ? " messages privés concernent ce voyage" : " message privé concerne ce voyage"; %>
		<%= Html.ActionLink(relMsgCount + msgLinklibelle, "ProjetMessages", "Messages", new { projetId = abo.Projet.IdProjet }, null)%>
		</div>
    </div>
    <div class="clear">  </div>
        <div  id="stats-projet">
          <small>Vu <%= abo.Projet.Visites%> fois.</small> - 
          <small>Publié le: <%= abo.Projet.DateCreation.ToShortDateString()%></small> - 
          <small>Modifié le: <%= abo.Projet.DateModification.ToShortDateString()%></small> -
          <small>Rejoint le: <%= abo.DateAbonnement.ToShortDateString()%></small>
        </div>
    </div>
 <% } %>
<%} %>
</div>