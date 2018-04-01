/*******搜索DIV点击*******/
$('.dropdown-search input, .dropdown-search label,.dropdown-search .form-group').click(function (e) {
    e.stopPropagation();
});
/*******页面背景填充*******/
$(".content-wrapper").css('min-height', $(window).height() - $('.main-footer').outerHeight() - $('.main-header').outerHeight());

/*******初始化*********/
function InitDatatables(dataTableObj, actionUrl, aoColumns, oTable, paging, rowCallBack, pageSize, initComplete, ordering) {
    if (paging == null) {
        paging = true;
    }
    if (pageSize == null) {
        pageSize = 20;
    }
    if (ordering == null) {
        ordering = true
    }
    oTable = dataTableObj.dataTable({
      //  "columnDefs": [
      //{
      //    "targets": [1],
      //    "visible": false
      //}
      //  ],
        "bJQu   eryUI": false,
        "sPaginationType": "full_numbers",
        'bLengthChange': true,
        "bFilter": false,
        "bInfo": true,
        "ordering": ordering,
        'bPaginate': paging,
        "bProcessing": true,
        "bAutoWidth": false,
        "bServerSide": true,
        "bStateSave": false,
        "iDisplayLength": pageSize,
        "aLengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "sAjaxSource": actionUrl,
        "aoColumns": aoColumns,
        "dom": 'rt<"bottom"<"col-md-3"i><"col-md-9"p><"clear">>',
        "fnCreatedRow": function (nRow, aData, iDataIndex) {
            if (oTable.fnSettings().aoColumns[0].bVisible) {
                $(nRow).prepend("<td>" + (iDataIndex + 1) + "</td>");
                //$(nRow).prepend("<td style='text-align:center;' data-id=''><input type='checkbox' class='tdchk'  /></td>");
                //$('td:eq(0)', nRow).html(iDataIndex + 1);//为第一个单元格增加序号
            }
        },
        "drawCallback": function (settings) {
            if (oTable.api().ajax.params().sEcho == 1) {
                var params = oTable.api().ajax.params();
                var sortIndex = "0";
                $.each(params, function (i, o) {
                    if (i.indexOf("mDataProp_") > -1) {
                        if (o == "sort") {
                            sortIndex = i.substr(10);
                        }
                    }
                });
                oTable.api().order([[sortIndex, 'asc']]);
            }
        },
        "fnRowCallback": rowCallBack,
        "initComplete": initComplete
    });
    oTable.find("thead tr").prepend("<th width='20'></th>");
    oTable.find("tbody").on('click', 'tr', function () {
        
        if ($(this).find("td").attr("class") != "dataTables_empty") {
           
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                oTable.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
        }
        
    });

    return oTable;
}

function iCheckBind() {
    oTable.find('input[type="checkbox"],input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue',
        increaseArea: '20%' // optional
    });
}

$("table").on("click", "input[type='checkbox'].thchk", function () {
    if ($(this).is(":checked")) {
        oTable.find("input[type='checkbox'].tdchk").prop("checked", true);
    }
    else {
        oTable.find("input[type='checkbox'].tdchk").prop("checked", false);
    }
});

/*******弹出表单*********/
function ShowModal(actionUrl, param, title, isLarge) {

    //表单初始化
    $(".modal-title").html(title);
    $("#modal-content").attr("action", actionUrl);
    if (isLarge == 2) {
        $(".modal-dialog").addClass("modal-lg-99");
        var height = $(window).height();
        //$(".modal-dialog .modal-body").height(height);
        //$(".modal-dialog .modal-body").css({ "overflow-y": "auto" });
    }
    else if (isLarge) {
        $(".modal-dialog").addClass("modal-lg");
    }

    $.ajax({
        type: "GET",
        url: actionUrl,
        data: param,
        async: false,
        beforeSend: function () {
            //
        },
        success: function (result) {
            $("#modal-content").html(result);
            $('#modal-form').modal('show');
            //RegisterForm();
        },
        error: function () {
            //
        },
        complete: function () {
            //
        }
    });
}

/*******弹出Dialog*********/
function ShowDialogModal(actionUrl, param, title) {

    //Dialog初始化
    $(".model-title").html(title);

    $.ajax({
        type: "GET",
        url: actionUrl,
        data: param,
        beforeSend: function () {
            //
        },
        success: function (result) {
            $("#dialogModal-content").html(result);
            $('#dialogModal-form').modal('show');
            //RegisterForm();
        },
        error: function () {
            //
        },
        complete: function () {
            //
        }
    });
}

/*******注册验证脚本*********/
function RegisterForm() {
    $('#modal-content').removeData('validator');
    $('#modal-content').removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse('#modal-content');
}

/*******关闭弹出框*********/
function CloseModal() {
    $('#modal-form').modal('hide');
    ClearForm($("#modal-content"));
}

/*******清除表单数据*********/
function ClearForm(obj) {
    obj.find(':input').not(':button, :submit, :reset').val('').removeAttr('checked').removeAttr('selected');
}

/*******保存表单*********/
function SaveModal(oTable, pActionUrl, pData) {
    var actionUrl = $("#modal-content").attr("action");
    var $form = $("#modal-content");
    var data = $form.serialize();

    if (pActionUrl) {
        actionUrl = pActionUrl;
    }
    if (pData) {
        data = pData;
    }
    if (!$form.valid()) {
        return;
    }

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: data,
        async: false,
        success: function (data) {
            //判断返回值，若为Object类型，即操作成功
            var result = ((typeof data == 'object') && (data.constructor == Object));
            if (result) {
                if (data.ResultType == "0") {
                    toastr.success(data.Message);
                    $('#modal-form').modal('hide');
                }
                else if (data.ResultType == "6") {
                    toastr.warning(data.Message);
                }
                else if (data.ResultType == "7") {
                    toastr.error(data.Message);
                }
                ReloadDataTable(oTable);
            }
            else {
                $("#modal-content").html(data);
            }
        },
        error: function () {
            var result = ((typeof data == 'object') && (data.constructor == Object));
            if (result) {
                toastr.error(data.Message);
            }
            else {
                $("#modal-content").html(data);
            }
        }
    });
}

function SaveModalGetParams(oTable, actionUrl, data) {
    var $form = $("#modal-content");

    if (!$form.valid()) {
        return;
    }

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: data,
        success: function (data) {
            //判断返回值，若为Object类型，即操作成功
            var result = ((typeof data == 'object') && (data.constructor == Object));
            if (result) {
                toastr.success(data.Message);
                $('#modal-form').modal('hide');
                ReloadDataTable(oTable);
            }
            else {
                $("#modal-content").html(data);
            }
        }
    });
}

/*******刷新表格*********/
function ReloadDataTable(obj) {
    obj.fnDraw();
}

/*******删除操作*********/
function DeleteRecord(actionUrl, param, oTable) {
    bootbox.setDefaults("locale", "zh_CN");
    bootbox.confirm("确定删除此记录?", function (r) {
        if (r) {
            $.ajax({
                type: "POST",
                url: actionUrl,
                data: param,
                async: false,
                beforeSend: function () {
                    //
                },
                success: function (result) {
                    if (result.ResultType == 0) {
                        toastr.success(result.Message);
                    }
                    else if (result.ResultType == 6) {
                        toastr.warning(result.Message);
                    }
                    else if (result.ResultType == 7) {
                        toastr.error(result.Message);
                    }
                    ReloadDataTable(oTable);
                },
                error: function (result) {
                    if (result) {
                        toastr.error(data.Message);
                    }
                    else {
                        $("#modal-content").html(data);
                    }
                    //
                },
                complete: function () {
                    //
                }
            });
        }
    });
}
/********点击Table的首列Checkbox则选择该行*********/
//oTable.find("tbody").on("click", "input:checkbox[class='tdchk']", function () {
//    $(this).parents("tr").addClass("selected");
//    return;
//})
/*******获取行值*********/
function getSelectRow(oTable, field) {
    var $select = oTable.$('tr.selected');
    if ($select.length > 0)
        return oTable.fnGetData(oTable.$('tr.selected').get(0))[field];
    else
        return null
}

/*******获取多行值*********/
function getSelectRows(oTable, field, sb) {
    var $select = oTable.find("input[class='tdchk']:checked");
    if ($select.length > 0) {
        if (sb == null) {
            sb == ",";
        }
        var str = $select.map(function (i, o) { return oTable.fnGetData($(this).parents("tr").get(0))[field] }).get().join(sb);
        return str
    }
    else {
        return null;
    }
}

/*******获取参数值*********/
function getRegexParam(name, url) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    if (url != null && url.length > 0) {
        var r = url.substr(1).match(reg);
    }
    else {
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    }
    if (r != null) return unescape(r[2]); return null; //返回参数值
}

/********** Show Dialog ************/

function ShowBootBoxDialog(title, url, param, content, className, buttons) {
    var msg;
    $.ajax({
        type: "GET",
        url: url,
        data: param,
        async: false,
        beforeSend: function () {
            //
        },
        success: function (result) {
            msg = result;
        },
        error: function () {
            //
        },
        complete: function () {
            //
        }
    });
    bootbox.dialog({
        title: title,
        message: msg,
        buttons: buttons
    });
    if (className != null) {
        if (className == "modal-lg-99") {
            var height = $(window).height() - 152;
            $(".bootbox .modal-dialog .modal-body").height(height);
            $(".bootbox .modal-dialog .modal-body").css({ "overflow-y": "auto" });
        }
        $(".bootbox .modal-dialog").addClass(className);
    }
}


/************INPUT 输入验证（数字）***************/
function ValidInt(self) {
    return self.value.replace(/[^\d]/g, '');
}

function ValidPostInt(self) {
    return self.value.clipboardData.setData('text', clipboardData.getData('text').replace(/[^\\d]/g, ''));
}

function ValidFloat(self) {
    return self.value.replace(/[^(\d)+(.)?]/g, '');
}
/**********INPUT 输入验证（数字）END*************/
