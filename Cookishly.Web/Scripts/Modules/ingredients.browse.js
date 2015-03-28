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