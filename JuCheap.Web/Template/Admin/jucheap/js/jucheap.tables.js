/*
* author : dj.wong at 2016/01/01 13:12:23
*/

jucheap.grid = function (config) {
    this.id = config.id;
    this.url = config.url;
    this.columns = config.columns;
    this.order = config.order;
    this.canFilter = config.canFilter === false ? false : true;
    this.rowClick = config.rowClick;

    if (!this.order) {
        this.order = [[1, "desc"]];
    }
}
//加载datatable
jucheap.grid.prototype.load = function() {
    var tablePrefix = "#table_server_";
    var _this = this;
    var _grid = $("#" + _this.id);

    //设置多选
    this.columns[0].data = function(e) {
        return '<label class="checkbox-custom inline check-success" style="margin:0"><input type="checkbox" value=" " id="checkbox-' + e.Id + '"/><label for="checkbox-' + e.Id + '" style="margin:3px 0 0 0">&nbsp;</label></label>';
    };

    _grid.DataTable({
        serverSide: true, //分页，取数据等等的都放到服务端去
        processing: true, //载入数据的时候是否显示“载入中”
        pageLength: 10, //首次加载的数据条数
        ordering: true, //排序操作在服务端进行，所以可以关了。
        order: _this.order,//默认排序的列和排序的方式
        responsive: true,
        select: false,
        filter: _this.canFilter,
        bDeferRender: false,
        ajax: {
            //类似jquery的ajax参数，基本都可以用。
            type: "GET", //后台指定了方式，默认get，外加datatable默认构造的参数很长，有可能超过get的最大长度。
            url: _this.url,
            //dataSrc: "rows", //默认data，也可以写其他的，格式化table的时候取里面的数据
            data: function(d) { //d 是原始的发送给服务器的数据，默认很长。
                var param = {}; //因为服务端排序，可以新建一个参数对象
                //取得排序的字段
                if (d.columns && d.order && d.order.length > 0) {
                    var orderBy = d.columns[d.order[0].column].name;
                    var orderDir = d.order[0].dir;
                    param.orderBy = orderBy;
                    param.orderDir = orderDir;
                }
                param.start = d.start; //开始的序号
                param.length = d.length; //要取的数据的

                var formData = $("#filter_form").serializeArray(); //把form里面的数据序列化成数组
                formData.forEach(function(e) {
                    param[e.name] = e.value;
                });
                return param; //自定义需要传递的参数。
            }
        },
        columns: _this.columns,
        initComplete: function (setting, json) {
            setting.rowClick = _this.rowClick;
            //console.log("initComplete-setting:" + setting.toString());
            //console.log("initComplete-json:" + json.toString());
            if (_this.canFilter) {
                //初始化完成之后替换原先的搜索框。
                //本来想把form标签放到hidden_filter 里面，因为事件绑定的缘故，还是拿出来。
                $(tablePrefix + "filter").html("<form id='filter_form'>" + $("#hidden_filter").html() + "</form>");
                $(tablePrefix + "filter").find("button[name='go_search']").click(function() {
                    _grid.DataTable().draw();
                });
            }
        },
        language: {
            lengthMenu: ['<select class="form-control m-b-10">' , '<option value="5">5</option>' , '<option value="10">10</option>' , '<option value="20">20</option>' , '<option value="30">30</option>' , '<option value="40">40</option>' , '<option value="50">50</option>' , '</select>&nbsp;&nbsp;条记录'].join(''), //左上角的分页大小显示。
            processing: "数据加载中，请稍后……", //处理页面数据的时候的显示
            paginate: {
                //分页的样式文本内容。
                previous: "上一页",
                next: "下一页",
                first: "首页",
                last: "尾页"
            },
            zeroRecords: "没有内容", //table tbody内容为空时，tbody的内容。
            //下面三者构成了总体的左下角的内容。
            info: "共_PAGES_ 页，显示第_START_ 到第 _END_ ，总共 _TOTAL_ 条 ", //左下角的信息显示，大写的词为关键字。
            infoEmpty: "0条记录", //筛选为空时左下角的显示。
            infoFiltered: "" //筛选之后的左下角筛选提示(另一个是分页信息显示，在上面的info中已经设置，所以可以不显示)，
        },
        drawCallback: function (settings) {
            //console.log(settings.rowClick);
            //单选按钮事件
            $(this).find("tbody tr").each(function () {
                var chb = $(this).find("td:first input[type='checkbox']");
                chb.click(function () {
                    var pp = $(this).parent().parent();
                    //设置选中样式
                    pp.toggleClass("selected");
                    chb.prop("checked", !chb.prop("checked"));
                    if (settings.rowClick && typeof (settings.rowClick) === "function") {
                        settings.rowClick.call(this);
                    }
                    return false;
                });
            });
            //行单击事件
            $(this).find("tbody tr").each(function() {
                $(this).click(function() {
                    //设置选中样式
                    $(this).toggleClass("selected");
                    var chb = $(this).find("td:first input[type='checkbox']");
                    chb.prop("checked", !chb.prop("checked"));
                    if (settings.rowClick && typeof (settings.rowClick) === "function") {
                        settings.rowClick.call(this);
                    }
                });
            });
        }
    });
}

//获取datatable选中行的最后一条数据
jucheap.grid.prototype.getSelectedRow = function () {
    var _this = this;
    var _grid = $("#" + _this.id);
    var selectedRow = null;
    _grid.find("tbody tr.selected:last").each(function () {
        selectedRow = _grid.DataTable().row($(this)).data();
    });
    return selectedRow;
}

//获取datatable所有选中行的数据
jucheap.grid.prototype.getSelectedRows = function () {
    var _this = this;
    var _grid = $("#" + _this.id);
    var selectedRows = [];
    _grid.find("tbody tr.selected").each(function () {
        selectedRows.push(_grid.DataTable().row($(this)).data());
    });
    return selectedRows;
}

//重新加载
jucheap.grid.prototype.reload = function() {
    $("#" + this.id).DataTable().draw();
}