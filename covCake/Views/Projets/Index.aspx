<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true"
    Inherits="covCake.BaseViewPage<covCake.Models.IndexProjetViewData>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
<% IProjet projet = this.Model.Projet;  %>
<%  var comFbText = "Trouvez vos compagnons de voyage pour en " + projet.PaysArrive.LibellePays + " et plein d'autres destinations sur CoVoyage.net"; %>
<meta name="description" id="metaDescription" content="<%= comFbText %>" />
<link rel="stylesheet" href="../../Content/prettyPhoto.css" type="text/css" media="screen" charset="utf-8" />
<script type="text/javascript" src="../../Scripts/jCarouselLite/jcarousellite_1.0.1.min.js"></script>
<script type="text/javascript" src="../../Scripts/prettyphoto/jquery.prettyPhoto.js"></script>
<!--
<script type="text/javascript" src="http://www.google.com/jsapi?key=ABQIAAAANE808zIvEVkCKOlZZpqjlxQKmKCZGjN4B49v6XmOzHXUQ6SP-BSwmc7UDNHsmeWORa6g_ONFTZIYjg"></script>
-->
<script type="text/javascript" src="http://maps.google.com/maps?file=api&hl=fr&v=2&sensor=false&key=ABQIAAAANE808zIvEVkCKOlZZpqjlxQKmKCZGjN4B49v6XmOzHXUQ6SP-BSwmc7UDNHsmeWORa6g_ONFTZIYjg"></script>


<% var fbTitle = projet.GetFriendDisplayName().Replace("Voyage vers", "Départ pour");  %>
<script type="text/javascript">

function fbs_click() {
    u=location.href;
    t= "<%= fbTitle %>";//document.title;
    window.open('http://www.facebook.com/sharer.php?u=' + encodeURIComponent(u) + '&t=' + encodeURIComponent(t), 'sharer', 'toolbar=0,status=0,width=626,height=436');
    return false;
}


    $(document).ready(InitGmap);
    $(document).unload(GUnload);

    $(function() {
    
        //mettre prettyPhoto APRES jCarouselLite
        $("#paysimgslider").jCarouselLite({
            //btnNext: ".next",
            //btnPrev: ".prev",
            visible: 2,
            auto: 5000
        });
        $("a[rel^='prettyPhoto']").prettyPhoto();
       // InitGmap();
    });
    

/*
    function LoadMapSearchControl() {
        if (true) {
            var options = {
                zoomControl: GSmapSearchControl.ZOOM_CONTROL_ENABLE_ALL,
                title: "Voyages &agrave;...",
                url: "index.php",
                idleMapZoom: GSmapSearchControl.ACTIVE_MAP_ZOOM - 9,
                activeMapZoom: GSmapSearchControl.ACTIVE_MAP_ZOOM - 9,
                mapTypeControl: GSmapSearchControl.MAP_TYPE_ENABLE_ALL

            }

            new GSmapSearchControl(
            document.getElementById("carte"),
            "Monrovia,Libéria",
            options
            );

        }

    }
    GMap.setOnLoadCallback(LoadMapSearchControl);
    
    */

    function InitGmap() {
       // alert("bien ou bien???");
        if (GBrowserIsCompatible()) {
            var carte = new GMap2(document.getElementById("carte"));
            carte.disableScrollWheelZoom();
            carte.addControl(new GMapTypeControl());
            carte.addControl(new GSmallMapControl());
            var address = "<%= projet.PaysArrive.CapitalePaysEng.SwapIfEmpty(projet.PaysArrive.LibelleEngPays) %>"; // "<%= projet.PaysArrive.LibellePays %>";  //"<%= projet.GetGeolocAddress() %>";
            var geocoder = new GClientGeocoder();

            geocoder.getLocations(address,
                                  function(reponse) {
                                      if (!reponse || reponse.Status.code != 200)
                                          alert(address + " introuvable");
                                      else {
                                          var place = reponse.Placemark[0];
                                          var lat = place.Point.coordinates[1];
                                          var lng = place.Point.coordinates[0];
                                          var point = new GLatLng(lat, lng);
                                          carte.setCenter(point, 5);
                                          var marker = new GMarker(point);

                                          carte.addOverlay(marker);
                                          carte.addOverlay(new GLayer("com.panoramio.popular"));//"com.panoramio.all"));

                                      }
                                  }
             );
        }
    }
    /*
    google.load("maps", "2");
    function InitGmap() {
        if (google.maps.BrowserIsCompatible()) {

            var carte = new google.maps.Map2(document.getElementById("carte"));
            // directionsPanel = document.getElementById("directions");
            //directions = new GDirections(map, directionsPanel);
            // dir = "from: 49.1847122 -0.3601488 to: 49.4292901 2.0810689";
            // directions.load(dir, { "locale": "fr" });
           // carte.enableScrollWheelZoom();
            carte.disableScrollWheelZoom();
            carte.addControl(new google.maps.MapTypeControl());
            carte.addControl(new google.maps.SmallMapControl());
            
         //   <% //var gMapPt = projet.GetLatLong(); %>
         //   var point = GLatLng(<%//= gMapPt.X  %>, <%//= gMapPt.Y %>);
            var address = "<%= projet.PaysArrive.CapitalePaysEng.SwapIfEmpty(projet.PaysArrive.LibelleEngPays) %>"; // "<%= projet.PaysArrive.LibellePays %>";  //"<%= projet.GetGeolocAddress() %>";
            var geocoder = new google.maps.ClientGeocoder();
           // alert(address);
            geocoder.getLocations(address,
                  function(reponse) {
                      if (!reponse || reponse.Status.code != 200)
                          alert(address + " introuvable");
                      else {
                          var place = reponse.Placemark[0];
                          var lat = place.Point.coordinates[1];
                          var lng = place.Point.coordinates[0];
                          //carte.setZoom(carte.getZoom() - 9);
                     //     var visibilite = new GLatLngBounds();
                         
                          //carte.setCenter(point, 13);
                         // alert(reponse);
                          var point = new google.maps.LatLng(lat, lng);
                          carte.setCenter(point, 5);
                          //... Complète "visibilite" avec le point nommé "point" ... 
                         // visibilite.extend(point);
                           ... Création d'un nouveau marqueur nommé "marker" ancré sur le point nommé "point" et repésenté par l'icône nommé "miniconerouge ... 
                          var marker = new google.maps.Marker(point);

                          carte.addOverlay(marker);
                          carte.addOverlay(new GLayer("com.panoramio.popular"));//"com.panoramio.all"));
                         // marker.openInfoWindowHtml(place.address);
                        
                      }
                  });

        }
    }
    
    google.setOnLoadCallback(InitGmap);*/
</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">



</asp:Content>

<asp:Content ID="Css" ContentPlaceHolderID="DevStyleCSS" runat="server">

<!-- FaceBook button CSS-->
<style type="text/css"> html .fb_share_button { display: -moz-inline-block; display: inline; padding:1px 20px 0 5px; height:15px; border:1px solid #d8dfea; background:url(http://static.ak.fbcdn.net/rsrc.php/z39E0/hash/ya8q506x.gif) no-repeat top right; } html .fb_share_button:hover { color:#fff; border-color:#295582; background:#3b5998 url(http://static.ak.fbcdn.net/rsrc.php/z39E0/hash/ya8q506x.gif) no-repeat top right; text-decoration:none; } </style> 

    <style type="text/css">

        li.subscribe
        {
        	  padding-left: 10px;
              margin-left: -20px;
            list-style-image: url(/Content/group_go.png);
        }
        
        
        #projetinfos
        {
        	float: left;
        }
     
        #actions
        {
        	margin-left: auto;
        	float: right;
        }
        
        #carte
        {
        	width: 100%;
        	height: 400px;
        	background-position: 50% 50%;
	        background-repeat: no-repeat;
	        background-image: url(/Content/ajax-loader.gif);
        }
        #infosDetailsTransport
        {
        	
	        /* 
	            width: 47%;
	        */
	      
        }
        
        #infosDetailsHebergement
        {
        	/* 
            float:left;
            width:47%;
            margin: 0pt;
            margin-right: 6px;
            overflow: auto;
           */

        }
        
        #infosProjet
        {
            width:358px;
            height:390px;
            margin: 0 0 0 10px;
            font-size:100%;
        }
        
        .box h4
        {
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 5px;
            margin-left: 0px;
            color: #490050;
            font-size: 120%;
        }
        .list_userphoto
        {
            float: left;
            margin-right: 8px;
        }
        .list_avatar, .list_user_name
        {
            float: left;
            padding-top: 6px;
        }
        
        #comments
        {
        	
        }
        
        #aboList
        {
           
        }
        
      
        .list_user_item
        {

            padding: 3px;
            display: block;
        }
        .list_user_container
        {

            padding: 3px;
        }
       
       
       span.subscribe
       {
       	    color: Green;
            /*font-size: 10pt;*/
            font-weight: bold;
            
            background-image: url(/Content/tick.png);
	        background-position: left 40%;
	        background-repeat: no-repeat;
	        padding-left: 22px;
       }
       
       
       #paysphotocontainer
       {
  
        margin: 8px;
          float: left;
          width: 30%;

       }
           #mapcontainer
       {
       	      float: right;
       	width: 60%;

       }
       #paysimgslider li
        {
	     margin: 10px;
        }
       
   
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% IProjet projet = this.Model.Projet;  %>
<% covCake.Services.FlickrPhotos flickrPics = this.Model.FlickrPictures;  %>


    <h2>
         <%= projet.GetDisplayName()%>
    </h2>
    
    <%= ViewData["errorMsg"] %>
    <%= Html.ValidationSummary() %>   
    <%= Html.ViewInfosSummary() %>

               <div id="projetinfos">
               <table>
             <tr>
             <td>
                        <div id="flagpaysprojet">
                            <a href="#" rel="facebox">
                                <img src="<%= Html.CountryFlagImgUrl(projet.PaysArrive) %>" alt="<%= projet.PaysArrive.LibellePays %>" />
                            </a>
                        </div>
                       
                       </td>
                       <td>
                            <div id="generalprojetinfos">
                                <ul style="list-style: none;">
                                    <li>
                                        Créateur : <%= Html.UserProfileLink(projet.OwnerUserProfile) %>
                                    </li>
                                    <li>
                                        Départ prévu le: <span class="gdgreen"><%= (projet.DateDebut.HasValue) ? projet.DateDebut.Value.ToShortDateString() : "Incertain" %></span> 
                                    </li>
                                    <li>
                                        Retour prévu le: <span class="gdgreen"><%= (projet.DateFin.HasValue) ? projet.DateFin.Value.ToShortDateString() : "Incertain"%></span> 
                                    </li>
                                  
                                </ul>
                            </div>
                            </td>
                            </tr>
                               </table>
                               
                               <div>
                               <!--
                               <a href="http://www.facebook.com/share.php?u=<%= this.Request.Url.ToString()%>" class="fb_share_button" onclick="return fbs_click()" target="_blank" style="text-decoration:none;">Partager sur facebook</a>
                                -->
                               <%= Html.ImageLink(Html.IconImageUrl("emblem-important16.png"),"Créer un alerte","Index", "Alertes") %> Créer une alerte
                               <br />
                               <%= Html.FacebookPublishLink(this.Request.Url.ToString(),"Partager sur facebook","fbs_click()") %>
                                
                               </div>
                             
                             <div>
                                <a href="#" >Informations sur <%= projet.PaysArrive.LibellePays %></a>
                             </div>
                </div>
            
            
                <div class="simplebox" id="actions">
                    <h3></h3>
                    <%if(projet.OwnerUserId == this.CurrentUserId)
                      { %>
                    <div class="option" >
                        <%= Html.ImageLink( Html.IconImageUrl("accessories-text-editor.png"), "", "Edit", "Projets", new { projetId = projet.IdProjet }, null)%>
                    <br />
                        <%= Html.ActionLink("Editer ce voyage", "Edit", "Projets", new { projetId = projet.IdProjet }, null)%>
                    </div>
                    <%}
                      else
                      { %>
                     <div class="option" >
                
                 <%if(this.IsAuthenticated)
                   {%>    
                             <% //TODO: faire le lien pr delete subscription en utilisant : projet.UserAbonnes.HaveSubscribed(this.CurrentUserId); %>
                                
                          <% if (this.CurrentUser.ParticipeA(projet))
                          {
                          %>
                           <%= Html.ImageLink( Html.IconImageUrl("cross_48.png"), "", "UnSubscribe", "Projets", new { projetId = projet.IdProjet }, null, new { width = 32, height = 32 })%>
                             <br />
                             <span class="unsubscribe"><%= Html.ActionLink("Ne plus participer à ce voyage", "UnSubscribe", "Projets", new { projetId = projet.IdProjet }, null)%></span>
                    
                        <%} else { %>
                             <%= Html.ImageLink( Html.IconImageUrl("knapsack-48x48.png"), "", "Subscribe", "Projets", new { projetId = projet.IdProjet }, null, new { width = 32, height = 32 })%>
                             <br />
                             <span class="subscribe"><%= Html.ActionLink("Participer à ce voyage!", "Subscribe", "Projets", new { projetId = projet.IdProjet }, null)%></span>
                
                                <%} %>
                <%} else  { %>
                       <%= Html.ImageLink( Html.IconImageUrl("knapsack-48x48.png"), "", "Login", "Account", new { fb = "true", returnUrl = this.Request.Url.PathAndQuery }, new { rel = "facebox" }, new { width = 32, height = 32 })%>
                        <br />
                        <span class="subscribe">
                         <%= Html.LoginModal("Participer à ce voyage!", this.Request.Url.PathAndQuery)%>
                        <%//= Html.ActionLink("Participer à ce voyage!", "Login", "Account", new { fb = "true", returnUrl = this.Request.Url.PathAndQuery }, new { rel = "facebox" })%> 
                        </span>
                       
              <%} %>
                </div>
                <div class="option" style="margin-right: 0px;">
                  
                <%if(this.IsAuthenticated)
                  { %>
                            <%= Html.ImageLink( Html.IconImageUrl("mail-forward.png"), "", "SendMessage", "Messages", new { projetId = projet.IdProjet }, new { rel = "facebox" })%>
                            <br />
                            <%= Html.ActionLink("Envoyer un message à " + projet.OwnerUserProfile.DisplayName, "SendMessage", "Messages", new { projetId = projet.IdProjet }, new { rel = "facebox" })%>
                        <% }
                  else
                  {%>
                            <%= Html.ImageLink( Html.IconImageUrl("mail-forward.png"), "", "Login", "Account", new { fb = "true", returnUrl = this.Request.Url.PathAndQuery }, new { rel = "facebox" })%>
                            <br />
                            <%= Html.LoginModal("Envoyer un message à " + projet.OwnerUserProfile.DisplayName, this.Request.Url.PathAndQuery)%>
                            <%//= Html.ActionLink("Envoyer un message à " + projet.OwnerUserProfile.DisplayName, "Login", "Account", new { fb = "true", returnUrl = this.Request.Url.PathAndQuery }, new { rel = "facebox" })%> 
                        <%} %>
                        
               </div>
               <%} %>
                  <div class="clear">  </div>
              </div>
             
                
          <div class="clear">   </div> 
          
    <br />
  
    <div class="left-container">
    <div class="simplebox" id="comments">
         <h3><%= Html.IconImage("comments.png") %> Commentaires</h3>
        <div class="comments-text">
           <%= projet.Commentaires.SwapIfEmpty("** Aucun commentaires **") %>  
        </div>
    </div>
    </div>

     <div class="right-container">
    <div class="simplebox" id="aboList">
        <h3><%= Html.IconImage("group.png") %> Participants</h3>
            <div class="list_user_item"> 
                <span><%= Html.UserSexeIcon( projet.OwnerUserProfile) %></span>
                <span><%= Html.UserProfileLink( projet.OwnerUserProfile) %></span>  - <small>(Créateur)</small>

            </div>
             <div class="clear">       </div>
            <% foreach(var item in projet.UserAbonnes)
               { %>
                    <div class="list_user_item">
                        <div class="list_userphoto">
                           <!-- <img width="30" alt="" src="<%= item.UserProfile.UserPhotoUrl()%>" /> 
                            -->
                            <%= Html.UserSexeIcon(item.UserProfile) %>
                        </div>
                         <div class="list_user_name">
                            <%= Html.UserProfileLink(item.UserProfile) %>
                         </div>
                         <small>
                            - <%= item.DateAbonnement.ToShortDateString()%>
                         </small>
                    </div>
                     <div class="clear">       </div>
            <%  } %>
    </div>
         </div>
         
         

          <div class="left-container">
          
            <div id="infosDetailsHebergement">
            <h2><%= Html.IconImage("go-home_16.png") %> Hebergement</h2>
            <%if(projet.Residence != null)      { %>
                <div class="barside">
                    Type: <span class="gdgreen"><%= projet.Residence.Type.ToTitleCase()%></span> <br />
                    Adresse: <span class="gdgreen"><%= projet.Residence.Adresse%></span><br />
                    Max hotes: <span class="gdgreen"><%= (projet.Residence.MaxHotes > 0) ? projet.Residence.MaxHotes.ToString() : "Non renseigné" %></span><br />
                    Prix: <span class="gdgreen"><%= projet.Residence.Prix%></span> €<br />
                </div>

            <% } else { %>
                <p> Auncun hebergement n'est précisé pour ce voyage. </p>
            <% } %>
          

          </div>
          </div>
          <div class="right-container">
          <div id="infosDetailsTransports">
            <h2><%= Html.IconImage("start-here_16.png") %> Tranport</h2>
            <% ITransport trans = projet.Transports.FirstOrDefault();%>
            <% if(trans != null) { %>
                <div class="barside">
                    Mode de transport: <span class="gdgreen"><%= trans.ModeTransport.ToTitleCase() %></span> <br />
                    <%if(!trans.Compagnie.IsNullOrEmpty())
                      {  %> Compagnie:  <span class="gdgreen"><%= trans.Compagnie %></span> <br /><% } %>
                   
                    <%if(!trans.NumVol.IsNullOrEmpty()) {  %> Numéro de vol:  <span class="gdgreen"><%= trans.NumVol %></span> <br /> <% } %>
          
                    Prix estimé: <span class="gdgreen"><%= trans.PrixAR %></span> €<br />
                    Details:  <span class="gdgreen"><%= trans.Details.SwapIfEmpty("Non renseigné")%></span> <br /> 
                    <br />
                </div>
            <%}else { %>
                <p> Auncun moyen de transport n'est précisé pour ce voyage. </p>
            <%} %>
            
            </div>
           
         </div>
        
          
          <div class="clear">  </div>

          <br />
            <br />
            <div>
            <div id="paysphotocontainer" class="simplebox">
            <h3>Images</h3>
                <div id="paysimgslider">
                    <ul class="jcarousel-skin-tango"> 
                    <% for (int i = 0; i < 15; i++)
                       {
                           //TODO: a virer!! il faut plutot iniatilser l'objet correctement
                           if (flickrPics.TotalPhotos < 1)
                               continue;
                         var photo = flickrPics.GetRandomPhoto();
                         var urlImg = Html.Image(photo.url_s,photo.title, new { width = "100", height= "100" });  
                     %>
                      <li>
                       <%=  Html.Link(urlImg,photo.url_m, new{rel = "prettyPhoto[gallery]"}) %>
                      </li>

                      <% } %>
                      <!--
                        <li><a href="http://static.flickr.com/75/199481072_b4a0d09597.jpg" rel="prettyPhoto[gallery]" > <img src="http://static.flickr.com/75/199481072_b4a0d09597_s.jpg" width="100" height="100" alt="" /> </a></li> 
                        <li><a href="http://static.flickr.com/57/199481087_33ae73a8de.jpg" rel="prettyPhoto[gallery]" ><img src="http://static.flickr.com/57/199481087_33ae73a8de_s.jpg" width="100" height="100" alt="" />  </a></li> 
                       -->
                   </ul> 
               </div>
              <small>flickr.com</small>
          </div>
          <div id="mapcontainer" class="simplebox">
             <h3>Carte :  <%= projet.PaysArrive.CapitalePaysEng.SwapIfEmpty(projet.PaysArrive.LibellePays)  %></h3>
         
             <div id="carte"> </div>
          </div>
          <div class="clear"> </div>
          </div>
</asp:Content>
