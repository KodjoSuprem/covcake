<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="covCake.Views.User.Index" %>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../../Content/styletab.css" rel="stylesheet" type="text/css" />
     <!--
         <link href="../../Scripts/jquery-tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
         <script type="text/javascript" src="../../Scripts/jquery-tooltip/jquery.tooltip.min.js" > </script>
     -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="server">

  <script type="text/javascript" >
      $(function() {
          $('#userphoto').tooltip({
              track: true,
              delay: 0,
              fade: 300,
              showURL: false,
              bodyHandler: function() {
                  return $("<img/>").attr("src", this.src);
              }
          });
      });
    
  </script>

</asp:Content>
<asp:Content ID="Css" ContentPlaceHolderID="DevStyleCSS" runat="server">
    <style type="text/css">
     #actions
        {
        	margin-left: auto;
        	float: right;
        }
        #userinfos
         {
         	float: left;
         }
        #projectlist
        {
        	
        }
        </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% this.SetNoCache();  %>
<% covCake.DataAccess.IUserProfile user = this.Model; %>

 
 <!-- DEBUT TABLE USER INFO-->
<div id="userinfos">
    <table >
    <tr>
        <td>
             <div class="userphoto">
                 <%= Html.Image(user.UserPhotoUrl(), "Photo de " + user.DisplayName, new { @id = "userphoto" })%>
             </div>
        </td>
        <td>
             <div id="textinfo">
             <h2> <%= (this.IsAuthenticated)? Html.ActionLink(this.Model.DisplayName, CovCake.Routes.MONCOMPTE):Html.UserProfileLink(user) %></h2>
                Sexe: <%=Html.UserSexeIcon(this.Model)%> <%=this.Model.SexeLibelle%><br />
                Age: <%=user.DateNaissance.GetAge() %> ans<br />
                Departement: <%=user.Departement.NomDept %> (<%= this.Model.Departement.NumDept%>)<br />
             </div>
         </td>
     </tr>
    </table>
</div>
 
 <div class="simplebox" id="actions">
  <div class="option" style="margin-right: 0px;">
   <h3></h3>
    <%if(this.IsAuthenticated)
              { %>
                <%= Html.ImageLink(Html.IconImageUrl("mail-forward.png"), "", "SendMessage", "Messages", new { toUid = user.UserId.Shrink() }, new { rel = "facebox" })%>
                <br />
                <%= Html.ActionLink("Envoyer un message à " + this.Model.DisplayName, "SendMessage", "Messages", new { toUid = user.UserId.Shrink() }, new { rel = "facebox" })%>            
                
                <% }else{%>
                <%= Html.ImageLink( Html.IconImageUrl("mail-forward.png"), "", "Login", "Account", new { fb = "true", returnUrl = this.Request.Url.PathAndQuery }, new { rel = "facebox" })%>
                <br />
                <%= Html.ActionLink("Envoyer un message à " + this.Model.DisplayName, "Login", "Account", new { fb = "true" , returnUrl = this.Request.Url.PathAndQuery }, new { rel = "facebox" })%> 
            <%} %>
</div>
    <div class="clear">  </div>
 
 </div>

    <div class="clear">  </div>


<br />

 <div class="simplebox" id="userdesc">
    <h3>Description</h3>
         <div id="userdesc-text">
            <%= user.Description%>
         </div>
 </div>

 <div >
   <h3><%= user.DisplayName %> participe aux voyages suivants</h3>
  <%if(user.Projets.Count() < 1){ %>
        <p><%= user.DisplayName%> ne participe pour l'instant à aucun voyage</p>
  <%} %>
    <table width="100%"  id="box-table-a"><!--  id="projectlist" -->
    <thead>
    	<tr>
        	<th scope="col">Destination</th>
            <th scope="col">Date de départ</th>
            <th scope="col">Durée</th>
            <th scope="col">Voyageurs</th>
            <th scope="col"></th>
        </tr>
    </thead>

       <% foreach(IProjet proj in user.Projets){        %>
       <tr>
        <td class="gdgreen">  <%= Html.CountryFlagImg(proj.PaysArrive,CovCakeHtmlHelper.FlagSize.s16) %> <%= proj.PaysArrive.LibellePays %>  </td>
        <td class="gdgreen"> <%= Html.IconImage("calendar.png","date départ") %> <%= (proj.DateDebut.HasValue) ? proj.DateDebut.ToShortDateString() : "--/--/----" %></td>
        <td class="gdgreen">  <%= Html.IconImage("time.png","Durée") %> <%= proj.NbJours %> jours </td>
        <td class="gdgreen">  <%= Html.IconImage("group.png", "participants")%> <%= proj.UserAbonnes.Count() %> co-voyageurs </td>
        <td class="gdgreen">  <%= Html.ProjetIndexLink("Voir les details",proj)%></td>
       </tr>
            
       <% // Writer.Write(proj.GetLongDisplayName());
              // Html.RenderPartial("BlockProjet",proj); 
              //Html.RenderPartial("SendMessageControl"); 
         }
       %>
</table>
 </div>    
    
    <div class="simplebox">
        <h3>Voyages déjà réalisés</h3>
      <%if(user.Projets.ProjetRealises().Count() < 1)      { %>
            <p><%= user.DisplayName%> n'a réalisé pour l'instant aucun voyage</p>
      <%} %>
   <% foreach(IProjet proj in user.Projets.ProjetRealises())    {        %>
    <tr>
        <td class="gdgreen">  <%= Html.IconImage("flag_green.png","Destination") %> <%= proj.PaysArrive.LibellePays %>  </td>
        <td class="gdgreen">  <%= Html.IconImage("calendar.png","date départ") %> <%= (proj.DateDebut.HasValue) ? proj.DateDebut.ToShortDateString() : "--/--/----" %></td>
        <td class="gdgreen">  <%= Html.IconImage("time.png","Durée") %> <%= proj.NbJours %> jours </td>
        <td class="gdgreen">  <%= Html.IconImage("group.png","participants") %> <%= proj.UserAbonnes.Count() %> co-voyageurs </td>
        <td class="gdgreen">  <%= Html.ProjetIndexLink("Voir les details",proj)%></td>
        <td class="gdgreen">  <%= Html.ActionLink("Voir l'album photo","ShowAlbum")%></td>
    </tr>
   <%} %>
  </div>
                   
            
</asp:Content>

<asp:Content ContentPlaceHolderID="AfterBodySection" runat="server">

<head>
<meta http-equiv="pragma" content="no-cache">
</head>
</asp:Content>