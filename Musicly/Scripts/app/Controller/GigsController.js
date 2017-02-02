var GigsController = function () {

    var attachEventsOnDocumentLoad = function () {
        var cancelButton = $(".btn-toggle-attendance[value='Going']");
        cancelButton.mouseover(function () {
            $(this).prop('value', 'Cancel');
        }).mouseleave(function () {
            $(this).prop('value', 'Going');
        });

        var followingButton = $(".js-toggle-following");
        if (followingButton.text() === "Following") {
            followingButton.mouseover(function () {
                $(this).text('Unfollow');
            }).mouseleave(function () {
                $(this).text('Following');
            });
        }
    }


    return {
        attachEventsOnDocumentLoad: attachEventsOnDocumentLoad,
    }

}();

