$(document).ready(function () {
    setTimeout(function () {

        autoSizeFileDiv();
    }, 800)
});

function autoSizeFileDiv() {
        var fileobjArr = $(".swfupload");
        
        for (var i = 0; i < fileobjArr.length; i++) {
            var parentdiv = fileobjArr[i].parentElement;
            $(parentdiv.children[0]).attr("width", $(parentdiv).width() + 4);
            $(parentdiv.children[0]).css({
                position: "absolute", 'top': $(parentdiv).offset().top,
                'left': $(parentdiv).offset().left - 2, 'z-index': 2
            });


        }
        //for (var j = 0; j < $("[guid]").length; j++) {
        //    $("[guid]")[j].click(function () {
        //        //alert("in");
        //    });
        //}
}

window.onresize = function () { autoSizeFileDiv(); }, document.body.onresize = function () { autoSizeFileDiv(); }