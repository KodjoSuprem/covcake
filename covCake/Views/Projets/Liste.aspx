<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="covCake.BaseViewPage<covCake.Models.ListeProjetViewData>" %> 

<asp:Content ContentPlaceHolderID="DevStyleCSS" runat="server" >

<style type="text/css">
    
  
    
#searchbar
{
	-moz-border-radius: 5px;
    -webkit-border-radius: 5px;
	border-style: solid;
	border-color: #adc800;
	border-width: 4px;
	text-align: center;
    background-color: #ecf3c1; /*#e6f77b;*/
	
    padding: 6px;
}

#searchbar input
{
	float: left;
	margin-right: 5px;
} 
#searchbar label
{	
	float: left;
	margin-right: 3px;
	/*
	font-size: 18px;
	*/
}

#pays, #jours, #date
{
	float: left;
	padding-left: 7px;
	padding-right: 7px;
   
}


</style>


</asp:Content>

<asp:Content ID="css" ContentPlaceHolderID="head" runat="server">

<link rel="stylesheet" type="text/css" href="../Content/jquery.autocomplete.css" /> 
<link rel="stylesheet" type="text/css" href="../Content/jquery-ui-1.7.1.custom.css" /> 

<script type='text/javascript' src='../Scripts/jquery.autocomplete.min.js'></script> 
<script type='text/javascript' src='../Scripts/jquery-ui-1.7.1.custom.min.js'></script> 
<script type="text/javascript" src="../Scripts/ui.datepicker-fr.js"></script> 
</asp:Content>

<asp:Content ID="java" ContentPlaceHolderID="JavaScript" runat="server">

<script type="text/javascript">



    $(function() {


        $.datepicker.setDefaults($.extend({ showMonthAfterYear: false, changeMonth: true, changeYear: true }));
        $('#dateFin').datepicker($.datepicker.regional['fr']);


        $("#searchForm").submit(function() {
            if ($.trim($("#paysArrive").val()) == 'ex : Brésil')
                $("#paysArrive").val("");
            if ($.trim($("#nbJours").val()) == 'ex : 5')
                $("#nbJours").val('');
            return true;
        });



        $('#dateDebut').datepicker({
            changeMonth: true,
            changeYear: true
        });

        $(".userphoto").tooltip({
            track: true,
            delay: 0,
            fade: 300,
            showURL: false,

            bodyHandler: function() {
                return $("<img/>").attr("src", this.src);
            }
        });

        $("#paysArrive").autocomplete("/services/ListePays", {
            width: 260,
            selectFirst: false,
            minChars: 0
        });

        $("#paysArrive").click(function() {
            if ($.trim($("#paysArrive").val()) == 'ex : Brésil') {
                $("#paysArrive").val("");
                $("#paysArrive").css("color", "#000");
            }
        });


        
        $("#paysArrive").blur(function() {
            if ($.trim($("#paysArrive").val()) == '') {
                $("#paysArrive").val("ex : Brésil");
                $("#paysArrive").css("color", "#888");
            }
        });

        $("#nbJours").click(function() {
            if ($.trim($("#nbJours").val()) == 'ex : 5') {
                $("#nbJours").val("");
                $("#nbJours").css("color", "#000");
            }
        });

        $("#nbJours").blur(function() {
            if ($.trim($("#nbJours").val()) == '') {
                $("#nbJours").val("ex : 5");
                $("#nbJours").css("color", "#888");
            }
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2>Rechercher un voyage</h2>

<% Html.RenderPartial("SearchBar"); %>

<br />

<% if(Model.IsSearchResults) { %>
    <h2>Recherche : <%= Model.SearchParams.paysArrive %> <% if(Model.SearchParams.dateDebut != null) { Writer.Write(" départs autour du " + Model.SearchParams.dateDebut.Value.ToShortDateString()); } else if(Model.SearchParams.nbJours != null) Writer.Write(" Pendant "+ Model.SearchParams.nbJours.Value + " jours.");   %></h2>
<%} %>

<%= Html.ValidationSummary() %>

<% if( ViewData.ContainsKey("noresult") || Model.ListeProjets.TotalCount < 0 ) { %>
    <ul class="validation-summary-errors"> 
       <li>Il n'y a aucun résultats concernant votre recherche</li>
    </ul>
<% } %>

<div style="text-align: right; margin: 0px 8px Opx Opx;" ><span class="minigray"><%= Model.ListeProjets.TotalCount %> Voyage<%= (Model.ListeProjets.TotalCount > 1)? "s":"" %> pour cette recherche</span></div>
<br />
    <table class="projets" width="100%">
    <% foreach(IProjet proj in ViewData.Model.ListeProjets){ %>
        <tr onmouseover="this.className='hlrow';" onmouseout="this.className=';'">
            <td width="45%">
                <span style="float: left; margin-right: 8px;">
                    <img alt="" src="<%= Html.CountryFlagImgUrl(proj.PaysArrive,CovCakeHtmlHelper.FlagSize.s24) %>" />
                </span>
              
                <h2><%=Html.ProjetIndexLink(proj.GetProjectTitle(), proj)%></h2>
                <div style="float: left;">
                    <a href="<%= Url.UserProfileUrl(proj.OwnerUserProfile) %>">
                        <img class="userphoto" alt="Nophoto_woman_small" src="http://assets.drivemeup.fr/images/nophoto_woman_small.gif?1224115783"
                            style="padding-top: 3px;" border="0" />
                    </a>
                </div>
                <div style="padding: 10px 0px 0px 3px; float: left;">
                        <%= Html.UserProfileLink(proj.OwnerUserProfile) %>
                </div>
                <br />
                <div class="clear">
                <span class="minigray">Vu <%= proj.Visites %> fois.</span> - 
                <span class="minigray">Publié le: <%= proj.DateCreation.ToShortDateString() %></span>
                </div>
                <div class="clear"></div>
            </td>
            
            <td>
            
                Départ prévu le: <span class="gdgreen"><%= proj.GetShortDateStringOr() %></span> 
                <br/>
                <% var nbsubscribers = (proj.UserAbonnes != null) ? proj.UserAbonnes.Count() + 1 : 0; %>
                <% if(nbsubscribers > 0){
                       var subscriberslib = "personne est du voyage";
                       if(nbsubscribers > 1) subscriberslib = "personnes sont du voyage";
                %>
               
                <span class="gdgreen" ><%= nbsubscribers +" "+ subscriberslib%></span> !
                <br/>
                <%} %>
                <% var prix = (proj.Residence != null) ? proj.Residence.Prix : 0d; 
                    prix += (proj.Transports.FirstOrDefault() != null) ? proj.Transports.FirstOrDefault().PrixAR : 0d; %>
                Prix estimé (transport et logement) : <span class="gdgreen"> <%= prix %></span> €
                <br/>
                Durée du sejour: <span class="gdgreen"><%= proj.GetDuree().ToString() %></span>            
            </td>
            
            <td style="text-align: right;">
            <%= Html.RouteLink("Afficher le Voyage",CovCake.Routes.PROJETINDEX, new { projetId = proj.IdProjet },null) %>
            <br />
            <% if (!this.IsAuthenticated) { %>
                 <%= Html.LoginModal("Envoyer un message", this.Request.Url.PathAndQuery)%>
            <%}else if(proj.OwnerUserId != this.CurrentUserId){ %>
                <%=Html.ActionLink("Envoyer un message", "SendMessage", "Messages", new { pId = proj.IdProjet }, new { rel = "facebox" })%>
            <% } %>
            
            
            </td>
        </tr>
        <% } %>
    </table>
    
    <br />
    
    <div class="pagination">
        <%= Html.Paginator(this.Model.ListeProjets.TotalPages,this.Model.ListeProjets.PageIndex) %>
    </div>
</asp:Content>
