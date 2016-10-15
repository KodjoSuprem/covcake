<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Oups! La page demandée est introuvable...</h2>

 <table>
    <tr>
    <td>
        <img alt="warning" src="/Content/warning.png" />
    </td>
    <td>
        <p style="font-weight:bold; font-size: 130%;"> <%= ViewData["badurl"] %> est indisponible.</p>
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
    
    <% Html.RenderPartial("BackToHomeLink"); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="DevStyleCSS" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="AfterBodySection" runat="server">
</asp:Content>
