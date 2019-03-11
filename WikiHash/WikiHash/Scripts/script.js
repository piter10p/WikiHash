//Add slide down effect to links with data-slide=true attributes
$(document).on("click", "a[data-slide=true]", function (event) {
    event.preventDefault();
    var targetOffset = $(this.hash).offset().top;
    var currentOffset = window.pageYOffset;

    if (currentOffset > targetOffset)
        $('html,body').animate({ scrollTop: targetOffset - 75 }, 500);
    else
        $('html,body').animate({ scrollTop: targetOffset - 75 }, 500);
});

//Load medias
function loadMedias() {
    $('media').each(function () {
        $(this).text("LOADING...");
        loadMedia($(this));
    });
}

//Alerts close
$("div.alert button").click(function () {
    $(this).parent().hide(200);
});

//Medias list button click
$("#medias-filter-button").click(function () {
    GenerateMediasList(false)
});

//Medias list button click in editor
$("#medias-filter-button-editor").click(function () {
    GenerateMediasList(true)
});

//Articles filter click
$("#articles-filter-button").click(function () {
    GenerateArticlesList()
});

//Contents mobile button click
$("div.contents-mobile").click(function () {
    var container = $("div.contents-mobile-container");
    $("div.contents-mobile-container").stop(true).toggle(500);
});

function GenerateMediasList(addOption) {
    $.ajax({
        url: "/Medias/GetMediasAJAX",
        method: "get",
        dataType: 'json',
        data: {
            filter: $("#medias-filter-input").val()
        }
    })
    .done(function (res) {
        var container = $("#medias-container");
        container.text("");

        if (res.ToMuchResults)
            container.text("Too many results.");
        else if (res.Medias.length == 0)
            container.text("No results.");
        else {
            for (var i = 0; i < res.Medias.length; i++) {
                var media = res.Medias[i];
                AddMediaToMediasContainer(media, addOption);
            }
        }
    });
}

function GenerateArticlesList() {
    $.ajax({
        url: "/Explore/GetArticlesAJAX",
        method: "get",
        dataType: 'json',
        data: {
            filter: $("#articles-filter-input").val(),
            category: $("#articles-category-input").val(),
        }
    })
    .done(function (res) {
        var container = $("#articles-container");
        container.text("");
        console.log(res);

        if (res.length == 0)
            container.text("No results.");
        else {
            for (var i = 0; i < res.length; i++) {
                var article = res[i];
                AddArticleToArticlesContainer(article);
            }
        }
    });
}

function AddMediaToMediasContainer(media, addOption) {
    var mediaText = "";

    var addText = "";

    if (addOption)
        addText = `<p><button type='button' class='btn btn-secondary' selectButton data-link='${media.Link}'>Select</button></p>`;

    if (media.Type == "image")
        mediaText = `<img src="${media.Url}" class="figure-small" />`;
    else
        mediaText = `<video class="figure-small" controls>
                                    <source src="${media.Url}" type="video/mp4">
                                    Your browser does not support the video tag.
                                </video>`;

    $("#medias-container").append($(`<div class="row medias-list-container">
                                        <div class="col">
                                            <a href="/Medias/Show/${media.Link}" target="_blank"><h5 class="underlined">${media.Title}</h5></a>
                                            <p>${media.Description}</p>
                                            ${addText}
                                        </div>
                                        <div class="col">
                                            ${mediaText}
                                        </div>
                                    </div>`));
}

function AddArticleToArticlesContainer(article) {

    $("#articles-container").append($(`<p>
                                            <a href="/Articles/${article.Link}">${article.Title}</a>
                                        </p>`));

    /*var mediaText = "";

    var addText = "";

    if (addOption)
        addText = `<p><button type='button' class='btn btn-secondary' selectButton data-link='${media.Link}'>Select</button></p>`;

    if (media.Type == "image")
        mediaText = `<img src="${media.Url}" class="figure-small" />`;
    else
        mediaText = `<video class="figure-small" controls>
                                    <source src="${media.Url}" type="video/mp4">
                                    Your browser does not support the video tag.
                                </video>`;

    $("#medias-container").append($(`<div class="row medias-list-container">
                                        <div class="col">
                                            <a href="/Medias/Show/${media.Link}" target="_blank"><h5 class="underlined">${media.Title}</h5></a>
                                            <p>${media.Description}</p>
                                            ${addText}
                                        </div>
                                        <div class="col">
                                            ${mediaText}
                                        </div>
                                    </div>`));*/
}

// Add slideDown animation to Bootstrap dropdown when expanding.
$('.dropdown').on('show.bs.dropdown', function () {
    $(this).find('.dropdown-menu').first().stop(true).slideDown();
});

// Add slideUp animation to Bootstrap dropdown when collapsing.
$('.dropdown').on('hide.bs.dropdown', function () {
    $(this).find('.dropdown-menu').first().stop(true).slideUp();
});

function loadMedia(mediaElement) {
    var link = mediaElement.data("link");
    var figureClass = "figure-standard";
    var figureClassData = mediaElement.data("class");

    if (figureClassData != null)
        figureClass = figureClassData;

    if (link == null)
        mediaElement.text("Media link is null.");
    else {
        $.ajax({
            url: "/Medias/GetMediaUrl",
            method: "get",
            dataType: 'json',
            data: {
                link: link
            }
        })
        .done(function (res) {
            mediaElement.text("");
            mediaElement.next("figure").remove();
            var figureContent = getFigureContent(res, figureClass);
            $(`<figure class="figure">
                    <a href="/Medias/Show/${link}">
                        ${figureContent}
                    </a>
                    <figcaption class="figure-caption text-center">${res.Title}</figcaption>
                </figure>`)
            .insertAfter(mediaElement);
        })
        .fail(function () {
            mediaElement.text("Can not get media from server.");
        });
    }
}

function getFigureContent(res, figureClass) {

    switch (res.Type) {
        case "image":
            return `<img src="${res.Url}" class="figure-img rounded ${figureClass}">`;
        case "video":
            return `<video class="${figureClass}" controls>
                        <source src="${res.Url}" type="video/mp4">
                        Your browser does not support the video tag.
                    </video>`;
        default:
            return "<p>Media type is unsupported.</p>";
    }
}

loadMedias();