//Add slide down effect to links with data-slide=true attributes
$('a[data-slide=true]').on('click', function (event) {
    event.preventDefault();
    var targetOffset = $(this.hash).offset().top;
    var currentOffset = window.pageYOffset;

    if (currentOffset > targetOffset)
        $('html,body').animate({ scrollTop: targetOffset - 75 }, 500);
    else
        $('html,body').animate({ scrollTop: targetOffset - 75 }, 500);

});

//Loads medias
$('media').each(function () {
    $(this).text("LOADING...");
    loadMedia($(this));
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
            var figureContent = getFigureContent(res, figureClass)
            mediaElement.text("");
            mediaElement.append(
                `<figure class="figure">
                    <a href="/Medias/Show/${link}">
                        ${figureContent}
                    </a>
                    <figcaption class="figure-caption text-center">${res.Title}</figcaption>
                </figure>`
            );
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