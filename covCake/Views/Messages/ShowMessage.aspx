<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="covCake.BaseViewPage<covCake.Models.ShowMessageViewData>" %>


<asp:Content ID="Content4" ContentPlaceHolderID="DevStyleCSS" runat="server">


<style type="text/css">
div.userphoto-message img
{
	width: 55px;
	
}
div.userphoto-message
{
   margin-right: 7px;
}

.userinfo-message
{
	margin: 7px
}

.author-message
{
	
}

.corp-message
{
    border-bottom: 1px solid #aaa;
}

</style>


</asp:Content>

<asp:Content ContentPlaceHolderID="head" runat="server">
    
     <link href="../../Scripts/jquery-tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
     <script type="text/javascript" src="../../Scripts/jquery-tooltip/jquery.tooltip.min.js" > </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="server">

  <script type="text/javascript" >
      $(function() {
          $('img[rel*=userphoto]').tooltip({
              track: true,
              delay: 0,
              fade: 300,
              showURL: false,
              bodyHandler: function() {
                  return $("<img/>").attr("src", this.src);
              }
          });

          $('#message').keydown(function() { LimitFieldLength(this, 4000) });
      });
    
  </script>

</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% IUserProfile fromUser = this.Model.Msg.FromUser;  %>
<% IMessagePrive firstThreadMsg =  this.Model.MsgThread.First(); %>
<% IUserProfile userCorrespondant = (firstThreadMsg.FromUserId != this.CurrentUserId) ? firstThreadMsg.FromUser : firstThreadMsg.ToUser; %>

   <% Html.RenderPartial("MessageActionBar"); %>
    
    <%= Html.ValidationSummary() %>
    
    <h2><%= firstThreadMsg.SujetMessage %></h2>
       
    <%//TODO: Supprimer msg, Sauvegarder, icones %>
    <%//TODO: matter comment est fait la lecture des messages envoyés sur facebook %>
    <p>
        Discution entre vous et <%= Html.UserProfileLink(userCorrespondant)%>
    </p>
    
    <% var sharedProj = CurrentUser.GetSharedProjects(userCorrespondant.UserId);  %>   
    <%if(sharedProj.Count() > 0){ %>
        <span class="gdgreen"><%= Html.IconImage("group_go.png") %>  <%= userCorrespondant.DisplayName%> vous accompagne dans <%= sharedProj.Count().ToString() %> voyage(s) ! </span>
    <%}else{%>
        <span class="info"><%= userCorrespondant.DisplayName%> ne vous accompagne dans aucun voyage</span>
    <%} %>
    
    <br />
    
    <table >
    
    <% foreach (var msg in this.Model.MsgThread)
       {%>
    
    <tr class="author-message">
        <td> 
        
        <div class="userphoto-message" style="float: left;">
            <%= Html.Image(msg.FromUser.UserPhotoUrl(), "Photo de " + msg.FromUser.DisplayName, new { rel = "userphoto" })%>
        </div>
        <div class="userinfo-message">
            <div class="name">
                <%= Html.UserProfileLink(msg.FromUser) %> <%= Html.UserSexeIcon(msg.FromUser) %>
             </div>
        <small >
            Age: <%= msg.FromUser.DateNaissance.GetAge()%> ans<br />
            Departement: <%= msg.FromUser.Departement.NomDept%> (<%= msg.FromUser.Departement.NumDept%>)<br />
        </small>
      </div>
     
     </td>
    <td class="corp-message">
        <div id="date"><small><%= msg.DateMessage.ToString() %></small> </div>
        <div id="texte">
            <%=  msg.TextMessage %> 
        </div>
    </td>
    </tr>
    
    
   
   <% } %>
    <% //TODO: Thread des messages du plus recent au plus vieux %>
     </table>
         <br />
     <div id="reponse">
     <h4>Répondre</h4>
     <br/>
     
<% using(Html.BeginForm("SendSingleMessage","Messages")){%>
<%=Html.Label("Message","message") %>
<%= Html.ValidationMessage("message") %>
<div style="width: 70%;">
<%= Html.TextArea("message")%>

</div>

<br />

<%= Html.Submit("Envoyer") %>

<%= Html.Hidden("relatedProjetId" ,ViewData.Model.Msg.ProjetRelatedId) %>
<% //Si le message ouvert est dans un thread : répondre à luser émateur du premier message %>
<% Guid toUserId = userCorrespondant.UserId;//(ViewData.Model.Msg.MsgResponseId != null) ? ViewData.Model.Msg.MsgResponse.FromUserId : ViewData.Model.Msg.FromUserId;   %>
<% //si le message ouvert est dans un thread : mettre répondre au premier du thread   %>
<% var responseMsgid = ViewData.Model.Msg.MsgResponseId ?? ViewData.Model.Msg.MsgId; %>

<%= Html.Hidden("toUserId", toUserId)%>
<%= Html.Hidden("fromUserId", CurrentUser.UserId)%>
<%= Html.Hidden("responseMsgId", responseMsgid)%>

<% } %>

</div>
<%//TODO: Icone message envoyé ou erreur %>

<div id="msgerr"  class="msginfo" style="border-color: Red; color: Red;  display: none;">Le message n'a pu être envoyé correctement</div>
<div id="msgsent" class="msginfo" style=" color: #85b700;display: none;">Le message a été envoyé correctement !</div>

    <%//TODO: Messages suivant messages précédents %>
    
    <%if(this.Model.MsgConnexes.Count > 0)
      { %>
    <div class="simplebox">
       <h3>Messages précédents de <%= this.Model.Msg.FromUser.DisplayName %></h3>
       <ul>
       <%foreach(IMessagePrive msg in this.Model.MsgConnexes)
         {%>
             <li><%= msg.DateMessage.ToShortDateString()%> - <a href="#"><%= msg.SujetMessage%></a></li>
         <%} %>
         </ul>
    </div>
    <%} %>
    
</asp:Content>



