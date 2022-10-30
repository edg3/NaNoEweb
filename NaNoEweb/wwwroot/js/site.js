$(document).on('click', '.novel-listed', function () {
    var id = $(this).attr('data-id');
    var url = "../../../Home/Act_LoadListing";
    var formData = new FormData();
    formData.append('id', id);
    $.ajax({
        type: 'POST',
        url: url,
        data: formData,
        processData: false,
        contentType: false
    }).done(function (response) {
        if (response.status === "success") {
            window.location = "../../../Write"
        }
        else {
            console.log(response);
        }
    });
});

$(document).on('click', '[action-name="create-new-novel"]', function () {
    window.location = "../../../Home/CreateNewNovel";
});

$(document).on('click', '[action-name="open-existing-novel"]', function () {
    window.location = "../../../Home/OpenExistingNovel";
});

$(document).on('click', '[action-name="novel-history"]', function () {
    window.location = "../../../Home/";
});

$(document).on('click', '[action-name="close-current-novel"]', function () {
    $.ajax({
        type: 'POST',
        url: '../../../Home/Act_UnloadListing',
        processData: false,
        contentType: false
    }).done(function (response) {
        if (response.status === "success") {
            window.location = "../../../"
        }
        else {
            console.log(response);
        }
    });
});

// Global vars
var isOnNotes = false;
// Global additional logic on ready
$(document).ready(function () {
    /* put 'no-enter' as a class on any input that accidentally submits form on 'enter key' when it isn't wanted */
    $(window).keydown(function (event) {
        if (event.keyCode == 13 && $(event.target).hasClass('no-enter')) {
            event.preventDefault();
            return false;
        }
    });

    /* Used to do writing scroll versus notes scrolls */
    $('.notes').mouseenter(function () { isOnNotes = true; });
    $('.notes').mouseleave(function () { isOnNotes = false; });

    /* Test jQuery UI */
    $("#dialog").dialog({
        autoOpen: false,
        width: 400,
        buttons: [
            {
                text: "Yes",
                click: function () {
                    alert('faked delete item: ' + $('#dialog').attr('data-id'));
                    $(this).dialog("close");
                }
            },
            {
                text: "Cancel",
                click: function () {
                    $(this).dialog("close");
                }
            }
        ]
    });
    // Link to open the dialog
    $("#dialog-link").click(function (event) {

        $('#dialog').attr('data-id', '123');
        $("#dialog").dialog("open");
        event.preventDefault();
    });
});