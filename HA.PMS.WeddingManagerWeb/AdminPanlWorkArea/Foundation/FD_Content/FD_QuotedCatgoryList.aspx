<%@ Page Language="C#" CodeBehind="FD_QuotedCatgoryList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.FD_QuotedCatgoryList" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script src="/Scripts/trselection.js"></script>

    <script type="text/javascript">

        function ShowWindows(KeyID, parentId, Control) {
            var Url = "Sys_DepartmentUpdate.aspx?deparId=" + KeyID + "&parent=" + parentId;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 680, 800, "a#" + $(Control).attr("id"));
        }


        function ShowUpdateWindows(KeyID) {
            var Url = "";

            Url = "FD_QuotedCatgoryProductCreateUpdate.aspx?Qckey=" + KeyID;
            showPopuWindows(Url, 700, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();


        }



        function ShowPopu(Parent, Action) {
            var Url = "/AdminPanlWorkArea/Foundation/FD_Content/FD_QuotedCatgoryCreateEdit.aspx?Paretnt=" + Parent + "&Action=" + Action;
            showPopuWindows(Url, 700, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }




        //UploadFile
        function ShowFileUploadPopu(Qckey) {
            var Url = "/AdminPanlWorkArea/Foundation/FD_Content/FD_QuotedCatgoryFileServer.aspx?QcKey=" + Qckey;
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


        //product
        function ShowProductPopu(Qckey) {
            var Url = "/AdminPanlWorkArea/Foundation/FD_Content/FD_QuotedCatgoryProduct.aspx?QcKey=" + Qckey;
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


        //product
        function ShowProductPopu1(Qckey) {
            var Url = "/AdminPanlWorkArea/Foundation/FD_Content/FD_QuotedCatgorySelectmanager.aspx?QcKey=" + Qckey;
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <br />
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <a id="CreateFirst" class="btn btn-primary  btn-mini" href="#" onclick="ShowPopu(0,'Create');">添加顶级类别</a>
    <a id="A1" class="btn btn-primary  btn-mini" href="FD_QuotedCatgoryProductManager.aspx?Vtype=1">报价产品管理</a>


    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5></h5>
            <span class="label label-info"></span>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>类别</th>
                        <th>操作</th>

                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repQuotedCatogry" runat="server" OnItemCommand="repQuotedCatogry_ItemCommand">
                        <ItemTemplate>
                            <tr skey='<%#Eval("QCKey") %>'>
                                <td><%#GetItemNbsp(Eval("ItemLevel")) %><%#Eval("Title") %></td>
                                <td>
                                    <a href="#" class="btn btn-mini btn-primary btn-success" onclick='ShowPopu(<%#Eval("QCKey") %>,"Create");' <%#Eval("ItemLevel").ToString()=="2"?"style='display:none'":"" %>>添加子类别</a>
                                    <a href="#" class="btn btn-mini btn-primary" onclick='ShowPopu(<%#Eval("QCKey") %>,"Update");'>修改</a>
                                    <a href="#" class="btn btn-mini btn-primary" onclick='ShowFileUploadPopu(<%#Eval("QCKey") %>,"Update");'>上传多媒体资料</a>
                                    <a href="#" class="btn btn-mini btn-primary" onclick='ShowProductPopu(<%#Eval("QCKey") %>,"Update");'>编辑标准产品</a>
                                    <a href="#" class="btn btn-mini btn-primary" onclick='ShowProductPopu1(<%#Eval("QCKey") %>);'>编辑已选产品</a>
                                    <a href="#" class="btn btn-mini btn-primary" onclick="ShowUpdateWindows(<%#Eval("QCKey") %>);">添加单个产品</a>
                                    <asp:LinkButton ID="btnUp" CssClass="btn btn-mini btn-danger" Visible="true" CommandArgument='<%#Eval("QCKey") %>' CommandName="up" runat="server">向上</asp:LinkButton>
                                    <asp:LinkButton ID="btnFlow" CssClass="btn btn-mini btn-danger" Visible="true" CommandArgument='<%#Eval("QCKey") %>' CommandName="down" runat="server">向下</asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnDelete" CssClass="btn btn-mini btn-danger" Visible="false" CommandArgument='<%#Eval("QCKey") %>' CommandName="Delete" runat="server">删除</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnForbidden" CssClass="btn btn-mini btn-danger" Visible="true" CommandArgument='<%#Eval("QCKey") %>' CommandName="Forbidden" runat="server">禁用</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnStart" CssClass="btn btn-mini btn-danger" Visible="true" CommandArgument='<%#Eval("QCKey") %>' CommandName="Start" runat="server">启用</asp:LinkButton>
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>

</asp:Content>
