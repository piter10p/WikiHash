var FramesCounter = 0;

$('div.modal#edit-frame-dialog').on('shown.bs.modal', function (e) {
    var contentFrame = $(e.relatedTarget).closest("div[contentFrame]");
    var widthValue = contentFrame.data("width");

    $("#edit-width-input").val(widthValue).change();
    $("#edit-frame-id").val(contentFrame.attr("id"));
})

$("#edit-save-button").click(function () {
    var contentFrameId = $("#edit-frame-id").val();
    var width = $("#edit-width-input").val();
    var contentFrame = $("#" + contentFrameId);
    setFrameWidth(contentFrame, width);

    $('#edit-frame-dialog').modal('toggle');
});

function setFrameWidth(frameElement, width) {
    frameElement.data("width", width);

    if (width == "auto") {
        frameElement.removeClass();
        frameElement.addClass("col");
    }
    else {
        var bootstrapWidth = Number.parseInt(width) * 2;
        frameElement.removeClass();
        frameElement.addClass("col-" + bootstrapWidth);
    }
}