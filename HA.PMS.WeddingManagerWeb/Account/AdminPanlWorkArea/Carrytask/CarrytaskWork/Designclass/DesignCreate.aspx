<%@ Page Language="C#" AutoEventWireup="true" Title="个人清单" CodeBehind="DesignCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.Designclass.DesignCreate" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Src="../../../Control/CarrytaskCustomerTitle.ascx" TagName="CarrytaskCustomerTitle" TagPrefix="uc1" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .btn_info {
            background-color: #5A7DE7;
            color: white;
            cursor: pointer;
            border: 1px solid #564f4f;
        }

            .btn_info:hover {
                background-color: #2050DF;
                color: white;
            }
    </style>
    <script type="text/javascript">
        function CheckRow() {
            if (confirm("请注意，下单后将无法撤销!")) {
                return true;
            } else {
                return false;
            }
        }

        function ShowThis() {
            ShowParentWindown("/AdminPanlWorkArea/QuotedPrice/QuotedPriceShowOrPrint.aspx?CustomerID=<%=Request["CustomerID"]%>&NeedPopu=1", 1280, 1500);
            return false;
        }

        //选择供应商
        function ChangeSuppByCatogry(Parent, CallBack) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectSupplierBythis.aspx?Callback=" + CallBack + "&ALL=true&ControlKey=hideSuppID&SetEmployeeName=txtSuppName&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function ShowUpdateWindows(KeyID, Control, Type) {
            var Url = "../../../Foundation/Fd_content/FD_MaterialUpdate.aspx?MaterialId=" + KeyID + "&type=" + Type;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 500, 1000, "a#" + $(Control).attr("id"));
        }

        //打印
        function preview() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
        }




        //上传图片
        function ShowFileUploadPopu(DesignClassID) {
            var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/DesignImageUpload.aspx?DesignClassID=" + DesignClassID + "&OrderID=<%=Request["OrderID"]%>&Type=<%=Request["Type"]%>";
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


        //查看图片
        function ShowFileShowPopu(DesignClassID, Type) {
            var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/DesignImageShowList.aspx?DesignClassID=" + DesignClassID + "&OrderID=<%=Request["OrderID"]%>&Type=" + Type;
            showPopuWindows(Url, 700, 800, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        //点击文本框 弹出部门人员列表
        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ALL=1&ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 480, 380, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


        $(document).ready(function () {
            $("#btnDispatchingConfirm").click(function () {

                if ($("#txtEmpLoyee").val() == "") {
                    alert("请选择设计师");
                    return false;
                }
                else if ($("#txtPlanDate").val() == "") {
                    alert("请选择计划完成时间");
                    return false;
                }
            });
        });

    </script>
</asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <uc1:CarrytaskCustomerTitle ID="CarrytaskCustomerTitle1" runat="server" />
    <div runat="server" style="height: 55px;" class="div_PrintOrExport">
        <asp:Button runat="server" ID="btnPrint2" CssClass="btn btn_info" Text="打印" Style="background-color: #5A7DE7; color: white; cursor: pointer; border: 1px solid #564f4f;" OnClientClick="return preview()" Visible="false" />
        <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="导出Excel" OnClick="Button2_Click" Visible="false" />
        <asp:Button runat="server" ID="btnBackUp" CssClass="btn btn-primary" Text="打回设计单" OnClick="btnBackUp_Click" Visible="false" />
    </div>

    <!--startprint-->
    <div style="overflow: scroll;">
        <div class="div">
            <table style="width: 60%; text-align: center;">
                <tr>
                    <td><b style="font-size: 13px;">主题：</b></td>
                    <td>
                        <asp:TextBox runat="server" ID="txtThemes" Text="" /></td>
                    <td><b style="font-size: 13px;">色调：</b></td>
                    <td>
                        <asp:TextBox runat="server" ID="txtColorTone" Text="" /></td>
                    <td><b style="font-size: 13px;">风格：</b></td>
                    <td>
                        <asp:TextBox runat="server" ID="txThemeStyle" Text="" /></td>
                    <td>
                        <asp:Button runat="server" ID="btn_SaveThemes" Text="保存" CssClass="btn btn-primary" OnClick="btn_SaveThemes_Click" /></td>
                </tr>
                <tr>
                    <td colspan="7" height="30"></td>
                </tr>
            </table>
        </div>

        <table style="text-align: left;">
            <tr>
                <td colspan="13">
                    <hr style="width: 100%; color: brown; height: 2px;" />
                </td>
            </tr>
            <tr>
                <th width="100">产品名称</th>
                <th width="150">说明</th>
                <th width="90">材质</th>
                <th width="90">采购单价</th>
                <th width="30">单位</th>
                <th width="80">采购数量</th>
                <th width="100">规格</th>
                <th width="auto">参考图</th>
                <th></th>
                <th width="auto">效果图</th>
                <th width="90px">供应商</th>
                <th width="75px">结算总价</th>
                <th runat="server" id="th_Save" width="200px">保存</th>
            </tr>
            <tr runat="server" id="tr_TextBoxInsert">
                <td>
                    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtNode" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMaterial" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged" runat="server" Width="85px" AutoPostBack="true"></asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtPurchasePrice" runat="server" Width="20"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtUnit" runat="server" Width="30"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtPurchaseQuantity" Width="20" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtSpec" runat="server" Width="75"></asp:TextBox>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <asp:DropDownList ID="ddlSupplierName" Width="80" runat="server"></asp:DropDownList>

                    <%--                 <input type="text" runat="server" id="txtSupplier" onclick="ChangeSuppByCatogry(this, 'btnSavesupperSave')" />
                    <input id="btnSetSuplier" class="btn btn-primary" type="button" value="指定供应商" onclick="ChangeSuppByCatogry(this, 'btnSavesupperSave')" />
                    <div style="display: none;">
                        <asp:Button ID="btnSaveItem" CommandName="SaveItem" runat="server" Text="保存" CssClass="btn btn-success" OnClick="btnSaveItem_Click" />
                        <asp:Button ID="btnSavesupperSave" CommandName="SaveItem" ClientIDMode="Static" runat="server" Text="供应商保存" CssClass="btn btn-success" OnClick="btnSavesupperSave_Click" />
                        <asp:HiddenField ID="hidePriceKey" runat="server" />
                        <asp:HiddenField ID="hideCategoryID" runat="server" />
                    </div>--%>
                </td>
                <td>自动计算
                </td>

                <td>
                    <asp:Button ID="btnSaveChange" runat="server" Text="保存" OnClick="btnSaveChange_Click" CssClass="btn btn-success" />
                    <asp:Button ID="Button1" runat="server" Text="查看原始报价单" OnClientClick="return ShowThis();" CssClass="btn btn-success" />
                </td>

            </tr>
            <!--可修改-->
            <asp:Repeater ID="RepDesignlist" runat="server" OnItemCommand="RepDesignlist_ItemCommand" OnItemDataBound="RepDesignlist_ItemDataBound">

                <ItemTemplate>
                    <tr>

                        <td>
                            <%#Eval("Title") %>
                            <asp:HiddenField runat="server" ID="HideDesignId" Value='<%#Eval("DesignclassID") %>' />
                        </td>
                        <td>
                            <%#Eval("Node") %></td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlMaterial" Width="85" SelectedValue='<%#Eval("Material") %>' DataSourceID="ObjMaterialSource" DataTextField="MaterialName" DataValueField="MaterialId" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPurchasePrice" runat="server" Text='<%#Eval("PurchasePrice")%>' Width="75"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox runat="server" ID="txtUnit" Text='<%#Eval("Unit")%>' Width="30px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtPurchaseQuantity" runat="server" Text='<%#Eval("PurchaseQuantity")%>' Width="75"></asp:TextBox></td>

                        <td>
                            <asp:TextBox runat="server" ID="txtSpec" Text='<%#Eval("Spec")%>' Width="75"></asp:TextBox>
                        </td>
                        <td>
                            <!---参考图-->
                            <div style="width: 85px">
                                <a href="#" onclick="ShowFileUploadPopu('<%#Eval("DesignclassID") %>')" <%=StatuHideViewInviteInfo() %> class="btn btn-mini   btn-primary">上传</a>
                                <a href="#" onclick="ShowFileShowPopu('<%#Eval("DesignclassID") %>',2)" class="btn btn-mini   btn-primary needshow">查看</a>
                            </div>
                            <table class="Table table-bordered table-striped" style="width: 100%;">
                                <tr>
                                    <asp:Repeater runat="server" ID="repShowImg">
                                        <ItemTemplate>
                                            <td>
                                                <div style="overflow: auto; width: 200px;">
                                                    <a target="_blank" href="<%#Eval("FileAddress") %>">
                                                        <asp:Image runat="server" ID="imgShows" ImageUrl='<%#Eval("FileAddress") %>' Width="200px" Height="150px" /></a>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>

                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <!---效果图-->
                            <div style="width: 85px">
                                <a href="#" onclick="ShowFileUploadPopu('<%#Eval("DesignclassID") %>')" <%=StatuHideViewInviteInfos() %> class="btn btn-mini   btn-primary">上传</a>
                                <a href="#" onclick="ShowFileShowPopu('<%#Eval("DesignclassID") %>',1)" class="btn btn-mini   btn-primary needshow">查看</a>
                            </div>
                            <table class="Table table-bordered table-striped" style="width: 100%;">
                                <tr>
                                    <asp:Repeater runat="server" ID="repShowResultImg">
                                        <ItemTemplate>
                                            <td>
                                                <div style="overflow: auto; width: 200px;">
                                                    <a target="_blank" href="<%#Eval("FileAddress") %>">
                                                        <asp:Image runat="server" ID="imgShows" ImageUrl='<%#Eval("FileAddress") %>' Width="200px" Height="150px" /></a>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                        </td>

                        <td>
                            <div style="width: 105px">
                                <asp:DropDownList runat="server" ID="ddlSupplier" Width="80" SelectedValue='<%#Eval("Supplier") %>' DataSourceID="ObjSupplierSource" DataTextField="Name" DataValueField="SupplierID"></asp:DropDownList>
                            </div>
                        </td>
                        <td>
                            <div style="width: 75px">
                                <%#Eval("TotalPrice")%>
                            </div>
                        </td>

                        <td>
                            <div style="width: 200px;">
                                <asp:LinkButton ID="lnkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("DesignclassID") %>' runat="server" OnClientClick="return confirm('你确定要删除该项吗？')">删除</asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnFinish" CommandName="Edit" CommandArgument='<%#Eval("DesignclassID") %>' runat="server">保存修改</asp:LinkButton>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="13">
                            <hr style="height: 2px; background-color: #808080;" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

            <!--显示-->
            <asp:Repeater ID="RepDesignListShow" runat="server" OnItemDataBound="RepDesignListShow_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("Title") %>
                            <asp:HiddenField runat="server" ID="HideDesignId" Value='<%#Eval("DesignclassID") %>' Visible="false" />
                        </td>
                        <td><%#Eval("Node") %></td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlMaterial" Width="75" SelectedValue='<%#Eval("Material") %>' DataSourceID="ObjMaterialSource" DataTextField="MaterialName" DataValueField="MaterialId" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                            <asp:Label runat="server" ID="lblMaterial"></asp:Label>
                        </td>
                        <td><%#Eval("PurchasePrice")%></td>

                        <td><%#Eval("Unit")%></td>
                        <td><%#Eval("PurchaseQuantity")%></td>
                        <td><%#Eval("Spec")%></td>
                        <td><a target="_blank" href='/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/DesignImageShowList.aspx?DesignClassID=<%#Eval("DesignclassID") %>&Type=2' class="btn btn-mini   btn-primary needshow">查看</a></td>
                        <td></td>
                        <td><a target="_blank" href='/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/DesignImageShowList.aspx?DesignClassID=<%#Eval("DesignclassID") %>&Type=1' class="btn btn-mini   btn-primary needshow">查看</a></td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlSupplier" Width="80" SelectedValue='<%#Eval("Supplier") %>' DataSourceID="ObjSupplierSource" DataTextField="Name" DataValueField="SupplierID" Visible="false"></asp:DropDownList>
                            <asp:Label runat="server" ID="lblSupplier"></asp:Label>
                        </td>
                        <td><%#Eval("TotalPrice")%></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="13">
                            <div style="height: 15px;"></div>
                        </td>
                    </tr>

                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <!--endprint-->
    <br />
    <div class="ui-menu-divider">
        <ul style="list-style: none; width: 50%; font-size: 12px; font-weight: bolder;">
            <li style="float: left;">设计师:<asp:Label runat="server" ID="lblDesignEmployee" />&nbsp;&nbsp;&nbsp;</li>

            <li>计划完成时间:<asp:Label runat="server" ID="lblPlanFinishDate" /></li>
        </ul>
    </div>
    <!--分派设计师-->
    <table style="text-align: left; width: 100%;" class="table table-bordered table-striped">
        <tr>
            <td runat="server" id="td_Dispatching">设计师：
                <asp:HiddenField runat="server" ClientIDMode="Static" Value="-1" ID="hideEmpLoyeeID" />
                <asp:TextBox runat="server" ID="txtEmpLoyee" class="txtEmpLoyeeName" ClientIDMode="Static" onclick="ShowPopu(this);" />
                <a href="#" onclick="ShowPopu(this);" class="SetState btn btn-primary" style="display: none;">选择</a>
                预计完成时间：<asp:TextBox runat="server" ID="txtPlanDate" ClientIDMode="Static" onclick="WdatePicker();" Width="90px" />
                <asp:LinkButton runat="server" ID="btnDispatchingConfirm" ClientIDMode="Static" CssClass="btn btn-primary" Text="确认下派" OnClick="btnDispatchingConfirm_Click" />&nbsp;&nbsp;&nbsp;
            </td>
            <td>
                <asp:Button runat="server" ID="btnSaveConfirm" CssClass="btn btn-primary" Text="确认下单" OnClick="btnSaveConfirm_Click" OnClientClick="return CheckRow();" />&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" ID="BtnExportImage" CssClass="btn btn-primary" Text="导出图片" OnClick="BtnExportImage_Click" Visible="false" />&nbsp;&nbsp;&nbsp;
                <a href="#" onclick='ShowUpdateWindows(0,this,"Add");' class="SetState btn btn-danger">新增材质</a>

            </td>
        </tr>
    </table>

    <!---数据源-->
    <div class="div_ObjSource">
        <!---材质数据源-->
        <asp:ObjectDataSource ID="ObjMaterialSource" runat="server" SelectMethod="GetByAll" TypeName="HA.PMS.BLLAssmblly.FD.Material"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjSupplierSource" runat="server" SelectMethod="GetByCategoryId" TypeName="HA.PMS.BLLAssmblly.FD.Supplier">
            <SelectParameters>
                <asp:Parameter DefaultValue="5" Name="CategoryId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <!--供应商数据源-->
    </div>

</asp:Content>
