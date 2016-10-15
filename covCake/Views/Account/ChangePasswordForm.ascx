<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePasswordForm.ascx.cs" Inherits="covCake.Views.Shared.ChangePassword" %>

<script type="text/javascript">

    function SubmitForm() {
        $("#msgerrblock").hide();
        var _currpass = $("#currpass").val();
        var _newpass = $("#newpass").val();
        var _confpass = $("#confpass").val();
        var _isajax = "true";
        $.post("/Account/ChangePassword",
            { currpass: _currpass, newpass: _newpass, confpass: _confpass, ajax: true },
            function(data) {
                //alert(data);

               if (data.code > 0) {
                    $("#msgerr").html(data.msg);
                    $("#msgerrblock").show();
                }

            }, "json");

        return false;
    }


</script>


<div id="modalmain">
 <h2>Changer de mot de passe</h2>
   
    <%= Html.ValidationSummary() %>

<div>
<span class="info">
    Le nouveau mot de passe doit faire <%= ViewData["PasswordLength"]%> caractères minimum.
</span>
</div>

       <div id="msgerrblock" class="msginfo" style="display: none; border-color: Red; color: Red;
        display: none;">
        <div style="float: left; padding: 0px 3px 0px 3px;">
            <img src="/Content/cross24_round.png" />
        </div>
        <div id="msgerr">
        </div>
        <div class="clear">
        </div>
    </div>
    <div id="changepassform">
     <form action="/Account/ChangePassword" method="post" onsubmit="return SubmitForm();">
        <div>
            <table>
                <tr>
                    <td>Mot de passe actuel:</td>
                    <td>
                        <%= Html.cPassword("currpass") %>
                        <%= Html.ValidationMessage("currpass") %>
                    </td>
                </tr>
                <tr>
                    <td>Nouveau mot de passe:</td>
                    <td>               
                        <%= Html.cPassword("newpass") %> 
                        <%= Html.ValidationMessage("newpass") %>
                    </td>
                </tr>
                <tr>
                    <td>Confirmation:</td>
                    <td>
                        <%= Html.cPassword("confpass") %>
                        <%= Html.ValidationMessage("confpass") %>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td><%= Html.cSubmit("Valider") %></td>
                </tr>
            </table>
        </div>
        </form>
    <% //} %>
    
       </div>
   </div>