(function ($) {
    'use strict';

    var $window = $(window);

    // :: 11.0 Preloader active code
    $window.on('load', function () {
        $('#preloader').fadeOut('slow', function () {
            $(this).remove();
        });
    });  

    // With JQuery
    $("#ex2").slider({});

    // Without JQuery
    var slider = new Slider('#ex2', {});

})(jQuery);

    