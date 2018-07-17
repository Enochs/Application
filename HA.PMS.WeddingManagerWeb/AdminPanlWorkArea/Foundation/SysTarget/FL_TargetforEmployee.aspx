<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FL_TargetforEmployee.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.SysTarget.FL_TargetforEmployee" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(":text").each(function (indexs, values) {
                $(this).addClass("centerTxt");
            });
            $("select").addClass("centerSelect");

            if ($("#hideIsManager").val() != "True") {
                var NowMonth = new Date().getMonth() + 1;

                for (i = 0; i < NowMonth; i++) {
                    
                    $(".Month" + i).attr("disabled", "disabled");
                    
                }
 
 
            }
        });

        $(window).load(function () {
            BindCtrlRegex();
            //BindCtrlEvent('input[check],textarea[check]');
        });
        function BindCtrlRegex() {
            $('input[check]').attr('reg', '^(-)?(([1-9]{1}\\d{0,15})|([0]{1}))(\\.(\\d){1,2})?$');
        }
        function CheckByClass()
        {
            return ValidateForm('input[check],textarea[check]');
        }
    </script>
    <style type="text/css">
        .centerTxt {
            width: 40px;
            height: 20px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }

        #ddlTarget {
            margin-top: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hideIsManager" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hideCreateEmployee" runat="server" ClientIDMode="Static" />
    <div style="overflow-y: auto;">
        <asp:Repeater ID="repList" runat="server">
            <HeaderTemplate>
                <span style="color: white; background-color: gray; width: 100%;">部门指标：</span>
                <table class="table table-bordered table-striped">
                    <thead>
                        <tr id="trContent">
                            <th>目标</th>
                            <th></th>
                            <th>1</th>
                            <th>2</th>
                            <th>3</th>
                            <th>4</th>
                            <th>5</th>
                            <th>6</th>
                            <th>7</th>
                            <th>8</th>
                            <th>9</th>
                            <th>10</th>
                            <th>11</th>
                            <th>12</th>
                            <th>当年合计</th>
                            <th>上年合计</th>
                            <th>历史累计</th>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="trContent">
                    <td><%#Eval("TargetTitle") %></td>
                    <td>计划</td>
                    <td><%#Eval("MonthPlan1") %></td>
                    <td><%#Eval("MonthPlan2") %></td>
                    <td><%#Eval("MonthPlan3") %></td>
                    <td><%#Eval("MonthPlan4") %></td>
                    <td><%#Eval("MonthPlan5") %></td>
                    <td><%#Eval("MonthPlan6") %></td>
                    <td><%#Eval("MonthPlan7") %></td>
                    <td><%#Eval("MonthPlan8") %></td>
                    <td><%#Eval("MonthPlan9") %></td>
                    <td><%#Eval("MonthPlan10") %></td>
                    <td><%#Eval("MonthPlan11") %></td>
                    <td><%#Eval("MonthPlan12") %></td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                </tr>
                <tr id="tr1">
                    <td style="border-bottom-style: none; border-top-style: none"></td>
                    <td>实际完成</td>
                    <td><%#Eval("MonthFinsh1") %></td>
                    <td><%#Eval("MonthFinish2") %></td>
                    <td><%#Eval("MonthFinish3") %></td>
                    <td><%#Eval("MonthFinish4") %></td>
                    <td><%#Eval("MonthFinish5") %></td>
                    <td><%#Eval("MonthFinish6") %></td>
                    <td><%#Eval("MonthFinish7") %></td>
                    <td><%#Eval("MonthFinish8") %></td>
                    <td><%#Eval("MonthFinish9") %></td>
                    <td><%#Eval("MonthFinish10") %></td>
                    <td><%#Eval("MonthFinish11") %></td>
                    <td><%#Eval("MonthFinish12") %></td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>

                </tr>
                <tr id="tr2">
                    <td style="border-bottom-style: none; border-top-style: none"></td>
                    <td>完成率</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </thead>
            </table>
            </FooterTemplate>
        </asp:Repeater>
        <span style="color: white; background-color: gray; width: 100%;">部门员工指标：</span>


        <table class="table table-bordered table-striped">
            <thead>
                <tr>
                    <th>员工姓名</th>

                    <th>指标</th>
                    <th colspan="12">月份</th>

                </tr>
                <tr>
                    <th style="white-space: nowrap;">&nbsp;</th>
                    <th>&nbsp;</th>
                    <th>一</th>
                    <th>二</th>
                    <th>三</th>
                    <th>四</th>
                    <th>五</th>
                    <th>六</th>
                    <th>七</th>
                    <th>八</th>
                    <th>九</th>
                    <th>十</th>
                    <th>十一</th>
                    <th>十二</th>
                </tr>
            </thead>
            <asp:Repeater ID="repEmployeeTargetList" runat="server">
                <ItemTemplate>
                    <tr <%#Eval("IsActive").ToString()=="True"?"style='color:red;'":"" %> class="TargetSpan" CreateEmployee="<%#Eval("CreateEmployeeID") %>">
                        <td><%#GetEmployeeNameByID(Eval("EmployeeID")) %>
                            <asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("FinishKey") %>' />
                            <asp:HiddenField ID="TargetKey" runat="server" Value='<%#Eval("TargetID") %>' />
                            <asp:HiddenField ID="EmployeeKey" runat="server" Value='<%#Eval("EmployeeID") %>' />

                        </td>
                        <td><%#Eval("TargetTitle") %></td>
                        <td>
                            <asp:TextBox MaxLength="10" check="0" ID="txtMonth1" runat="server" Text='<%#Eval("MonthPlan1") %>' CssClass="Month1"></asp:TextBox></td>
                        <td >
                            <asp:TextBox MaxLength="10" check="0" ID="txtMonth2" runat="server" Text='<%#Eval("MonthPlan2") %>' CssClass="Month2"></asp:TextBox></td>
                        <td >
                            <asp:TextBox MaxLength="10" check="0" ID="txtMonth3" runat="server" Text='<%#Eval("MonthPlan3") %>' CssClass="Month3"></asp:TextBox></td>
                        <td>
                            <asp:TextBox MaxLength="10" check="0" ID="txtMonth4" runat="server" Text='<%#Eval("MonthPlan4") %>' CssClass="Month4"></asp:TextBox></td>
                        <td>
                            <asp:TextBox MaxLength="10" check="0" ID="txtMonth5" runat="server" Text='<%#Eval("MonthPlan5") %>' CssClass="Month5"></asp:TextBox></td>
                        <td>
                            <asp:TextBox MaxLength="10" check="0" ID="txtMonth6" runat="server" Text='<%#Eval("MonthPlan6") %>' CssClass="Month6"></asp:TextBox></td>
                        <td>
                            <asp:TextBox MaxLength="10" check="0" ID="txtMonth7" runat="server" Text='<%#Eval("MonthPlan7") %>' CssClass="Month7"></asp:TextBox></td>
                        <td>
                            <asp:TextBox MaxLength="10" check="0" ID="txtMonth8" runat="server" Text='<%#Eval("MonthPlan8") %>' CssClass="Month8"></asp:TextBox></td>
                        <td>
                            <asp:TextBox MaxLength="10" check="0" ID="txtMonth9" runat="server" Text='<%#Eval("MonthPlan9") %>' CssClass="Month9"></asp:TextBox></td>
                        <td>
                            <asp:TextBox MaxLength="10" check="0" ID="txtMonth10" runat="server" Text='<%#Eval("MonthPlan10") %>' CssClass="Month10"></asp:TextBox></td>
                        <td >
                            <asp:TextBox MaxLength="10" check="0" ID="txtMonth11" runat="server" Text='<%#Eval("MonthPlan11") %>' CssClass="Month11"></asp:TextBox></td>
                        <td >
                            <asp:TextBox MaxLength="10" check="0" ID="txtMonth12" runat="server" Text='<%#Eval("MonthPlan12") %>' CssClass="Month12"></asp:TextBox></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

            <tr>
                <td colspan="13">
                    <asp:Button ID="btnSaveChange" runat="server" Text="保存" CssClass="btn btn-success" OnClick="btnSaveChange_Click" />
                    <asp:Button ID="btnLock" runat="server" Text="全部锁定" CssClass="btn btn-success" OnClick="btnLock_Click" />
                    <asp:Button ID="btnUnLock" runat="server" Text="全部解除" CssClass="btn btn-success" OnClick="btnUnLock_Click" />

                </td>
            </tr>
        </table>



        <asp:Repeater ID="repMine" runat="server">
            <HeaderTemplate>
                <span style="color: white; background-color: gray; width: 100%;">个人指标：</span>
                <table class="table table-bordered table-striped">
                    <tr>
                        <th style="white-space: nowrap;">员工姓名</th>

                        <th>指标</th>
                        <th colspan="12">月份</th>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>一月</td>
                        <td>二月</td>
                        <td>三月</td>
                        <td>四月</td>
                        <td>五月</td>
                        <td>六月</td>
                        <td>七月</td>
                        <td>八月</td>
                        <td>九月</td>
                        <td>十月</td>
                        <td>十一月</td>
                        <td>十二月</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="TargetSpan" CreateEmployee="<%#Eval("CreateEmployeeID") %>">
                    <td><%#GetEmployeeNameByID(Eval("EmployeeID")) %>
                        <asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("FinishKey") %>' />
                        <asp:HiddenField ID="TargetKey" runat="server" Value='<%#Eval("TargetID") %>' />
                        <asp:HiddenField ID="EmployeeKey" runat="server" Value='<%#Eval("EmployeeID") %>' />
                    </td>
                    <td style="white-space: normal;"><%#Eval("TargetTitle") %></td>
                    <td>
                        <asp:TextBox MaxLength="10" check="0" ID="txtMonth1" runat="server" Width="70" Text='<%#Eval("MonthPlan1") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox MaxLength="10" check="0" ID="txtMonth2" runat="server" Width="70" Text='<%#Eval("MonthPlan2") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox MaxLength="10" check="0" ID="txtMonth3" runat="server" Width="70" Text='<%#Eval("MonthPlan3") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox MaxLength="10" check="0" ID="txtMonth4" runat="server" Width="70" Text='<%#Eval("MonthPlan4") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox MaxLength="10" check="0" ID="txtMonth5" runat="server" Width="70" Text='<%#Eval("MonthPlan5") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox MaxLength="10" check="0" ID="txtMonth6" runat="server" Width="70" Text='<%#Eval("MonthPlan6") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox MaxLength="10" check="0" ID="txtMonth7" runat="server" Width="70" Text='<%#Eval("MonthPlan7") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox MaxLength="10" check="0" ID="txtMonth8" runat="server" Width="70" Text='<%#Eval("MonthPlan8") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox MaxLength="10" check="0" ID="txtMonth9" runat="server" Width="70" Text='<%#Eval("MonthPlan9") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox MaxLength="10" check="0" ID="txtMonth10" runat="server" Width="70" Text='<%#Eval("MonthPlan10") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox MaxLength="10" check="0" ID="txtMonth11" runat="server" Width="70" Text='<%#Eval("MonthPlan11") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox MaxLength="10" check="0" ID="txtMonth12" runat="server" Width="70" Text='<%#Eval("MonthPlan12") %>'></asp:TextBox></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr>
                    <td colspan="13">
                        <asp:Button ID="btnSaveMine" runat="server" Text="保存" OnClick="btnSaveMine_Click" CssClass="btn btn-success" />
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>

    </div>
</asp:Content>
