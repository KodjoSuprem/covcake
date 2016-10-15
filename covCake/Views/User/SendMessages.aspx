<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="SendMessages.aspx.cs" Inherits="covCake.Views.User.SendMessage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using(Html.BeginForm()) { %>

<div id="projet-selector">
<div class="colorbox">
<div class="title">Selectionnez un voyage</div>

<% foreach(var proj in CurrentUser.Projets) {%>
 <label class='f_checkbox'> 
    <%=Html.RadioButton("projRelated",proj.IdProjet) %> <%= proj.GetShortDisplayName() + " " + proj.GetDuree() %>
 </label>
<% }%>


</div>
</div>

<div id="subscribers">
<div class="colorbox">
<div class="title">Utilisateurs qui souhaitent vous acompagner</div>

<%if(ViewData.Model.Subscribers.Count >= 1)
  { %>
    <%foreach(var user in ViewData.Model.Subscribers)
  { %>

    <label class='f_checkbox'><%= Html.CheckBox("to_" + user.UserId)%> <%= user.DisplayName + " " + user.Ville + "(" + user.Departement + ")"%></label> 

   <%} %>

<% }
  else
  {%>

<div>Personne ne vous acompagne sur ce voyage</div>
<%} %>
</div>
</div>

<div id="sendmessage">
<table>
<tr>
<td>
Sujet:
</td>
<td>
    <input type="text" />
</td>
</tr>
<tr>
<td>
Message:
</td>
<td>
<textarea cols="25" rows="15">
Votre message sera envoyé aux utilisateurs selectionnés!
</textarea>
</td>
</tr>
<tr>
<td></td>
<td>
<input type="submit" value="Envoyer" />
</td>
</tr>
</table>







<%} %>
</div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="EditInPlace" runat="server">



</asp:Content>