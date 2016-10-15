<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<h2>Erreur</h2>
 <%var errorMsg = "Une erreur est survenue lors du traitement de votre requête."; %>
  <% if(ViewData.ContainsKey("ErrorMsg")) errorMsg = Html.Encode(ViewData["ErrorMsg"]); %>
     
     
<div id="msgerr"  class="msginfo" style="border-color: Red; color: Red;  display: none;">
    <div style="float: left; padding: 0px 3px 0px 3px;">
        <img src="/Content/cross24_round.png" />
    </div> <%= errorMsg%>
    
    <div class="clear"> </div>
</div>