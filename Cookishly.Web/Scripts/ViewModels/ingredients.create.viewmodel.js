function IngredientsCreateViewModel(imageContainer) {
    var self = this;

    self.ingredientName = ko.observable('');
    self.imageOptions = ko.observableArray([]);

    self.getImageOptions = function () {
        if (self.ingredientName().length > 2) {
            //$.ajax({
            //    method: 'get',
            //    url: 'http://pixabay.com/api/',
            //    data: {
            //        username: 'mhornfeck',
            //        key: '6d43f1c27b532322e530',
            //        q: self.ingredientName(),
            //        page: 1,
            //        per_page: 15
            //    },
            //    contentType: "application/json; charset=utf-8"
            //}).done(setImageOptions);

            var username = 'mhornfeck';
            var apikey = '6d43f1c27b532322e530';
            var url = "http://pixabay.com/api/?username="+username+"&key="+apikey+"&q="+encodeURIComponent(self.ingredientName());
            $.getJSON(url, setImageOptions);
        }
    }

    var setImageOptions = function (data) {
        if (data && (parseInt(data.totalHits) > 0)) {
            imageContainer.slick("unslick");

            self.imageOptions.removeAll();
            for (var i = 0; i < data.hits.length; i += 1) {
                self.imageOptions.push(new ImageOption(data.hits[i]));
            }

            
            imageContainer.slick({
                infinite: true,
                slidesToShow: 5,
                slidesToScroll: 5
            });
        }
    }
}

function ImageOption(data) {
    this.id = ko.observable(data.id);
    this.url = ko.observable(data.webformatURL);
}