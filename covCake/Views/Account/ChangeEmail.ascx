<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">

    function SubmitForm() {

        var _newmail = $("#newmail").val();

        $.post("/Account/ChangeEmail",
            { newmail: _newmail, ajax: "true" },
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
    <h2>Changement d'adresse email</h2>
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
    <div>
        Vous souhaitez changer d'adresse email.
        <br />
        Cette adresse est également utilisée pour vous connecter au site.
        <br />
        <form id="newmailForm" method="post" action="" onsubmit="return SubmitForm();">
        <%= Html.Label("Nouvelle adresse","newmail") %>
        <%= Html.cTextBox("newmail") %>
        <br />
        <%= Html.cSubmit("Valider") %>
        </form>
    </div>
</div>
