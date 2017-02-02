var ApplicationUserController = function () {
    var fail = function () {
        alert("Something went wrong");
    }


    var attachEventsOnDocumentLoad = function () {
        var followingButton = $(".js-toggle-following");
        if (followingButton.text() === "Following") {
            followingButton.mouseover(function () {
                $(this).text('Unfollow');
            }).mouseleave(function () {
                $(this).text('Following');
            });
        }
    }

    var toggleFollowing = function () {
        $("#allGigs").on('click', '.js-toggle-following', function (e) {
            var currentButton = $(e.target);
            if (currentButton.text() === "Unfollow") {
                $.post("/api/UserFollowings/Unfollow", { followeeId: currentButton.attr("data-artist-id") })
                    .done(function () {
                        currentButton.text('Follow');
                        currentButton.off("mouseover").off("mouseleave");
                    })
                    .fail(fail);
            } else if (currentButton.text() === "Follow") {
                $.post("/api/UserFollowings/Follow", { followeeId: currentButton.attr("data-artist-id") })
                    .done(function () {
                        currentButton.text('Following');
                        currentButton.mouseover(function () {
                            $(this).text('Unfollow');
                        }).mouseleave(function () {
                            $(this).text('Following');
                        });
                    }).fail(fail);
            }
        });

    }


    return {
        attachEventsOnDocumentLoad: attachEventsOnDocumentLoad,
        toggleFollowing: toggleFollowing
    }
}();