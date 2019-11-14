// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function asyncAjax(options) {
    return new Promise((resolve, reject) => {
        options.success = function (data) {
            resolve(data);
        };

        options.error = function (error) {
            resolve(error);
        };

        $.ajax(options);
    });
}