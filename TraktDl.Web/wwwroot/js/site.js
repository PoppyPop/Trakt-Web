// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const uri = 'api/show';

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
        return text.substring(0, 38)+"...";
    } 
    return text;
});

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: 'GET',
        url: uri + '/missings',
        success: function (data) {
            $('#todos').empty();
            //getCount(data.length);
            $.each(data, function (key, item) {

                var source = $('#template').html();
                var template = Handlebars.compile(source);

                $('#progress-wrapper').append(template(item));
            });

            todos = data;
        }
    });
}