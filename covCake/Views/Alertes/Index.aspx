<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="covCake.BaseViewPage<covCake.Models.AlerteViewData>" %>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="server">
<script type="text/javascript">
        $(function() {

            $("#delAlerts").click(function() {
                if (confirm("Supprimer les alertes selectionnées?"));
                {
                    $("#formDelAlerts").submit();
                    return true;
                }
                return false;
            });

            $("#checkAll").click(function() {
                // alert("fuck");
                var checked_status = this.checked;
                $(".del").each(function() {
                    //   alert("fuck");
                    this.checked = checked_status;
                });
            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Mes alertes</h2>
    
 <%= Html.ValidationSummary() %>
 
 <%= Html.ActionLink("Ajouter une nouvelle alerte mail","Create") %>


 <%if (this.Model.Alertes.Count < 1)
   { %>

<div>
Vous n'avez pas encore créé d'alertes email.<br />
Les alertes vous permettent de recevoir par email les nouveaux projets de voyage qui correspondent à vos critères.
</div>
<%} else{%>


   <span style="margin-right: auto; margin-left: 0px;">
   <a href="#" id="delAlerts" style=" text-decoration: none;">
    <img src="/Content/cross.png" alt="supprimer" /> Supprimer 
   </a> 
   </span>


<% Html.RenderPartial("AlertesList", this.Model);%>


<%} %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
  <link rel="stylesheet" type="text/css" href="../Content/grid_950.css" /> 
</asp:Content>



<asp:Content ID="Content4" ContentPlaceHolderID="DevStyleCSS" runat="server">
<style type="text/css">

</style>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="AfterBodySection" runat="server">
</asp:Content>
