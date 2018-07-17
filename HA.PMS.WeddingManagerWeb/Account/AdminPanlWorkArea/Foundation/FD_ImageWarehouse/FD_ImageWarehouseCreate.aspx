<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_ImageWarehouseCreate.aspx.cs" StylesheetTheme="None" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_ImageWarehouse.FD_ImageWarehouseCreate" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/FileLoad/uploadify.css" rel="stylesheet" />

    <script type="text/javascript" src="/Scripts/jquery-1.3.2.min.js"></script>

    <script type="text/javascript" src="/Scripts/LoadFile/swfobject.js"></script>

    <script type="text/javascript" src="/Scripts/LoadFile/jquery.uploadify.v2.1.0.min.js"></script>
    <script type="text/javascript">

        //上传文件的结果状态
        function ResultState(event, queueID, fileObj, response, data) {
            if (response != "") {
                showInfo("成功上传" + response, true); //showInfo方法设置上传结果     

            }
            else {
                showInfo("文件上传出错！", false);
            }
        }
        //上传状态提示效果
        function showInfo(msg, type) {
            var msgClass = type == true ? "textstyle2" : "textstyle1";
            $("#result").removeClass();
            $("#result").addClass(msgClass);
            var msgResults = "";
            if (msg.toString().length > 12) {
                $("#result").attr("title", msg);
                msgResults = msg.toString().substring(0, 12) + "...";
            } else {
                msgResults = msg;
                $("#result").attr("title", "");
            }
            $("#result").html(msgResults);
        }
        //如果点击‘上传文件’时选择文件为空，则提示
        function checkImport() {
            if ($.trim($('#fileQueue').html()) == "") {
                alert('请先选择要上传的文件！');
                return false;
            }
            return true;
        }


        $(document).ready(function () {

            $("#fileInput1").uploadify({
                'uploader': '/Content/FileLoad/uploadify.swf',
                'script': '/LoadFiles.ashx?typeId=' + $("#<%=ddlImageType.ClientID%>").val(),

                'folder': '/Files/ImageWareHouse',
                'queueID': 'fileQueue',
                'auto': false,
                'multi': true,
                "fileExt": "*.bmp;*.gif;*.jpg;*.png;*.jpeg",//设置可以选择的文件的类型，格式如："*.doc;*.pdf;*.rar" 。 
                "fileDesc": "请选择bmp gif jpg png jpeg文件",//来设置选择文件对话框中的提示文本
                "width": 110,
                "height": 30,
                "sizeLimit": "1048576",
                "queueSizeLimit": "10",
                "cancelImg": "/Content/FileLoad/cancel.png",
                "onComplete": ResultState,
                "onError": function (event, queueId, fileObj, errorObj) {
                    if (errorObj.type == "HTTP") {
                        alert("您的网络出现错误，请按F5刷新重试");
                    } else if (errorObj.type == "IO") {
                        alert("对不起上传的文件有误");
                    } else if (errorObj.type == "Security") {
                        alert("请你改善你浏览器安全级别设置");
                    }

                },
                "onSelectOnce": function (event, data) {

                    //alert("对不起上传的文件大小不能超过1M(兆)");
                },
                "onQueueFull": function (event, queueSizeLimit) {
                    alert("对不起，你本次上传的文件个数超过了10个。\n 系统自动保留前10个文件列表， 请分批上传");
                    return false;
                },
                "onCancel": function (event, queueId, fileObj, data) {

                }
            });
            showPopuWindows($("#loadImg").attr("href"), 700, 1000, "a#loadImg");

        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>图片上传界面</h5>
            <span class="label label-info">上传</span>
        </div>
        <div class="widget-content">


            <div>
                请你选择图片类型，然后进行上传图片操作。
                <br />
                图片类别:
                <asp:DropDownList ID="ddlImageType" runat="server"></asp:DropDownList>
                <br />
                <input id="fileInput1" name="fileInput1" type="file" /><br />
                <a class="btn btn-success" href="javascript:if(checkImport()){ 
                    $('#fileInput1').uploadifyUpload(); }">开始上传</a>
                <br />

                <span id="result"></span>
                <span style="color: red;">注意：每次上传只能上传10个文件,
                                   每个文件上传的大小不能超过1M（兆）</span>

                <!--上传队列-->
                <div id="fileQueue"></div>
            </div>
        </div>
    </div>
</asp:Content>
