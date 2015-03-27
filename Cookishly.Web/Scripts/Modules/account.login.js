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