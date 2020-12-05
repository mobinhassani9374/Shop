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


  $("[data-role='date']").persianDatepicker({
    format: 'YYYY/MM/DD',
    initialValue: false,
    initialValueType: 'persian'
  });

  $('form').on('submit', function () {
    var l = Ladda.create(document.querySelector('.ladda-button'));
    l.start();
  })

  $('[data-role="confirm"]').on('click', function () {
    return confirm('آیا مطمئن هستید ؟؟');
  })

  var elements = document.getElementsByTagName("INPUT");
  for (var i = 0; i < elements.length; i++) {
    elements[i].oninvalid = function (e) {
      e.target.setCustomValidity("");
      if (!e.target.validity.valid) {
        e.target.setCustomValidity("این فیلد نمی تواند فاقد مقدار باشد ");
      }
    };
    elements[i].oninput = function (e) {
      e.target.setCustomValidity("");
    };
  }

  var elements1 = document.getElementsByTagName("SELECT");
  for (var j = 0; j < elements1.length; j++) {
    elements1[j].oninvalid = function (e) {
      e.target.setCustomValidity("");
      if (!e.target.validity.valid) {
        e.target.setCustomValidity("این فیلد نمی تواند فاقد مقدار باشد ");
      }
    };
    elements1[j].oninput = function (e) {
      e.target.setCustomValidity("");
    };
  }

  var elements2 = document.getElementsByTagName("textarea");
  for (var k = 0; k < elements2.length; k++) {
    elements2[k].oninvalid = function (e) {
      e.target.setCustomValidity("");
      if (!e.target.validity.valid) {
        e.target.setCustomValidity("این فیلد نمی تواند فاقد مقدار باشد ");
      }
    };
    elements2[k].oninput = function (e) {
      e.target.setCustomValidity("");
    };
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