var IngredientsCreate = (function () {
    var self = this;

    var $imageOptionsContainer = $('#IngredientImageOptionsContainer');
    var viewModel = new IngredientsCreateViewModel($imageOptionsContainer);

    self.addSlick = function () {
        $imageOptionsContainer.slick();
    }

    self.init = function () {
        ko.applyBindings(viewModel);
        self.addSlick();
    }

    return self;

})();

$(function () {
    IngredientsCreate.init();
});