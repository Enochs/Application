<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" StylesheetTheme="Default" Inherits="HA.PMS.WeddingManagerWeb.Test.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />


    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>

    <script>
        $(function () {
            //../AdminPanlWorkArea/Foundation/FD_PPTWarehouse/FD_PPTWareHouseCreate.aspx
            $('#tabs').tabs();
                
               
    
        });
        function addSrc(objNode,src) {
            if ($(objNode).attr("src") == "") {
               
                $(objNode).attr("src", src + "?r=" + Math.random());
                alert( $(objNode).attr("src"));
            }
            
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
     <table width="1100" border="0" cellspacing="0" cellpadding="0">
            <thead>
                <tr>
                  <th width="100">1</th>
                  <th width="100">2</th>
                  <th width="100">3</th>
                  <th width="100">4</th>
                  <th width="100">5</th>
                  <th width="100">6</th>
                  <th width="100">7</th>
                  <th width="100">8</th>
                  <th width="100">9</th>
                  <th width="100">10</th>
                  <th width="100">11</th>
                </tr>
              </thead>
              <tr>
                <td width="100" align="center">1</td>
                <td width="100" align="center">2</td>
                <td width="100" align="center">3</td>
                <td width="100" align="center">4</td>
                <td width="100" align="center">5</td>
                <td width="100" align="center">6</td>
                <td width="100" align="center">7</td>
                <td width="100" align="center">8</td>
                <td width="100" align="center">9</td>
                <td width="100" align="center">10</td>
                <td width="100" align="center">11</td>
              </tr>
              <tr>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
              </tr>
              <tr>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
              </tr>
              <tr>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
                <td width="100" align="center">&nbsp;</td>
              </tr>
              <tr>
                <td width="100" align="center">1</td>
                <td width="1000" colspan="10" align="center"><table width="1000" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="100" align="center">2</td>
                    <td width="100" align="center">3</td>
                    <td width="100" align="center">4</td>
                    <td width="100" align="center">5</td>
                    <td width="100" align="center">6</td>
                    <td width="100" align="center">7</td>
                    <td width="100" align="center">8</td>
                    <td width="100" align="center">9</td>
                    <td width="100" align="center">10</td>
                    <td width="100">11</td>
                  </tr>
                  <tr>
                    <td width="100" align="center">&nbsp;</td>
                    <td width="100" align="center">&nbsp;</td>
                    <td width="100" align="center">&nbsp;</td>
                    <td width="100" align="center">&nbsp;</td>
                    <td width="100" align="center">&nbsp;</td>
                    <td width="100" align="center">&nbsp;</td>
                    <td width="100" align="center">&nbsp;</td>
                    <td width="100" align="center">&nbsp;</td>
                    <td width="100" align="center">&nbsp;</td>
                    <td width="100">&nbsp;</td>
                  </tr>
                </table></td>
              </tr>
            </table>

            <%--<div  id="tabs" class="widget-box">
                    <div  class="widget-title">
                        <ul  class="nav nav-tabs">
                            <li class="active"><a data-toggle="tab" href="#tab1">新消息</a></li>
                            <li><a data-toggle="tab" href="#tab2">新人信息录入</a></li>
                            <li><a data-toggle="tab" href="#tab3">添加新渠道</a></li>
                            <li><a data-toggle="tab" href="#tab4">渠道返利统计</a></li>
                            <li><a data-toggle="tab" href="#tab5">渠道汇总统计</a></li>

                        </ul>
                    </div>
                    <div class="widget-content tab-content">
                        <div id="tab1" class="tab-pane active">
                            <div class="alert">
                                <button class="close" data-dismiss="alert">×</button>
                                <strong>您今日共有 7 项任务!</strong> 请安排时间处理。
                            </div>
                            <div class="widget-box">
                                <div class="widget-content">


                                    <div class="btn-group">
                                        <button class="btn btn-primary">全部渠道类型</button>
                                        <button data-toggle="dropdown" class="btn btn-primary dropdown-toggle"><span class="caret"></span></button>
                                        <ul class="dropdown-menu">
                                            <li><a href="#">全部渠道类型</a></li>
                                            <li><a href="#">全部渠道类型</a></li>
                                            <li><a href="#">全部渠道类型</a></li>
                                            <li class="divider"></li>
                                            <li><a href="#">全部渠道类型</a></li>
                                        </ul>
                                    </div>
                                    <div class="btn-group">
                                        <button class="btn btn-danger">全部渠道名称</button>
                                        <button data-toggle="dropdown" class="btn btn-danger dropdown-toggle"><span class="caret"></span></button>
                                        <ul class="dropdown-menu">
                                            <li><a href="#">全部渠道名称</a></li>
                                            <li><a href="#">全部渠道名称</a></li>
                                            <li><a href="#">全部渠道名称</a></li>
                                            <li class="divider"></li>
                                            <li><a href="#">全部渠道名称</a></li>
                                        </ul>
                                    </div>
                                    <div class="btn-group">
                                        <button class="btn btn-warning">所有收款人</button>
                                        <button data-toggle="dropdown" class="btn btn-warning dropdown-toggle"><span class="caret"></span></button>
                                        <ul class="dropdown-menu">
                                            <li><a href="#">所有收款人</a></li>
                                            <li><a href="#">所有收款人</a></li>
                                            <li><a href="#">所有收款人</a></li>
                                            <li class="divider"></li>
                                            <li><a href="#">所有收款人</a></li>
                                        </ul>
                                    </div>
                                    <div class="btn-group">
                                        <button class="btn btn-success">支付时间</button>
                                        <button data-toggle="dropdown" class="btn btn-success dropdown-toggle"><span class="caret"></span></button>
                                        <ul class="dropdown-menu">
                                            <li><a href="#">支付时间</a></li>
                                            <li><a href="#">支付时间</a></li>
                                            <li><a href="#">支付时间</a></li>
                                            <li class="divider"></li>
                                            <li><a href="#">支付时间</a></li>
                                        </ul>
                                    </div>

                                    <div class="btn-group">
                                        <button class="btn">查看统计结果</button>
                                    </div>
                                </div>
                            </div>


                            <div class="widget-box">
                                <div class="widget-title">
                                    <span class="icon"><i class="icon-th"></i></span>
                                    <h5>x年x月渠道返利表</h5>
                                </div>
                                <div class="widget-content nopadding">
                                    <table class="table table-bordered table-striped">
                                        <thead>
                                            <tr>
                                                <th>ID</th>
                                                <th>渠道名称</th>
                                                <th>类型</th>
                                                <th>支付日期</th>
                                                <th>金额</th>
                                                <th>收款人</th>
                                                <th>收款人电话</th>
                                                <th>经办人</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr class="odd gradeX">
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td class="center">&nbsp;</td>
                                                <td class="center">&nbsp;</td>
                                            </tr>
                                            <tr class="even gradeC">
                                                <td>&nbsp;</td>
                                                <td>天籁酒店</td>
                                                <td>酒店</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td class="center">&nbsp;</td>
                                                <td class="center">&nbsp;</td>
                                            </tr>
                                            <tr class="odd gradeA">
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td class="center">&nbsp;</td>
                                                <td class="center">&nbsp;</td>
                                            </tr>
                                            <tr class="even gradeA">
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td class="center">&nbsp;</td>
                                                <td class="center">&nbsp;</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>


                            <div class="pagination">
                                <ul>
                                    <li><a href="#">上一页</a></li>
                                    <li class="active"><a href="#">1</a> </li>
                                    <li><a href="#">2</a></li>
                                    <li><a href="#">3</a></li>
                                    <li><a href="#">4</a></li>
                                    <li><a href="#">下一页</a></li>
                                </ul>
                            </div>



                        </div>

                        <div id="tab2" class="tab-pane">
                           



                        </div>

                        <div id="tab3" class="tab-pane">
                            <p>。。。。. </p>
                        </div>

                        <div id="tab4" class="tab-pane">
                            <p>。。。。. </p>
                        </div>

                        <div id="tab5" class="tab-pane">
                            <p>。。。。. </p>
                        </div>

                    </div>
                </div>--%>



            <div id="tabs">
                <ul>
                    <li><a id="travelExpenseTab"   href="#travelExpenseDiv">A 选项</a></li>
                    <li><a id="noTravelExpenseTab" onclick="$('#noTravelExpenseFrame').attr('src','../AdminPanlWorkArea/Foundation/FD_PPTWarehouse/FD_PPTWareHouseCreate.aspx')" href="#noTravelExpenseDiv">B 选项</a></li>
 
                </ul>
                <div id="travelExpenseDiv">
                    <iframe id="travelExpenseFrame"  src="../AdminPanlWorkArea/Foundation/FD_PPTWarehouse/FD_PPTWareHouseManager.aspx" width="100%"    frameborder="0" marginheight="0" marginwidth="0">ssssssssssss</iframe>
                      
                    
                </div>
               

                <div id="noTravelExpenseDiv">
                    <iframe id="noTravelExpenseFrame"  src="" width="100%" frameborder="0" marginheight="0" marginwidth="0">ffffffffff</iframe>
                      
                       
                </div>
 


            </div>

        </div>


    </form>
</body>
</html>
