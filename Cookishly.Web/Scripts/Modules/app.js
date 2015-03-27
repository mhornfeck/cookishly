var App = (function() {
    var self = this;

    self.getTokenUrl = 'http://localhost/Cookishly.Api/token';

    self.login = function(userdata) {
        var data = userdata + '&grant_type=password';
        $.post(self.getTokenUrl, data)
            .done(handleLoginResponse)
            .fail(handleError);
    }

    self.getAccessToken = function () {
        return sessionStorage.getItem("accessToken");
    };

    var handleLoginResponse = function(response) {
        var accessToken = response.access_token;
        setAccessToken(accessToken);

        return true;
    }

    var handleError = function(response) {
        alert(JSON.stringify(response, null, 4));
        return false;
    }

    var setAccessToken = function (accessToken) {
        sessionStorage.setItem("accessToken", accessToken);
    };

    return self;

})();