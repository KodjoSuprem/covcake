<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteOffline.aspx.cs" Inherits="covCake.ErrorPages.SiteClosed" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<% Response.StatusCode = (int)System.Net.HttpStatusCode.ServiceUnavailable; %>
 
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Expires" content="-1"/>
    <meta name="robots" content="noindex,nofollow"/>
    <title>Maintenance</title>
</head>
<body>
   
    <div>
        Le site est actuellement en maintenance et sera de nouveau en ligne très bientôt.<br/>
        Veuillez nous excuser pour la gêne occasionnée.
    </div>

</body>
</html>
