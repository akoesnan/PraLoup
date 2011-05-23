function loginp(uids, sesk) {
    var str = FB.JSON.stringify({ uid: uids, sessionkey: sesk });
   
    $.post(
        '/Praloup.WebApp/Account/Register',
        { json: str },
        function (data) {
            
        }
    );
}

function EnableForm() {
    var input = $("input").get();
    for (var i = 0; i < input.length; i++) {
        input[i].disabled = false;
    }
}

function DisableForm() {
    var input = $("input").get();
    for (var i = 0; i < input.length; i++) {
        input[i].disabled = true;
    }
}