
$("#ddlId").on('change', function () {
    LoadUserInformation();
});

function LoadUserInformation() {
    var userId = $("#UserId").val();
    var clientId = '@ViewBag.ClientId';
    if (userId) {   // evaluates to true, is userId value is not: null, undefined, NaN, empty string, 0, false
        $.ajax({
            url: '@Url.Action("PopulateModifyUserRole", "UserAdministration")',
            type: 'POST',
            dataType: 'html',
            data: { userId: userId },
            success: function (data) {
                $('#modifyUserRolePartial').html(data);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $("#notifyMessage").notify("An error has occured while processing your request.", { position: "left", className: "error" });
            }
        });
        return false; // prevent the default submit action
    }
};