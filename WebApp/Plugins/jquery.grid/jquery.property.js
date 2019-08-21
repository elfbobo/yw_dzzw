(function ($) {
	$.fn.extend({
		Property: function (options) {
			var defaults = {
				columns: [],
				width: "100%",
				height: 0,
				read: false,
				EditBefor: function () { },
				EditComplete: function () { }
			}
			var options = $.extend(defaults, options);
			return this.each(function () {
				var propertyContent = $(this);
				var opt = options;
				propertyContent.css({ "height": opt.height, "width": opt.width }).addClass("property");

				SetContent(opt.columns);

				//设置内容
				function SetContent(json) {

					propertyContent.children().remove();
					$.each(json, function (i, dat) {
						var div = $("<div />", { "class": "propertytitle" });
						var span = $("<span />").css({ "margin-left": "3px", "margin-right": "3px", "cursor": "pointer" });
						span.addClass("collapse");
						span.html("&nbsp;&nbsp;&nbsp;&nbsp;");
						span.click(function () {
							$(this).parent().next().toggle();
							if ($(this).attr("class") == "collapse") {
								$(this).addClass("expand").removeClass("collapse");
							} else {
								$(this).addClass("collapse").removeClass("expand");
							}
						});
						div.append(span);

						div.append("<span>" + dat.group + "</span>");
						propertyContent.append(div);
						var table = $("<table />", { "class": "propertytable" });
						table.attr({ "cellspacing": 0, "cellpadding": "0" }).width("100%");
						$.each(dat.child, function (j, data) {
							var tr = $("<tr />");
							trHover(tr);
							if (!data.readonly)
								tr.dblclick(function () { trDbClick($(this)); });//
							var td = $("<td />", { "class": "propertytd" });
							td.text(data.name);
							td.width("40%");
							tr.append(td);
							var td2 = $("<td />", { "class": "propertytd" });
							td2.text(data.value);
							if (data.select)
								td2.data("s", data.select);
							td2.data("filed", data.filed);
							tr.append(td2);
							table.append(tr);
						});

						propertyContent.append(table);
					});


				}

				function trDbClick(tr) {
					propertyContent.find(".tabletr").removeClass("tabletr");
					tr.addClass("tabletr");
					var td = tr.find("td:last");
					if (td.find(":input").length <= 0) {
						var text = td.text();
						if (td.data("s")) {
							var sle = $("<select />").css("width", "99%");
							sle.append("<option></option>");
							$.each(td.data("s"), function (i, dat) {
								sle.append("<option value='" + dat.value + "'>" + dat.name + "</option>");
							});
							sle.find("option:contains('" + text + "')").attr("selected", "selected");
							var filed = td.data("filed");
							sle.blur(function () {
								$(this).parent().text($(this).find("option:selected").text());

								opt.EditComplete($(this).val(), filed, $(this));
							});
							td.html(sle);
							sle.focus();
							opt.EditBefor(text, filed, sle);
						}
						else {
							var input = $("<input />").css("width", "99%");
							input.val(text);
							var filed = td.data("filed");
							input.blur(function () {

								$(this).parent().text($(this).val());
								opt.EditComplete($(this).val(), filed, $(this));

							});

							td.html(input);
							input.focus();
							opt.EditBefor(text, filed, input);
						}
					}
				}

				function trHover(tr) {
					tr.hover(function () { $(this).addClass("tablehover"); }, function () {
						if ($(this).find(":checkbox").attr("checked") != "checked")
							$(this).removeClass("tablehover");
					});
				}

			});
		}

	});
})(jQuery);