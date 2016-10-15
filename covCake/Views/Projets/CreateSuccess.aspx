<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="covCake.BaseViewPage<covCake.DataAccess.IProjet>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= this.Model.GetLongDisplayName() %></h2>
    
    <p>
        Votre projet de voyage à bien été créé! <br />
        Les autres membres du site peuvent désormais vous rejoindre et vous envoyer des messages.
    </p>

    <h3>Récapitulatif</h3>
     <%= Html.ImageLink( "/Content/page_edit.png", "edit", "Edit", "Projets", new { projetId = this.Model.IdProjet })%>
    <%= Html.ActionLink("Modifier", "Edit", new { projetId = this.Model.IdProjet })%>
   
   <br />
  
  <%//TODO: Faire le récapitulatif du voyage %>
  <%//TODO: consulter les offres des partenaires %>
  
  <%= Html.ActionLink("Continuer","MonCompte","User") %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="DevStyleCSS" runat="server">
</asp:Content>
