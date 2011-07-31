function FacebookLogin() {
    FB.login(function (response) {
        if (response.session) {
            if (response.perms) {
                // user is logged in and granted some permissions.
                // perms is a comma separated list of granted permissions
                $.post("http://localhost/Praloup.WebApp/Account/OAuth", { access_token: response.session.access_token, expires: response.session.expires }, function (data) {
                    alert('Http POST – ' + data.Data);
                });
            } else {
                // user is logged in, but did not grant any permissions
            }
        } else {
            // user is not logged in
        }
    }, { perms: 'publish_stream, user_about_me, read_friendlists,user_photos,friends_photos' });
}

function RenderAccountList(cont, events, selectedFriends) {
    cont.empty();
    for (var y in events) {
        var x = events[y];
        $(" <div class='userlabel'>" +
            "<div class='image'>" +
            "<img src='" + x.image + "' />" +
            "</div>" +
            "<div class='name' >" +
            x.name
        + "</div><div class='clear'></div></div>"
        ).appendTo(cont);
    }
}



function AddNewUsers(cont, selectedFriends) {
    cont.empty();
    for (var y in selectedFriends) {
        var x = selectedFriends[y];
        $(" <div class='userlabel'>" +
            "<div class='name' >" +
            x.name
        + "</div></div>"
        ).appendTo(cont);
    }
}


function displaywindow(name) {
    $('#' + name).jqmShow();
}

function CloseDealListEditor(controlid, dialogid, divid, hiddenInput, storagevar) {
    var json = GetJsonForDealControl($('#' + controlid));

    // add new deal row
    AddDisplayRow(controlid, json, divid, dialogid, storagevar, hiddenInput, true, true);
}

function AddDisplayRow(controlid, json, divid, dialogid, storagevar, hiddenInput) {
    // add new deal row
    var x = jQuery.parseJSON(json);
    var html = "<div>" +
            x.Name;
    html += "</div>";
    var div = $(html);y
    var link1 = $("<a> Edit </a>").click(   
         function () {
             RemoveListEditor(controlid, divid, hiddenInput, dialogid, storagevar, json);
             OpenDealListEditor(controlid, dialogid, divid, json);
         });
    link1.appendTo(div);


    var link2 = $("<a> Remove </a>").click(
         function () {
             RemoveListEditor(controlid, divid, hiddenInput, dialogid, storagevar, json);
         });
    link2.appendTo(div);


    div.appendTo($('#' + divid));
    storagevar[storagevar.length] = json;

    if (hiddenInput.val() == "") {
        hiddenInput.val(json);
    }
    else {
        hiddenInput.val(hiddenInput.val() + "," + json);
    }
}

function RemoveListEditor(controlid, divid, hiddenInput, dialogid, storagevar, json) {
    for (var i = 0; i < storagevar.length; ++i) {
        if (json == storagevar[i]) {
            storagevar.splice(i, 1);
            break;
        }
    }
    hiddenInput.value = "";
    $('#' + divid).empty();
    for (var i = 0; i < storagevar.length; ++i) {

        AddDisplayRow(controlid, storagevar[i], divid, dialogid, storage, hiddenInput);
    }
}

function GetJsonForDealControl(cont) {
    var array = [
                    "OriginalValue",
                    "CurrentValue",
                    "Saving",
                    "StartDateTime",
                    "EndDateTime",
                    "Description",
                    "FinePrint",
                    "RedemptionInstructions",
                    "Available"
                 ];
    var dealjson = "{";
    var found = 0;
    cont.find("*").each(function () {
        $(this).children().each(
            function () {
                var current = this.name;
                if (current == undefined) {
                    return;
                }
                for (t in array) {
                    if (current == array[t]) {
                        if (found != 0) {
                            dealjson += ',';
                        }
                        found++;
                        dealjson += '"' + array[t] + '": "' + escapeSpecialChars($(this).val()) + '"';
                        break;
                    }
                }
            });
    });
    return dealjson += "}";
}
function escapeSpecialChars(string) {
    return string.replace(/\\/g, "\\\\")
            .replace(/\n/g, "\\n")
            .replace(/\r/g, "\\r")
            .replace(/\t/g, "\\t")
            .replace(/\f/g, "\\f")
            .replace(/"/g, "\\\"")
            .replace(/'/g, "\\\'")
            .replace(/\&/g, "\\&"); 
            
}

function SetDealControlForJson(controlid, dealjson) {
    var cont = $("#" + controlid);
    var x = jQuery.parseJSON(dealjson);
    cont.children().each(function () {
        $(this).children().each(
            function () {
                var name = this.name;
                if (name == undefined) {
                    return;
                }
                var current = $(this);
                jQuery.each(x, function (k, v) {
                    if (name == k) {
                        current.val(v);
                        return;
                    }
                });
            });
    });
}

function OpenDealListEditor(controlid, dialogid, divid, dealjson) {
    if (dealjson == undefined) {
        dealjson = '{"DealListOriginalValue": "",' +
                    '"DealListCurrentValue": "",' +
                    '"DealListSaving": "",' +
                    '"DealListStartDateTime": "",' +
                    '"DealListEndDateTime": "",' +
                    '"DealListDescription": "",' +
                    '"DealListFinePrint": "",' +
                    '"DealListRedemptionInstructions": "",' +
                    '"DealListAvailable": ""}';
    }
    SetDealControlForJson(controlid, dealjson);

    displaywindow(dialogid);
}
