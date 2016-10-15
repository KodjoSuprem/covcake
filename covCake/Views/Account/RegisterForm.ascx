<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterForm.ascx.cs"
    Inherits="covCake.Views.Shared.RegisterForm" %>

<h2>S'inscrire</h2>
<p>
    Use the form below to create a new account.
</p>
<% var RequiredField = "<span class=\"required\">*</span>";  %>
<%= Html.ValidationSummary() %>
<div>  
<% using(Html.BeginForm())
{ 
%>
    <table>
      
        <tr>
            <td>
                <%= Html.HelpBullet("Une adresse email valide permet de recevoir les alertes concernant vos voyages.") %>
                <%= Html.Label(RequiredField+" Email","email") %>
            </td>
            <td>
                <%= Html.cTextBox("email") %>
            </td>
            <td>
                <%= Html.ValidationMessage("email") %>
            </td>
        </tr>
        <tr>
            <td>
                <%= Html.Label(RequiredField+" Prenom","prenom") %>
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
               <%= Html.HelpBullet("Seule la premiere lettre de votre nom sera affiché. ex: Julien C.") %> 
               <%= Html.Label(RequiredField+" Nom","nom") %>
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
                <%= Html.Label(RequiredField+" Sexe","sexe") %>
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
                <%= Html.Label(RequiredField+" Date de naissance","age") %>
            </td>
            <% var sel = covCake.Services.CovCakeServices.GetDateNaissSelector(); %>
            <td>
                <%= Html.DropDownList("age_jour",sel.Jours) %>
                <%= Html.DropDownList("age_mois",sel.Mois) %>
                19<%= Html.cTextBox("age_annee", "", new { maxlength = "2", style="width:20px;" })%>
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
            <td><%= Html.HelpBullet("Vos futurs compagnons de voyage habitent peut-être le même département que vous ;)") %> <%= Html.Label("Departement","numdept") %> </td>
             <td><%= Html.DropDownList("numdept", covCake.Services.DepartementService.ListeDept())%> </td>
               <td></td>
        </tr>
        <tr>
             <td>  
                 <%= Html.HelpBullet("Vos futurs compagnons de voyage habitent peut-être la même ville que vous ;)") %> 
                 <%= Html.Label("Ville","ville") %> 
             </td>
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
                 <%= Html.HelpBullet(ViewData["PasswordLength"] + " caractères minimum")%> 
                 <%= Html.Label(RequiredField+" Mot de passe","password") %>
            </td>
            <td>
                <%= Html.cPassword("password") %>
            </td>
            <td>
                <%= Html.ValidationMessage("password") %>
            </td>
        </tr>
        <tr>
            <td>
                <%= Html.Label(RequiredField+" Confirmez", "confirmPassword")%>
            </td>
            <td>
                <%= Html.cPassword("confirmPassword") %>
            </td>
            <td>
                <%= Html.ValidationMessage("confirmPassword") %>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
              <!--  <input type="submit" class="button" value="S'enregistrer" /> -->
                <%= Html.cSubmit("S'enregistrer") %>
            </td>
            <td>
            </td>
        </tr>
       
    </table>
<% } %>
</div>
