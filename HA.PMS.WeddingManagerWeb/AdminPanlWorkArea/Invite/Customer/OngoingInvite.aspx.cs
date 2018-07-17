
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:黄晓可
 Date:2013.3.16
 Description:创建正在邀约
 History:修改日志
 
 Author:黄晓可
 date:2013.3.17
 version:好爱1.0
 description:实现正在邀约
 
 
 
 */
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
using System.Linq;
using System.IO;
using System.Web.UI;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer
{
    public partial class OngoingInvite : SystemPage
    {

        HA.PMS.BLLAssmblly.Flow.Invite ObjInvtieBLL = new BLLAssmblly.Flow.Invite();
        Customers ObjCustomersBLL = new Customers();

        int SourceCount = 0;
        string ColumneName = "AgainDate";


        #region 页面初始化
        /// <summary>
        /// 页面家挨
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptAllManager.DataSource = ObjCustomersBLL.GetByAll().Take(1).ToList();
                rptAllManager.DataBind();
                //Load加载时绑定数据源
                DataBinder();
            }

        }
        #endregion

        #region 渠道类型选择 绑定渠道名称
        /// <summary>
        /// 选择渠道类型 绑定渠道名称
        /// </summary>
        protected void ddlChanneltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlreferrr.Items.Clear();
            ddlChannelname.Items.Clear();
            if (ddlChanneltype.SelectedValue.ToInt32() == -1)
            {
                ListItem currentList = ddlChannelname.Items.FindByValue("0");
                if (currentList != null)
                {
                    currentList.Selected = true;
                }
            }
            else
            {
                ddlChannelname.BindByParent(ddlChanneltype.SelectedValue.ToInt32());
            }

            //ddlChannelname.BindByParent(ddlChanneltype.SelectedValue.ToInt32());
        }
        #endregion

        #region 选择渠道 绑定联系人
        /// <summary>
        /// 绑定渠道联系人
        /// </summary>
        protected void ddlChannelname_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlChannelname.SelectedValue.ToInt32() == 0)
            {
                ddlreferrr.Items.Clear();
            }
            else
            {
                ddlreferrr.BinderbyChannel(ddlChannelname.SelectedValue.ToInt32());
            }
            // ddlreferrr.BinderbyChannel(ddlChannelname.SelectedValue.ToInt32());
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据到Repeater控件中
        /// </summary>
        private void DataBinder()
        {
            rptAllManager.Visible = false;
            rptAllManager.DataSource = ObjCustomersBLL.GetByAll().Take(1).ToList();
            rptAllManager.DataBind();
        }
        #endregion

        #region 设置保存沟通时间
        /// <summary>
        /// 保存全部设置好沟通时间的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveDate_Click(object sender, EventArgs e)
        {
            TextBox ObjTxtDate;
            HiddenField ObjHideKey;
            FL_Invite ObjInvte;
            HiddenField ObjOldDate;
            ///邀约沟通记录操作
            InvtieContent ObjInvtieContentBLL = new InvtieContent();

            for (int i = 0; i < rptAllManager.Items.Count; i++)
            {
                Repeater ObjItem = rptAllManager.Items[i].FindControl("repTelemarketingManager") as Repeater;
                for (int j = 0; j < ObjItem.Items.Count; j++)
                {
                    ObjTxtDate = (TextBox)ObjItem.Items[i].FindControl("txtAgainDate");
                    ObjHideKey = (HiddenField)ObjItem.Items[i].FindControl("HideKey");
                    ObjOldDate = (HiddenField)ObjItem.Items[i].FindControl("hideOldDate");

                    if (ObjTxtDate.Text.Trim() != string.Empty)
                    {

                        if (ObjOldDate.Value.ToDateTime().ToShortDateString() != ObjTxtDate.Text.Trim().ToDateTime().ToShortDateString())
                        {
                            ObjInvte = ObjInvtieBLL.GetByID(ObjHideKey.Value.ToInt32());
                            ObjInvte.AgainDate = ObjTxtDate.Text.ToDateTime();
                            ObjInvtieBLL.Update(ObjInvte);
                            MissionManager ObjMissManagerBLL = new MissionManager();
                            ObjMissManagerBLL.WeddingMissionCreate(ObjInvte.CustomerID, 1, (int)MissionTypes.Invite, ObjTxtDate.Text.Trim().ToDateTime(), ObjInvte.EmpLoyeeID.Value, "?CustomerID=" + ObjInvte.CustomerID.ToString(), MissionChannel.FL_TelemarketingManager, ObjInvte.EmpLoyeeID.Value, ObjInvte.InviteID);
                        }
                    }
                }
            }

            JavaScriptTools.AlertWindow("保存完毕", Page);
            DataBinder();
        }
        #endregion

        #region 查询  分页
        /// <summary>
        /// 查询  分页 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 隐藏 超期文字变色(红色)
        /// <summary>
        /// 隐藏
        /// </summary>    
        protected string StatuHideViewInviteInfo()
        {
            return !new Employee().IsManager(User.Identity.Name.ToInt32()) ? "style='display:none'" : string.Empty;
        }

        public string OverChange(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            var Model = ObjInvtieBLL.GetByCustomerID(CustomerID);
            if (Model.AgainDate.ToString().ToDateTime().ToShortDateString().ToDateTime() <= DateTime.Now.ToShortDateString().ToDateTime())
            {
                return "style='color:red;'";
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 外部绑定 Repeater完成
        /// <summary>
        /// 数据再次绑定 内部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void rptAllManager_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater RptData = e.Item.FindControl("repTelemarketingManager") as Repeater;

            var objParmList = new List<PMSParameters>();

            //渠道类型
            if (ddlChanneltype.SelectedItem != null)
            {
                if (ddlChanneltype.SelectedItem.Text != "无")
                {
                    objParmList.Add("ChannelType", ddlChanneltype.SelectedValue.ToString().ToInt32(), NSqlTypes.Equal);
                }
            }

            //渠道
            if (ddlChannelname.SelectedItem != null)
            {
                if (ddlChannelname.SelectedItem.Text != "请选择")
                {
                    objParmList.Add("Channel", ddlChannelname.SelectedItem.Text.ToString(), NSqlTypes.StringEquals);
                }
            }

            //按照推荐查询
            if (ddlreferrr.Items.Count != 0)
            {
                if (string.IsNullOrEmpty(ddlreferrr.SelectedItem.Text) || ddlreferrr.SelectedItem.Text != "请选择")
                {
                    objParmList.Add("Referee", ddlreferrr.SelectedItem.Text.ToString(), NSqlTypes.StringEquals);
                }
            }


            //是否按照责任人查询
            if (MyManager.SelectedValue.ToInt32() == 0)
            {
                MyManager.GetEmployeePar(objParmList, "InviteEmployee");
            }
            else
            {
                objParmList.Add("InviteEmployee", MyManager.SelectedValue, NSqlTypes.Equal);
            }
            //邀约中
            objParmList.Add("State", "5", NSqlTypes.Equal);

            //沟通次数
            if (txtContentCount.Text != string.Empty)
            {
                objParmList.Add("FllowCount", txtContentCount.Text.ToInt32(), NSqlTypes.Equal);
            }


            //婚期
            if (ddlDateRanger.SelectedValue == "1" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                ColumneName = "PartyDate";
            }

            //录入时间
            if (ddlDateRanger.SelectedValue == "2" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add("RecorderDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                ColumneName = "RecorderDate";
            }

            //最近沟通时间
            if (ddlDateRanger.SelectedValue == "3" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add("LastFollowDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                ColumneName = "LastFollowDate";
            }

            //计划沟通时间
            if (ddlDateRanger.SelectedValue == "4" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add("AgainDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                ColumneName = "AgainDate";
            }


            //联系电话
            if (txtCellPhone.Text != string.Empty)
            {
                objParmList.Add("ContactPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }

            //新人姓名
            objParmList.Add(txtContactMan.Text != string.Empty, "ContactMan", txtContactMan.Text, NSqlTypes.LIKE);

            var query = ObjInvtieBLL.GetByWhereParameter(objParmList, ColumneName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;

            repTelemarketingManager.DataSource = query;
            repTelemarketingManager.DataBind();

        }
        #endregion

        #region 导出Excel
        /// <summary>
        /// 导出Excel
        /// </summary>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            HA.PMS.BLLAssmblly.Flow.Customers ObjCustomerBLL = new Customers();
            List<PMSParameters> ObjParmList = new List<PMSParameters>();

            var ObjAllDataList = ObjInvtieBLL.GetByWhereParameter(GetWhere(ObjParmList), ColumneName, 10000, 1, out SourceCount);
            for (int i = 0; i < rptAllManager.Items.Count; i++)
            {
                var ObjDataItem = rptAllManager.Items;
                Repeater RptData = ObjDataItem[i].FindControl("repTelemarketingManager") as Repeater;
                RptData.DataSource = ObjAllDataList;
                RptData.DataBind();
            }

            rptAllManager.Visible = true;

            Response.Clear();
            //获取或设置一个值，该值指示是否缓冲输出，并在完成处理整个响应之后将其发送
            Response.Buffer = true;
            //获取或设置输出流的HTTP字符集
            Response.Charset = "GB2312";
            //将HTTP头添加到输出流
            Response.AppendHeader("Content-Disposition", "attachment;filename=电话邀约列表" + ".xls");
            //获取或设置输出流的HTTP字符集
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //获取或设置输出流的HTTP MIME类型
            Response.ContentType = "application/ms-excel";
            System.IO.StringWriter onstringwriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter onhtmltextwriter = new System.Web.UI.HtmlTextWriter(onstringwriter);
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            this.rptAllManager.RenderControl(htw);
            string html = sw.ToString().Trim();
            Response.Output.Write(html);
            Response.Flush();
            Response.End();
        }
        #endregion

        #region 条件 GetWhere

        public List<PMSParameters> GetWhere(List<PMSParameters> objParmList)
        {
            //是否按照责任人查询
            if (MyManager.SelectedValue.ToInt32() == 0)
            {
                MyManager.GetEmployeePar(objParmList, "InviteEmployee");
            }
            else
            {
                objParmList.Add("InviteEmployee", MyManager.SelectedValue, NSqlTypes.Equal);
            }
            //邀约中
            objParmList.Add("State", "5", NSqlTypes.Equal);

            //沟通次数
            if (txtContentCount.Text != string.Empty)
            {
                objParmList.Add("FllowCount", txtContentCount.Text.ToInt32(), NSqlTypes.Equal);

            }

            string ColumneName = "PartyDate";
            //婚期
            if (ddlDateRanger.SelectedValue == "1" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                ColumneName = "PartyDate";
            }

            //录入时间
            if (ddlDateRanger.SelectedValue == "2" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add("RecorderDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                ColumneName = "RecorderDate";
            }

            //到店时间
            if (ddlDateRanger.SelectedValue == "3" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add("ComeDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                ColumneName = "ComeDate";
            }

            //计划沟通时间
            if (ddlDateRanger.SelectedValue == "4" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add("AgainDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                ColumneName = "AgainDate";
            }


            //联系电话
            if (txtCellPhone.Text != string.Empty)
            {
                objParmList.Add("ContactPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }

            //新人姓名
            objParmList.Add(txtContactMan.Text != string.Empty, "ContactMan", txtContactMan.Text, NSqlTypes.LIKE);
            return objParmList;
        }
        #endregion

        #region 会员标志是否显示   绑定事件
        /// <summary>
        /// 会员标志
        /// </summary>
        protected void repTelemarketingManager_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            Image imgIcon = e.Item.FindControl("ImgIcon") as Image;
            int CustomerID = (e.Item.FindControl("HideCustomerID") as HiddenField).Value.ToString().ToInt32();
            var CustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            if (CustomerModel.IsVip == true)            //该客户是会员
            {
                imgIcon.Visible = true;
            }
            else     //不是会员
            {
                imgIcon.Visible = false;
            }
        }
        #endregion
    }
}