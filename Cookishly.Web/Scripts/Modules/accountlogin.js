///#source 1 1 /Scripts/Modules/account.login.js
var AccountLogin = (function () {
    var self = this;

    var $submitButton = $('#SubmitLoginButton');
    var $loginForm = $('#LoginForm');

    self.init = function() {
        $('#SubmitLoginButton').click(function (e) {
            e.preventDefault();
            var formData = $('#LoginForm').serialize();
            App.login(formData);
            $('#LoginForm').submit();
        });
    }

    return self;

})();

AccountLogin.init();
///#source 1 1 /Scripts/Modules/app.js
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
