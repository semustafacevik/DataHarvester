//Search Query Function
$(function () {
    $(".inner-form").on("click", ".btn-search", function () {

        var query = $("#search").val()

        if (query == "") {
            var searching = "<br/><br/><h5 class=\"search-error\">Lütfen aramak istediğiniz domain formatını giriniz.</h5>"
            $("#ShowResult").html(searching)
        }

        else if (query.startsWith("www") || query.startsWith("http") || query.includes(" ")) {
            var searching = "<br/><br/><h5 class=\"search-error\">Girmiş olduğunuz domain formatını kontrol ediniz. \
                         <br/><br/>Yanlış formatlar: www.domain.com - http://domain.com - https://domain.com - doma in.com \
                         <br/><br/>Doğru format: domain.com</h5>"

            $("#ShowResult").html(searching)
        }

        else {
            var searching = "<br/><br/><h5 class=\"search-success\">\"" + query + "\" için arama yapılıyor...</h5>"
            $("#ShowResult").html(searching)

            $.ajax({
                type: "GET",
                url: "/User/IsAuthenticated",
                success: function (auth) {
                    if (auth == "True")
                    {
                        $.ajax({
                            type: "GET",
                            url: "/Member/Search?query=" + query,
                            success: function (data) {
                                if (data.includes("class=\"search-error\">")) {
                                    $("#ShowResult").html(data)
                                }
                                else {
                                    $("#ShowResults").html(data)
                                    $("#ShowResult").html("")
                                }
                            }
                        })
                    }
                    else {
                        $.ajax({
                            type: "GET",
                            url: "/Home/Search?query=" + query,
                            success: function (data) {
                                $("#ShowResult").html(data)
                            }
                        })
                    }
                }
            })
        }
    })
})


// Delete Account 
$(function () {
    $(".user-form").on("click", "#delUser", function () {
        bootbox.confirm("Hesabınızı silmek istediğinize emin misiniz?", function (result) {
            if (result) {
                $.ajax({
                    type: "GET",
                    url: "/User/Delete",
                    success: function () {
                        bootbox.alert("Hesabınız başarıyla silindi.", function () {
                            window.location.href = "/Login"
                        })
                    }
                })
            }
        })
    })
})


$(function () {
    $(".profile-for-email").on("click", ".profile-username", function () {

        var selectedProfile = $(this).text()
        var query = $("#search").val()

        $.ajax({
            type: "GET",
            url: "/Member/GetMailsForSelectedProfile?name=" + selectedProfile + "&query=" + query,
            success: function (data) {
                $("#email-tips").html(data)
            }
        })
    })
})
