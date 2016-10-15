<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="EditInfos.aspx.cs" Inherits="covCake.Views.User.EditInfos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AfterBodySection" runat="server">

<head>
<meta http-equiv="pragma" content="no-cache">
</head>
</asp:Content>
<asp:Content ContentPlaceHolderID="head" runat="server">

<meta http-equiv="pragma" content="no-cache">

</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">

<script type="text/javascript">
/*
    $("[@name=userImage]").change(function() {

    var path = $("[@name=userImage]").val();
    if(path != "")
        $("[@name=userImage]").submit();

    });
*/
    $(function() {
        $(".userphoto").tooltip({
            track: true,
            delay: 0,
            fade: 300,
            showURL: false,

            bodyHandler: function() {
                return $("<img/>").attr("src", this.src);
            }
        });
    });

</script>

</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">

<% this.SetNoCache(); %>
<% var RequiredField = "<span class=\"required\">*</span>";  %>

<h2>Editer mon profil</h2>

<%// Html.ValidationSummaryWrapped() %>

<br />

<% using(Html.BeginForm("EditInfos","User",FormMethod.Post,new{id = "editInfosForm" ,enctype="multipart/form-data"}))
{ %>
<div class="center" >
 <div style="display : block; margin: 0 auto;">
    <%= Html.Image(this.UserPhotoUrl, "Ma photo", new { width = 200, style = "display : block; margin: 0 auto;", @class="userphoto" }/*, new { @id = "userphoto" }*/)%>
  </div>
 <br />
 <div class="minigray">Changer mon image</div>
 <input id="userImgBtn" type="file" name="userImage" onchange="if(this.form.userImage.value != '')this.form.submit();" />
 <% //Html.File("userImage") %>

 </div>
 
 <%= Html.ValidationSummary() %>
 
<div>
    <table>
        
        <tr>
            <td>
                <%=  Html.Label(RequiredField+" Email","email") %>
            </td>
            <td>
            <span class="info">
            <%= this.Model.email %></span>
            <span> <%= Html.LightBoxLink("Changer d'adresse email", "ChangeEmail", "Account")%></span>
            
            </td>
            <td>
                <%= Html.ValidationMessage("email") %>
            </td>
        </tr>
        <tr>
            <td>
                <%= Html.Label(RequiredField + " Prenom", "prenom")%>
            </td>
            <td>
                <%= Html.cTextBox("prenom")%>
            </td>
            <td>
                <%= Html.ValidationMessage("prenom")%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Html.Label(RequiredField + " Nom", "nom")%>
            </td>
            <td>
                <%= Html.cTextBox("nom") %>
               
            </td>
            <td>
                <%= Html.ValidationMessage("nom") %>
            </td>
        </tr>
         <tr>
            <td>
                <%= Html.Label(RequiredField + " Sexe", "sexe")%>
            </td>
            <td>
               Homme <%= Html.RadioButton("sexe", "true", true)%>
               Femme <%= Html.RadioButton("sexe","false") %>
            </td>
            <td>
                <%= Html.ValidationMessage("sexe") %>
            </td>
        </tr>
        <tr>
            <td>
                <%= Html.Label(RequiredField + " Date de naissance", "age")%>
            </td>
            <% var sel = covCake.Services.CovCakeServices.GetDateNaissSelector( ViewData.Model.age_jour, ViewData.Model.age_mois, ViewData.Model.age_annee);
               //sel.Jours = new SelectList(sel.Jours.Items, ViewData.Model.age_jour, sel.Jours.DataTextField, sel.Jours.DataValueField);
               //sel.Mois = new SelectList(sel.Mois.Items, ViewData.Model.age_mois, sel.Mois.DataTextField, sel.Mois.DataValueField);

             %>
            <td>
                <%= Html.DropDownList("age_jour",sel.Jours) %>
                <%= Html.DropDownList("age_mois",sel.Mois) %>
                
                19<%= Html.cTextBox("age_annee",ViewData.Model.age_annee, new { maxlength = "2", style="width:20px;" })%>
            </td>
            <td>
                <%= Html.ValidationMessage("age") %>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <div>-----------Facultatif ---------</div>
            </td>
        </tr>
            
        <tr>
            <td> <%= Html.Label("Pays","pays") %> </td>
            <td><%= Html.DropDownList("pays", covCake.Services.PaysServices.ListePays(ViewData.Model.pays))%> </td>
            <td></td>
        </tr>
            
        <tr>
            <td><%= Html.Label("Departement","numdept") %></td>
            <td><%= Html.DropDownList("numdept", covCake.Services.DepartementService.ListeDept(ViewData.Model.numdept))%> </td>
            <td></td>
        </tr>
        <tr>
             <td><%= Html.Label("Ville","ville") %> </td>
             <td><%= Html.cTextBox("ville") %> </td>
             <td><%= Html.ValidationMessage("ville") %> </td>
        </tr>
        <tr>
            <td colspan="3">
            <div>---------------------</div>
            </td>
        </tr>
        <tr>
            <td>
                <%= Html.Label("Mot de passe","changePassword") %>
            </td>
            <td>
               <!-- <%= Html.ActionLink("Changer mon mot de passe","ChangePassword")%> -->
                <%= Html.LightBoxLink("Changer mon mot de passe", "ChangePassword", "Account")%>
 <%//= Html.ActionLink("Changer mon mot de passe", "ChangePassword", "Account", new { fb = "true" }, new { rel = "facebox" })%> 

            </td>
            <td>
               
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <%= Html.cSubmit("Valider")%>
            </td>
            <td>
            </td>
        </tr>
        <% } %>
    </table>
</div>

</asp:Content>

