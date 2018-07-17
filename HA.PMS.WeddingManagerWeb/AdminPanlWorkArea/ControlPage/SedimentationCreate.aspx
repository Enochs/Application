<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="SedimentationCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.SedimentationCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script type="text/javascript">
           $(document).ready(function () {
               $("html,body").css({ "width": "800px", "height": "800px", "background-color": "transparent" });
           });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
         <span style="color:green;">
             提示&nbsp;1.沉淀库的存放文件夹需要把对应文件存放在目录为  软件安装\Files\Sedimentation
           <br />
                       2.该存放文件夹的地址里面可以存放多个文件夹，程序将自动读取文件
           <br />
                       3.文件的格式，包括视频文件，文本，图片和PPT
         </span>
        <div class="widget-content">
            <asp:Button ID="btnSaveAll" runat="server" Text="保存选择" OnClick="btnSaveAll_Click" CssClass="btn btn-success" />
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>选择</th>
                        <th>文件名</th>
                        <th>文件类型</th>
                        <th>文件大小</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptFiles" runat="server">
                        <ItemTemplate>


                            <tr>
                                <td>
                                    <asp:HiddenField ID="hfFiles" runat="server" />
                                    <asp:CheckBox ID="chSinger" runat="server" />


                                </td>
                                <td>

                                    <asp:Literal ID="ltlFileName" Text='<%#Eval("fileNames") %>' runat="server"></asp:Literal>

                                </td>
                                <td><span style="display: none;">
                                    <asp:Literal ID="ltlFilePaths" Text='<%#Eval("filePaths") %>' runat="server"></asp:Literal></span>
                                    <%#GetFileType(Eval("filePaths")) %>

                                </td>
                                <td><%#Eval("fileSizes") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
