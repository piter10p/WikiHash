var FramesCounter = 0;

var toolbarOptions = [
    ['bold', 'italic', 'underline', 'strike', { 'script': 'sub' }, { 'script': 'super' }],
    ['blockquote', 'code-block'],
    [{ align: '' }, { align: 'center' }, { align: 'right' }, { align: 'justify' }],
    [{ 'list': 'ordered' }, { 'list': 'bullet' }]
];

//Edit dialog show
$('div.modal#edit-frame-dialog').on('show.bs.modal', function (e) {
    setEditDialogData(e);
})

//Save frame button click
$("#edit-save-button").click(function () {
    saveContentFrameChanges();
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
    var element = `<div contentframe="" id="Frame-${FramesCounter}" class="col-12" data-width="6" style="display: none;">
                        <div class="content-frame content-frame-edit">

                            <div class="ql-container ql-disabled"><div class="ql-editor" data-gramm="false" contenteditable="false"><p class="ql-align-left">New Content Frame.</p></div><div class="ql-clipboard" contenteditable="true" tabindex="-1"></div></div>

                            <div class="content-frame-edit-buttons noselect">
                                <span class="fa fa-caret-up" aria-hidden="true"></span>
                                <span class="fa fa-caret-down" aria-hidden="true"></span>
                                <span class="fa fa-pencil" aria-hidden="true" data-toggle="modal" data-target="#edit-frame-dialog"></span>
                            </div>
                        </div>
                    </div>`;

    $(element).insertBefore($(this).closest("div[contentFrame]")).slideDown(200);

    FramesCounter++;
});

//Tab click
$("ul.edit-topbar li").click(function () {
    activateTab($(this));
});

function activateTab(tabElement) {
    tabElement.parent().children().removeAttr("selected");
    tabElement.attr("selected", '');
    $("#edit-tabs").children().hide();
    $("#" + tabElement.data("tab")).show();
}

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

//Save changes button click
$("#save-changes-button").click(function () {
    saveChanges();
});

//Select button click in medias list
$(document).on('click', "button[selectButton]", function (e) {
    var button = $(e.target);
    var link = button.data("link");

    $("#selected-media-link").text(link);
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
        Title: $("#title").text(),
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
}

//Unused. For tests only
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

        var isMedia = frameContainMedia($(this));

        var content;

        if (!isMedia)
            content = getEditorContentElement($(this)).html();
        else
            content = getContentElement($(this)).children().first().prop('outerHTML');;

        var frame = {
            Content: content,
            Width: $(this).data("width")
        };

        framesArray.push(frame);
    });

    return framesArray;
}

/*Editor Dialog*/
function setEditDialogData(sender) {
    var contentFrame = $(sender.relatedTarget).closest("div[contentFrame]");
    setBasicEditDialogData(contentFrame);
    fillContentEditor(contentFrame);
}

function setBasicEditDialogData(contentFrame) {
    var widthValue = contentFrame.data("width");
    $("#edit-width-input").val(widthValue).change();
    $("#edit-frame-id").val(contentFrame.attr("id"));
}

function fillContentEditor(contentFrame) {
    var isMedia = frameContainMedia(contentFrame);

    clearMediaEditor();
    clearTextEditor();

    if (!isMedia) {
        var contentElement = getEditorContentElement(contentFrame);
        fillTextEditor(contentElement);
        activateTab($("li[data-tab=edit-text-tab]"));
    }
    else {
        var contentElement = getContentElement(contentFrame);
        fillMediaEditor(contentElement);
        activateTab($("li[data-tab=edit-media-tab]"));
    }
        
}

function frameContainMedia(contentElement) {
    var media = contentElement.find("media");

    if (media.length > 0)
        return true;
    return false;
}

function fillTextEditor(contentElement) {
    quill.root.innerHTML = contentElement.html();
}

function fillMediaEditor(contentElement) {
    var media = contentElement.children().first();

    $("#selected-media-link").text(media.data("link"));

    switch (media.data("class")) {
        case "figure-small":
            $("#media-size").val("small");
            break;

        case "figure-big":
            $("#media-size").val("big");
            break;

        default:
            $("#media-size").val("normal");
            break;
    }
}

function clearTextEditor() {
    quill.root.innerHTML = "";
}

function clearMediaEditor() {
    $("#selected-media-link").text("");
}

function saveContentFrameChanges() {
    var contentFrameId = $("#edit-frame-id").val();
    var width = $("#edit-width-input").val();
    var contentFrame = $("#" + contentFrameId);
    setFrameWidth(contentFrame, width);

    if ($("ul.edit-topbar li[selected]").data("tab") == "edit-text-tab")
        setFrameTextContent(contentFrame, quill.root.innerHTML);
    else {
        var link = $('#selected-media-link').text();
        var size = $('#media-size').val();

        if (link == "")
            alert("Select media.");
        else {
            var content = `<media data-link="${link}" data-class="figure-${size}"></media>`;
            setFrameMediaContent(contentFrame, content);
        }
    }

    $('#edit-frame-dialog').modal('toggle');
}

function setFrameTextContent(contentFrame, content) {
    var contentElement = getContentElement(contentFrame);
    contentElement.prop("class", "ql-container ql-disabled");
    contentElement.html(`<div class="ql-editor" data-gramm="false" contenteditable="false">${content}</div><div class="ql-clipboard" contenteditable="true" tabindex="-1"></div>`);
}

function setFrameMediaContent(contentFrame, content) {
    var contentElement = getContentElement(contentFrame);
    contentElement.prop("class", "");
    contentElement.html(content);
    loadMedias();
}

function getContentElement(contentFrame) {
    return contentFrame.children().first().children().first();
}

function getEditorContentElement(contentFrame) {
    return getContentElement(contentFrame).children().first();
}

