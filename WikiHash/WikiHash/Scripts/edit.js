var FramesCounter = 0;

$('div.modal#edit-frame-dialog').on('shown.bs.modal', function (e) {
    var contentFrame = $(e.relatedTarget).closest("div[contentFrame]");
    var widthValue = contentFrame.data("width");

    $("#edit-width-input").val(widthValue).change();
    $("#edit-frame-id").val(contentFrame.attr("id"));
})

//Save frame button click
$("#edit-save-button").click(function () {
    var contentFrameId = $("#edit-frame-id").val();
    var width = $("#edit-width-input").val();
    var contentFrame = $("#" + contentFrameId);
    setFrameWidth(contentFrame, width);

    $('#edit-frame-dialog').modal('toggle');
});

//Remove frame button click
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

//Add frame button click
$(document).on('click', ' div.content-frame-edit-add span', function () {
    var element = `<div contentFrame style="display: none;" id='Frame-${FramesCounter}' class='col-12' data-width='6'>
                    <div class="content-frame content-frame-edit">
                        <p>New Content Frame.</p>
                        <div class="content-frame-edit-buttons noselect">
                            <span class="fa fa-caret-up" aria-hidden="true"></span>
                            <span class="fa fa-caret-down" aria-hidden="true"></span>
                            <span class="fa fa-pencil" aria-hidden="true"
                                    data-toggle="modal" data-target="#edit-frame-dialog"></span>
                        </div>
                    </div>
                </div>`;

    $(element).insertBefore($(this).closest("div[contentFrame]")).slideDown(200);

    FramesCounter++;
});

//Tab click
$("ul.edit-topbar li").click(function () {
    $(this).parent().children().removeAttr("selected");
    $(this).attr("selected", '');
    $("#edit-tabs").children().hide();
    $("#" + $(this).data("tab")).show();
});

//Frame up button
$(document).on('click', "div.content-frame-edit-buttons span.fa-caret-up", function () {
    var contentFrame = $(this).closest("div[contentFrame]");
    var targetFrame = contentFrame.prev();

    slideToElementPosition(contentFrame, targetFrame, function () {
        targetFrame.before(contentFrame);
    });

    slideToElementPosition(targetFrame, contentFrame);
});

//Frame down button
$(document).on('click', "div.content-frame-edit-buttons span.fa-caret-down", function () {
    var contentFrame = $(this).closest("div[contentFrame]");
    var targetFrame = contentFrame.next();

    slideToElementPosition(contentFrame, targetFrame, function () {
        targetFrame.after(contentFrame);
    });

    slideToElementPosition(targetFrame, contentFrame);
});

//Textarea auto size
$('textarea').each(function () {
    this.setAttribute('style', 'height:' + (this.scrollHeight) + 'px;overflow-y:hidden;');
}).on('input', function () {
    this.style.height = 'auto';
    this.style.height = (this.scrollHeight) + 'px';
});

//Save changes button click
$("#save-changes-button").click(function () {
    saveChanges();
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

function slideToElementPosition(elementToAnimate, target, callback) {
    var top = target.position().top - elementToAnimate.position().top;
    var left = target.position().left - elementToAnimate.position().left;

    elementToAnimate.animate({ top: top , left: left}, 200, function () {
        elementToAnimate.css("top", 0).css("left", 0);
        if (typeof callback !== 'undefined') {
            callback();
        }
    });
}

function saveChanges() {
    var output = {
        Title: $("#title").val(),
        Body: getBody()
    };

    var json = JSON.stringify(output);

    console.log(output);

    $.ajax({
        url: "/Save",
        method: "POST",
        data: { articleJson: json }
    })
    .done(function (data) {
        if (data == "ok")
            $("#saved-alert").show(200);
        else
            $("#save-error-alert").show(200);
    })
    .fail(function () {
        $("#connection-error-alert").show(200);
    });
    //submit("/Save", "post", json);
}

function submit(action, method, value) {
    var form = $('<form/>', {
        action: action,
        method: method
    });
    form.append($('<input/>', {
        type: 'hidden',
        name: 'articleJson',
        value: value
    }));
    form.appendTo('body').submit();
}

function getBody() {
    var output = {
        Sections: getSections()
    };

    return output;
}

function getSections() {
    var sectionsArray = [];

    $("section").each(function () {

        var section = {
            Title: $(this).find("h3").first().find("input").first().val(),
            ContentFrames: getContentFrames($(this))
        };

        sectionsArray.push(section);
    });

    return sectionsArray;
}

function getContentFrames(sectionElement) {
    var framesArray = [];

    sectionElement.find("[contentFrame]:not([newContentFrame])").each(function () {
        var frame = {
            Content: $(this).find(">:first-child").html(),
            Width: $(this).data("width")
        };

        framesArray.push(frame);
    });

    return framesArray;
}