<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="covCake.BaseViewPage<covCake.Models.AlerteViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Alertes email</h2>

<div>
Gr�ce aux alertes vous recevez par email les projets de voyage qui correspondent � vos crit�res.<br />
ex: <br />
    -Pour recevoir tout les derniers voyages ne sp�cifiez rien. <br />
    -Pour tout les d�parts vers le Br�sil entre Juin et Octobre sp�cifiez uniquement les champs apropri�s
</div>
<br />
<% using (Html.BeginForm()) { %>

<fieldset>
<h3>Destination</h3>
<!--
<input type="text" disabled="disabled" value="test"/>
-->
<%= Html.Label("Pays", "paysArrive")%>
<%= Html.DropDownList("paysArrive", covCake.Services.PaysServices.PaysDestination(),"Tous")%> 
<%= Html.ValidationMessage("paysArrive")%>
<br />
<%= Html.Label("Ville", "villeArrive")%>
<%= Html.cTextBox("villeArrive") %> 
<%= Html.ValidationMessage("villeArrive")%>

</fieldset>

<fieldset>
<h3>Date de d�part</h3>

<%= Html.Label("Date de d�part", "dateDebut")%>
<%= Html.cTextBox("dateDebut")%> 
<%= Html.ValidationMessage("dateDebut")%>


<h3>P�riode des d�parts</h3>
<%= Html.Label("Entre", "moisDebut")%>
<%= Html.DropDownList("moisDebut", covCake.Services.CovCakeServices.GetDateNaissSelector().Mois, "") %> 
<%= Html.ValidationMessage("moisDebut")%>

<%= Html.Label("Et", "moisFin")%>
<%= Html.DropDownList("moisFin", covCake.Services.CovCakeServices.GetDateNaissSelector().Mois, "")%> 
<%= Html.ValidationMessage("moisFin")%>

<h3>Dur�e du s�jour</h3>
<%= Html.Label("Nombre de jours", "nbJours")%>
<%= Html.cTextBox("nbJours")%> 
<%= Html.ValidationMessage("nbJours")%>
</fieldset>

<%= Html.Submit("Enregistrer cette alerte", new { @class = "button" })%>

<%} %>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../Content/jquery-ui-1.7.1.custom.css" /> 
    <script type='text/javascript' src='../Scripts/jquery-ui-1.7.1.custom.min.js'></script> 
    <script type="text/javascript" src="../Scripts/ui.datepicker-fr.js"></script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="server">

<script type="text/javascript">

    function CleanPeriodeSelectors(block) {
            $("#moisDebut").val("");
            $("#moisFin").val("");
    }
    function CleanDateDebutSelector(block) {
        $("#dateDebut").val(""); 
               }

    $(function() {
        $("#dateDebut").change(function() {

            if ($("#dateDebut").val() != "") {
                CleanPeriodeSelectors();
            }
        });

        $("#moisDebut").change(function() {
            CleanDateDebutSelector();
        });

        $("#moisFin").change(function() {
            CleanDateDebutSelector();
        });
    });
    
    $.datepicker.setDefaults($.extend({ showMonthAfterYear: false, changeMonth: true, changeYear: true }));
       
    $(function() {
        $('#dateDebut').datepicker($.datepicker.regional['fr']);
    });
        
</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="DevStyleCSS" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="AfterBodySection" runat="server">
</asp:Content>
