
"use strict"; 

	function revolutionSliderActiver () {
		if ($('.rev_slider_wrapper #slider1').length) {
			jQuery("#slider1").revolution({
				sliderType:"standard",
				sliderLayout:"auto",
				dottedOverlay:"yes",
				delay:5000,
				navigation: {
					arrows:{enable:true,
							left: {
	                        h_align: "left",
	                        v_align: "center",
	                        h_offset: 60,
	                        v_offset: 0
	                    },
	                    right: {
	                        h_align: "right",
	                        v_align: "center",
	                        h_offset: 60,
	                        v_offset: 0
	                    }

					} 
				}, 
	            gridwidth: [1200, 940, 720, 480],
	            gridheight: [700, 600, 500, 400],
	            lazyType: "none",
	            parallax: {
	                type: "mouse",
	                origo: "slidercenter",
	                speed: 2000,
	                levels: [2, 3, 4, 5, 6, 7, 12, 16, 10, 50],
	            },
	            shadow: 0,
	            spinner: "off",
	            stopLoop: "off",
	            stopAfterLoops: -1,
	            stopAtSlide: -1,
	            shuffle: "off",
	            autoHeight: "off",
	            hideThumbsOnMobile: "off",
	            hideSliderAtLimit: 0,
	            hideCaptionAtLimit: 0,
	            hideAllCaptionAtLilmit: 0,
	            debugMode: false,
	            fallbacks: {
	                simplifyAll: "off",
	                nextSlideOnWindowFocus: "off",
	                disableFocusListener: false,
	            }
			});
		};
	}


	function mainmenu() {
	    var navcollapse = $('.main-menu .navigation li');
	        navcollapse.hover(function() {
	            $(this).children('ul').stop(true, false, true).slideToggle(300);
	        });
		if($('.main-menu .mobile-menu li.dropdown ul').length){
			$('.main-menu .mobile-menu li.dropdown').append('<div class="dropdown-btn"></div>');
			
			$('.main-menu .mobile-menu li.dropdown .dropdown-btn').on('click', function() {
				$(this).prev('ul').slideToggle(500);
			});
		}
	}


	function stickyHeader() {
	    if ($('.stricky').length) {
	        var strickyScrollPos = 100;
	        if ($(window).scrollTop() > strickyScrollPos) {
	            $('.stricky').addClass('stricky-fixed');
	            $('.scroll-to-top').fadeIn(1500);
	        } else if ($(this).scrollTop() <= strickyScrollPos) {
	            $('.stricky').removeClass('stricky-fixed');
	            $('.scroll-to-top').fadeOut(1500);
	        }
	    };
	}


	function GalleryFancyboxActivator () {
	  var galleryFcb = $('.fancybox');
	  if(galleryFcb.length){
	    galleryFcb.fancybox({
	      openEffect  : 'elastic',
	      closeEffect : 'elastic',
	      helpers : {
	        media : {}
	      }
	    });
	  }
	}


	function selectMenu () {
		if ($('.select-menu').length) {
			$('.select-menu').selectmenu();
		};
	}


	function tooltip () {
		if ($('.tool_tip').length) {
				$('.tool_tip').tooltip();
			};
		$
	}


	if($('.progress-levels .progress-box .bar-fill').length){
		$(".progress-box .bar-fill").each(function() {
			var progressWidth = $(this).attr('data-percent');
			$(this).css('width',progressWidth+'%');
			$(this).children('.percent').html(progressWidth+'%');
		});
	}


	function clientCarousel () {
		if ($('.client-carousel').length) {
			$('.client-carousel.owl-carousel').owlCarousel({
			    loop: true,
			    margin: 30,
			    nav: false,
		        dots: false,
			    autoWidth: false,
			    autoplay:true,
			    autoplayTimeout:3000,
			    autoplayHoverPause:true,
			    responsive: {
			        0:{
			            items:1,
			            autoWidth: false
			        },
			        380:{
			            items:2,
			            autoWidth: false
			        },
			        540:{
			            items:3,
			            autoWidth: false
			        },
			        740:{
			            items:4,
			            autoWidth: false
			        },
			        1000:{
			            items:5,
			            autoWidth: false
			        }
			    }
			});
		};
	}


	if ($('.testimonials-carousel').length) {
		$('.testimonials-carousel').owlCarousel({
			loop:true,
			margin:30,
			nav:false,
			dots:true,
			autoplayHoverPause:false,
			autoplay: false,
			smartSpeed: 700,
			navText: [ '<span class="fa fa-angle-left"></span>', '<span class="fa fa-angle-right"></span>' ],
			responsive:{
				0:{
					items:1
				},
				600:{
					items:1
				},
				760:{
					items:2
				},
				1024:{
					items:3
				},
				1100:{
					items:3
				}
			}
		});    		
	}


	if ($('.four-item-carousel').length) {
		$('.four-item-carousel').owlCarousel({
			loop:true,
			margin:30,
			nav:true,
			smartSpeed: 700,
			autoplay: 4000,
			navText: [ '<span class="fa fa-angle-left"></span>', '<span class="fa fa-angle-right"></span>' ],
			responsive:{
				0:{
					items:1
				},
				600:{
					items:2
				},
				800:{
					items:2
				},
				1024:{
					items:3
				},
				1200:{
					items:4
				}
			}
		});    		
	}


	if ($('.five-item-carousel').length) {
		$('.five-item-carousel').owlCarousel({
			loop:true,
			margin:10,
			nav:true,
			smartSpeed: 700,
			autoplay: 4000,
			navText: [ '<span class="fa fa-angle-left"></span>', '<span class="fa fa-angle-right"></span>' ],
			responsive:{
				0:{
					items:1
				},
				600:{
					items:2
				},
				800:{
					items:3
				},
				1024:{
					items:4
				},
				1200:{
					items:5
				}
			}
		});    		
	}	

	
	function factCounter() {
		if($('.fact-counter').length){
			$('.fact-counter .counter-column.animated').each(function() {
		
				var $t = $(this),
					n = $t.find(".count-text").attr("data-stop"),
					r = parseInt($t.find(".count-text").attr("data-speed"), 10);
					
				if (!$t.hasClass("counted")) {
					$t.addClass("counted");
					$({
						countNum: $t.find(".count-text").text()
					}).animate({
						countNum: n
					}, {
						duration: r,
						easing: "linear",
						step: function() {
							$t.find(".count-text").text(Math.floor(this.countNum));
						},
						complete: function() {
							$t.find(".count-text").text(this.countNum);
						}
					});
				}
				
			});
		}
	}


	function fleetGallery () {
		if ($('.mixit-gallery').length) {
			$('.mixit-gallery').mixItUp();
		};
	}


	function videoFancybox () {
		if ($("a.video-fancybox").length) {
			$("a.video-fancybox").on('click', function() {
			    $.fancybox({
			            'padding'       : 0,
			            'autoScale'     : false,
			            'transitionIn'  : 'none',
			            'transitionOut' : 'none',
			            'title'         : this.title,
			            'width'         : 680,
			            'height'        : 495,
			            'href'          : this.href.replace(new RegExp("watch\\?v=", "i"), 'v/'),
			            'type'          : 'swf',
			            openEffect      : 'elastic',
					    closeEffect     : 'elastic',
					    helpers         : {
					    	media : {}
					    },
			            'swf'           : {
			            	'wmode'             : 'transparent',
			                'allowfullscreen'   : 'true'
			            }
			        });

			    return false;
			});
		};
	}


	function selectDropdown() {
	    if ($(".selectmenu").length) {
	        $(".selectmenu").selectmenu();

	        var changeSelectMenu = function(event, item) {
	            $(this).trigger('change', item);
	        };
	        $(".selectmenu").selectmenu({ change: changeSelectMenu });
	    };
	}


	function handlePreloader() {
		if($('.preloader').length){
			$('.preloader').delay(200).fadeOut(500);
		}
	}


	function scrollToTop() {
	    if ($('.scroll-top').length) {

	        $(window).scroll(function() {
	            if ($(this).scrollTop() > 200) {
	                $('.scroll-top').fadeIn();
	            } else {
	                $('.scroll-top').fadeOut();
	            }
	        });

	        $('.scroll-top').on('click', function() {
	            $('html, body').animate({ scrollTop: 0 }, 1500);
	            return false;
	        });
	    }
	}


	function singleProduct () {
	    
	  $('.flexslider').flexslider({
	    animation: "slide",
	    controlNav: "thumbnails"
	  });
	}


	if($('.filter-list').length){
		$('.filter-list').mixItUp({});
	}


	if($("#contact-form").length){
		$("#contact-form").validate({
		    submitHandler: function(form) {
		      var form_btn = $(form).find('button[type="submit"]');
		      var form_result_div = '#form-result';
		      $(form_result_div).remove();
		      form_btn.before('<div id="form-result" class="alert alert-success" role="alert" style="display: none;"></div>');
		      var form_btn_old_msg = form_btn.html();
		      form_btn.html(form_btn.prop('disabled', true).data("loading-text"));
		      $(form).ajaxSubmit({
		        dataType:  'json',
		        success: function(data) {
		          if( data.status == 'true' ) {
		            $(form).find('.form-control').val('');
		          }
		          form_btn.prop('disabled', false).html(form_btn_old_msg);
		          $(form_result_div).html(data.message).fadeIn('slow');
		          setTimeout(function(){ $(form_result_div).fadeOut('slow') }, 6000);
		        }
		      });
		    }
		});
	}


	if($('.wow').length){
		var wow = new WOW(
		  {
			boxClass:     'wow',     
			animateClass: 'animated',
			offset:       0,         
			mobile:       true,      
			live:         true       
		  }
		);
		wow.init();
	}


	jQuery(document).on('ready', function () {
		(function ($) {
			revolutionSliderActiver();		
			selectMenu();		
			tooltip();		
			fleetGallery();
			GalleryFancyboxActivator();
			videoFancybox();
			selectDropdown();
			handlePreloader();
			scrollToTop();
			singleProduct();
			mainmenu();	
		})(jQuery);
	});

	jQuery(window).on('load', function () {
		(function ($) {
			clientCarousel();
		})(jQuery);
	});

	jQuery(window).on('scroll', function () {	
		(function ($) {
			stickyHeader();
			factCounter();
		})(jQuery);
	});
