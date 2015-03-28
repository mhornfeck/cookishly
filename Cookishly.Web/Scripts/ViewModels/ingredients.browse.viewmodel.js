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