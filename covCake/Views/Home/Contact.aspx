<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="covCake.BaseViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DevStyleCSS" runat="server">
<style type="text/css">
/*
#contact { display: block; width: 650px; margin: 70px auto; padding: 35px; border: 1px solid #cbcbcb; background-color: #FFF; -moz-border-radius: 5px; -webkit-border-radius:5px; }
    
#contact p, label, legend { font: 1.5em "Lucida Grande", "Lucida Sans Unicode", Arial, sans-serif; }
*/
#contact h1 { margin: 10px 0 10px; font-size: 24px; color: #333333; }
#contact hr { color: inherit; height: 0; margin: 6px 0 6px 0; padding: 0; border: 1px solid #d9d9d9; border-style: none none solid; }


/* Form style */

#contact label { display: inline-block; float: left; height: 26px; line-height: 26px; width: 155px; font-size: 1.5em; }
#contact input, textarea, select { width: 280px; margin: 0; padding: 5px; color: #666;  border: 1px solid #ccc; margin: 5px 0;font-family: "Trebuchet MS" , Arial, sans-serif; font-size:1.5em; -moz-border-radius: 5px; -webkit-border-radius:5px; }   
/*font:1.5em "Lucida Grande", "Lucida Sans Unicode", Arial, sans-serif; -moz-border-radius: 5px; -webkit-border-radius:5px; }    */
textarea{ background-color: #fff; }
#contact input:focus, textarea:focus, select:focus { border: 1px solid #999; background-color: #fff; color:#333; }
/*
#contact .button { width: auto; }
*/
#contact fieldset { padding:20px; border:1px solid #eee; -moz-border-radius: 5px; -webkit-border-radius:5px; }
#contact legend { padding:7px 10px; font-size: 120%; font-weight:bold; color:#333333; border:1px solid #eee; -moz-border-radius: 5px; -webkit-border-radius:5px; }

#contact span.required{ font-size: 13px; color: #ff0000; } /* Select the colour of the * if the field is required. */

/* Style for the error message */

#contact .error_message { display: block; height: 22px; line-height: 22px; background: #FBE3E4 url('assets/error.gif') no-repeat 10px center; padding: 3px 10px 3px 35px; margin: 10px 0; color:#8a1f11;border: 1px solid #FBC2C4; -moz-border-radius: 5px; -webkit-border-radius:5px; }

#contact #succsess_page h1 { background: url('assets/success.gif') left no-repeat; padding-left:22px; }
</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Contacter CoVoyage.net</h2>
<%= Html.ValidationSummary() %>
<%= Html.ViewInfosSummary() %>
<% //TODO: Changer le message plagié %>
    <p>Pour tout type de contact, question, idée, suggestion ou encouragement n’hésitez pas à nous envoyer un mail :) :
<strong><span class="gdgreen"><%= Html.MailTo(CovCakeConfiguration.SiteContactEmail) %></span></strong>
</p>

<div id="contact"> 

            <fieldset> 
            
            <legend>Message</legend> 
<%using(Html.BeginForm())  { %>

			<label for="nom" ><span class="required">*</span> Votre nom</label> 
             <%=Html.cTextBox("nom", "", new { size = 30 })%> <%= Html.ValidationMessage("nom") %>
 
			<br /> 
            <label for="email" ><span class="required">*</span> Votre email</label> 
            <%=Html.cTextBox("email", "", new { size = 30 })%> <%= Html.ValidationMessage("email") %>
 			
			<br /> 
            <label for="sujet" ><span class="required">*</span> Sujet</label> 
            <%=Html.cTextBox("sujet", "", new { size = 30 })%> <%= Html.ValidationMessage("sujet") %>

			<br /> 
            <label for="message" ><span class="required">*</span> Message</label> 
            <textarea name="message" cols="40" rows="3"  id="message" style="width: 350px;"></textarea> <%= Html.ValidationMessage("message") %>
            <br />
            <label></label>
            <input type="submit" class="button" value="Envoyer" /> 
         
<%} %>  
            
            </fieldset> 

     </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="JavaScript" runat="server">
</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="AfterBodySection" runat="server">
</asp:Content>
