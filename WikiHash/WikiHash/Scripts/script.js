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