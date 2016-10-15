<%@ Page Language="C#"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<% Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound; %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Expires" content="-1"/>
    <meta name="robots" content="noindex,nofollow"/>
      <title>Page introuvable</title>  
</head>
<body>
      <h2>Oups! La page demandée est introuvable...</h2>

 <table>
    <tr>
    <td>
        <img alt="warning" src="/Content/warning.png" />
    </td>
    <td>
        <p style="font-weight:bold; font-size: 130%;"> La page demandée est indisponible!</p>
    </td>
    
    </tr>
    </table>

   <p style="text-align: center;">
   Pour nous signaler une <strong>erreur</strong>, vous pouvez nous contacter à l'adresse <%= Html.MailTo(CovCakeConfiguration.SiteAdminEmail, "", "Erreur technique sur Coyoyage.net: ")%>.
    </p>
    
    <!--
    <div class="warningblock">
        <p style="vertical-align: middle; display: inline;"> <%= ViewData["badurl"] %> est introuvable.</p>
    </div>
    -->
    
    <!--
    <p>Peut-être cherchiez vous les liens suivants: </p>
    -->
    <% for (int i = 0; i < 30; ++i) { %>
    

    <%} %>
    
    <div style="text-align: center;">
    <span class="backhome" >
          <a href="<%= Page.ResolveClientUrl("~/")%>">Retour à l'accueil</a>
    </span>
</div>
</body>
</html>
