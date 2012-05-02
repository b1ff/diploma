/// <reference path="easyXDM.debug.js" />


// incapsulate work with easyXDM
(function ($) {

    $.gamification = new (function () {
        var sendPostRequest = function (url, options, responseFn, errorFn) {
            var requestParams = {
                url: url,
                type: "POST",
                dataType: "json",
                contentType: 'text/json',
                crossDomain: true,
                traditional: true,
                data: JSON.stringify(options),
                success: function (responseData) {
                    if (responseFn != null)
                        responseFn(responseData);
                },
                error: function (errorData) {
                    if (errorFn != null)
                        errorFn(errorData);
                }
            };

            $.ajax(requestParams);
        };

        this.doAction = function (data, responseFn) {
            var requestParams = {
                "GamerKey": '',
                "GamerName": '',
                "ActionId": ''
            };
            $.extend(requestParams, data);
            sendPostRequest('http://dell:999/ActionsService.svc/DoAction', requestParams, responseFn);
        };

        this.test = function (responseFn) {
            sendPostRequest('http://dell:999/ActionsService.svc/TestPost', null, responseFn);
        };
    });
})(jQuery);