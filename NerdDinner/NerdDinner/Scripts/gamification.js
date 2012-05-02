// incapsulate work with easyXDM
(function ($) {

    $.gamification = new (function () {
        var sendRequest = function (url, options, responseFn, errorFn, method) {
            method = method == null ? "POST" : method;
            var requestParams = {
                url: url,
                type: method,
                dataType: "json",
                contentType: 'text/json',
                crossDomain: true,
                traditional: true,
                data: method == 'GET' ? options : JSON.stringify(options),
                success: function (responseData) {
                    console.log(responseData);
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
            sendRequest('http://dell:999/ActionsService.svc/DoAction', requestParams, responseFn);
        };

        this.test = function (responseFn) {
            sendRequest('http://dell:999/ActionsService.svc/TestPost', null, responseFn);
        };

        this.getGamerData = function (gamerName, gamerKey, responseFn) {

            var transformAchievementsUrls = function (responseData) {
                for (var key in responseData.Achievements) {
                    var imageUrl = responseData.Achievements[key].FileName;
                    imageUrl = 'http://gamify.com/Storage/Achievements/' + imageUrl;
                    responseData.Achievements[key].FileName = imageUrl;
                }

                responseFn(responseData);
            };

            sendRequest('http://dell:999/GamersService.svc/GamerData',
                { "GamerKey": gamerKey, "GamerName": gamerName }, transformAchievementsUrls, null, 'GET');
        };
    });
})(jQuery);