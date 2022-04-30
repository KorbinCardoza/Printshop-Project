// Gets the current user's name
function getUser(url) {
    $.ajax({
        url: url,
        contentType: "application/html; charset=utf-8",
        type: "GET",
        dataType: "html",
        success: function (result) {
            $("#currentUserSpinner").remove();
            $("#currentUser").hide().append(`Hello, ${result}`).fadeIn(500);
        },
        error: function (err) {
            console.log(`Failed to load ${url}. Reason: ${err} ${err.msg}`);
        }
    });
}