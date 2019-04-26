(function ($) {
    ko.bindingViewModel = function (data, bindElement) {

        var self = this;

        this.queryCondition = ko.mapping.fromJS(data.queryCondition);
        this.defaultQueryParams = {
            queryParams: function (param) {
                var params = self.queryCondition;
                params.limit = param.limit;
                params.offset = param.offset;
                return params;
            }
        };

        var tableParams = $.extend({}, this.defaultQueryParams, data.tableParams || {});
        this.bootstrapTable = new ko.bootstrapTableViewModel(tableParams);

        //清空事件
        this.clearClick = function () {
            $.each(self.queryCondition, function (key, value) {
                //只有监控属性才清空
                if (typeof (value) == "function") {
                    this(''); //value('');
                }
            });
            self.bootstrapTable.refresh();
        };

        //查询事件
        this.queryClick = function () {
            self.bootstrapTable.refresh();
        };

        //新增事件
        this.addClick = function () {
            var dialog = $('<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"></div>');
            dialog.load(data.urls.edit, null, function () { });

            $("body").append(dialog);
            dialog.modal().on('hidden.bs.modal', function () {
                //关闭弹出框的时候清除绑定(这个清空包括清空绑定和清空注册事件)
                ko.cleanNode(document.getElementById("formEdit"));
                dialog.remove();
                self.bootstrapTable.refresh();
            });
        };

        //编辑事件
        this.editClick = function () {
            var arrselectedData = self.bootstrapTable.getSelections();
            if (arrselectedData.length <= 0 || arrselectedData.length > 1) {
                alert("每次只能编辑一行");
                return;
            }
            var dialog = $('<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"></div>');
            dialog.load(data.urls.edit, arrselectedData[0], function () { });

            $("body").append(dialog);
            dialog.modal().on('hidden.bs.modal', function () {
                //关闭弹出框的时候清除绑定(这个清空包括清空绑定和清空注册事件)
                ko.cleanNode(document.getElementById("formEdit"));
                dialog.remove();
                self.bootstrapTable.refresh();
            });
        };

        //删除事件
        this.deleteClick = function () {
            var arrselectedData = self.bootstrapTable.getSelections();
            if (!arrselectedData || arrselectedData.length <= 0) {
                alert("请至少选择一行");
                return;
            }
            $.ajax({
                url: data.urls.delete,
                type: "post",
                contentType: 'application/json',
                data: JSON.stringify(arrselectedData),
                success: function (data, status) {
                    alert(status);
                    self.bootstrapTable.refresh();
                }
            });
        };

        ko.applyBindings(self, bindElement);
    };
})(jQuery);