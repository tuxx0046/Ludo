$(document).ready(function () {

    // Sticky navigation bar
    $(".js--section-features").waypoint(function (direction) {
        if (direction == "down") {
            $("nav").addClass("sticky");
        }
        else {
            $("nav").removeClass("sticky");

        }
    }, {
        offset: "60px;"
    });


    // Scroll to signup form
    $(".js--scroll-to-form").click(function () {
        $("html, body").animate({ scrollTop: $(".js--section-form").offset().top }, 1000);
    });


    // Scroll to features
    $(".js--scroll-to-features").click(function () {
        $("html, body").animate({ scrollTop: $(".js--section-features").offset().top }, 1000);
    });


    // Navigation bar scroll
    $('a[href*="#"]')
        // Remove links that don't actually link to anything
        .not('[href="#"]')
        .not('[href="#0"]')
        .click(function (event) {
            // On-page links
            if (
                location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '')
                &&
                location.hostname == this.hostname
            ) {
                // Figure out element to scroll to
                var target = $(this.hash);
                target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                // Does a scroll target exist?
                if (target.length) {
                    // Only prevent default if animation is actually gonna happen
                    event.preventDefault();
                    $('html, body').animate({
                        scrollTop: target.offset().top
                    }, 1000, function () {
                        // Callback after animation
                        // Must change focus!
                        var $target = $(target);
                        $target.focus();
                        if ($target.is(":focus")) { // Checking if the target was focused
                            return false;
                        } else {
                            $target.attr('tabindex', '-1'); // Adding tabindex for elements not focusable
                            $target.focus(); // Set focus again
                        };
                    });
                }
            }
        });

    $("h1").addClass("animated fadeInUp");


    // Animations on scroll https://daneden.github.io/animate.css/
    $(".js--animate-features").waypoint(function () {
        $(".js--animate-features").addClass("animated fadeIn");
    }, {
        offset: "60%;"
    });


    // Mobile navigation animation
    let checkIcon = true;
    $(".js--mobile-nav-icon").click(function () {
        let nav = $(".js--main-nav");

        if (checkIcon == true) {
            $('.js--nav-icon').attr('name', 'close');
            checkIcon = false;
        } else if (checkIcon == false) {
            $('.js--nav-icon').attr('name', 'menu');
            checkIcon = true;
        }
        nav.slideToggle(200);


    })

    // Copyright set to current year
    let year = new Date().getFullYear();
    let copyrightText = "Copyright &copy; " + year + " by The Animal Life. All rights reserved.";
    $(".copyright-text").html(copyrightText);
})