$(function () {
  $('#bootstrap-touch-slider').bsTouchSlider();

  toastr.options = {
    "closeButton": true,
    "debug": false,
    "progressBar": true,
    "positionClass": "toast-bottom-right",
    "preventDuplicates": false,
    "onclick": null,
    "timeOut": "5000",
  }

  $('[data-fancybox="gallery"]').fancybox({
    // Options will go here
  });

  $('.slick__product').slick({
    infinite: false,
    centerMode: false,
    centerPadding: '60px',
    slidesToShow: 5,
    arrows: false,
    autoplay: true,
    autoplaySpeed: 3000,
    focusOnSelect: false,
    accessibility: false,
    responsive: [
      {
        breakpoint: 900,
        settings: {
          arrows: false,
          centerMode: true,
          centerPadding: '60px',
          slidesToShow: 3
        }
      },
      {
        breakpoint: 768,
        settings: {
          arrows: false,
          centerMode: true,
          centerPadding: '60px',
          slidesToShow: 2
        }
      },
      {
        breakpoint: 480,
        settings: {
          arrows: false,
          centerMode: true,
          centerPadding: '40px',
          slidesToShow: 1
        }
      }
    ]
  });

  $('[data-role="slick-left"]').on('click', function () {
    $('.slick__product').slick('slickPrev');
  })
  $('[data-role="slick-right"]').on('click', function () {
    $('.slick__product').slick('slickNext');
  })
  $('.offer__product').slick({
    infinite: false,
    centerMode: false,
    centerPadding: '60px',
    slidesToShow: 2,
    arrows: false,
    autoplay: true,
    autoplaySpeed: 3000,
    focusOnSelect: false,
    accessibility: false,
    responsive: [
      {
        breakpoint: 768,
        settings: {
          arrows: false,
          centerMode: true,
          centerPadding: '60px',
          slidesToShow: 1
        }
      }
    ]
  });

  $('[data-role="offer-left"]').on('click', function () {
    $('.offer__product').slick('slickPrev');
  })
  $('[data-role="offer-right"]').on('click', function () {
    $('.offer__product').slick('slickNext');
  })

  $('[data-role="category"]').on('click', function () {
    $('#sidenav').css('width', '250px');
  })
  $('[data-role="category-close"]').on('click', function () {
    $('#sidenav').css('width', '0px');
  })

  var toggler = document.getElementsByClassName("parent");
  var i;

  for (i = 0; i < toggler.length; i++) {
    toggler[i].addEventListener("click", function () {
      this.parentElement.querySelector(".nested").classList.toggle("active");
      this.classList.toggle("parent-down");
    });
  }

  $('[data-role="scroll-top"]').on('click', function () {
    $("html, body").animate({ scrollTop: 0 }, "slow");
  })
})