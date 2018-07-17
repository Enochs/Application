<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_GuardianImageOper.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian.FD_GuardianImageOper" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function ShowOperImg( Control) { 
           
          
            var Url = "/AdminPanlWorkArea/Foundation/FD_Hotel/FD_HotelImageLoadFile.aspx?GuardianId=" + <%=ViewState["GuardianId"]%> + "&GuradianFileType=2&toOperPage=" + <%=ViewState["load"]%>;
              $(Control).attr("id", "updateShow" + 4444);
              showPopuWindows(Url,  700, 300, "a#" + $(Control).attr("id"));
           
           
        }


        $(document).ready(function () {

            showPopuWindows($("#createImg").attr("href"), 600, 300, "a#createImg");

            $(".grouped_elements").each(function (indexs, values) {
                if ($.trim($(this).html()) == "") {
                    $(this).remove();
                }
            });
            //加载对应图片浏览效果 begin
            $(".grouped_elements").each(function (indexs, values) {
                var imgChildren = $(this).children("img");
                $(this).attr("href", imgChildren.attr("src"));

            });



            $("a.grouped_elements").fancybox();
        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">

        <div class="widget-content">
          
            <a id="createImg"  onclick="ShowOperImg(this);"  class="btn  btn-primary  btn-mini">添加图片
            </a>
            <table class="table table-bordered table-striped" style="width:600px;">
                <thead>
                    <tr>

                        <th>图片名称</th>
                        <th>图片</th>

                        <%--  <th>风格说明</th>--%>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptImg" OnItemCommand="rptImg_ItemCommand" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("ImageName") %></td>
                                <td>


                                    <a class="grouped_elements" href="#" rel="group1">
                                        <img src="<%#Eval("ImagePath") %>" width="100" hidden="70" />
                                    </a>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("GuardianImageID") %>' CssClass="btn btn-danger btn-mini" runat="server">删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>


                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="ImagePager" AlwaysShow="true" OnPageChanged="ImagePager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>

</asp:Content>
