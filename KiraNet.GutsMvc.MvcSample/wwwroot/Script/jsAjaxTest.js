$(function () {
    $("#getTest").click(function () {
        var user = { id: "6123", name: "Kira" };
        $.get("http://localhost:17758/home/getrazor/", user, function (data) {
            alert('get:'+data.status);
        });
    })
});

$(function () {
    $("#postTest").click(function () {
        var user = { id: "6123", name: "Kira" };
        $.get("http://localhost:17758/home/getrazor/", user, function (data) {
            alert('post:'+data.status);
        });
    })
});