<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="covCake.BaseViewPage" %>

<asp:Content ContentPlaceHolderID="DevStyleCSS" runat="server">
<style type="text/css">

#email
{
	width: 180px;
}
#sujet, #message
{
	width: 250px;
}
</style>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Contact</h2>
<%= Html.ValidationSummary() %>
<%= Html.ViewInfosSummary() %>


    <p>Pour toute question, idée, suggestion ou encoragement n’hésitez pas à nous envoyer un mail :
<strong><span class="gdgreen"><%= Html.MailTo(CovCakeConfiguration.SiteContactEmail) %></span></strong>
</p>



<%using(Html.BeginForm())  { %>
   
    <h3>Message pour CoVoyage</h3>
    <br />
     <%=Html.Label("Votre adresse email:","email") %> <%= Html.ValidationMessage("email") %><br />
    <%=Html.TextBox("email") %> 
    
    <%=Html.Label("Sujet:","sujet") %> <%= Html.ValidationMessage("sujet") %> <br />
    <%=Html.TextBox("sujet") %> 
  
    <%=Html.Label("Votre message pour CoVoyage:","message") %> <%= Html.ValidationMessage("message") %><br />
    <%=Html.TextArea("message") %> 
    
    <%=Html.Submit("Envoyer") %><br />

<%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="server">
</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="AfterBodySection" runat="server">
</asp:Content>
