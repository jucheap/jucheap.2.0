(function () {
    var frm = $("#txtContentBody");
    var menu = $("#jucheapMenu");
    menu.find("a").each(function() {
        $(this).click(function() {
            var url = $(this).attr("data-url");
            var text = $(this).text();
            if (!/#/gi.test(url)) {
                //显示loading提示
                var loading = layer.load(2);
                frm.load(function () {
                    //iframe加载完成后隐藏loading提示
                    layer.close(loading);
                });
                frm.attr("src", url);
                //set current menu style
                var p = $(this).parent();
                var ppp = p.parent();
                $(this).find(".nav-active:first").removeClass("nav-active");
                ppp.addClass("nav-active");
                menu.find(".active:first").removeClass("active");
                p.addClass("active");
                $("#txtMenuNameTip").text(text);
            }
            return true;
        });
    });
})();