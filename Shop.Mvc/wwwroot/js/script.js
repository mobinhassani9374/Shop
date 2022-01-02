$(function ()
{
  // $('#bootstrap-touch-slider').bsTouchSlider();


  $(window).on("load", function ()
  {
    $('.loading').fadeOut();
  })

  toastr.options = {
    "closeButton": true,
    "debug": false,
    "progressBar": true,
    "positionClass": "toast-bottom-left",
    "preventDuplicates": false,
    "onclick": null,
    "timeOut": "8000",
  }


  // $(".dropdown").hover(
  //   function () {
  //     $('.dropdown-menu', this).not('.in .dropdown-menu').stop(true, true).slideDown("400");
  //     $(this).toggleClass('open');
  //   },
  //   function () {
  //     $('.dropdown-menu', this).not('.in .dropdown-menu').stop(true, true).slideUp("400");
  //     $(this).toggleClass('open');
  //   }
  // );
  // $("[data-role='date']").persianDatepicker({
  //   format: 'YYYY/MM/DD',
  //   initialValue: false,
  //   initialValueType: 'persian'
  // });

  $('.grid').isotope({
    itemSelector: '.grid-item',
    originLeft: false
  });

  $('form').on('submit', function ()
  {
    var l = Ladda.create(document.querySelector('button.ladda-button'));
    l.start();
  })

  $('[data-role="confirm"]').on('click', function (event)
  {
    var l = Ladda.create(this);
    if (confirm('آیا مطمئن هستید ؟؟'))
    {
      l.start();
      return true;
    }
    else
    {
      setTimeout(function ()
      {
        l.stop();
      }, 100)
      return false;
    }
  })

  $('a.ladda-button').on('click', function ()
  {
    var l = Ladda.create(this);
    l.start();
  })

  var elements = document.getElementsByTagName("INPUT");
  for (var i = 0; i < elements.length; i++)
  {
    elements[i].oninvalid = function (e)
    {
      e.target.setCustomValidity("");
      if (!e.target.validity.valid)
      {
        e.target.setCustomValidity("این فیلد نمی تواند فاقد مقدار باشد ");
      }
    };
    elements[i].oninput = function (e)
    {
      e.target.setCustomValidity("");
    };
  }

  var elements1 = document.getElementsByTagName("SELECT");
  for (var j = 0; j < elements1.length; j++)
  {
    elements1[j].oninvalid = function (e)
    {
      e.target.setCustomValidity("");
      if (!e.target.validity.valid)
      {
        e.target.setCustomValidity("این فیلد نمی تواند فاقد مقدار باشد ");
      }
    };
    elements1[j].oninput = function (e)
    {
      e.target.setCustomValidity("");
    };
  }

  var elements2 = document.getElementsByTagName("textarea");
  for (var k = 0; k < elements2.length; k++)
  {
    elements2[k].oninvalid = function (e)
    {
      e.target.setCustomValidity("");
      if (!e.target.validity.valid)
      {
        e.target.setCustomValidity("این فیلد نمی تواند فاقد مقدار باشد ");
      }
    };
    elements2[k].oninput = function (e)
    {
      e.target.setCustomValidity("");
    };
  }

  $('[data-fancybox="gallery"]').fancybox({
    loop: true
  });

  $('.offer__product').slick({
    infinite: false,
    centerMode: false,
    centerPadding: '60px',
    slidesToShow: 3,
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

  $('[data-role="offer-left"]').on('click', function ()
  {
    $('.offer__product').slick('slickPrev');
  })
  $('[data-role="offer-right"]').on('click', function ()
  {
    $('.offer__product').slick('slickNext');
  })

  $('[data-role="category"]').on('click', function ()
  {
    $('#sidenav').css('width', '300px');
  })
  $('[data-role="category-close"]').on('click', function ()
  {
    $('#sidenav').css('width', '0px');
  })

  $('[data-toggle="tooltip"]').tooltip();

  var toggler = document.getElementsByClassName("parent");
  var i;

  for (i = 0; i < toggler.length; i++)
  {
    toggler[i].addEventListener("click", function ()
    {
      this.parentElement.querySelector(".nested").classList.toggle("active");
      this.classList.toggle("parent-down");
    });
  }

  $('[data-role="scroll-top"]').on('click', function ()
  {
    $("html, body").animate({ scrollTop: 0 }, "slow");
  })

  // $('[data-role="seperator"]').map(function (index, item)
  // {
  //   var value;
  //   var pattern = /(-?\d+)(\d{3})/;
  //   while (pattern.test(value))
  //     value = value.toString().replace(pattern, "$1,$2");
  //   $(item).html($(item).html());
  // })
})