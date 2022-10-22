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