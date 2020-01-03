$(function () {

    $(".inner-form").on("click", ".btn-search", function () {
        var query = $("#search").val()

        if (query == "") {
            var searching = "<br/><br/><h4 class=\"search-error\">Lütfen aramak istediğiniz domain formatını giriniz.</h4>"
            $("#ShowResults").html(searching)
        }

        else if (query.startsWith("www") || query.startsWith("http") || query.includes(" ")) {
            var searching = "<br/><br/><h4 class=\"search-error\">Girmiş olduğunuz domain formatını kontrol ediniz. \
                         <br/><br/>Yanlış formatlar: www.domain.com - http://domain.com - https://domain.com - doma in.com \
                         <br/><br/>Doğru format: domain.com "

            $("#ShowResults").html(searching)
        }

        else {
            var searching = "<br/><br/><h4 class=\"search-success\">\"" + query + "\" adına arama yapılıyor...</h4>"
            $("#ShowResults").html(searching)

            $.ajax({
                type: "GET",
                url: "Home/Search?word=" + query,
                success: function (data) {
                    $("#ShowResults").html(data)
                }
            })
        }
    })

    //$(".inner-form").on("click", ".btn-search", function () {
    //    var query = $("#search").val()
    //    $.ajax({
    //        type: "GET",
    //        url: "Home/Search?word=" + query,
    //        success: function (data) {
    //            $("#ShowResults").html(data)
    //        }
    //    })
    //})
})