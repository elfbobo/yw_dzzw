/// <reference path="Config.js" />
//document.write('<script src="../../scripts/Config.js" type="text/javascript"></script>');
function chooseUsers() {
    var sFeature = "dialogWidth:1000px; dialogHeight:500px;center:yes;help:no;resizable:yes;scroll:no;status:no";
    var rnd = Math.round(Math.random() * 10000); //产生随机数，能自动刷新
    var sPath = "ChooseUsers.aspx?tempid=" + rnd + "&Model=" + arguments[0];
    var result = window.showModalDialog(sPath, "", sFeature);
    return result;
};