function showDialogUnit(eve) {
    $(eve).attr("sign", "clicking");
    $("#dialogunit").find("iframe").attr("src", "../../Shared/SelectProjUnitAndPerson.aspx?ProjGuid=<%=strProjGuid %>")
    $("#dialogunit").css({ top: 30, left: ($(window).width() - $(".dialog").width()) / 2 }).slideDown();
}

function selunitspers(unitobj, perobj) {
    $("#rowstable").find("a[sign='clicking']").parents("#rows").find("td[node='UnitGuid']").find("label").html(unitobj[0].Guid);

    $("#rowstable").find("a[sign='clicking']").parents("#rows").find("td[node='UnitName']").find("label").html(unitobj[0].UnitsName);
    $("#rowstable").find("a[sign='clicking']").parents("#rows").find("td[node='UnitType']").find("label").html(unitobj[0].UnitsTypeName);
    $("#rowstable").find("a[sign='clicking']").parents("#rows").find("td[node='PersonGuid']").find("label").html(perobj[0].Guid);

    $("#rowstable").find("a[sign='clicking']").parents("#rows").find("td[node='PersonName']").find("label").html(perobj[0].Name);
    $("#rowstable").find("a[sign='clicking']").parents("#rows").find("td[node='ProjGuid']").find("label").html(unitobj[0].ProjGuid);

    $("#rowstable").find("a[sign='clicking']").removeAttr("sign");
}