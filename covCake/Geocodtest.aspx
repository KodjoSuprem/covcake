<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Geocodtest.aspx.cs" Inherits="covCake.Geocodtest" %>

<?xml version="1.0" encoding="utf-8" ?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
xml:lang="fr">
<head>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <!-- Chargement du script Google AJAX APIs en précisant votre clé -->

    <script type="text/javascript" src="http://www.google.com/jsapi?key=ABQIAAAANE808zIvEVkCKOlZZpqjlxQKmKCZGjN4B49v6XmOzHXUQ6SP-BSwmc7UDNHsmeWORa6g_ONFTZIYjg""></script>

    <script type="text/javascript">
        /* Chargement du module "maps" dans sa version "2" */
        google.load("maps", "2");
        /* Déclaration des variables globales */
        var maCarte;
        var monGeocodeur;
        var visibilite;
        var compilInfo;
        var miniconerouge;
        var address;
        var precision = ["Adresse inconnue", "du Pays", "de la Région", "du Département", "de la Ville", "du Code postal", "de la Rue", "d'une Intersection", "de l'adresse", "Maximum"];

        /* Fonction initialize() */
        function initialize() {
            /* Si le navigateur est compatible avec l'API de Google Maps ... */
            if (google.maps.BrowserIsCompatible()) {
                /* ... Création d'une nouvelle carte nommée "maCarte" qui s'affichera à l'intérieur de la <div> ayant pour identifiant id="EmplacementDeMaCarte" ... */
                maCarte = new google.maps.Map2(document.getElementById("EmplacementDeMaCarte"));
                /* ... Ajout d'un mini contrôle sur la carte nommée "maCarte" ... */
                maCarte.addControl(new google.maps.SmallMapControl());
                /* ... La carte nommée "maCarte" est centrée sur la Latitude 47.341571, la Longitude 0.514233, avec un niveau de zoom égal à 13 ... */
                maCarte.setCenter(new google.maps.LatLng(47.388588, 0.689049), 9);
                /* ... Caractéristiques de l'icône nommé "miniconerouge" ... */
                miniconerouge = new google.maps.Icon();
                miniconerouge.image = "http://labs.google.com/ridefinder/images/mm_20_red.png";
                miniconerouge.shadow = "http://labs.google.com/ridefinder/images/mm_20_shadow.png";
                miniconerouge.iconSize = new google.maps.Size(12, 20);
                miniconerouge.shadowSize = new google.maps.Size(22, 20);
                miniconerouge.iconAnchor = new google.maps.Point(6, 20);
                miniconerouge.infoWindowAnchor = new google.maps.Point(5, 1);
                /* ... Création d'un nouveau géocodeur nommé "monGeocodeur" ... */
                monGeocodeur = new google.maps.ClientGeocoder();
                /* Si le navigateur n'est pas compatible avec l'API de Google Maps ... */
            } else {
                /* ... affichage du message "Désolé, mais votre navigateur n'est pas compatible avec Google Maps". */
                alert('Désolé, mais votre navigateur n\'est pas compatible avec Google Maps');
            }
        }

        /* Fonction showlocation() */
        function showlocation(addRecherche) {
            address = addRecherche;
            compilInfo = "";
            document.getElementById("info").innerHTML = "";
            monGeocodeur.getLocations(address, AfficheAdresse);
        }

        /* Fonction creationMarqueur() */
        function creationMarqueur(numero, lat, lng) {
            /* ... Création d'un nouveau point nommé "point" ayant pour latitude "lat" et longitude "lng" ... */
            var point = new google.maps.LatLng(lat, lng);
            /* ... Complète "visibilite" avec le point nommé "point" ... */
            visibilite.extend(point);
            /* ... Création d'un nouveau marqueur nommé "marker" ancré sur le point nommé "point" et repésenté par l'icône nommé "miniconerouge ... */
            var marker = new google.maps.Marker(point, miniconerouge);
            /* ... Création d'un observateur d'événement lié au marqueur nommé "marker" ... */
            /* ... L'événement observé est le "click" sur le marqueur nommé "marker" ... */
            google.maps.Event.addListener(marker, "click", function() {
                /* ... Dès qu'un "click" sera détecté sur le marqueur nommé "marker", ouverture d'une info-bulle ... */
                /* ... à l'intérieur de laquelle est affiché le numéro du marqueur, sa latitude et sa longitude ... */
                marker.openInfoWindowHtml("Marqueur : " + (numero + 1) + "<br /><br /><b>Latitude</b> : " + lat + "<br /><br /><b>Longitude</b> : " + lng);
            });
            return marker;
        }

        /* Fonction AfficheAdresse() */
        function AfficheAdresse(reponse) {
            /* ... visibilite : va contenir tous les points issus du géocodage et sera utilisé pour optimiser l'affichage de la carte ... */
            visibilite = new GLatLngBounds();
            /* ... Suppression de tous les recouvrements situés sur la carte nommée "maCarte" ... */
            maCarte.clearOverlays();
            /* ... Si, le code obtenu en réponse de la requête de géocodage n'existe pas ou qu'elle est différente de 200 ... */
            if (!reponse || reponse.Status.code != 200) {
                /* ... Affichage d'un message d'alerte ... */
                alert("Désolé, Il nous est impossible de géocoder votre adresse : \r\n\nCode Erreur : " + reponse.Status.code + '\r\n\nRetrouvez la signification de ce code à cette adresse :\r\n\nhttp://www.google.com/apis/maps/documentation/reference.html#GGeoStatusCode');
                /* ... Sinon ... */
            } else {
                /* ... "nombreReponse" est égal au nombre de réponses obtenues suite à la requête de géocodage ... */
                var nombreReponse = reponse.Placemark.length;
                /* ... On fait une boucle sur chacune des réponses pour en déterminer les caractéristiques (Statut de la requête, Précision, Adresse formatée, Pays, Région, Département, Ville, Rue, Code Postal, Latitude, Longitude, Altitude) ... */
                for (a = 0; a < nombreReponse; a++) {
                    place = reponse.Placemark[a];
                    var Gstatusrequete = reponse.Status.code;
                    var Gprecision = place.AddressDetails.Accuracy;
                    var adresserequete = address;
                    var GadresseFormatee = place.address;
                    if (place.AddressDetails.Country) {
                        var Gpays = place.AddressDetails.Country.CountryNameCode;
                        if (place.AddressDetails.Country.AdministrativeArea) {
                            var Gregion = place.AddressDetails.Country.AdministrativeArea.AdministrativeAreaName;
                            if (place.AddressDetails.Country.AdministrativeArea.SubAdministrativeArea) {
                                var Gdepartement = place.AddressDetails.Country.AdministrativeArea.SubAdministrativeArea.SubAdministrativeAreaName;
                                if (place.AddressDetails.Country.AdministrativeArea.SubAdministrativeArea.Locality) {
                                    var Gville = place.AddressDetails.Country.AdministrativeArea.SubAdministrativeArea.Locality.LocalityName;
                                    if (place.AddressDetails.Country.AdministrativeArea.SubAdministrativeArea.Locality.Thoroughfare) {
                                        var Grue = place.AddressDetails.Country.AdministrativeArea.SubAdministrativeArea.Locality.Thoroughfare.ThoroughfareName;
                                    }
                                    if (place.AddressDetails.Country.AdministrativeArea.SubAdministrativeArea.Locality.PostalCode) {
                                        var Gcodepostal = place.AddressDetails.Country.AdministrativeArea.SubAdministrativeArea.Locality.PostalCode.PostalCodeNumber;
                                    }
                                }
                            }
                        }
                    }
                    var Galtitude = place.Point.coordinates[2];
                    var Glatitude = place.Point.coordinates[1];
                    var Glongitude = place.Point.coordinates[0];
                    /* ... "compilinfo" : on construit un tableau contenant les caratéristiques de chaque réponse ... */
                    compilInfo = compilInfo + '<p>'
+ '<table class="tablotexte">'
+ '<tr><th colspan="2"><b>Réponse N° ' + (a + 1) + ' / ' + nombreReponse + '</b></td></th>'
+ '<tr><td><b>Etat de la requète</b></td><td>' + Gstatusrequete + '</td></tr>'
+ '<tr><td><b>Adresse Demandée</b></td><td>' + adresserequete + '</td></tr><tr><td style="width: 150px;"><b>Adresse Formatée</b></td><td>' + place.address + '</td></tr>'
+ '<tr><td><b>Niveau de précision</b></td><td><b>' + Gprecision + '</b> -> La précision du géocodage se situe au niveau <b>' + precision[Gprecision] + '</b></td></tr>'
+ '<tr><td><b>Pays</b></td><td>' + Gpays + '</td></tr>'
+ '<tr><td><b>Région</b></td><td>' + Gregion + '</td></tr>'
+ '<tr><td><b>Département</b></td><td>' + Gdepartement + '</td></tr>'
+ '<tr><td><b>Ville</b></td><td>' + Gville + '</td></tr>'
+ '<tr><td><b>Rue</b></td><td>' + Grue + '</td></tr>'
+ '<tr><td><b>Code Postal</b></td><td>' + Gcodepostal + '</td></tr>'
+ '<tr><td><b>Latitude</b></td><td>' + Glatitude + '</td></tr>'
+ '<tr><td><b>Longitude</b></td><td>' + Glongitude + '</td></tr>'
+ '<tr><td><b>Altitude</b></td><td>' + Galtitude + '</td></tr>'
+ '</table>'
+ '</p>';
                    /* ... Ajout, à la carte nommée "maCarte", d'un marqueur par appelle de la fonction creationMarqueur(a, Glatitude, Glongitude) ... */
                    maCarte.addOverlay(creationMarqueur(a, Glatitude, Glongitude));

                }
                /* ... Une fois la boucle terminée, on affiche e contenu de "compilInfo" dans la balise <div> ayant pour identifiant id=info ... */
                document.getElementById("info").innerHTML = compilInfo;
                /* ... Centre la carte nommée "maCarte" de façon optimisée. C'est à dire, avec le niveau de zoom et le centre idéals de façon à voir tous les marqueurs sur la carte ... */
                maCarte.setCenter(visibilite.getCenter(), maCarte.getBoundsZoomLevel(visibilite));
            }
        }
        /* Appelle la fonction "initialize" lorsque la page web sera chargée */
        google.setOnLoadCallback(initialize);
    </script>

</head>
<body>
    <!-- La carte nommée "maCarte", va venir s'afficher à l' intérieur de -->
    <!-- la balise <div> ayant pour identifiant id="EmplacementDeMaCarte". -->
    <!-- Elle fera donc 740 pixels de large et 400 pixels de haut. -->
    <div id="EmplacementDeMaCarte" style="width: 740px; height: 400px">
    </div>
    <form action="#" method="post" enctype="text/plain" onsubmit="showlocation(this.q.value); return false;">
    <div class="cadre">
        <b>Géocoder cette adresse postale</b> :
        <input type="text" name="q" value="10 rue bretonneau, 37000 Tours, france" size="40"
            id="qa" /><input type="submit" name="rechercher" value="Rechercher" />
    </div>
    </form>
    <div id="info">
    </div>
    <!-- Si JavaScript n'est pas activé sur votre navigateur, le message d'alerte situé entre les balises <noscript></noscript> s'affichera -->
    <noscript>
        <p>
            Attention :
        </p>
        <p>
            Afin de pouvoir utiliser Google Maps, JavaScript doit être activé.</p>
        <p>
            Or, il semble que JavaScript est désactivé ou qu'il n'est pas supporté par votre
            navigateur.</p>
        <p>
            Pour afficher Google Maps, activez JavaScript en modifiant les options de votre
            navigateur, puis essayez à nouveau.</p>
    </noscript>
</body>
</html>
