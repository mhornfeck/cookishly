var App = (function() {
    var self = this;

    var token;

    self.getTokenUrl = 'http://localhost/Cookishly.Api/token';

    self.login = function(userdata) {
        var data = userdata + '&grant_type=password';
        $.post(self.getTokenUrl, data)
            .done(handleLoginResponse)
            .fail(handleError);
    }

    self.getAccessToken = function () {
        //return sessionStorage.getItem("accessToken");
        alert("get " + token);
        return token;
    };

    self.setAccessToken = function (accessToken) {
        sessionStorage.setItem("accessToken", accessToken);
        token = accessToken;
        alert("set " + token);
    };

    self.returnNumber = function(num) {
        return num;
    }

    var handleLoginResponse = function (response) {
        alert(response.access_token);
        var accessToken = response.access_token;
        setAccessToken(accessToken);

        return true;
    }

    var handleError = function(response) {
        alert(JSON.stringify(response, null, 4));
        return false;
    }

    return self;

})();