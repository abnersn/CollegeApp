/* ====== Index ======

1. JEKYLL INSTANT SEARCH
2. SCROLLBAR CONTENT
3. TOOLTIPS AND POPOVER
4. JVECTORMAP DASHBOARD
5. JVECTORMAP ANALYTICS
6. JVECTORMAP WIDGET
7. MULTIPLE SELECT
8. LOADING BUTTON
  8.1. BIND NORMAL BUTTONS
  8.2. BIND PROGRESS BUTTONS AND SIMULATE LOADING PROGRESS
9. TOASTER
10. PROGRESS BAR

====== End ======*/

$(document).ready(function() {
  "use strict";
    
  /*======== 2. SCROLLBAR CONTENT ========*/

  function scrollWithBigMedia(media) {
    var $elDataScrollHeight = $("[data-scroll-height]");
    if (media.matches) {
      /* The viewport is greater than, or equal to media screen size */
      $elDataScrollHeight.each(function() {
        var scrollHeight = $(this).attr("data-scroll-height");
        $(this).css({ height: scrollHeight + "px", overflow: "hidden" });
      });

      //For content that needs scroll
      $(".slim-scroll")
        .slimScroll({
          opacity: 0,
          height: "100%",
          color: "#999",
          size: "5px",
          wheelStep: 10
        })
        .mouseover(function() {
          $(this)
            .next(".slimScrollBar")
            .css("opacity", 0.4);
        });
    } else {
      /* The viewport is less than media screen size */
      $elDataScrollHeight.css({ height: "auto", overflow: "auto" });
    }
  }

  var media = window.matchMedia("(min-width: 992px)");
  scrollWithBigMedia(media); // Call listener function at run time
  media.addListener(scrollWithBigMedia); // Attach listener function on state changes

  /*======== 3. TOOLTIPS AND POPOVER ========*/
  $('[data-toggle="tooltip"]').tooltip({
    container: "body",
    template:
      '<div class="tooltip" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
  });
  $('[data-toggle="popover"]').popover();
    
  /*======== 7. MULTIPLE SELECT ========*/
  $(".js-example-basic-multiple").select2();

  /*======== 8. LOADING BUTTON ========*/
  /* 8.1. BIND NORMAL BUTTONS */
  Ladda.bind(".ladda-button", {
    timeout: 5000
  });

  /* 7.2. BIND PROGRESS BUTTONS AND SIMULATE LOADING PROGRESS */
  Ladda.bind(".progress-demo button", {
    callback: function(instance) {
      var progress = 0;
      var interval = setInterval(function() {
        progress = Math.min(progress + Math.random() * 0.1, 1);
        instance.setProgress(progress);

        if (progress === 1) {
          instance.stop();
          clearInterval(interval);
        }
      }, 200);
    }
  });

  /*======== 9. TOASTER ========*/
  function callToaster(positionClass) {
    if (document.getElementById("toaster")) {
      toastr.options = {
        closeButton: true,
        debug: false,
        newestOnTop: false,
        progressBar: true,
        positionClass: positionClass,
        preventDuplicates: false,
        onclick: null,
        showDuration: "300",
        hideDuration: "1000",
        timeOut: "5000",
        extendedTimeOut: "1000",
        showEasing: "swing",
        hideEasing: "linear",
        showMethod: "fadeIn",
        hideMethod: "fadeOut"
      };
      toastr.success("Welcome to sleek", "Howdy!");
    }
  }

  if (document.dir != "rtl" ){
    callToaster("toast-top-right");
  }else {
    callToaster("toast-top-left");
  }
});
