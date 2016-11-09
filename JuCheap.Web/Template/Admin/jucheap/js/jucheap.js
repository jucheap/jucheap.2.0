(function () {
    //基础扩展Start
    //日期扩展
    Date.prototype.format = function (fmt) {
        var o = {
            "M+": this.getMonth() + 1, //月份   
            "d+": this.getDate(), //日   
            "H+": this.getHours(), //小时   
            "m+": this.getMinutes(), //分   
            "s+": this.getSeconds(), //秒   
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
            "S": this.getMilliseconds() //毫秒   
        };
        if (/(y+)/.test(fmt))
            fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o) {
            if (o.hasOwnProperty(k)) {
                if (new RegExp("(" + k + ")").test(fmt))
                    fmt = fmt.replace(RegExp.$1, (RegExp.$1.length === 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            }
        }
        return fmt;
    };
    //字符串扩展
    String.prototype.getDate = function () {
        var res = /\d{13}/.exec(this);
        var date = new Date(parseInt(res));
        return date.format("yyyy-MM-dd HH:mm:ss");
    }
    //基础扩展End
    
    //iframe里面实现自定义滚动条
    //$("html").niceScroll({
    //    styler: "fb",
    //    cursorcolor: "#a979d1",
    //    cursorwidth: '5',
    //    cursorborderradius: '15px',
    //    background: '#404040',
    //    cursorborder: '',
    //    zindex: '12000'
    //});
})();

//辅助类
jucheap = {};
//返回上一页
jucheap.goback = function(e) {
    $(e).button("loading").delay(1000).queue(function() {
        history.go(-1);
    });
}
//跳转到指定的页面
jucheap.goAction = function (e, moudleId, menuId, btnId, url, grid) {
    var addReg = /add/gi;
    var editReg = /edit/gi;
    var deleteReg = /delete/gi;
    var authen = /authen/gi;
    url = url.toLowerCase();
    if (addReg.test(url)) {
        jucheap.addModel(e, moudleId, menuId, btnId, url, grid);
    } else if (editReg.test(url)) {
        jucheap.editModel(e, moudleId, menuId, btnId, url, grid);
    } else if (deleteReg.test(url)) {
        jucheap.delModel(e, moudleId, menuId, btnId, url, grid);
    } else if (authen.test(url)) {
        jucheap.authen(e, moudleId, menuId, btnId, url, grid);
    }
}
//addModel
jucheap.addModel = function (e, moudleId, menuId, btnId, url, grid) {
    $(e).button("loading");
    window.location.href = [url, moudleId, menuId, btnId].join("/");
}

//editModel
jucheap.editModel = function (e, moudleId, menuId, btnId, url, grid) {
    var editRow = grid.getSelectedRow();
    if (editRow && editRow.Id) {
        $(e).button("loading");
        window.location.href = [url, moudleId, menuId, btnId, editRow.Id].join("/");
    } else {
        parent.layer.alert("请选择需要编辑的数据");
    }
}
//delete
jucheap.delModel = function (e, moudleId, menuId, btnId, url, grid) {
    var delRows = grid.getSelectedRows();
    if (delRows != null && delRows.length > 0) {
        parent.layer.confirm("确认要删除这" + delRows.length + "条数据？", {
            btn: ['确定', '取消'] //按钮
        }, function () {
            var ids = [];
            $(delRows).each(function(i, data) {
                ids.push(data.Id);
            });
            $(e).button("loading");
            $.ajax({
                url: [url, moudleId, menuId, btnId].join("/"),
                type: "POST",
                dataType: "JSON",
                data: JSON.stringify(ids),
                contentType: "application/json, charset=utf-8",
                success: function (res) {
                    $(e).button("reset");
                    if (res.flag) {
                        parent.layer.alert("删除成功");
                        grid.reload();
                    } else {
                        parent.layer.alert("删除失败：" + res.msg);
                    }
                }
            });
        }, function () {
            
        });
    } else {
        parent.layer.alert("请选择要删除的数据！");
    }
}
//authen
jucheap.authen = function (e, moudleId, menuId, btnId, url, grid) {
    var editRow = grid.getSelectedRow();
    if (editRow && editRow.Id) {
        $(e).button("loading");
        window.location.href = [url, moudleId, menuId, btnId, editRow.Id].join("/");
    } else {
        parent.layer.alert("请选择需要授权的数据");
    }
}

jucheap.buttonLoading = function(form) {
    $(form).find("button[type='submit']:first").button("loading").queue(function () {
        form.submit();
    });
}