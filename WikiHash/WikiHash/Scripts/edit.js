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

$("#edit-remove-button").click(function () {
    var confirmation = confirm("Are you sure?");

    if (confirmation) {
        var frameId = $("#edit-frame-id").val();
        $('#edit-frame-dialog').modal('toggle');

        $("#" + frameId).slideUp(200, function () {
            $("#" + frameId).remove();
        });
    }
});

$("div.content-frame-edit-add span").click(function () {
    var element = `<div contentFrame style="display: none;" id='Frame-${FramesCounter}' class='col-12' data-width='6'>
                        <div class="content-frame content-frame-edit">
                            New Content Frame.
                            <div class="content-frame-edit-buttons">
                                <span class="fa fa-pencil" aria-hidden="true"
                                        data-toggle="modal" data-target="#edit-frame-dialog"></span>
                            </div>
                        </div>
                    </div>`;

    $(element).insertBefore($(this).closest("div[contentFrame]")).slideDown(200);

    FramesCounter++;
});

$("ul.edit-topbar li").click(function () {
    $(this).parent().children().removeAttr("selected");
    $(this).attr("selected", '');
    $("#edit-tabs").children().hide();
    $("#" + $(this).data("tab")).show();
});

//Textarea auto size
$('textarea').each(function () {
    this.setAttribute('style', 'height:' + (this.scrollHeight) + 'px;overflow-y:hidden;');
}).on('input', function () {
    this.style.height = 'auto';
    this.style.height = (this.scrollHeight) + 'px';
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