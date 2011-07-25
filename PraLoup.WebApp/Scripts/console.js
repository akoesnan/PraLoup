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



