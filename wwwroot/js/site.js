// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener('DOMContentLoaded', function () {
	document.body.classList.add('motion-ready');

	const animatedTargets = document.querySelectorAll('.page-header, .paper-card, .table-responsive, .form-container, .search-container');
	animatedTargets.forEach(function (element, index) {
		element.classList.add('reveal-panel');
		element.style.setProperty('--reveal-delay', `${index * 55}ms`);
		window.setTimeout(function () {
			element.classList.add('is-revealed');
		}, 20);
	});

	document.querySelectorAll('form').forEach(function (form) {
		form.addEventListener('submit', function () {
			const submitButton = form.querySelector('button[type="submit"]');
			if (submitButton) {
				submitButton.classList.add('btn-pending');
				submitButton.disabled = true;
			}
		});
	});

	document.querySelectorAll('a.btn-outline-dark, a.btn-danger, button.btn-danger').forEach(function (action) {
		action.addEventListener('click', function () {
			action.classList.add('action-flash');
			window.setTimeout(function () {
				action.classList.remove('action-flash');
			}, 220);
		});
	});

	document.querySelectorAll('.paper-table tbody tr').forEach(function (row, index) {
		row.style.setProperty('--row-delay', `${index * 18}ms`);
		row.classList.add('row-reveal');
		window.setTimeout(function () {
			row.classList.add('is-visible');
		}, 60);
	});
});
