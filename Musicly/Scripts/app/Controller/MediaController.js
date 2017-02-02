/// <reference path="jQuery.1.10.2/Content/Scripts/jquery-1.10.2.js" />


var mediaController = function () {
    var video = document.querySelector("#video"),
        playPause = $("#playPause"),
        btnRewind = $("#rewind"),
        btnVolume = $("#volume"),
        timeNow = $("#currentTime"),
        totalTime = $("#totalTime"),
        timeRail = $("#rail"),
        scrubber = document.getElementById("scrubber"),
        volumeScrubber = $("#volumeScrubber"),
        volumeRail = $("#volumeRail");

    var timeStep = 5;

    //time formate method
    function formatTime(seconds) {
        seconds = Math.round(seconds);
        var minutes = 0;

        if (seconds >= 60) {
            minutes = Math.floor(seconds / 60);
            seconds = seconds - (minutes * 60);
        }

        seconds = seconds + '';
        if (seconds.length === 1 || minutes.length === 1) {
            seconds = '0' + seconds;
            minutes = '0' + minutes;
        }
        return minutes + ':' + seconds;
    };

    var onVideoMetaLoad = video.addEventListener('loadedmetadata', function () {
        $(".dtm-video-duration").removeClass("display-non").addClass("display-inlineblock");
        $(".dtm-video-duration").text(formatTime(video.duration));
    }, true);

    var onVideoPlay = video.addEventListener('play', function () {
        $("#btn-start").fadeOut();
        $(".dtm-video-duration").addClass("display-non").removeClass("display-inlineblock");
        $(".dtm-bottom-controls").addClass("display-block").removeClass("display-non");
        totalTime.text(formatTime(video.duration));
        volumeScrubber.height(100 - (video.volume * 100) + "%");
    }, true);

    var btnStartClick =
        $("#btn-start").click(function () {
            $(this).text("pause_circle_filled");
            $(".dtm-bottom-controls").removeClass("display-non").addClass("display-block");
            video.play();
            if (video.played) {
                $("#playPause").text("pause");
            }
        });

    var btnPlayPauseClick =
        playPause.click(function () {
            if (video.paused || video.ended) {
                video.play();
                $("#playPause").text("pause");
            } else {
                video.pause();
                $("#playPause").text("play_arrow");
            }
        });

    var btnRewindClick =
        btnRewind.click(function () {
            for (var i = 0; i < timeStep; i++) {
                video.currentTime -= i;
            }
        });


    var btnMuteClicked =
        btnVolume.click(function () {
            if (!video.muted) {
                $(this).text("volume_off");

            } else {
                $(this).text("volume_up");
            }
            video.muted = !video.muted;
        });


    var onVideoTimeUpdate = video.addEventListener('timeupdate', function () {
        var valueNow = (video.currentTime / video.duration) * 100;
        timeNow.text(formatTime(video.currentTime));
        scrubber.style = "width:" + valueNow + "%;";
    }, true);

    var onVideoProgress = video.addEventListener('progress', function () {
        var index = (video.buffered.length - 1);
        var end = video.buffered.end(index);
        var length = (end / video.duration) * 100;
        var prog = document.getElementById("dtm-progress");
        prog.style = "width:" + length + "%;";
    }, true);

    var onTimeRailClick = timeRail.click(function (e) {
        var xCord = e.pageX - $("#rail").offset().left;

        var width = (xCord / this.clientWidth) * 100;

        var time = (width / 100) * video.duration;

        scrubber.style = "width:" + width + "%;";
        video.currentTime = time;
    });

    var onVolumeRailClick = volumeRail.click(function (e) {
        var scrubberHeight = e.pageY - volumeRail.offset().top;
        volumeScrubber.height(scrubberHeight);

        var totalHeight = 130;

        var volume = 1 - (scrubberHeight / totalHeight);

        video.volume = volume;
      
    });

    var onVideoEnded = video.addEventListener('ended', function () {
        video.pause();
        $("#playPause").text("replay");
    }, true);

   

    var onVideoError = video.addEventListener('error', function () {
        $(".dtm-media-container").addClass("display-non");
        $(".dtm-bottom-controls").addClass("display-non");
        alert("video is not available to you");
    }, true);

    function expandToFullscreen(element) {

        $("#player-container").removeClass().addClass("dtm-player-container-fullscreen");
        $("#mediaContainer").removeClass().addClass(".dtm-media-container-fullscreen");
        $("#video").removeClass().addClass("dtm-video-element-fullscreen");
        if (element.requestFullscreen) {
            element.requestFullscreen();
        } else if (element.mozRequestFullscreen) {
            element.mozRequestFullscreen();
        } else if (element.webkitRequestFullScreen) {
            element.webkitRequestFullScreen();
        } else if (element.msRequestFullscreen) {
            element.msRequestFullscreen();
        }

        $(".btn-video-expand").text("fullscreen_exit");
    };

    function exitToFullscreen() {
        $("#player-container").removeClass().addClass("dtm-player-container col-lg-8 col-sm-8 col-md-8 col-xs-12 col-md-offset-2 col-lg-offset-2 col-sm-offset-2");
        $("#video").removeClass().addClass("dtm-video-element");
        $("#mediaContainer").removeClass().addClass("dtm-media-container");
        if (document.exitFullscreen) {
            document.exitFullscreen();
        } else if (document.webkitExitFullscreen) {
            document.webkitExitFullscreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.msExitFullscreen) {
            document.msExitFullscreen();
        }

        $(".btn-video-expand").text("fullscreen");
    };


    var onVideoExpandClick =
        $(".btn-video-expand").click(function () {

            if ($('.btn-video-expand:contains("fullscreen_exit")').length > 0) {
                exitToFullscreen();
            } else if ($('.btn-video-expand:contains("fullscreen")').length > 0) {
                expandToFullscreen(document.getElementById("player-container"));
            }
        });

    return {
        btnPlayPauseClick: btnPlayPauseClick,
        btnStopClick: btnStopClick,
        btnRewindClick: btnRewindClick,
        btnFastFarwordClick: btnFastFarwordClick,
        btnMuteClicked: btnMuteClicked,
        btnStartClick: btnStartClick,
        formatTime: formatTime,
        video: video,
        onVideoPlay: onVideoPlay,
        onVideoTimeUpdate: onVideoTimeUpdate,
        onVideoProgress: onVideoProgress,
        onTimeRailClick: onTimeRailClick,
        onVolumeRailClick: onVolumeRailClick,
        onVideoEnded: onVideoEnded,
        onVideoMetaLoad: onVideoMetaLoad,
        onVideoError: onVideoError,
        onVideoExpandClick: onVideoExpandClick
    }

}();






