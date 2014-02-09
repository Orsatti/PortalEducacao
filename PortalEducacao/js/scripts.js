jQuery(document).ready(function() { 
	
	"use strict";
	
	// Main Menu Drop Down
	jQuery('ul#main-menu').superfish({ 
        delay:       600,
        animation:   {opacity:'show',height:'show'},
        speed:       'fast',
        autoArrows:  true,
        dropShadows: false
    });
	
	// Accordion
	jQuery( ".accordion" ).accordion( { autoHeight: false } );

	// Toggle	
	jQuery( ".toggle > .inner" ).hide();
	jQuery(".toggle .title").bind('click',function() {
		jQuery(this).toggleClass('active');
		if (jQuery(this).hasClass('active')) {
			jQuery(this).closest('.toggle').find('.inner').slideDown(200, 'easeOutCirc');
		} else {
			jQuery(this).closest('.toggle').find('.inner').slideUp(200, 'easeOutCirc');
		}
	});
	
	// Tabs
	jQuery(function() {
		jQuery( "#tabs" ).tabs();
	});
	
	// PrettyPhoto
	jQuery(document).ready(function(){
		jQuery("a[rel^='prettyPhoto']").prettyPhoto();
	});
	
	// Search Button Toggle
	jQuery(".menu-search-button").click(function() {
		jQuery(".menu-search-field").toggleClass("menu-search-focus", 200);
	});

});

// Main Slider
jQuery(window).load(function(){
	
	"use strict";
	
	jQuery('.slider').flexslider({
		animation: "slide",
		controlNav: false,
		before: function(slider) {
			slider.removeClass('slide-loader');
		}
	});
	
});

// Page Slider
jQuery(window).load(function(){
	
	"use strict";
	
	jQuery('.page-slider').flexslider({
		animation: "slide",
		controlNav: false,
		start: function(slider){
			jQuery('body').removeClass('loading');
		}
	});
	
});

// Block Slider
jQuery(window).load(function(){
	
	"use strict";
	
	jQuery('.slider-blocks').flexslider({
		animation: "slide",
		controlNav: false,
		directionNav: true,
		slideshow: false,
		start: function(slider){
			jQuery('body').removeClass('loading');
		}
	});
	
});

// Scroll to top
jQuery(document).ready(function(){
	
	"use strict";
	
	jQuery(window).scroll(function(){
		if (jQuery(this).scrollTop() > 100) {
			jQuery('.scrollup').fadeIn();
		} else {
			jQuery('.scrollup').fadeOut();
		}
	});
	
	jQuery('.scrollup').click(function(){
		jQuery("html, body").animate({ scrollTop: 0 }, 600);
		return false;
	});
	
});

jQuery(function () {
	
	"use strict";
	
	// Mobile Menu
	jQuery('#main-menu').tinyNav({
		active: 'selected',
		header: 'Select a page...',
		label: ''
	});
	
	// Form Select Styling
	jQuery("select").uniform();
	
});