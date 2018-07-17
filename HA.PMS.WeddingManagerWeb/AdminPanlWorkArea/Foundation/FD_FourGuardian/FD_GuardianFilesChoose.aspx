<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_GuardianFilesChoose.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian.FD_GuardianFilesChoose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $("html,body").css({ "width": "600px", "height": "300px" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">

        <div class="widget-content">
            <asp:Button ID="btnSaveAll" runat="server" Text="保存" CssClass="btn btn-success" OnClick="btnSaveAll_Click" />
            <br />
            <asp:Label runat="server" ID="lblFileName" Text="文件名称" />
            <asp:TextBox runat="server" ID="txtFileName" />
            <asp:Button runat="server" ID="btnLook" CssClass="btn btn-primary" Text="查询" OnClick="btnLook_Click" />
            <table class="table table-bordered table-striped" style="width: 600px;">
                <thead>
                    <tr>
                        <th style="white-space: nowrap;">选择</th>
                        <th style="white-space: nowrap;">文件名</th>
                        <%--  <th style="white-space:nowrap;">预览</th>--%>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptGuradian" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="word-break: break-all;">
                                    <asp:HiddenField ID="hfValue" Value='<%#Eval("GuradianFileId") %>' runat="server" />
                                    <asp:CheckBox ID="chSinger" runat="server" />


                                </td>
                                <td style="word-break: break-all;">
                                    <asp:Literal ID="ltlGuradianFileName" Text='<%#Eval("GuradianFileName") %>' runat="server"></asp:Literal>


                                </td>
                                <td style="word-break: break-all; display: none;">
                                    <asp:Literal ID="ltlGuradianFilePath" Text='<%#Eval("GuradianFilePath") %>' runat="server"></asp:Literal>
                                </td>


                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <asp:Button ID="Button1" runat="server" Text="保存" CssClass="btn btn-success" OnClick="btnSaveAll_Click" />
        </div>
    </div>

</asp:Content>
