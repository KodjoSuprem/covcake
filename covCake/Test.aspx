<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="covCake.WebForm1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
  "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
  <head>
    <meta http-equiv="content-type" content="text/html; charset=utf-8"/>
    <title>Google Maps JavaScript API Example</title>
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAANE808zIvEVkCKOlZZpqjlxQKmKCZGjN4B49v6XmOzHXUQ6SP-BSwmc7UDNHsmeWORa6g_ONFTZIYjg"
      type="text/javascript"></script>
    <script type="text/javascript">

    //<![CDATA[

    function load() {
      if (GBrowserIsCompatible()) {
          var map = new GMap2(document.getElementById("map"));
          //centrer sur l'itinéraire.!!
        map.setCenter(new GLatLng(37.4419, -122.1419), 1);
        map.enableScrollWheelZoom();
        map.addControl(new GMapTypeControl());
        map.addControl(new GSmallMapControl())

      }
    }

    //]]>
    </script>
  </head>
  <body onload="load()" onunload="GUnload()">
  <div>
 
  </div>
  
    <div id="map" style="width:500px;height:300px"></div>
  </body>
</html>