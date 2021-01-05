// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
	// initialize start up page here
	$('[data-toggle="popover"]').popover();

	getReasonsCount();

	// use ajax to fetch the reason data
	function getReasonsCount() {
		$.ajax({
			url: "/Home/GetReasonCount",
			type: "get",
			contentType: "application/json",
			success: function (result, status, xhr) {
				console.log("success");
				console.log(result);

				// set the count to the view
				$("#number-of-reasons").html("Found <strong>" + result + "</strong> Reasons!")
			},
			error: function (xhr, status, error) {
				console.log("error");
				console.log(xhr);
			}
		});
	}
});
