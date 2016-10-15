<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<covCake.Models.AlerteViewData>" %>

<%var alertList = this.Model.Alertes; %>

<% using (Html.BeginForm("DeleteAll", "Alertes", FormMethod.Post, new { id = "formDelAlerts" }))
   { %>
<table>

<thead>
        <tr>
            <th scope="col" style="width: 10px" >N°</th>
            <th scope="col">Pays</th>
            <th scope="col">Ville</th>
            <th scope="col">Date de départ</th>
            <th scope="col">Période départ</th>
            <th scope="col">Nombre de jours</th>
            <th scope="col">Créé le</th>
            <th scope="col"> </th>
            <th scope="col"><%= Html.CheckBox("checkAll")%></th>
        </tr>
       </thead>
       <tbody>
<% foreach (IAlerte alerte in alertList)
   {
%> 
 <% var moisDeb = alerte.PeriodeDebut.GetMonthString();
    var moiFin = alerte.PeriodeFin.GetMonthString();
    var periodeTxt = (moisDeb.IsNullOrEmpty() && moiFin.IsNullOrEmpty()) ? "Toutes" : moisDeb + " - " + moiFin; 
  %>
<tr>
    <td>
    <%= "n°" + alerte.IdAlerte%>
    </td>
    <td>
    <%= alerte.PaysArrive.Libelle().SwapIfEmpty("Tous")%>
    </td>
    <td>
    <%= alerte.VilleArrive.SwapIfEmpty("Toutes")%>
    </td>
     <td>
    <%= alerte.DateDebutProjet.ToShortDateString().SwapIfEmpty("Toutes")%>
    </td>
     <td>
      <%= periodeTxt%>
    </td>
     <td>
    <%= alerte.NbJours.ToString().SwapIfEmpty("--")%>
    </td>
    <td>
    <%= alerte.DateCreation.ToShortDateString()%>
    </td>
     <td>
        <%= Html.ImageLink("/Content/page_edit.png", "Editer", "Edit", "Alertes", new { alertId = alerte.IdAlerte })%>
    </td>
     <td>
     <%= Html.CheckBox("alertIds", new { value = alerte.IdAlerte, @class = "del" })%>
    </td>
</tr>
<%
    }
  
%>

</tbody>
</table>
<%} %>