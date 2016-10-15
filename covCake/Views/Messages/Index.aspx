<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  AutoEventWireup="true"  CodeBehind="Index.aspx.cs" Inherits="covCake.Views.Messages.Index"%>


<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="server">
    <script type="text/javascript">
        $(function() {

            $("#delMsg").click(function() {
                if (confirm("Supprimer les messages selectionnés?"));
                {
                    $("#formDelMsg").submit();
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

<asp:Content ID="Content4" ContentPlaceHolderID="DevStyleCSS" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<% string titre;
   switch(this.Model.TypeBoite)
   {
       case TypeBoiteMessage.Inbox:
           titre = "Messages Reçus";
           break;
       case TypeBoiteMessage.Outbox:
           titre = "Messages Envoyés";
           break;
       case TypeBoiteMessage.Trash:
           titre = "Corbeille";
           break;
       case TypeBoiteMessage.Saved:
           titre = "Messages sauvegardés";
           break;
       default:
           titre = "Messages Reçus";
           break;
   }
  %>
    
  
<% Html.RenderPartial("MessageActionBar"); %>
    
    <h2><%= titre %></h2>
    <br />
        <%= Html.ValidationSummary() %>

    <% using(Html.BeginForm("DeleteAll","Messages",FormMethod.Post,new { id = "formDelMsg"})){ %>
    <%= Html.Hidden("folder", this.Model.TypeBoite.GetValue()) %>
   <!-- <input type="submit" value="fuck" /> -->
 <span style="margin-right: auto; margin-left: 0px;">
     <a href="#" id="delMsg" style=" text-decoration: none;">
        <img src="/Content/cross.png" alt="supprimer" />Supprimer 
     </a> 
 </span>
    <div class="simplebox">
    <% if(this.Model.Messages.Count == 0) { %>
           <p>Aucun message</p> 
        <% } else  {%>
        <table class="zebra" width="100%">
        <colgroup>	
            <col style="background-color: #fff;" />
           
        </colgroup>
         <thead>
        <tr>
            <th scope="col" style="width: 10px" > Lu</th>
            <th scope="col">De</th>
            <th scope="col">Objet</th>
            <th scope="col">Reçu le</th>
            <th scope="col"><%= Html.CheckBox("checkAll") %></th>
        </tr>
       </thead>
       <tbody>
       
    <%    bool pair = true;
         foreach(var msg in this.Model.Messages)
          {%>

        <tr class="<%= (pair) ? "odd" : ""  %>" style="width: 13px"  >
            <td style="background-color: #fff;">
            <%if (msg.IsNewThread())//
              { %>
                  <%= Html.IconImage("email.png") %>
            <%} %>
            </td>
            <td><%= msg.FromUser.DisplayName%></td>
            <td ><%= Html.ActionLink(msg.SujetMessage, "ShowMessage", new { msgId = msg.MsgId })%></td>
            <td><%= msg.DateMessage.ToShortDateString() %> - <%= msg.DateMessage.ToShortTimeString() %>  </td>
            <td><%= Html.CheckBox("msgIds", new { value = msg.MsgId, @class = "del" })%></td>
       </tr>
    <%   if(pair) pair = false; else pair = true;
         }
        } %>
        </tbody>
     </table>
<%} %>
     <div class="pagination">
       <%= Html.Paginator(this.Model.Messages.TotalPages, this.Model.Messages.PageIndex, "Index", new { folder = this.Model.TypeBoite.GetValue() })%>
     </div>
    </div>


</asp:Content>
