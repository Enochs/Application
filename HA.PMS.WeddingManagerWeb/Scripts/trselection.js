$(document).ready(function () {
    $(".table-select tbody tr[skey=" + window.parent.document.all.skey + "]").attr("style", "background-color: rgb(166, 178, 195);color: white");
    $(".table-select").removeClass("table-bordered").removeClass("table-striped");
    $(".table-select tbody tr").click(function () {
        $(this).addClass("trselected").siblings().removeClass("trselected");
        $(this).attr("style", "background-color: rgb(166, 178, 195);color: white").siblings().removeAttr("style");
        window.parent.document.all.skey = $(this).attr("skey");
    });
});