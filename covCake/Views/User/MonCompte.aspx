<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="MonCompte.aspx.cs" Inherits="covCake.Views.User.MonCompte" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
  <script type="text/javascript" src="../../Scripts/editinplace/jquery.inplace.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="JavaScript" runat="server">
<script type="text/javascript">
function beginUserProjetLists(args) {
        // Highlight selected group
        // $('#leftColumn li').removeClass('selected');
        // $(this).parent().addClass('selected');

        // Animate
        $('#projets').hide('normal');
        // $("#errorloadlist").hide();

    }

  
    function successUserProjetLists() {
        // Animate
        $('#projets').show('normal');
        //$("#errorloadlist").hide();

    }

    function failureUserProjetLists() {
        $("#errorloadlist").show('normal');
        
        alert("Impossible de charger la page");
    }
function confirmDelete()
{
 return confirm("Etes vous sur de vouloir supprimer ce voyage?");
}

    $(function() {
        $("#userdescription-edit").editInPlace({
            url: "/User/ChangeUserDesc", //"http://www.davehauenstein.com/code/jquery-edit-in-place/example/",
            params: "ajax=yes",
            bg_over: "#f3ffaf",
            field_type: "textarea",
            default_text: "** Ecrivez quelques mots à propos de vous :) **",
            saving_image: "/Content/ajax-loader.gif"
        });
    });
</script>
</asp:Content>
<asp:Content ID="css" ContentPlaceHolderID="DevStyleCSS" runat="server">
<style type="text/css">


.projettitle
{
    float: left;	
}




#projetItemInfos
{
	margin-bottom: 5px;
}


#actions, #details, #dates
{
	float: left;
	margin-right: 10px;
}

#actions
{
    margin-right: 0px;
}
#details
{
	width: 50%;
}


#dates
{
	margin-right: 20px;
	
}
#stats-projet
{
	border-bottom: 1px solid #aaa;
	padding: 0px 0px 3px 0px;
	margin: 0;
}

div.subsclist h2
{
    color: #ff6d00;
}

div.subsclist span.gdgreen
{
    color: #ff6d00;
}
#userinfos
{

}

#userdescription
{/*
	background-color: #aaf1f9;
	width: 300px;*/
}

#userdescription-edit
{
	min-height: 25px;
}

#messages
{
	
}

.inplace_field
{
	width: 100%;
	height: 100%;
}


div.userphoto img
{
	width: 120px;
	text-align: center;
	/*float: left; border: 1px #555 solid;*/
	background-color: #fff;
	margin-right: 10px;
}



span.news
{
    color: #f79537;
    font-size: 10pt;
    font-weight: bold;


	background-image: url(/Content/software-update-available.png);
	background-position: left 40%;
	background-repeat: no-repeat;
	padding-left: 22px;
	
}

span.nonews
{
    color: #c0c0c0;
    /*font-size: 10pt;*/
    font-weight: bold;
    
    background-image: url(/Content/gray_software-update-available.png);
	background-position: left 40%;
	background-repeat: no-repeat;
	padding-left: 22px;
}



</style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%
  //TODO: Avertir lorsque projet depassé
  //TODO: Syeteme d'alerte quand quelqun poste une destination   
%>
<% IUserProfile user = CurrentUser; %>
<!-- DEBUT TABLE USER INFO-->

<div class="left-container">
<div id="userinfos">
<table >
<tr>
<td>
 <div class="userphoto">
  <%= Html.ImageLink( user.UserPhotoUrl() ,"Cliquer pour changer ma photo","EditInfos") %>
 </div>
</td>
<td>
 <div id="textinfo">
 <h2> <%= Html.ActionLink(user.DisplayName,"EditInfos")  %></h2>
    Sexe: <%=Html.UserSexeIcon(user) %> <%=user.SexeLibelle %><br />
    Age: <%=user.DateNaissance.GetAge() %> ans<br />
    Département: <%=user.Departement.NomDept %> (<%= user.Departement.NumDept %>)<br />
 </div>
 </td>
 </tr>
</table>
</div>
</div>

<div class="right-container">

<div class="simplebox" id="options">
<h3></h3>

<div class="option">
    <%= Html.ImageLink( Html.IconImageUrl("travel-48x48.png"), "", "Create", "Projets", null, null, new { width = 32, height = 32 })%>
<br />
    <strong><%= Html.ActionLink("Nouveau voyage","Create","Projets") %></strong>
</div>
<div class="option">
    <%= Html.ImageLink( Html.IconImageUrl("mail-message-new.png"), "", "Index", "Messages")%> 
    <br />
    <%= Html.ActionLink("Mes Messages", "Index","Messages")%>
</div>
<div class="option">
    <%= Html.ImageLink( Html.IconImageUrl("contact-new.png"), "", "EditInfos")%>
    <br />
    <%= Html.ActionLink("Mes informations", "EditInfos") %>
</div>
<div class="option" style="margin-right: 0px;">
    <%= Html.ImageLink( Html.IconImageUrl("emblem-important.png"), "", "Index", "Alertes")%>
    <br />
    <%= Html.ActionLink("Mes alertes","Index", "Alertes") %>
</div>

<div class="clear">  </div>
</div>
</div>

<!-- FIN TABLE USER INFO -->

<div class="clear"> </div>

<br />
<%= Html.ValidationSummary() %>
        

<%if(ViewData.ContainsKey("ViewInfos")){ %>
      <%= Html.UnorderedList(ViewData["ViewInfos"] as IEnumerable<string>, new { @class = "validation-summary-infos" })%>
<% } %>


<!--     NEWS      -->
<%//TODO: Page Profile: News: -- Nb Abonnés, Nb %>
<div class="simplebox" id="Div1">
<h3>News depuis votre derniere connexion</h3>

<% var newMsgCount = this.Model.NewsNewMessages.Count(); %>

<% var newAbonnesCount = this.Model.NewsAbonnes.Count(); %>
<% var newMatesCount  = this.Model.NewsMates.Count(); %>
<% var newSimilarProjetsCount  = this.Model.NewsSimilarProjets.Count(); %>

<% var newMatesLibelle = (newMatesCount < 1)?"Personne va voyager avec vous! ": "Personnes  vont voyager avec vous!" ; %>
<% var newAbonnesLibelle = (newAbonnesCount < 1) ? "Nouvelle personne s'est joint à vos projets!" : "Nouvelles personnes ont joints vos projets!"; %>
<% var newMsgLibelle = (newMsgCount < 1) ? "Nouveau message!" : "Nouveaux messages!"; %>
<% var newSimilarProjetsLibelle = (newSimilarProjetsCount == 1) ? "Projet silimaire aux votres a été créé!" : "Projets silimaires aux votres ont été créés!"; %>

<%var nonews = "no"; %>
<div class="left-container">

<% if(newMsgCount > 0)  nonews = ""; else nonews = "no"; %>
 <span class="<%= nonews %>news"> <%= newMsgCount %> <%=newMsgLibelle%></span>
 
<br />

<% if(newMatesCount > 0) nonews = ""; else  nonews = "no"; %>
    <span class="<%= nonews %>news" ><%= newMatesCount %> <%= newMatesLibelle %></span>
<br />
</div>

<div class="right-container">
<% if(newAbonnesCount > 0) nonews = ""; else nonews = "no";  %>
 <span class="<%= nonews%>news" ><%= newAbonnesCount %> <%=newAbonnesLibelle %></span>
<br />

<% if(newSimilarProjetsCount > 0) nonews = ""; else nonews = "no";%>
 <span class="<%= nonews%>news" ><%= newSimilarProjetsCount %> <%=newSimilarProjetsLibelle %></span>
</div>

<div class="clear"> </div>
</div>

<!-- _____________ -->


<div class="left-container">

<div class="simplebox" id="messages">
<h3>Messages privés</h3>
<% var msgCount = user.MessagesRecus.GetAllReceiverUnreadedMessages().Count().ToString(); %>
<span class="newmessages"> Vous avez <%= Html.ActionLink(msgCount, "Index", "Messages")%> nouveau(x) message(s) - 
<%= Html.ActionLink("Consulter","Index","Messages") %></span>
</div>


</div>
<!-- DEBUT TABLE MAIL-DESCRIPTION -->

<div class="right-container">

<div class="simplebox" id="userdescription">
<h3>Description Personnelle <%= Html.HelpBullet("Cliquez sur votre description pour l éditer") %></h3>
<div id="userdescription-edit" >
<% if (string.IsNullOrEmpty(user.Description))
   { %>
** Ecrivez quelques mots à propos de vous :) **
<%} %>
    <%= user.Description %>
</div>
</div>
</div>
<!-- FIN TABLE MAIL-DESCRIPTION -->

<div class="clear">  </div>
<br />


<!-- Liste des projets-->
<div id="listselector">
<% var UserProjIconUrl = Html.IconImageUrl("travel-48x48.png"); %>
<% var UserSubscIconUrl = Html.IconImageUrl("knapsack-48x48.png"); %>
<%   %>
<%= Ajax.ImageLink(UserProjIconUrl, "mes voyages", "UserProjetsList", "User", new AjaxOptions() { UpdateTargetId = "projets", HttpMethod = "GET", OnFailure = "failureUserProjetLists", OnBegin = "beginUserProjetLists", OnSuccess = "successUserProjetLists" }, null, new { width = 32, height = 32 })%>
<%= Ajax.ActionLink("Mes Voyages", "UserProjetsList", new { }, new AjaxOptions() { UpdateTargetId = "projets", HttpMethod = "GET", OnFailure = "failureUserProjetLists", OnBegin = "beginUserProjetLists", OnSuccess = "successUserProjetLists" })%>

<%= Ajax.ImageLink(UserSubscIconUrl, "", "UserAbonnementsList", "User", new AjaxOptions() { UpdateTargetId = "projets", HttpMethod = "GET", OnFailure = "failureUserProjetLists", OnBegin = "beginUserProjetLists", OnSuccess = "successUserProjetLists" }, null, new { width = 32, height = 32 })%>
<%= Ajax.ActionLink("Mes Participations", "UserAbonnementsList", new { }, new AjaxOptions() { UpdateTargetId = "projets", HttpMethod = "GET", OnFailure = "failureUserProjetLists", OnBegin = "beginUserProjetLists", OnSuccess = "successUserProjetLists" })%>

<%= Ajax.ImageLink(UserSubscIconUrl, "", "UserAbonnesList", "User", new AjaxOptions() { UpdateTargetId = "projets", HttpMethod = "GET", OnFailure = "failureUserProjetLists", OnBegin = "beginUserProjetLists", OnSuccess = "successUserProjetLists" }, null, new { width = 32, height = 32 })%>
<%= Ajax.ActionLink("Participants à mes projets", "UserAbonnesList", new { }, new AjaxOptions() { UpdateTargetId = "projets", HttpMethod = "GET", OnFailure = "failureUserProjetLists", OnBegin = "beginUserProjetLists", OnSuccess = "successUserProjetLists" })%>


</div>

<br />

<div id="projets">

<% Html.RenderPartial("UserProjetsList",this.Model); %>

<div id="errorloadlist" style="display: none;">
<%= Html.IconImage("cancel_48.png") %>  Impossible de charger les données

</div>
</div>

</asp:Content>


