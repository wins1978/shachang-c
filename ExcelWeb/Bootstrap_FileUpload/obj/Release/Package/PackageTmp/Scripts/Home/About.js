﻿//(1)初始化
$(function () {
    var oTable = new TableInit();
    oTable.Init();
});
var TableInit = function () {
    var oTableInit = new Object();
    oTableInit.Init = function () {
        $('#tb_departments').bootstrapTable({
            url: '/Home/GetDepartment', //请求后台的URL（*）
            method: 'get', //请求方式（*）
            toolbar: '#toolbar', //工具按钮用哪个容器
            striped: true, //是否显示行间隔色
            cache: false, //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true, //是否显示分页（*）
            sortable: false, //是否启用排序
            sortOrder: "asc", //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server", //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1, //初始化加载第一页，默认第一页
            pageSize: 10, //每页的记录行数（*）
            pageList: [10, 25, 50, 100], //可供选择的每页的行数（*）
            search: false, //是否显示表格搜索，此搜索是客户端搜索,也可以是服务端检索
            strictSearch: true,
            showColumns: true, //是否显示所有的列
            showRefresh: true, //是否显示刷新按钮
            minimumCountColumns: 2, //最少允许的列数
            clickToSelect: true, //是否启用点击选中行
            height: 500, //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "id", //每一行的唯一标识，一般为主键列
            showToggle: true, //是否显示详细视图和列表视图的切换按钮
            cardView: false, //是否显示详细视图
            detailView: false, //是否显示父子表
            columns: [{
                checkbox: true
            }, {
                field: 'one',
                title: '列1',
                editable: {
                    type: 'text',
                    title: '用户名',
                    validate: function (v) {
                        if (!v) return '用户名不能为空';
                    }
                }
                //验证数字
                //editable: {
                //    type: 'text',
                //    title: '年龄',
                //    validate: function (v) {
                //        if (isNaN(v)) return '年龄必须是数字';
                //        var age = parseInt(v);
                //        if (age <= 0) return '年龄必须是正整数';
                //    }
                //}
                //时间框
                //editable: {
                //    type: 'datetime',
                //    title: '时间'
                //}
                //选择框
                //editable: {
                //    type: 'select',
                //    title: '部门',
                //    source: [{ value: "1", text: "研发部" }, { value: "2", text: "销售部" }, { value: "3", text: "行政部" }]
                //}
                //复选框
                //editable: {
                //type: "checklist",
                //separator:",",
                //source: [{ value: 'bsb', text: '篮球' },
                //     { value: 'ftb', text: '足球' },
                //     { value: 'wsm', text: '游泳' }],
                //}
                //select2
                //暂未使用到

                //取后台数据
                //editable: {
                //    type: 'select',
                //    title: '部门',
                //    source: function () {
                //        var result = [];
                //        $.ajax({
                //            url: '/Editable/GetDepartments',
                //            async: false,
                //            type: "get",
                //            data: {},
                //            success: function (data, status) {
                //                $.each(data, function (key, value) {
                //                    result.push({ value: value.ID, text: value.Name });
                //                });
                //            }
                //        });
                //        return result;
                //    }
                //}


            }, {
                field: 'two',
                title: '列2',
            }],
            //保存的使用
            onEditableSave: function (field, row, oldValue, $el) {
                //可进行异步操作

                $.ajax({
                    type: "post",
                    url: "/Home/Edit",
                    data: row,
                    dataType: 'JSON',
                    success: function (data, status) {
                        if (status == "success") {
                            alert('提交数据成功');
                        }
                    },
                    error: function () {
                        alert('编辑失败');
                    },
                    complete: function () {

                    }

                });
            }

        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit, //页面大小
            offset: params.offset, //页码
        };
        return temp;
    };
    return oTableInit;
};

//(2)关键字检索
$("#btn_query").click(function () {
    //点击查询是 使用刷新 处理刷新参数
    var opt = {
        url: "/index/GetDepartment",
        silent: true,
        query: {
            text1: $("#txt_search_departmentname").val(), //条件1
            text2: $("#txt_search_statu").val()           //条件2 ....
        }
    };
    $('#tb_departments').bootstrapTable('refresh', opt);

});

//(3)修改一、获取编号进入下一页
$("#btn_edit").click(function () {
    var i = 0;
    var id;
    $("input[name='btSelectItem']:checked").each(function () {
        i++;
        id = $(this).parents("tr").attr("data-uniqueid");
    })
    if (i > 1) {
        alert("编辑只支持单一操作")
    } else {
        if (i != 0) {
            alert("获取选中的id为" + id);
            window.location.href = "/index/index";
        } else {
            alert("请选中要编辑的数据");
        }

    }

})


//(4)删除及批量删除

$("#btn_delete").click(function () {
    if (confirm("确认要删除吗？")) {
        var idlist = "";
        $("input[name='btSelectItem']:checked").each(function () {
            idlist += $(this).parents("tr").attr("data-uniqueid") + ",";
        })
        alert("删除的列表为" + idlist);

    }
});
