
var master = {};

master.FormatControl = function (replaceName,replaceId) {

    $("[name*='" + replaceName + "'][type!='submit'][type!='file']").each(function () {
        var name = $(this).attr("name").replace(replaceName, "");
        $(this).attr("name", name);
    });
    $("[id*='" + replaceId + "'][type!='submit']").each(function () {
        var name = $(this).attr("id").replace(replaceId, "");
        $(this).attr("id", name);
    });

    $(":input[type=submit],:input[type=button]").button(); 
}