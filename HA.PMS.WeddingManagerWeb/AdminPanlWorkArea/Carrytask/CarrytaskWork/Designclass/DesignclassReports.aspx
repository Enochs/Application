<%@ Page Title="个人清单" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="DesignclassReports.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.Designclass.DesignclassReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tablestyle {
            border-width: 1px;
            border-style: solid;
            border-color: #9c8484;
            border-collapse: collapse;
            font-size: 13px;
            font-weight: 100;
            width: auto;
            color: #2F4F4F;
        }

            .tablestyle tr th {
                background-color: #a19c9c;
                height: 35px;
            }

            .tablestyle tr td {
                height: 30px;
                background-color: #efe1e1;
            }

        .btnprints {
            background-color: #5A7DE7;
            color: white;
            cursor: pointer;
            border: 1px solid gray;
            font-size: 13px;
            width: auto;
        }

        .btn-primary {
            color: white;
            background-color: #1e92de;
            cursor: pointer;
        }

            .btn-primary:hover {
                background-color: #2050DF;
                color: white;
            }

        .btnprints:hover {
            background-color: #2050DF;
            color: white;
        }

        ul li {
            list-style: none;
            float: left;
            font-size: 14px;
            color: #451111;
        }
    </style>

    <script type="text/javascript">

        //查看原始报价单
        function ShowThis() {
            ShowParentWindown("/AdminPanlWorkArea/QuotedPrice/QuotedPriceShowOrPrint.aspx?CustomerID=<%=Request["CustomerID"]%>&NeedPopu=1", 1280, 1500);
            return false;
        }

        //选择设计师
        function ShowEmployeePopu1(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmployeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmployeesID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
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



        $(document).ready(function () {

            //前期设计判断
            $("#btnSaveConfirm").click(function () {
                if ($("#txtDesignerContent").val() == "") {
                    alert("请输入设计说明");
                    return false;
                }
                if ($("#hideEmployeeID").val() == "") {
                    alert("请选择前期设计" + $("#hideEmployeeID").val());
                    return false;
                }
            });


            //执行设计判断
            $("#btnDispatchingConfirm").click(function () {
                if ($("#hideEmployeesID").val() == "-1") {
                    alert("请选择总调度人");
                    return false;
                }

                if ($("#txtPlanDate").val() == "") {
                    alert("请选择计划完成时间");
                    return false;
                }
            });
        });



        //打印  可进行预览
        function preview() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
        }

        function method2(tableid) //读取表格中每个单元到EXCEL中 
        {
            alert("信息q")
            var curTbl = document.getElementById(tableid);
            alert("信息5")
            var oXL = new ActiveXObject("Excel.Application");
            alert("信息4")
            //创建AX对象excel 
            var oWB = oXL.Workbooks.Add();
            alert("信息3")
            //获取workbook对象 
            var oSheet = oWB.ActiveSheet;
            alert("信息2")
            //激活当前sheet 
            var Lenr = curTbl.rows.length;
            alert("信息1")
            //取得表格行数 
            for (i = 0; i < Lenr; i++) {
                var Lenc = curTbl.rows(i).cells.length;
                //取得每行的列数 
                for (j = 0; j < Lenc; j++) {
                    oSheet.Cells(i + 1, j + 1).value = curTbl.rows(i).cells(j).innerText;
                    //赋值 
                }
            }
            oXL.Visible = true;
            //设置excel可见属性 
        }

        //查看图片
        function ShowFileShowPopu(DesignClassID, Type) {
            var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/DesignImageShowList.aspx?DesignClassID=" + DesignClassID + "&OrderID=<%=Request["OrderID"]%>&Type=" + Type;
            showPopuWindows(Url, 700, 800, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>

    <br />

    <!--显示执行设计-->
    <!--startprint-->
    <div runat="server" id="divPrint" class="divPrint">
        <ul>
            <li>执行设计:&nbsp;</li>
            <li>
                <asp:Label runat="server" ID="lblDesignEmployee" />&nbsp;&nbsp;</li>
            <li>计划完成时间:&nbsp;</li>
            <li>
                <asp:Label runat="server" ID="lblPlanDate" />&nbsp;&nbsp;</li>
            <li style="height: 30px;"></li>
        </ul>
        <table class="tablestyle" id="tblDesignList" border="1" style="width: 98%;">
            <tr>
                <th>产品名称</th>
                <th>下单时间</th>
                <th>说明</th>
                <th>采购数量</th>
                <th>材质</th>
                <th>采购单价</th>
                <th>单位</th>
                <th>参考图</th>
                <th>效果图</th>
                <th>供应商</th>
                <th>结算总价</th>
                <th <%=CostVisible() %>>实际到货数量</th>
                <th <%=CostVisible() %>>>操作</th>
            </tr>
            <asp:Repeater runat="server" ID="rptDesignList" OnItemCommand="rptDesignList_ItemCommand" OnItemDataBound="rptDesignList_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("Title") %>
                            <asp:HiddenField runat="server" ID="HideDesignID" Value='<%#Eval("DesignclassID") %>' />
                            <asp:HiddenField runat="server" ID="HideState" Value='<%#Eval("DesignState") %>' />
                        </td>
                        <td><%#Eval("CreateDate","{0:yyyy-MM-dd}") %></td>
                        <td><%#Eval("Node") %></td>
                        <td><%#Eval("PurchaseQuantity") %></td>
                        <td><%#Eval("MaterialName") %></td>
                        <td><%#Eval("PurchasePrice") %></td>
                        <td><%#Eval("Unit") %></td>
                        <td><a href="#" onclick="ShowFileShowPopu('<%#Eval("DesignclassID") %>',2)" class="btn btn-mini   btn-primary needshow">查看</a></td>
                        <td><a href="#" onclick="ShowFileShowPopu('<%#Eval("DesignclassID") %>',1)" class="btn btn-mini   btn-primary needshow">查看</a></td>
                        <td><%#Eval("SupplierName") %></td>
                        <td><%#Eval("TotalPrice") %></td>
                        <td <%=CostVisible() %>>
                            <asp:TextBox runat="server" ID="txtRealQuantity" Text='<%#Eval("RealQuantity") %>' ClientIDMode="Static" Style="width: 30px;" /></td>
                        <td <%=CostVisible() %>>
                            <asp:Button runat="server" ID="btn_Confirm" CommandName="Confirm" CommandArgument='<%#Eval("DesignclassID") %>' Text="确定" CssClass="btn btn-primary" /></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td>当前状态:</td>
                <td><%=GetState() %></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label runat="server" ID="lblTotalPriceSum" /></td>
                <td <%=CostVisible() %>></td>
                <td <%=CostVisible() %>>
                    <asp:Button runat="server" ID="btnSave" OnClick="btn_Save_Click" Text="统一保存" CssClass="btn btn-primary" /></td>
            </tr>
        </table>
    </div>
    <!--endprint-->

    <!--打印按钮-->
    <div class="div_handle">
        <asp:Button runat="server" ID="btnPrint2" CssClass="btn btn-primary" Text="打印" OnClientClick="return preview()" />
    </div>

    <!--添加执行设计-->
    <div runat="server" id="divAdd" class="div_Add">
        <table style="text-align: left;">
            <tr>
                <td colspan="12">
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
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlMaterial" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged" runat="server" Width="85px" AutoPostBack="true"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtPurchasePrice" runat="server" Width="20"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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

                <td>
                    <asp:DropDownList ID="ddlSupplierName" Width="80" runat="server"></asp:DropDownList>
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
                                <a href="#" onclick="ShowFileUploadPopu('<%#Eval("DesignclassID") %>')" class="btn btn-mini   btn-primary">上传</a>
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

        </table>

        <!--分派设计师-->
        <table style="text-align: left; width: 100%;" class="table table-bordered table-striped">
            <tr>
                <td runat="server" id="td_Dispatching">设计师：
                    <asp:HiddenField runat="server" ClientIDMode="Static" Value="-1" ID="hideEmployeesID" />
                    <asp:TextBox runat="server" ID="txtEmpLoyee" class="txtEmpLoyeeName" ClientIDMode="Static" onclick="ShowPopu(this);" />
                    <a href="#" onclick="ShowPopu(this);" class="SetState btn btn-primary" style="display: none;">选择</a>
                    计划完成时间：<asp:TextBox runat="server" ID="txtPlanDate" ClientIDMode="Static" onclick="WdatePicker();" Width="90px" />
                    <asp:LinkButton runat="server" ID="btnDispatchingConfirm" ClientIDMode="Static" CssClass="btn btn-primary" Text="确认下派" OnClick="btnDispatchingConfirm_Click" />&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>


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
