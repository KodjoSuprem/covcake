<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true"
    Inherits="covCake.BaseViewPage" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="container_12">
    <h2>        <%= Html.Encode(ViewData["Message"]) %></h2>
<div class="grid_12 nomargin">
    <center>
        <strong style="font-size: 100px;">
            <% 
#if DEBUG 
    this.Writer.Write("DEBUG MODE");
#else
     this.Writer.Write("RELEASE MODE");       
#endif %>
        </strong>
    </center>

</div>
<div class="clear">&nbsp;</div>
<br />
<div class="grid_6 alpha">

Voyages prochains Voyages prochains Voyages prochains Voyages prochains
Voyages prochains Voyages prochains Voyages prochains Voyages prochains
</div>
<div class="grid_6 omega">
News du site News du site News du site News du site News du site News du site
 News du site News du site News du site News du site News du site News du site
</div>

<div class="clear" >&nbsp;</div>
<br />
<div class="grid_12 nomargin">
La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas 
La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas 
</div>

<div class="clear" >&nbsp;</div>
   
   <div class="grid_4 alpha">
    La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas 
   </div>

   <div class="grid_4">
    La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas 
   </div>
    
   <div class="grid_4 omega">
    La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas La ché pas 
   </div>

<div class="clear" >&nbsp;</div>
   
 </div>
 <div class="clear" >&nbsp;</div>
</asp:Content>
