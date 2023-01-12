$(document).on("keyup", ".search-box .input-search", function () {
    let inputVal = $(this).val().trim();

    $("#search-list-product li").slice(0).remove();
    $ajax({
        url: "home/search",
        type: "Get",
        contentType: "application/x-ww-form-urlencoded",
        data: {
            search: inputVal
        },
        success: function (res) {
            $("#search-list-product").append(res);
        }
    });
});