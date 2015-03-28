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

    self.getApiUrl = function(url) {
        return 'http://localhost/Cookishly.Api/api' + url;
    };

    self.getAccessToken = function () {
        return sessionStorage.getItem("accessToken");
    };

    self.setAccessToken = function (accessToken) {
        sessionStorage.setItem("accessToken", accessToken);
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
///#source 1 1 /Scripts/ViewModels/ingredients.browse.viewmodel.js
function IngredientBrowseViewModel() {
    var self = this;

    self.pageNumber = 0;
    self.pageSize = 12;

    self.ingredients = ko.observableArray([]);

    self.getIngredients = function () {

        $.ajax({
            method: 'get',
            url: App.getApiUrl('/ingredients'),
            data: {
                offset: self.pageNumber,
                limit: self.pageSize
            },
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + App.getAccessToken()
            }
        }).done(setIngredients);
    }

    var setIngredients = function (data) {
        if (data) {
            self.ingredients.removeAll();
            for (var i = 0; i < data.Items.length; i += 1) {
                self.ingredients.push(new Ingredient(data.Items[i]));
            }
        }
    }
}

function Ingredient(data) {
    this.id = ko.observable(data.Id);
    this.name= ko.observable(data.Name);
    this.imageUrl = ko.observable(data.ImageUrl);
}
///#source 1 1 /Scripts/Modules/ingredients.browse.js
var IngredientsBrowse = (function () {
    var self = this;

    var viewModel = new IngredientBrowseViewModel();

    self.init = function () {
        viewModel.getIngredients();
        ko.applyBindings(viewModel);
    }

    return self;

})();

$(function () {
    IngredientsBrowse.init();
});
