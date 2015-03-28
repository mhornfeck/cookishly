///<reference path="../../../Cookishly.Web/Scripts/Modules/app.js"/>
///<reference path="../Lib/jquery-1.10.2.js" />
///<reference path="../Lib/jquery.mockjax.js" />
///<reference path="../Lib/jasmine-2.0.2.js" />

describe("App module tests", function () {
    var requestUrl = 'http://localhost/Cookishly.Api/token';

    beforeEach(function () {
        
    });

    afterEach(function () {

    });

    it("should set the access token on login", function () {
        var accesstoken = 'F5BD66E6-F72E-4A6D-9429-56CEB79D3AC5';

        $.mockjax({
            url: requestUrl,
            status: 200,
            responseText: {
                access_token: accesstoken
            },
            onAfterComplete: function () {
                var result = App.getAccessToken();
                expect(result).toBe(accesstoken);
            }
        });

        App.login({ username: 'testuser', password: 'password' });        
    });

});