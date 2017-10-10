alert(123);
$(function () {
    $("#getTest").click(function () {
        $.get("http://localhost:17758/home/gettest/", { name: "Kira", id: "6123" }, function (data) {
            alert("id:" + data.id + "\nname:" + data.name)
        });
    })
});