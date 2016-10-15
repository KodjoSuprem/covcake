<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true"
Inherits="covCake.BaseViewPage" %>

<asp:Content ID="css" ContentPlaceHolderID="head" runat="server">

<link rel="stylesheet" type="text/css" href="../Content/jquery.autocomplete.css" /> 
<link rel="stylesheet" type="text/css" href="../Content/jquery-ui-1.7.1.custom.css" /> 

<script type='text/javascript' src='../Scripts/jquery.autocomplete.min.js'></script> 
<script type='text/javascript' src='../Scripts/jquery-ui-1.7.1.custom.min.js'></script> 
<script type="text/javascript" src="../Scripts/ui.datepicker-fr.js"></script>
 
<% //this.Request.Url.LocalPath %>
</asp:Content>

<asp:Content ID="java" ContentPlaceHolderID="JavaScript" runat="server">

<script type="text/javascript">

    $(function() {


        $.datepicker.setDefaults($.extend({ showMonthAfterYear: false, changeMonth: true, changeYear: true }));
        $("#dateFin").datepicker($.datepicker.regional['fr']);

        $("#dateDebut").datepicker({
            changeMonth: true,
            changeYear: true
        });

        $("#dateDebut").change(function() {
            var dd = $("#dateDebut").val().split('/');
            var date = new Date(dd[1] + '/' + dd[0] + '/' + dd[2]); //$("#dateDebut").datepicker('getdate');
//            alert(date);

            $("#dateFin").datepicker("setDate", date);
        });
        //TODO: lorsque on select datedebut MAJ du datepicker dateFin


        var idPays = $("#paysArrive :selected").val();
        $("#villeArrive").autocomplete("/Services/Geocode?codePays=" + idPays, {
            width: 260,
            selectFirst: false,
            minChars: 2
        });
        $("#adresse").autocomplete("/Services/Geocode?codePays=" + idPays, {
            width: 260,
            selectFirst: false/*,
            minChars: 2*/
        });

        $("#showdate").click(function() {
            ShowDates();
            return false;
        });

        $("#showjours").click(function() {
            ShowIncertain();
            return false;
        });

        //        if ($("#incertain").val() == "")
        //            $("#incertain").val("false");
        if ($("#incertain").val() == "true")
            ShowIncertain();
        else
            ShowDates();

        function ShowDates() {
            $("#jours").hide("slow");
            $("#dates").show("slow");
            //on efface la duree quand on efface les dates
            $("#nbjours").val("");
            $("#incertain").val("false");
        }
        function ShowIncertain() {
            $("#jours").show("slow");
            $("#dates").hide("slow");
            //on efface les dates lorsque on selectionne la durée
            $("#dateDebut").val("");
            $("#dateFin").val("");
            $("#incertain").val("true");
        }

        //        $("#villeArrive").keyup(function() {
        //            // var url = "http://maps.google.com/maps/geo?q=1600+Amphitheatre+Parkway,+Mountain+View,+CA&output=json&oe=utf8&sensor=false&key=ABQIAAAANE808zIvEVkCKOlZZpqjlxQKmKCZGjN4B49v6XmOzHXUQ6SP-BSwmc7UDNHsmeWORa6g_ONFTZIYjg";
        //            var url = "/services/Geocode2";
        //            // alert(url);
        //            var txt = $("#villeArrive").val();
        //            $.getJSON(url, { q: txt }, function(rez) {

        //                var nbAdr = rez.Placemark.length;
        //                var tAdr = [];
        //                for (i = 0; i < nbAdr; i++) {
        //                    tAdr[i] = rez.Placemark[i].address;
        //                    alert(tAdr[i]);
        //                }

        //                $("#villeArrive").autocomplete(tAdr, {
        //                    width: 260,
        //                    selectFirst: false
        //                });

        //                $("#geo").text(rez.Placemark[0].address);
        //            });
        //        });



        /*
        $("#villeArrive").keyup(function{
        var beg =  $("#villeArrive").val();
       
    
    });
    
    .autocomplete("/", {
        width: 260,
        selectFirst: false
        });*/


        jQuery("#paysArrive").change(function() {
            var paysId = $("#paysArrive :selected").val();
            var dateDep = $("#dateDebut").val();
            var jours = $("#nbjours").val();
            $("#relatedProjList").hide();
            $.get(
                    "/Projets/GetRelatedProjets",
                    { paysArriveId: paysId, dateDebut: dateDep, nbJours: jours },
                    function(partialRez) { $("#relatedProjList").html(partialRez); $("#relatedProjList").show(); },
                    "html"
                );
        });

        jQuery("#paysDepartLnk").click(function() {
            jQuery("#paysDepartLnk").hide("slow");
            jQuery("#paysDepart").show("slow");
        });

        jQuery("#paysDepart").change(function() {
            jQuery("#paysDepartLnk").text(jQuery("#paysDepart :selected").text());
            jQuery("#paysDepartLnk").show("slow");
            jQuery("#paysDepart").hide("slow");

        });





        ToggleModeTrans(); //toggle on load
        ToggleTypeRes(); //toggle on load

        $("#modetrans").change(function() {
            // alert($("#typeres :selected").text().indexOf("Selectionnez"));
            if ($("#modetrans :selected").text().indexOf("Selectionnez") >= 0) {
                $("#prixtrans").val("");
                $("#numvol").val("");
                $("#dateFin").val("");
            }
            ToggleModeTrans();
        });

        $("#typeres").change(function() {
            //   alert($("#typeres :selected").text().indexOf("Selectionnez"));
            if ($("#typeres :selected").text().indexOf("Selectionnez") >= 0) {
                $("#maxhotes").val("");
                $("#prixres").val("");
            }
            ToggleTypeRes();
        });


    });

function ToggleTypeRes() {
    var texte = $("#typeres :selected").val().toUpperCase();

    if (texte == "AMIS" || texte == "CHEZ MOI")
        jQuery("#related-AMIS").show("slow");
    else
        jQuery("#related-AMIS").hide("slow"); //vider les champs texte
}

function ToggleModeTrans() {
    var texte = $("#modetrans :selected").val().toUpperCase();
    if (texte == "AVION")
        jQuery("#related-AVION").show("slow");
    else
        jQuery("#related-AVION").hide("slow"); //vider les champs textes
}
/*
    function SetVisibleRelated(select) {
        texte = select.value.toUpperCase();

        if (select.name == "modetrans") {
            if (texte == "AVION")
                jQuery("#related-AVION").show("slow");
            else {
                jQuery("#related-AVION").hide("slow"); //vider les champs textes
            }
        }
        else {
            if (texte == "AMIS" || texte == "CHEZ MOI")
                jQuery("#related-AMIS").show("slow");
            else
            {
                jQuery("#related-AMIS").hide("slow"); //vider les champs texte
                
                }
        }
    } 
    */
</script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="DevStyleCSS" runat="server">

<style type="text/css">
    

    
#blockNewProjet
{
     float: left;
}    

#similarProjets
{

}

div.related
{
    display: none;
}




</style>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<%//TODO: Verifications, callendar, check same Voyages %>

<div id="geo"></div>
<h1>Nouveau Voyage</h1>

  <%= Html.ValidationSummary() %>
  <!--
<%if(Html.ValidationSummary() != null){ %>
    <div class="validationsummary">
        <%= Html.ValidationSummary() %>
    </div>
<%} %>
-->
<div id="blockNewProjet">
<% using(Html.BeginForm())   { %>
<fieldset>
<h3>Destination</h3>
<p>Pays de départ : <a id="paysDepartLnk" href="#">France</a>  <%= Html.DropDownList("paysDepart", covCake.Services.PaysServices.ListePays(), new { style = "display: none;" })%> </p>

<%= Html.Label("Pays", "paysArrive")%>
<%= Html.DropDownList("paysArrive", covCake.Services.PaysServices.PaysDestination())%> 
<%= Html.ValidationMessage("paysArrive")%>

<%= Html.Label("Ville de sejour", "villeArrive")%>
<%= Html.cTextBox("villeArrive")%>
<%= Html.ValidationMessage("villeArrive")%>

<div id="dates">
<%= Html.Label("Date de départ prévue", "dateDebut")%>
<%= Html.cTextBox("dateDebut")%>
<%= Html.ValidationMessage("dateDebut")%>

<%= Html.Label("Date de retour prévue", "dateFin")%>
<%= Html.cTextBox("dateFin")%>
<%= Html.ValidationMessage("dateFin")%>

<%= Html.Label("Pas sûr??", "incertain")%>
<a id="showjours" href="#">Saisissez la durée souhaitée du voyage</a>
</div>


<div id="jours" style="display: none;">
<%= Html.Label("Combien de jours dure votre voyage","jours") %>
<%= Html.cTextBox("nbjours")%>
<%= Html.ValidationMessage("nbjours")%>

<%= Html.Label("Vous connaissez les dates?", "incertain")%>
<a id="showdate" href="#">Saisissez les dates du voyages</a>
<%= Html.Hidden("incertain") %>
</div>
</fieldset> 

<fieldset>
<h3>Moyen de transport</h3>

<%= Html.Label("Mode","modetrans") %>
<%= Html.DropDownList("modetrans",
        covCake.Services.TransportServices.ListeModes(), "Selectionnez un mode de transport")%>

<!-- relatif AVION -->
<div class="related" id="related-AVION"  >
<div class="warning">Ces informations ne seront visibles que par les personnes qui se joignent à vous ;)</div>
<%= Html.Label("Numero de Vol","numvol") %>
<%= Html.cTextBox("numvol") %>

<%= Html.Label("Heure de départ","heuredDepart") %>
<%= Html.cTextBox("heureDepart")%>
<%= Html.ValidationMessage("heureDepart")%>
</div>
<%= Html.Label("Prix estimé","prixtrans") %>
<%= Html.cTextBox("prixtrans")%> €
<%= Html.ValidationMessage("prixtrans")%>
</fieldset>


<fieldset>
<h3>Hebergement</h3>
<%= Html.Label("Type","typeres") %>
<%= Html.DropDownList("typeres",
        covCake.Services.ResidenceServices.ListeType(), "Selectionnez un mode de transport")%>
<!-- relatif Amis -->
<div class="related" id="related-AMIS" >
<%= Html.Label("Nombre Maximum de personne pouvant être ebergées","maxhotes") %>
<%= Html.cTextBox("maxhotes") %>
</div>

<%= Html.Label("Prix estimé", "prixres")%>
<%= Html.cTextBox("prixres")%> €
<%= Html.ValidationMessage("prixres")%>
<!-- relatif Amis -->

<%= Html.Label("Adresse", "adresse")%>
<%= Html.TextArea("adresse")%> 
<%= Html.ValidationMessage("adresse")%>

</fieldset>
<fieldset>
<h3>Commentaires</h3>
<% // Html.Label("Commentaires", "commentaires")%>
<p>Laissez quelques indications par exemple sur ce qui vous motive à partir dans ce pays</p>
<%= Html.TextArea("commentaires")%> 
</fieldset>

<%= Html.Submit("Créer") %>
<%} %>
</div>
<%//TODO: Destinations similaires + popup context javascript %>
<div class="simplebox" id="similarProjets">
<h3>Projets similaires</h3>
<p>
Certaines personnes souhaitent peut être se rendre au même endroit que vous.<br/>
Jettez d'abord un &oelig;uil sur leurs projets et rejoignez les  ;).
</p>

<div id="relatedProjList"></div>

</div>
</asp:Content>
