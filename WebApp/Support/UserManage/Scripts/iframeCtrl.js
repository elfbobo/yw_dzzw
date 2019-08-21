$(document).ready(function () {
    $(window.parent.document).find("iframe").height(window.document.body.scrollHeight + 20);
});

function SetWinHeight(obj) {
    var win = obj;
    if (document.getElementById) {
        if (win && !window.opera) {
            if (win.contentDocument && win.contentDocument.body.offsetHeight)
                win.height = win.contentDocument.body.offsetHeight + 10;
            else if (win.Document && win.Document.body.scrollHeight)
                win.height = win.Document.body.scrollHeight + 10;
        }
    }
}