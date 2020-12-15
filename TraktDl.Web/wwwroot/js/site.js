// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const uri = 'api/show';

HandlebarsIntl.registerWith(Handlebars);

Handlebars.registerHelper('progress-status', function (percent) {
    if (percent >= 0 && percent < 25) {
        return "danger";
    } else if (percent >= 25 && percent < 50) {
        return "warning";
    } else if (percent >= 50 && percent < 75) {
        return "info";
    } else {
        return "success";
    }
});

Handlebars.registerHelper('truncate', function (text) {
    if (text && text.length > 41) {
        return text.substring(0, 38) + "...";
    }
    return text;
});

Handlebars.registerHelper('episode-status', function (status) {
    if (status === 0) {
        return "other";
    } else if (status === 1) {
        return "check";
    } else {
        return "delete";
    }
});

$(document).ready(function () {
    getData();
});

function resetBlacklist() {
    $('#blacklistReset').addClass("fa-spin");

    $.ajax({
        type: 'POST',
        url: uri + '/ResetBlacklist',
        success: function (data) {
            $('#blacklistReset').removeClass("fa-spin");
            getData();
        }
    });
}

function resetImages() {
    $('#imagesReset').addClass("fa-spin");

    $.ajax({
        type: 'POST',
        url: uri + '/ResetImages',
        success: function (data) {
            $('#imagesReset').removeClass("fa-spin");
            getData();
        }
    });
}

function updateTraking() {
    $('#updateButton').addClass("fa-spin");

    $.ajax({
        type: 'POST',
        url: uri,
        success: function (data) {
            $('#updateButton').removeClass("fa-spin");
            getData();
        }
    });
}

function updateImages() {
    $('#imagesButton').addClass("fa-spin");

    $.ajax({
        type: 'POST',
        url: uri + '/Images',
        success: function (data) {
            $('#imagesButton').removeClass("fa-spin");
            getData();
        }
    });
}

function updateImage(show, next) {

    $('#show-wait-poster-' + show).LoadingOverlay("show");

    if (next) {
        $('#show-wait-fanart-' + show).LoadingOverlay("show");
    }

    $.ajax({
        type: 'POST',
        url: uri + '/' + show + '/Images',
        success: function (data) {

            $('#show-poster-' + show).attr("src", data.posterUrl);
            $('#show-wait-poster-' + show).LoadingOverlay("hide");

            if (data.nextEpisodeToCollect) {
                $('#show-collect-poster-' + show).attr("src", data.nextEpisodeToCollect.posterUrl);
                $('#show-collect-name-' + show).text(data.nextEpisodeToCollect.name);
                $('#show-collect-date-' + show).text(data.nextEpisodeToCollect.airDate);
            }

            if (next) {
                $('#show-wait-fanart-' + show).LoadingOverlay("hide");
            }
        }
    });
}

function getData() {
    $('#progress-wrapper').empty();
    $('#refreshButton').addClass("fa-spin");

    $.ajax({
        type: 'GET',
        url: uri + '/missings',
        success: function (data) {
            $('#refreshButton').removeClass("fa-spin");
            $.each(data, function (key, item) {
                $('#progress-wrapper').append(Handlebars.templates.row(item));

                if (!item.posterUrl || (item.nextEpisodeToCollect && !item.nextEpisodeToCollect.posterUrl)) {
                    updateImage(item.id, item.nextEpisodeToCollect);
                }

            });
        }
    });
}