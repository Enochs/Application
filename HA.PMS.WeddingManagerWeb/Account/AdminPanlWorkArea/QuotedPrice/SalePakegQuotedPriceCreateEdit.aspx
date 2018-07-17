<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalePakegQuotedPriceCreateEdit.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.SalePakegQuotedPriceCreateEdit" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>


<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">
      function RealSubmit() {
          if (confirm("导入后将删除现有报价单！且无法恢复！")) {
              return true;
          } else {
              return false;
          }

      }
      function CheckPage() {
          if (parseFloat($("#txtRealAmount").val()) > 0) {

              return true;

          } else {
              alert("报价单销售价不能小于0");
              $("#txtRealAmount").focus();
              return false;

          }
      }

      $(document).ready(function () {
          $("a#inline").fancybox();
          if ($("#hidecheck").val() != "0") {
              $(".CheckNode").hide();
          }

          showPopuWindows($("#SelectPG").attr("href"), 500, 300, "a#SelectPG");
          $(".NeedHideLable").hide();


          $(".btnSubmit").click(function () {


          });

      });

      $(document).ready(function () {
          var SelectItem = $("#ddlType").find("option:selected").text()
          if (SelectItem == "新增") {
              $(".NoAdd").hide();
          }

          $("#ddlType").change(function () {
              var SelectItem = $(this).find("option:selected").text()
              if (SelectItem == "新增") {
                  $(".NoAdd").hide();
              } else {
                  $(".NoAdd").show();
              }
          }
          )

          document.bgColor = "#ff0000";


          $(".SaleItem").change(function () {
              eachAmount();
          });

          $(".SelectSG").each(function () {
              showPopuWindows($(this).attr("href"), 640, 200, $(this));
              $("#hideSecondCategoryID").attr("value", $(this).attr("CategoryID"));
          });

          $(".SlectFour").each(function () {
              showPopuWindows($(this).attr("href"), 400, 300, $(this));
          });




          //单价改变时
          $(".Quantity").change(function () {

              var PriceIndex = ".SetSubtotal" + $(this).attr("ProductID");
              var ParetnTotal = "#txtTotal" + $(this).attr("ParentCategoryID");
              var TotalNum = 0;
              var OldTexValue = $(ParetnTotal).val();
              var OLdValue = 0;
              if (OldTexValue != "") {
                  OLdValue = parseFloat(OldTexValue);
              }

              //alert(PriceIndex);
              var NewValue = 0;

              var Quantity = parseFloat($(PriceIndex).find(".Quantity").val());
              var SalePrice = parseFloat($(PriceIndex).find(".SalePrice").val());

              var Subtotal = Quantity * SalePrice;

              //var OldTotal = parseFloat($("#txtAggregateAmount").attr("value"));
              if ($(PriceIndex).find(".Subtotal").val() == "") {
                  OLdValue = Subtotal + OLdValue;
                  //OldTotal = Subtotal + OLdValue;
              } else {
                  OLdValue = OLdValue - parseFloat($(PriceIndex).find(".Subtotal").val());
                  OLdValue = OLdValue + Subtotal;
              }

              $(PriceIndex).find(".Subtotal").attr("value", Subtotal);
              $(ParetnTotal).attr("value", OLdValue);
              SetTotalAmount();
              //$(".First" + $(this).attr("ParentCategoryID")).find(".Subtotal").each(function () {
              //    TotalNum += parseFloat($(this).val());
              //});

          });


          //单价改变时
          $(".SalePrice").change(function () {

              var PriceIndex = ".SetSubtotal" + $(this).attr("ProductID");
              var ParetnTotal = "#txtTotal" + $(this).attr("ParentCategoryID");
              var TotalNum = 0;
              var OldTexValue = $(ParetnTotal).val();
              var OLdValue = 0;
              if (OldTexValue != "") {
                  OLdValue = parseFloat(OldTexValue);
              }

              var NewValue = 0;

              var Quantity = parseFloat($(PriceIndex).find(".Quantity").val());
              var SalePrice = parseFloat($(PriceIndex).find(".SalePrice").val());

              var Subtotal = Quantity * SalePrice;

              //var OldTotal = parseFloat($("#txtAggregateAmount").attr("value"));
              if ($(PriceIndex).find(".Subtotal").val() == "") {
                  OLdValue = Subtotal + OLdValue;
                  //OldTotal = Subtotal + OLdValue;
              } else {
                  OLdValue = OLdValue - parseFloat($(PriceIndex).find(".Subtotal").val());
                  OLdValue = OLdValue + Subtotal;
              }

              $(PriceIndex).find(".Subtotal").attr("value", Subtotal);
              $(ParetnTotal).attr("value", OLdValue);
              SetTotalAmount();
              //$(".First" + $(this).attr("ParentCategoryID")).find(".Subtotal").each(function () {
              //    TotalNum += parseFloat($(this).val());
              //});

          });

      });


      //计算销售总价
      function SetTotalAmount() {
          var TotalAmount = 0;
          $(".Subtotal").each(function () {
              if ($(this).val() != "") {
                  TotalAmount = TotalAmount + parseFloat($(this).val());
              }
          });
          $("#txtAggregateAmount").attr("value", TotalAmount);
          $("#hideAggregateAmount").attr("value", TotalAmount);
      }

      //循环销售价
      function SetRealAmount() {

      }

      //
      function eachAmount() {
          var TotalAmount = 0;
          $(".SaleItem").each(function () {
              if ($(this).val() != "") {
                  TotalAmount = TotalAmount + parseFloat($(this).val());
              }
              $("#txtRealAmount").attr("value", TotalAmount);
          });
      }


      function CheckDelete() {
          if (confirm("确认删除！删除后无法恢复")) {
              return true;
          } else {
              return false;
          }
      }

      function SetfunCategoryID(Control) {
          $("#hideSecondCategoryID").attr("value", $(Control).attr("categoryid"));

          return false;
      }


      function SetProduct(Control) {
          $("#hideThirdCategoryID").attr("value", $(Control).attr("categoryid"));
          return false;
      }


      ///计算小计
      function GetAvgSum(Control) {
          $(".SetSubtotal").find();
          $(".SetSubtotal").find();
          $(".SetSubtotal").find();
      }



      //上传图片
      function ShowFileUploadPopu(QuotedID, Kind) {
          var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPriceImageUpload.aspx?QuotedID=" + QuotedID + "&Kind=" + Kind;
          showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
          $("#SelectEmpLoyeeBythis").click();
      }

     //查看图片
      function ShowFileShowPopu(QuotedID, ChangeID) {
          var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPriceItemFileList.aspx?QuotedID=" + QuotedID + "&ChangeID=" + ChangeID;
          showPopuWindows(Url, 700, 800, "#SelectEmpLoyeeBythis");
          $("#SelectEmpLoyeeBythis").click();
      }


      function ShowPrint() {
          window.open('QuotedPriceShow.aspx?QuotedID=<%=GetQuotedID()%>');
        }

        function BeginPrint() {
            window.open('QuotedPriceShowOrPrint.aspx?QuotedID=<%=GetQuotedID()%>');
            return false;
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">

    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>

    <table border="0" style="width: 1325px; margin-left: 60px; margin-right: 60px;">

        <tr>
            <td align="left">报价单名称：<asp:TextBox ID="txtQuotedTitle" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                         <a id="SelectPG" class="SelectPG btn btn-warning" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=0&ControlKey=hidePgValue&Callback=btnStarFirstpg"><b style="color: black;">选择类别</b></a>
                <div style="display: none;">
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hidePgValue" />
                    <asp:Button ID="btnStarFirstpg" ClientIDMode="Static" runat="server" Text="生成一级分类" OnClick="btnStarFirstpg_Click" />
                    <asp:Button ID="btnCreateSecond" ClientIDMode="Static" runat="server" Text="生成二级分类" OnClick="btnCreateSecond_Click" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideSecondValue" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideSecondCategoryID" />
                    <asp:Button ID="btnCreateThired" ClientIDMode="Static" runat="server" Text="生成产品" OnClick="btnCreateThired_Click" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThirdValue" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThirdCategoryID" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideFoureGingang" />
                    <asp:HiddenField runat="server" ID="hideIschek" />
                </div>
 
                <asp:Label ID="Label1" runat="server" BackColor="#FFCC99" Width="35" Height="25"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="类别"></asp:Label>
             
                <asp:Label ID="Label3" runat="server" BackColor="#CCFFCC" Width="35" Height="25"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="项目"></asp:Label>

 
                <asp:Label ID="Label6" runat="server" Text="其余为产品"></asp:Label>
        </tr>
    </table>
    <br />
    <div style="overflow-x: auto; overflow-y: hidden; margin-left: 60px; text-align: center;">
        <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound" OnItemCommand="repfirst_ItemCommand">
            <ItemTemplate>
                <asp:HiddenField ID="hidefirstCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                <asp:HiddenField ID="hideKey" Value='<%#Eval("ChangeID") %>' runat="server" />
                <table class="First<%#Eval("CategoryID") %>" border="1" style="border-color: gray;" cellspacing="20">
                    <tr>
                        <td width="120" style="white-space: nowrap;"><b>类别</b></td>
                        <td width="150" style="white-space: nowrap;"><b>项目</b></td>
                        <td width="140" style="white-space: nowrap;"><b>产品/服务内容</b></td>
                        <td width="380" style="white-space: nowrap;"><b>具体要求</b></td>
                        <td width="100" style="white-space: nowrap;"><b>图片</b></td>
                        <td width="75" style="white-space: nowrap;"><b>单价</b></td>
                        <td width="80" style="white-space: nowrap;"><b>单位</b></td>
                        <td width="80" style="white-space: nowrap;"><b>数量</b></td>
                        <td width="80" style="white-space: nowrap;"><b>小计</b></td>
                        <td width="150" style="white-space: nowrap; display: none;"><b>说明</b></td>
                        <td width="80" style="white-space: nowrap;"><b>操作</b></td>
                    </tr>
                    <asp:Repeater ID="repdatalist" runat="server" OnItemCommand="repdatalist_ItemCommand">
                      <ItemTemplate>
                            <tr <%#Eval("ItemLevel").ToString()=="1"?"style=background-color:#FFCC99;":""%> <%#Eval("ItemLevel").ToString()=="2"?"style=background-color:#CCFFCC;":""%>>
                                <td <%#Container.ItemIndex>0?"class='NeedHide'  style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'" : "style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:'nowrap;'"%> <%#Eval("ParentCategoryID")%>>
                                    <b <%#Container.ItemIndex>0?"class='NeedHideLable'": ""%>><%#Eval("ParentCategoryName") %></b><br />
                                    <label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;<a id="SelectSG" <%#HideSelectItem(Container.ItemIndex) %> categoryid="<%#Eval("CategoryID") %>" class="SelectSG btn btn-succes btn-mini" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=<%#Eval("ItemLevel").ToString()=="1"?Eval("CategoryID"):Eval("ParentCategoryID") %>&ControlKey=hideSecondValue&Callback=btnCreateSecond" onclick="return SetfunCategoryID(this);">增加项目</a>
                                    </label>
                                    <asp:HiddenField ID="hidePriceKey" Value='<%#Eval("ChangeID") %>' runat="server" />
                                    <asp:HiddenField ID="hideCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                                </td>
                                <td <%#Eval("ItemLevel").ToString()=="3"?"style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'": "style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:nowrap;'" %>>
                                    <b <%#HideSelectProduct(Eval("ItemLevel")) %>><%#Eval("CategoryName") %></b><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<a id="A2" <%#HideSelectProduct(Eval("ItemLevel")) %> categoryid="<%#Eval("CategoryID") %>" class="SelectSG btn btn-primary btn-mini" href="/AdminPanlWorkArea/ControlPage/SelectProduct.aspx?CategoryID=<%#Eval("CategoryID") %>&ControlKey=hideThirdValue&Callback=btnCreateThired&PartyDate=<%#Request["PartyDate"] %>&CustomerID=<%=Request["CustomerID"] %>" onclick="SetProduct(this);">增加产品</a></td>
                                <td >
                                    <%#Eval("ServiceContent")%>
                                    <div style="display: none;">
                                        <asp:TextBox ID="txtProductName" Enabled="false" runat="server" Text='<%#Eval("ServiceContent") %>' Width="120"></asp:TextBox>
                                    </div>
                                </td>
                                <td style="text-align:left;">
                                    <asp:TextBox ID="txtRequirement" runat="server" Width="95%" Text='<%#Eval("Requirement") %>' MaxLength="50"  TextMode="MultiLine" Rows="2"></asp:TextBox></td>
                                <td>
                                    <%--<a href="#"  style="color:red;"  onclick="ShowFileUploadPopu('<%#Eval("QuotedID") %>','<%#Eval("ChangeID") %>')">上传图片</a>
                                    <a id="inline"   href="#data<%#Eval("ChangeID") %>" kesrc="#data<%#Eval("ChangeID")%>">查看</a>--%>

                                    <a href="#" onclick="ShowFileUploadPopu('<%#Eval("QuotedID") %>','<%#Eval("ChangeID") %>')" class="btn btn-mini   btn-primary">上传</a>
                                    <a href='#' onclick="ShowFileShowPopu('<%#Eval("QuotedID") %>','<%#Eval("ChangeID") %>')" class="btn btn-mini  btn-primary needshow">查看</a>
                                    

                                    <div style="display: none;">
                                        <div id='data<%#Eval("ChangeID") %>'>
                                             <%#GetKindImage(Eval("ChangeID")) %>
                                        </div>
                                    </div>
                                </td>
                                <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>">
                                  .  <asp:TextBox ID="txtSalePrice" CssClass="SalePrice" runat="server" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("UnitPrice") %>' Width="75"></asp:TextBox></td>
                                <td>
                                    <%#Eval("Unit") %></td>
                                <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW"%>">
                                    <asp:TextBox ID="txtQuantity" CssClass="Quantity" runat="server" Width="30" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("Quantity") %>'></asp:TextBox></td>
                                <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %> Total<%#Eval("ParentCategoryID") %>">
                                    <asp:TextBox ID="txtSubtotal" class='Subtotal' runat="server" Width="75" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW"' Text='<%#Eval("Subtotal") %>'></asp:TextBox></td>
                                <td style="display: none;">
                                    <asp:TextBox ID="txtRemark" runat="server" Width="140" Text='<%#Eval("Remark") %>' MaxLength="50"></asp:TextBox></td>
                                <td>
                                    <asp:LinkButton ID="lnkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("ChangeID") %>' runat="server" OnClientClick="return CheckDelete();" CssClass="btn btn-danger btn-mini">删除</asp:LinkButton></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div style="margin-right: 135px; text-align: right;">
                    <b>分项合计:</b>
                    <input id='txtTotal<%#Eval("CategoryID") %>' style="width: 75px;" disabled="disabled" type="text" class="ItemAmount" value="<%#Eval("ItemAmount")==null?"0":Eval("ItemAmount")%>" />

                    <div style="display: none;">销售价:<asp:TextBox ID="txtSaleItem" CssClass="SaleItem" Width="75" runat="server" Text='<%#Eval("ItemSaleAmount") %>'></asp:TextBox></div>
                    <asp:Button ID="btnSaveItem" CommandName="SaveItem" runat="server" Text="保存分项" CssClass="btn btn-success" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <br />
    <table style="margin-left: 60px;">
        <tr>
            <td>报价单总价</td>
            <td>

                <asp:TextBox ID="txtAggregateAmount" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                <asp:HiddenField ID="hideAggregateAmount" ClientIDMode="Static" runat="server" />
            </td>
            <td>销售销售总价</td>
            <td>
                <asp:TextBox ID="txtRealAmount" runat="server" ClientIDMode="Static" MaxLength="8"></asp:TextBox>
            </td>
            <td>预付款</td>
            <td>
                <asp:TextBox ID="txtEarnestMoney" runat="server" ClientIDMode="Static" MaxLength="8"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>说明</td>
            <td colspan="5">
                <asp:TextBox ID="txtRemark" runat="server" Rows="3" TextMode="MultiLine" Width="75%" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Button ID="btnSaveChange" runat="server" Text="保存" OnClick="btnSaveChange_Click" CssClass="btn btn-primary" OnClientClick="return CheckPage();" />
                <input id="btnShow" type="button" value="预览" onclick="ShowPrint();" class="btn btn-success" />
                <asp:Button ID="btnPrint" runat="server" Text="打印" CssClass="btn btn-primary" OnClientClick="return BeginPrint();" Visible="false" />

            </td>
        </tr>
    </table>
    <br />
</asp:Content>
