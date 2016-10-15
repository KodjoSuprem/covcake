<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"    Inherits="covCake.BaseViewPage<covCake.Models.CreateProjetViewData>" %>

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

            $('#dateDebut').datepicker({
                changeMonth: true,
                changeYear: true
            });

            var idPays = $("#paysArrive :selected").val();
            $("#villeArrive").autocomplete("/services/Geocode?codePays=" + <%= this.Model.Projet.IdPaysArrive %>, {
                width: 260,
                selectFirst: false,
                minChars: 2
            });
            $("#adresse").autocomplete("/services/Geocode?codePays=" + <%= this.Model.Projet.IdPaysArrive %>, {
                width: 260,
                selectFirst: false,
                minChars: 2
            });

            $("#deltrans").click(function(){
            
                $("#modetrans option:contains(Selectionnez)").attr("selected", true);
               
                $("#prixtrans").val("");
                    $("#numvol").val("");
                    $("#dateFin").val("");
             return false;
            
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
           // alert($("#incertain").val().toLowerCase());
            if ($("#incertain").val().toLowerCase() == "true")
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
        .btnclearfields
        {
        	float: left;
        }
        #blockNewProjet
        {
            
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

    
    <h1>Editer mon voyage : <%= this.Model.Projet.GetShortDisplayName() %></h1>
    <p>Modifier les informations qui concernent votre voyage. Vos compagnons seront averti des changements effectués.</p>
    <%= Html.ValidationSummary() %>
    
    <div id="blockNewProjet">
        <% using(Html.BeginForm()){ %>
        <fieldset>
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
		     
               <div >
		                <a id="deltrans" href="#"><%= Html.Image("/Content/cross.png", "supprimer")%> cliquez pour supprimer</a> 
		     </div>
		     <div class="clear">  </div>
            <%= Html.Label("Mode","modetrans") %>
            <%= Html.DropDownList("modetrans", 
    covCake.Services.TransportServices.ListeModesDefaultMessage("Selectionnez un mode de transport"))%>
            <!-- relatif AVION -->
            <div class="related" id="related-AVION">
                <div class="warning">
                    Ces informations ne seront visibles que par les personnes qui se joignent à vous ;)
                </div>
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
            <h3> Hebergement</h3>
            <%= Html.Label("Type","typeres") %>
            <%= Html.DropDownList("typeres", 
    covCake.Services.ResidenceServices.ListeTypeDefaultMessage("Selectionnez un type d'hebergement"))%>
            <!-- relatif Amis -->
            <div class="related" id="related-AMIS">
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
            <h3> Commentaires</h3>
            <p>
                Laissez quelques indications par exemple sur ce qui vous motive à partir dans ce
                pays
            </p>
            <%= Html.TextArea("commentaires")%>
        </fieldset>
        <%= Html.Hidden("projetid") %>
      <%= Html.cSubmit("Modifier") %>
        <%} %>
    </div>

  
</asp:Content>
