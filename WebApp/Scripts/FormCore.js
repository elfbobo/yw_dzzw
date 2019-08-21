//2013-10-25  田飞飞
//int='true' 整数验证
//double="true" 小数验证
//email='true' email验证
//card='true' 身份证验证
//tell='true' 电话验证
//empty='true'
//region='true'  minfor='id'

function FormCore() {


    function IntOnly(oInput) {
        if ('' != oInput.value.replace(/\d/g, '')) {
            oInput.value = oInput.value.replace(/\D/g, '');
        }
    }

    function FloatOnly(oInput) {
        if ('' != oInput.value.replace(/\d{1,}\.{0,1}\d{0,}/, '')) {

            oInput.value = oInput.value.match(/\d{1,}\.{0,1}\d{0,}/) == null ? '' : oInput.value.match(/\d{1,}\.{0,1}\d{0,}/);

        }

        var strValue = oInput.value;

        if (strValue.toString().indexOf('.') != -1) {

            var index = strValue.toString().indexOf('.');

            oInput.value = strValue.toString().substring(0, index + 5);

        }
        else {

            oInput.value = strValue.toString();

        }
    }

    function EmailValidate(email) {
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (filter.test(email))
            return true;
        else
            return false;
    }

    function CardValidate(card) {
        var filter = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/;
        if (filter.test(card))
            return true;
        else
            return false;
    }


    function TellValidate(tell) {
        var filter = /((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)/;
        if (filter.test(tell))
            return true;
        else
            return false;
    }

    function RegionValidate(min, max) {
        if (!isNaN(min.val())) {
            if (parseFloat(max.val()) < parseFloat(min.val()))
                return false;
            return true;
        }
        else {
            try {
                var datemin = Date.parse(min.val());
                var datemax = Date.parse(max.val());
                if (datemax < datemin)
                    return false;
                return true;
            }
            catch (e) {
                return false;
            }
        }
    }


    this.FormVildateLoad = function () {

        $('input[int="true"]').keyup(function () {
            IntOnly($(this).get(0));
        });

        $('input[double="true"]').keyup(function () {
            FloatOnly($(this).get(0));
        });

        $('[empty="true"]').each(function (i) {
            $(this).parent().prev().html("<font color='red'>*</font>" + $(this).parent().prev().html());
            $(this).bind("focusout", function () {
                var oldgb = $(this).css("border-color");
                if ($(this).attr("U")) {
                    if ($(this).val() == " " + $(this).attr("U")) {
                        $(this).css({ "border-color": "red" });
                    }
                    else
                        $(this).css({ "border-color": oldgb });
                }
                else {
                    if ($(this).val() == "") {
                        $(this).css({ "border-color": "red" });
                    }
                    else
                        $(this).css({ "border-color": oldgb });
                }
            });
        });

        $('[email="true"]').each(function (i) {

            $(this).bind("focusout", function () {
                var oldgb = $(this).css("border-color");
                if ($(this).val() != "") {
                    if (EmailValidate($(this).val())) {
                        $(this).css({ "border-color": oldgb });
                    }
                    else
                        $(this).css({ "border-color": "red" });
                }
            });

        });

        $('[card="true"]').each(function (i) {

            $(this).bind("focusout", function () {
                var oldgb = $(this).css("border-color");
                if ($(this).val() != "") {
                    if (CardValidate($(this).val())) {
                        $(this).css({ "border-color": oldgb });
                    }
                    else
                        $(this).css({ "border-color": "red" });
                }
            });

        });

        $('[tell="true"]').each(function (i) {

            $(this).bind("focusout", function () {
                var oldgb = $(this).css("border-color");
                if ($(this).val() != "") {
                    if (TellValidate($(this).val())) {
                        $(this).css({ "border-color": oldgb });
                    }
                    else
                        $(this).css({ "border-color": "red" });
                }
            });

        });


        $('[region="true"]').each(function (i) {

            $(this).bind("focusout", function () {
                var oldgb = $(this).css("border-color");
                if ($(this).val() != "" && $('#' + $(this).attr("minfor")).val() != "") {
                    if (RegionValidate($('#' + $(this).attr("minfor")), $(this))) {
                        $(this).css({ "border-color": oldgb });
                        $('#' + $(this).attr("minfor")).css({ "border-color": oldgb });
                    }
                    else {
                        $(this).css({ "border-color": "yellow" });
                        $('#' + $(this).attr("minfor")).css({ "border-color": "#f6f803" });
                    }
                }
            });

            var $this = $(this);
            $("#" + $this.attr("minfor")).bind("focusout", function () {
                var oldgb = $(this).css("border-color");
                if ($(this).val() != "" && $this.val() != "") {
                    if (RegionValidate($(this), $this)) {
                        $this.css({ "border-color": oldgb });
                        $(this).css({ "border-color": oldgb });
                    }
                    else {
                        $this.css({ "border-color": "#f6f803" });
                        $(this).css({ "border-color": "#f6f803" });
                    }
                }
            });



        });



        $('input[U]').each(function (i) {
            var $this = $(this);
            $this.val($this.val() + ' ' + $this.attr("U"));
          
            $this.keyup(function () {
                $this.val($this.val() + ' ' + $this.attr("U"));
                position( $this);
                
            }).focus(function () {
                position( $this);
            }).mouseup(function () {
                position($this);
            });
        });
    }

    function position(boj) {
       
        var elem = boj.get(0);
        var value = boj.val().indexOf(' ' + boj.attr("U"));
        if (elem && (elem.tagName == "TEXTAREA" || elem.type.toLowerCase() == "text")) {
            if ($.browser.msie) {
                var rng = elem.createTextRange();
                rng.move("character", boj.val().indexOf(' '+boj.attr("U")));
                rng.select();
            } else {
                if (value === undefined) {
                    return elem.selectionStart;
                } else if (typeof value === "number") {
                    elem.selectionEnd = value;
                    elem.selectionStart = value;
                }
            }
        } else {
            if (value === undefined)
                return undefined;
        }
    }

    this.TextFocusPostion = function (obj,value)
    {
        position(value, obj);
    }

    this.FormVildateSubmit = function () {
        var count = 0;
        $('[empty="true"]').each(function (i) {
            var oldgb = $(this).css("border-color");
            if ($(this).attr("U"))
            {
                if ($(this).val() == ' ' + $(this).attr("U")) {

                    $(this).css({ "border-color": "red" });
                    $(this).bind("focusout", function () {
                        if ($(this).val() != ' ' + $(this).attr("U")) {
                            $(this).css({ "border-color": oldgb });
                        }
                    });
                    count++;
                }
            }
            else{
                if ($(this).val() == "") {

                    $(this).css({ "border-color": "red" });
                    $(this).bind("focusout", function () {
                        if ($(this).val() != "") {
                            $(this).css({ "border-color": oldgb });
                        }
                    });
                    count++;
                }
            }
        });
        var mailCount = 0;
        $('[email="true"]').each(function (i) {
            var oldgb = $(this).css("border-color");
            if ($(this).val() != "") {
                if (!EmailValidate($(this).val())) {
                    $(this).css({ "border-color": "red" });
                    $(this).bind("focusout", function () {
                        if ($(this).val() != "") {
                            if (EmailValidate($(this).val())) {
                                $(this).css({ "border-color": oldgb });
                            }
                        }
                    });

                    mailCount++;

                }
            }
        });

        var cardCount = 0;

        $('[card="true"]').each(function (i) {
            var oldgb = $(this).css("border-color");
            if ($(this).val() != "") {
                if (!CardValidate($(this).val())) {
                    $(this).css({ "border-color": "red" });
                    $(this).bind("focusout", function () {
                        if ($(this).val() != "") {
                            if (CardValidate($(this).val())) {
                                $(this).css({ "border-color": oldgb });
                            }
                        }
                    });

                    cardCount++;

                }
            }
        });

        var tellCount = 0;

        $('[tell="true"]').each(function (i) {
            var oldgb = $(this).css("border-color");
            if ($(this).val() != "") {
                if (!TellValidate($(this).val())) {
                    $(this).css({ "border-color": "red" });
                    $(this).bind("focusout", function () {
                        if ($(this).val() != "") {
                            if (TellValidate($(this).val())) {
                                $(this).css({ "border-color": oldgb });
                            }
                        }
                    });

                    tellCount++;

                }
            }
        });

        var msg = "";
        if (count > 0)
            msg += "带 * 的为必填项";
        if (mailCount > 0) {
            if (msg != "")
                msg += "\n邮箱格式不正确";
            else
                msg += "邮箱格式不正确";

        }
        if (cardCount > 0) {
            if (msg != "")
                msg += "\n身份证格式不正确";
            else
                msg += "身份证格式不正确";
        }
        if (tellCount > 0) {
            if (msg != "")
                msg += "\n电话号码格式不正确";
            else
                msg += "电话号码格式不正确";
        }
        if (count + mailCount + cardCount + tellCount > 0) {
            alert(msg);
            return false;
        }
        $('[U]').each(function (i) {
            var $this = $(this);
            $this.val($this.val().replace(" "+$this.attr("U"),""));
        });
        return true;
    }


}



