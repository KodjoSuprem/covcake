<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendMessageModal.ascx.cs" Inherits="covCake.Views.User.SendMessageModal" %>


<script type="text/javascript">

$("#message").keyup(function(){
    
  $("#messagemissing").hide();
 });
    function SubmitForm() {
    
    if($.trim($("#message").val()) == "")
    {
        $("#messagemissing").show();
        return false;
    }
    
        var _toUserId = $("#toUserId").val();
          var _fromUserId = $("#fromUserId").val();
            var _relatedProjetId = $("#relatedProjetId").val();
             var _message = $("#message").val();
             var _sujet = $("#sujet").val();
            // alert(_fromUserId);
             $.post("/Messages/SendSingleMessage",
        { toUserId: _toUserId, fromUserId: _fromUserId, relatedProjetId: _relatedProjetId, message: _message, sujet: _sujet, ajax: true },
            function(data) {
                //alert(data);
                $("#messageForm").hide();
                if (data == true) {
                    $("#msgsent").show();
                    $("#msgerr").hide();
                }
                else {
                    $("#msgsent").hide();
                    $("#msgerr").show();
                }

            },"json");
         
            return false;
    }
</script>

<div id="modalmain">

<h2>Message à <span style="color: #ff6d00;"><%= ViewData.Model.ToUserProfile.DisplayName%></span></h2>

<% if (this.ViewData.Model.RelatedProjet != null)
   { %>
    <h3><%= Html.CountryFlagImg(ViewData.Model.RelatedProjet.PaysArrive, CovCakeHtmlHelper.FlagSize.s16)%> <%=ViewData.Model.RelatedProjet.GetShortDisplayName()%></h3>
<% } %>
<br />
<div id="messagemissing" style="display: none;">
<ul class="validation-summary-errors">
<li>Vous devez entrer un message</li>
</ul>
</div>

<form id="messageForm" method="post" action="" onsubmit="return SubmitForm();">

<%=Html.Label("Sujet", "sujet")%>
<br />
<% %>
<% //if(ViewData.Model.RelatedProjet != null) %>
<%= Html.cTextBox("sujet",ViewData.Model.sujet) %> <br />
<br />
<%= Html.Label("Message","message") %>
<div>
<%= Html.TextArea("message") %>
</div>
<br />
<%= Html.cSubmit("Envoyer") %>

<%= Html.Hidden("relatedProjetId" ,ViewData.Model.relatedProjetId) %>
<%= Html.Hidden("toUserId", ViewData.Model.toUserId)%>
<%= Html.Hidden("fromUserId", CurrentUser.UserId)%>


</form>
<%//TODO: Icone message envoyé ou erreur %>

<div id="msgerr"  class="msginfo" style="border-color: Red; color: Red;  display: none;">
    <div style="float: left; padding: 0px 3px 0px 3px;">
        <img src="/Content/cross24_round.png" />
    </div>
    Erreur. Le message n'a pas pu être envoyé 
    <div class="clear">
    </div>
</div>
<div id="msgsent" class="msginfo" style=" color: #85b700;display: none;"><div style="float: left; padding: 0px 3px 0px 3px;"><img src="/Content/tick24_round.png" /></div>Le message a été envoyé correctement !<div class="clear"> </div></div>
</div>