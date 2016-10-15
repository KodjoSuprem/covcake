<%@ Control Language="C#" Inherits="covCake.BaseViewUserControl<covCake.Models.MonCompteViewData>" %>
<script type="text/javascript">



</script>
<h2>Mes voyages</h2>

 <div id="paginator" class="pagination">
<%= Html.AjaxPaginator(this.Model.UserProjets.TotalPages, this.Model.UserProjets.PageIndex, "UserProjetsList", new AjaxOptions() { UpdateTargetId = "projets", HttpMethod = "GET" })%>
</div>

<% if (this.Model.UserProjets.Count < 1)
   {%>
<div> <%= Html.ActionLink("Créer vos projets","Create","Projets") %> de voyage ou <%= Html.ActionLink("rejoignez un voyage","Liste","Projets") %> </div>
<% } %>
<div id="projetsListe">
<%foreach(IProjet proj in this.Model.UserProjets) { %>
<div id="projetitem">
    <div id="projettitle">
            <div class="projettitle">
		        <h2>
		            <%= proj.PaysDepart.LibellePays%> -> <%= proj.VilleArrive %> (<%= proj.PaysArrive.LibellePays%>)
		        </h2>
		       <% if(proj.IsOutDated()){ %>
		         <span class="info">
		           La Date de départ est dépassée! <%= Html.HelpBullet("Marquez ce projet comme réalisé: Vous pourrez remplir le carnet de route et commencer à ajouter les photos du voyage à votre album!!")%>
		         </span>
		         <%} %>
		    </div>
            <div>
		        <ul class="projet-edit-menu">
		         
		            <li>
		                 <%= Html.ImageLink( "/Content/cross.png", "Supprimer", "Delete", "Projets", new { projetId = proj.IdProjet }, new { id = "del_" + proj.IdProjet, onclick="return confirmDelete();" })%>
		            </li>
		            <li>
		                 <%= Html.ImageLink("/Content/page_edit.png","Editer","Edit","Projets",new{projetId = proj.IdProjet}) %>
		            </li>
		               <%if (proj.IsOutDated()){ %>
		                <li>
		              
       		                 <%= Html.ImageLink( "/Content/tick.png", "Marquer comme réalisé", "Delete", "Projets", new { projetId = proj.IdProjet })%>
		                 Marquer comme Réalisé
		                </li>
		            <% } %>
		        </ul>
		    </div>
	</div>
	
	<div class="clear"> </div>
	
	<div id="projetItemInfos">
	
		<div id="dates"><!-- Dates -->
		<% var dateDepart = (proj.DateDebut.HasValue) ? proj.DateDebut.Value.ToShortDateString() : "Incertaine"; %>
		<% var dateRetour = (proj.DateFin.HasValue) ? proj.DateFin.Value.ToShortDateString() : "Incertaine"; %>

		Depart : <span class="gdgreen"><%= dateDepart %></span>
		<br/>
		Retour : <span class="gdgreen"><%= dateRetour%></span>
		<%if(proj.Incertain)  { %>
		<br />
		Nombre de jours: <span class="gdgreen"><%= proj.NbJours%></span>
		<%} %>
		</div>
		
		<div id="details"><!-- Infos -->
		<%
        ITransport trans = proj.Transports.FirstOrDefault();
        IResidence resi = proj.Residence;
      
        var minTransPrix = 0d;
        if(trans == null)
            trans = ViewData.Model.FakeTransport;
        else minTransPrix = proj.Transports.Min(t => t.PrixAR);
	    if(resi == null)
            resi = ViewData.Model.FakeResidence;
		 %>
		 <% //TODO: Detail lorsqu'on clique sur mode transports %>
		Par: <span class="gdgreen"><%= trans.ModeTransport%></span> <br/>
		Hebergement: <span class="gdgreen">(<%= resi.Type%>) <%= resi.Adresse%></span><br/>
		Coût estimé: <span class="gdgreen"><%= (minTransPrix + resi.Prix)  %>€ </span> (<%= minTransPrix + "+" + resi.Prix %>)
		</div>
		
		<div id="actions"><!-- subcribment -->
		<% var subsCount = proj.UserAbonnes.Count(); 
           var subsTextNull = "Personne ne vous accompagne pour l'instant";
           var subsTextUnique = "personne vous accompagne!";
           var subsText = "personnes vous accompagne!";
              switch(subsCount)
              {
                  case 1: subsText = subsTextUnique; break;
                  case 0: subsText = subsTextNull; break;
              }
        %>
		<span><%= (subsCount == 0) ? "" : subsCount.ToString() %></span> <%= subsText %>
		<br/>
		<%//TODO: Lien sur les messages privés concernant le voyage %>
		<%//TODO: comptabiliser ke les thread ou ke les messages reçu %>
		<% var relMsgCount = proj.MessagePrives.Count();  %>
		<% var msgLinklibelle = (relMsgCount > 1)?" messages privés concernent ce voyage" : " message privé concerne ce voyage"; %>
		<%= Html.ActionLink(relMsgCount + msgLinklibelle,"ProjetMessages","Messages",new{ projetId = proj.IdProjet},null)  %>
		</div>
    </div>
    <div class="clear">  </div>
        <div  id="stats-projet">
          <span class="minigray">Vu <%= proj.Visites %> fois.</span> - 
          <span class="minigray">Publié le: <%= proj.DateCreation.ToShortDateString() %></span> - 
          <span class="minigray">Modifié le: <%= proj.DateModification.ToShortDateString() %></span> 
        </div>
    </div>
 <% } %>

</div>