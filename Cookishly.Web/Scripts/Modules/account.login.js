var AccountLogin = (function () {
    var self = this;

    var $submitButton;
    var $loginForm;

    self.init = function () {
        $submitButton = $('#SubmitLoginButton');
        $loginForm = $('#LoginForm');

        $submitButton.click(function (e) {
            e.preventDefault();
            var formData = $loginForm.serialize();
            App.login(formData);
            $loginForm.submit();
        });
    }

    return self;

})();

$(function () {
    AccountLogin.init();
});
