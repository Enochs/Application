<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskTab.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskTab" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/FL_MissionMananger.aspx";
        $(document).ready(function () {


              //if(TabType=1)
              //{
                $("#tab1").click();
              //    $(".HAtab").removeClass("active");
              //    $("#tab1").addClass("active");
              //}

              //if(TabType=2)
              //{
              //    $("#tab2").click();
              //    $(".HAtab").removeClass("active");
              //    $("#tab2").addClass("active");
              //}


              //if(TabType=3)
              //{
              //    $("#tab3").click();
              //    $(".HAtab").removeClass("active");
              //    $("#tab3").addClass("active");
              //}
          });

          function Settabs(ControlID, Index, Uri) {
              URI = "/AdminPanlWorkArea/Carrytask/" + Uri + ".aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&NeedPopu=1";
              $(".HAtab").removeClass("active");
              $(ControlID).addClass("active");
              $("#Iframe1").attr("src", URI);
          }
    </script>
    <%--src="/AdminPanlWorkArea/Carrytask/<%=BinderPage() %>.aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&NeedPopu=1"--%>
    <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs">
                    <li class="HAtab" id="tab1" style="background-color: #2E363F;<%=ReadOnly()%>" id="DefaultTab" onclick="Settabs(this,1,'MyCarrytaskList');"><a data-toggle="tab" href="#111">处理任务</a></li>
                    <li class="HAtab" id="tab2" onclick="Settabs(this,2,'ProductList');"><a data-toggle="tab" href="#111">物料表</a></li>
                    <li class="HAtab" id="tab3" onclick="Settabs(this,3,'CarrytaskProfessionalteam');" <%=HideCreate()%>><a data-toggle="tab" href="#111">专业团队</a></li>
                    <li class="HAtab" id="tab4" onclick="Settabs(this,4,'DispatchingEmpLoyeeManager');" <%=HideCreate()%>><a data-toggle="tab" href="#111">项目人员统筹</a></li>
                </ul>
            </div>
            <div class="widget-content tab-content" style="height:800px; overflow:scroll">
             
                <div class="tab-pane active" id="tab1" >
                    <iframe class="framchild " id="Iframe1" width="100%" style="min-height:900px" frameborder="0" name="table">
                        
                    </iframe>

           
                   
                </div>
            </div>
        </div>
    </div>
</asp:Content> 