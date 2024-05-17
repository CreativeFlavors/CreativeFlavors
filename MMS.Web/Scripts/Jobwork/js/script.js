$(document).ready(function () {

    resise();
    resisescroll();

});
function resise() {

    var win = $(window).height();

    var win_width = $(window).width();

    var widleft = $(".left-side-container").width();
    var htcpy = $(".copyrights").height();
    var httphdr = $(".top-header").height();
    $(".left-side-container").css({
        height: win

    });
    $(".right-side-container").css({
        height: win
    });
    $(".dashboard-content-pad").css({
        height: win - httphdr - 54

    });
}
function resisescroll() {

    var win = $(window).height();


    var htlogo = $(".logo-section-background").height();
//    var htlogobtm = $(".profile-div").height();

    var fnht = win - htlogo;
    $(".dashboard-menu1").css({
        height: fnht
    });

}

//$(document).ready(function () {
//
//    var win_width = $(window).width();
//    $(".nav-icon").click(function () {
//        if (win_width >= 768)
//        {
//            if ($(".left-side-container").hasClass("left-menu"))
//            {
//                $(".left-side-container").removeClass("left-menu");
//                $(".left-side-container").css({"width": 80});
//                $(".resize-menu").css({"display": "inline-block"});
//                $(".menu").css({"display": "none"});
//                $(".resize-logo").css({"display": "block"});
//                $(".menu-logo").css({"display": "none"});
//                $(".copyrights").css({"display": "none"});
//                resise();
//            } else
//            {
//                $(".left-side-container").addClass("left-menu");
//                $(".left-side-container").css({"width": 256});
//                $(".resize-menu").css({"display": "none"});
//                $(".menu").css({"display": "block"});
//                $(".resize-logo").css({"display": "none"});
//                $(".menu-logo").css({"display": "block"});
//                $(".copyrights").css({"display": "block"});
//                resise();
//            }
//        }
//
//    });

$(".nav-icon1").click(function () {

    if ($(".left-side-container").hasClass("left-menu"))
    {
        $(".left-side-container").removeClass("left-menu");
        $(".left-side-container").css({"left": 0});
          $("body").css({"overflow": "hidden"});
    } else
    {
        $(".left-side-container").addClass("left-menu");
        $(".left-side-container").css({"left": -282});
        $("body").css({"overflow": "auto"});
    }

});

$(window).resize(function ()
{
    resise();
    resisescroll();
});
$(window).load(function ()
{
    resise();
    resisescroll();
});



