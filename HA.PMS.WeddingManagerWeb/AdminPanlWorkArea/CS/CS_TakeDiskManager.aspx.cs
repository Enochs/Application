/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.25
 Description:未收件管理页面
 History:修改日志

 Author:杨洋
 date:2013.3.25
 version:好爱1.0
 description:修改描述
 
 
 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS
{

    public class FL_CustomersWineshopComparer : IEqualityComparer<FL_Customers>
    {

        public bool Equals(FL_Customers x, FL_Customers y)
        {


            if (Object.ReferenceEquals(x, y)) return true;


            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;


            return x.Wineshop == y.Wineshop;
        }
        public int GetHashCode(FL_Customers c)
        {

            if (Object.ReferenceEquals(c, null)) return 0;


            int hashWineshop = c.Wineshop == null ? 0 : c.Wineshop.GetHashCode();


            //  int hashProductCode = c.Channel.GetHashCode();


            return hashWineshop;
        }

    }


    public partial class CS_TakeDiskManager : SystemPage
    {
        TakeDisk objTakeDiskBLL = new TakeDisk();
        Customers objCustomersBLL = new Customers();

        #region 页面初始化
        /// <summary>
        /// 初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataBinder();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void DataBinder()
        {

            #region 相关的查询
            //对应的视图对象
            //View_GetTakeDisk cS_TakeDisk = new View_GetTakeDisk();
            //cS_TakeDisk.Groom = txtGroom.Text.Trim();
            //cS_TakeDisk.GroomCellPhone = txtGroomCellPhone.Text.Trim();
            //cS_TakeDisk.Wineshop = ddlHotel.SelectedItem.Text;

            List<PMSParameters> ObjParameterList = new List<PMSParameters>();

            if (!string.IsNullOrEmpty(txtGroom.Text))
            {
                ObjParameterList.Add("Bride", txtGroom.Text, NSqlTypes.LIKE);
                ObjParameterList.Add("Groom", txtGroom.Text, NSqlTypes.ORLike);
            }
            if (!string.IsNullOrEmpty(txtGroomCellPhone.Text))
            {
                ObjParameterList.Add("BrideCellPhone", txtGroomCellPhone.Text, NSqlTypes.StringEquals);
                ObjParameterList.Add("GroomCellPhone", txtGroomCellPhone.Text, NSqlTypes.OR);

            }

            ObjParameterList.Add("State", 0, NSqlTypes.Equal);
            ObjParameterList.Add("IsCheck", 0, NSqlTypes.Bit);
            ObjParameterList.Add("CustomerState", 206 + "," + 208, NSqlTypes.IN);

            //开始时间
            DateTime startTime = "1949-10-1".ToDateTime();
            //如果没有选择结束时间就默认是当前时间

            DateTime endTime = "2100-1-1".ToDateTime();
            if (!string.IsNullOrEmpty(txtStart.Text))
            {
                startTime = txtStart.Text.ToDateTime();
            }

            if (!string.IsNullOrEmpty(txtEnd.Text))
            {
                endTime = txtEnd.Text.ToDateTime();
            }

            ObjParameterList.Add("PartyDate", startTime + "," + endTime, NSqlTypes.DateBetween);

            //策划师
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                ObjParameterList.Add("QuotedEmployee", MyManager.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }
            else
            {
                ObjParameterList.Add("QuotedEmployee", User.Identity.Name.ToInt32(), NSqlTypes.Equal, true);
            }

            #endregion

            #region 分页页码
            int startIndex = TalkeDiskPager.StartRecordIndex;
            int resourceCount = 0;
            var ObjDataSource = objTakeDiskBLL.GetByWhere(ObjParameterList, "PartyDate", TalkeDiskPager.PageSize, TalkeDiskPager.CurrentPageIndex, out resourceCount);
            TalkeDiskPager.RecordCount = resourceCount;

            rptTalkeDisk.DataSource = ObjDataSource;
            rptTalkeDisk.DataBind();

            #endregion

        }
        #endregion

        #region 修改  审核
        /// <summary>
        /// 修改或者审核
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptTalkeDisk_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            bool IsReceive = true;

            if (e.CommandName == "Edit")
            {
                //照片取件时间
                TextBox txtConsigneeTime = e.Item.FindControl("txtConsigneeTime") as TextBox;
                //照片取件说明
                TextBox txtConsigneeContent = e.Item.FindControl("txtConsigneeContent") as TextBox;
                //视频取件时间
                TextBox txtGetFileTime = e.Item.FindControl("txtGetFileTime") as TextBox;
                //视频取件说明
                TextBox txtGetFileContent = e.Item.FindControl("txtGetFileContent") as TextBox;

                //照片供应商
                TextBox txtConsigneeWork = e.Item.FindControl("txtConsigneeWork") as TextBox;
                //视频供应商
                TextBox txtGetFileWork = e.Item.FindControl("txtGetFileWork") as TextBox;

                //查询更新记录
                int TakeID = e.CommandArgument.ToString().ToInt32();
                CS_TakeDisk c_TakeDisk = objTakeDiskBLL.GetByID(TakeID);

                c_TakeDisk.IsCheck = true;

                //if (txtConsigneeTime.Text == string.Empty && txtGetFileTime.Text == string.Empty)
                //{
                //    JavaScriptTools.AlertWindow("请您至少选择一个收件时间", Page);
                //    return;
                //}


                //有照片的取件
                if (c_TakeDisk.HavePhoto.Value)
                {
                    if (txtConsigneeTime.Enabled == true)
                    {
                        if (txtConsigneeTime.Text != string.Empty)
                        {

                            c_TakeDisk.ConsigneeTime = txtConsigneeTime.Text.ToDateTime();
                        }
                        else
                        {
                            IsReceive = false;
                        }
                    }

                }
                c_TakeDisk.ConsigneeContent = txtConsigneeContent.Text.Trim().ToString();
                c_TakeDisk.ConsigneeWork = txtConsigneeWork.Text.Trim().ToString();

                //有视频的取件
                if (c_TakeDisk.HavePhoto.Value)
                {
                    if (txtGetFileTime.Enabled == true)
                    {
                        if (txtGetFileTime.Text != string.Empty)
                        {
                            c_TakeDisk.GetFileTime = txtGetFileTime.Text.ToDateTime();
                        }
                        else
                        {
                            IsReceive = false;
                        }
                    }

                }
                c_TakeDisk.GetFileContent = txtGetFileContent.Text.Trim().ToString();
                c_TakeDisk.GetFileWork = txtGetFileWork.Text.Trim().ToString();


                //状态设置为已收件未通知
                if (IsReceive == true || (c_TakeDisk.GetFileTime.ToString() != "" && c_TakeDisk.ConsigneeTime.ToString() != ""))
                {
                    c_TakeDisk.IsCheck = true;

                }
                else
                {
                    c_TakeDisk.IsCheck = false;
                }
                c_TakeDisk.FirstEmployee = User.Identity.Name.ToInt32();
                objTakeDiskBLL.Update(c_TakeDisk);
                //重新绑定数据源
                DataBinder();


            }
            else if (e.CommandName == "LookDetails")
            {
                int CustomerID = e.CommandArgument.ToString().ToInt32();
                CostSum ObjCostSumModel = new CostSum();
                FL_CostSum Model = ObjCostSumModel.GetByCustomerID(CustomerID).FirstOrDefault();
            }
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 分页 上一页/下一页
        /// <summary>
        /// 分页
        /// </summary>  
        protected void TalkeDiskPager_PageChanged(object sender, EventArgs e)
        {

            DataBinder();
        }
        #endregion

        #region 数据绑定完成事件
        /// <summary>
        /// 没有视频  视频收件时间就禁用 不能选择/照片也是同样
        /// </summary>
        protected void rptTalkeDisk_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            //照片取件时间
            TextBox txtConsigneeTime = e.Item.FindControl("txtConsigneeTime") as TextBox;
            //视频取件时间
            TextBox txtGetFileTime = e.Item.FindControl("txtGetFileTime") as TextBox;

            //照片责任人
            TextBox txtConsigneeWork = e.Item.FindControl("txtConsigneeWork") as TextBox;
            //视频责任人
            TextBox txtGetFileWork = e.Item.FindControl("txtGetFileWork") as TextBox;


            View_GetTakeDisk Model = (View_GetTakeDisk)e.Item.DataItem;
            CS_TakeDisk c_TakeDisk = objTakeDiskBLL.GetByID(Model.TakeID);
            //有照片
            if (txtConsigneeWork.Text.Trim() != "")
            {
                txtConsigneeTime.Enabled = true;
            }
            else
            {
                txtConsigneeTime.Enabled = false;
            }
            //有视频
            if (txtGetFileWork.Text.Trim() != "")
            {
                txtGetFileTime.Enabled = true;
            }
            else
            {
                txtGetFileTime.Enabled = false;
            }
        }
        #endregion

        #region 获取摄影师  摄像师
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="TypeID"></param>
        public string GetGuardName(object Source, int TypeID)    //2.摄影 视频   3.摄像 照片
        {
            int CustomerID = Source.ToString().ToInt32();
            string name = objTakeDiskBLL.GetGuardianName(CustomerID, TypeID);
            return name;
        }
        #endregion


        #region 获取各种QuotedID OrderID Dispatching 执行明细
        /// <summary>
        /// 获取之后 跳转执行明细页面
        /// </summary>
        /// <returns></returns>
        public string GetQuotedID(object Source)
        {
            int Type = Source.ToString().ToInt32();
            CostSum ObjCostSumModel = new CostSum();
            //FL_CostSum Model = ObjCostSumModel.GetByCustomerID(CustomerID).FirstOrDefault();
            return "";
        }
        #endregion


        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();

            HandleModel.HandleContent = "取件管理-公司收件,客户姓名:";

            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 3;     //策划报价
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion
    }
}