
//$(document).ready(function () {
//    // slideing function 
//    $(".hamburger-menu").click(function () {
//        $(".left-part,.right-part").toggleClass("icon-section");
//    });
//    // end 
//    $(".hamburger-menu").click(function () {
//        $(".left-part").addClass("open");
//        $(".close").addClass("close1");
//        // $(".dashboard,.right-part").addClass("overflow");
//    });
//    $(".close").click(function () {
//        $(".left-part").removeClass("open");
//        $(".close").removeClass("close1");
//        // $(".dashboard,.right-part").removeClass("overflow");
//    });
//});

//// sidebar closes when click on document
//$(document).click(function () {
//    $('.left-part').removeClass('open');
//    $(".close").removeClass("close1");
//    // $(".right-part").removeClass("overflow");
//});
//$('.menu-item,.logo,.hamburger-menu').click(function (e) {
//    e.stopPropagation();

//});

//// eye-icon changing type password or text start
//$(document).ready(function () {
//    $('.i-icon').click(function () {
//        $(this).toggleClass("icon-active");
//        if ($(this).prev().attr('type') == 'password') {
//            $(this).prev().attr('type', 'text');
//            //   $(this).find('#pw-close').hide();
//        } else {
//            $(this).prev().attr('type', 'password');
//        }
//    });
//});
//// eye-icon changing type password or text end




//$(document).ready(function () {
//    $(".sel-dropdown").change(function () {
//        $(this).attr('data-option', 'hidden')
//        if ($(this).val()) {
//            $(this).removeAttr('data-option')
//        }
//    })
//    // slideing menu
//    //$(".report-section").click(function () {
//    //    console.log("dddd");
//    //    console.log($(this).index(), "value");
//    //    $(this).next(".report-inner-section").slideToggle();
//    //})

//    $(".profile-icon,.profile-details").click(function () {
//        $(".my-profile-popup").slideToggle();
//    })

//    $(".role-section-inner").click(function () {
//        $(".role-section-inner-slider").slideToggle();
//    })


//    $(".chart-close-icon span").click(function () {
//        $(".chat-box").toggleClass("show");
//        $(".chart-close-icon span").toggleClass("chat-active");
//        $("body,.main-page").toggleClass("hide-active");
//        $(".layer-chat").toggleClass("bg-popup");
//    })
//    $(".notification-icon,.notif_close_btn").click(function () {
//        $("#notification_popup").toggleClass("active");
//    })
//});



////   file uploader 

///*---==>Custom FIle Upload--==>*/
//$(".file-upload").each(function () {

//    var fI = $(this).children('input'),
//        btn = $(this).children('.btn-upload'),
//        i1 = $(this).children('.i-pic-upload'),
//        i2 = $(this).children('.i-deselect'),
//        dN = "No file Selected",
//        tfN = $(this).find('.text-file-name'),
//        bT = $(this).find('.text-browse');
//    bT.text('Browse');
//    tfN.text('No file Selected');

//    btn.click(function (b) {
//        b.preventDefault();
//        fI.click();
//    });
//    function readURL(input) {
//        if (input.files && input.files[0]) {
//            var reader = new FileReader();
//            reader.onload = function (e) {
//                i1.css("background", "url(" + e.target.result + ") no-repeat center").removeClass('fa fa-camera');
//            }
//            reader.readAsDataURL(input.files[0]);
//        }
//    }
//    fI.change(function (e) {
//        readURL(this);
//        var fN = e.target.files[0].name;
//        tfN.text(fN);
//        i2.fadeIn(200);
//        bT.text('Browse');
//    });
//    i2.click(function () {
//        i2.fadeOut(100);
//        window.setTimeout(function () {
//            i1.css("background", "#ebebeb").addClass('fa fa-camera');
//            bT.text('Browse');
//            tfN.text('No file Selected');
//            fI.val(null);
//        }, 200);

//    });
//});


////********* Mohan start*******/ 
//$(document).ready(function () {
//    $(".msg-frame-img-div").click(function () {
//        $(".chat-poopup").toggleClass("chat-popup-show");
//        $(".chat-popup-hide").toggleClass("chat-popup-hide-show");
//        $("body").toggleClass("chat-popup-open");
//        $(".msg-frame-img-div").toggleClass("active-img-cross");
//    });
//});
////*********Mohan  end*******/









