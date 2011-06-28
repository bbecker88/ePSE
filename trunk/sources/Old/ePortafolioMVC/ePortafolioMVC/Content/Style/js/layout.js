(function($){
	var initLayout = function() {
		var hash = window.location.hash.replace('#', '');
		var currentTab = $('ul.navigationTabs a')
							.bind('click', showTab)
							.filter('a[rel=' + hash + ']');
		if (currentTab.size() == 0) {
			currentTab = $('ul.navigationTabs a:first');
		}
		var now = new Date();
		showTab.apply(currentTab.get(0));
		$('#date').DatePicker({
			flat: true,
			date: '2010-12-07',
			current: '2010-12-07',
			calendars: 1,
			starts: 1
		});
		$('#widgetCalendar div.datepicker').css('position', 'absolute');
	};
	
//	now.getFullYear()+'-'+now.getMonth()+'-'+now.getDay(),
	
	var showTab = function(e) {
		var tabIndex = $('ul.navigationTabs a')
							.removeClass('active')
							.index(this);
		$(this)
			.addClass('active')
			.blur();
		$('div.tab')
			.hide()
				.eq(tabIndex)
				.show();
	};
	
	EYE.register(initLayout, 'init');
})(jQuery)