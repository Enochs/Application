/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.11
 Description:本周收件管理页面
 History:修改日志

 Author:杨洋
 Date:2013.4.11
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
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS
{
    public partial class CS_TakeDiskManagerTswkTake : SystemPage
    {
        TakeDisk objTakeDiskBLL = new TakeDisk();
        Customers objCustomersBLL = new Customers();

        #region 页面初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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
        /// 绑定
        /// </summary>
        protected void DataBinder()
        {

            #region 相关的查询
            List<PMSParameters> ObjParameterList = new List<PMSParameters>();

            if (!string.IsNullOrEmpty(txtGroom.Text))       //新娘姓名
            {
                ObjParameterList.Add("Bride", txtGroom.Text, NSqlTypes.LIKE);
                ObjParameterList.Add("Groom", txtGroom.Text, NSqlTypes.ORLike);
            }
            if (!string.IsNullOrEmpty(txtGroomCellPhone.Text))  //新娘联系电话
            {
                ObjParameterList.Add("BrideCellPhone", txtGroomCellPhone.Text, NSqlTypes.StringEquals);
                ObjParameterList.Add("GroomCellPhone", txtGroomCellPhone.Text, NSqlTypes.OR);
            }
            if (DateRanger.IsNotBothEmpty)      //婚期
            {
                ObjParameterList.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

            //策划师
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                ObjParameterList.Add("QuotedEmployee", MyManager.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }
            else
            {
                ObjParameterList.Add("QuotedEmployee", User.Identity.Name.ToInt32(), NSqlTypes.Equal, true);
            }

            ObjParameterList.Add("State", 0);
            ObjParameterList.Add("IsCheck", 1, NSqlTypes.Bit);
            //ObjParameterList.Add("CustomerState", 206);
            #endregion
            #region 分页页码
            int startIndex = TalkeDiskPager.StartRecordIndex;
            int resourceCount = 0;
            var query = objTakeDiskBLL.GetByWhere(ObjParameterList, "PartyDate", TalkeDiskPager.PageSize, TalkeDiskPager.CurrentPageIndex, out resourceCount);
            TalkeDiskPager.RecordCount = resourceCount;

            rptTalkeDisk.DataSource = query;
            rptTalkeDisk.DataBind();

            #endregion

        }
        #endregion

        #region 点击确定
        /// <summary>
        /// 查询功能
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

        #region 导出
        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnExport_Click(object sender, EventArgs e)
        {
            StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/TakeDiskTswkTakeModel.xml"));

            string ObjTempletContent = Objreader.ReadToEnd();
            System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();
            Objreader.Close();

            #region 相关的查询
            //对应的视图对象
            View_GetTakeDisk cS_TakeDisk = new View_GetTakeDisk();
            cS_TakeDisk.Groom = txtGroom.Text.Trim();
            cS_TakeDisk.GroomCellPhone = txtGroomCellPhone.Text.Trim();

            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();

            if (!string.IsNullOrEmpty(txtGroom.Text))
            {
                ObjParameterList.Add(new ObjectParameter("Bride_LIKE", cS_TakeDisk.Groom));
            }
            if (!string.IsNullOrEmpty(txtGroomCellPhone.Text))
            {
                ObjParameterList.Add(new ObjectParameter("GroomCellPhone_LIKE", cS_TakeDisk.GroomCellPhone));
            }

            DateTime EndDate = DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek);

            ObjParameterList.Add(new ObjectParameter("TakePlanTime_between", DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek)).ToShortDateString() + "," + EndDate.ToShortDateString()));
            //TakeState
            ObjParameterList.Add(new ObjectParameter("State", 2));
            #endregion

            var ItemList = objTakeDiskBLL.GetbyParameter(ObjParameterList.ToArray());
            foreach (var ObjDataItem in ItemList)
            {
                ObjDataString.Append("<Row>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Bride + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.BrideCellPhone + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetDateStr(ObjDataItem.PartyDate) + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Wineshop + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetOrderEmpLoyeeName(GetOrderIdByCustomerID(ObjDataItem.CustomerID)) + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetQuotedEmpLoyeeName(GetOrderIdByCustomerID(ObjDataItem.CustomerID)) + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetDateStr(ObjDataItem.ConsigneeTime) + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetDateStr(ObjDataItem.NoticeTime) + "</Data></Cell>\r\n");
                ObjDataString.Append("</Row>\r\n");
                // <Row>
                // <Cell><Data ss:Type="String">新人姓名</Data></Cell>
                // <Cell><Data ss:Type="String">电话</Data></Cell>
                // <Cell><Data ss:Type="String">婚期</Data></Cell>
                // <Cell><Data ss:Type="String">酒店</Data></Cell>
                // <Cell><Data ss:Type="String">婚礼顾问</Data></Cell>
                // <Cell><Data ss:Type="String">策划师</Data></Cell>
                // <Cell><Data ss:Type="String">计划取件时间</Data></Cell>
                // <Cell><Data ss:Type="String">收件时间</Data></Cell>
                // <Cell><Data ss:Type="String">通知新人时间</Data></Cell>
                // <Cell><Data ss:Type="String">实际取件时间</Data></Cell>
                //</Row>
            }
            ObjTempletContent = ObjTempletContent.Replace("<!=DataRow>", ObjDataString.ToString());
            IOTools.DownLoadByString(ObjTempletContent, "xls");
        }
        #endregion

        #region 获取最大时间
        /// <summary>
        /// 绑定收件时间(最大的一个  因为有两个 一个视频的时间 还有照片的时间 取最大值)
        /// </summary>
        /// <param name="ConsignTime"></param>
        /// <param name="GetFileTime"></param>
        /// <returns></returns>

        public string GetMaxDate(object ConsignTime, object GetFileTime)        //ConsignTime 照片收件时间 GetFileTime 视频收件时间
        {
            if (ConsignTime == null && GetFileTime != null)
            {
                return GetFileTime.ToString();
            }
            else if (ConsignTime != null && GetFileTime == null)
            {
                return ConsignTime.ToString();
            }
            else if (ConsignTime != null && GetFileTime != null)
            {

                DateTime ConsignTimes = ConsignTime.ToString().ToDateTime();
                DateTime GetFileTimes = GetFileTime.ToString().ToDateTime();
                if (ConsignTimes >= GetFileTimes)
                {
                    return ConsignTimes.ToShortDateString();
                }
                else
                {
                    return GetFileTimes.ToShortDateString();
                }
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}