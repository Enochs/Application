
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.11
 Description:已收件管理页面
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
    public partial class CS_TakeDiskManagerHaveTake : SystemPage
    {
        TakeDisk objTakeDiskBLL = new TakeDisk();
        Customers objCustomersBLL = new Customers();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                DataBinder();
 
            }
        }
        
           
     
        protected void DataBinder()
        {

            #region 相关的查询
            //对应的视图对象
            List<PMSParameters> ObjParameterList = new List<PMSParameters>();

            if (!string.IsNullOrEmpty(txtGroom.Text))
            {
                ObjParameterList.Add("Bride", txtGroom.Text, NSqlTypes.LIKE);
            }
            if (!string.IsNullOrEmpty(txtGroomCellPhone.Text))
            {
                ObjParameterList.Add("BrideCellPhone", txtGroomCellPhone.Text, NSqlTypes.StringEquals);

            }

            ObjParameterList.Add("State", 2);


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

        protected void rptTalkeDisk_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "Edit")
            {
                
                TextBox txtrealityTime = e.Item.FindControl("txtrealityTime") as TextBox;
                //取件人
                TextBox txtTakeCustomer = e.Item.FindControl("txtTakeCustomer") as TextBox;
                //取件时间
                TextBox txtTakePhone = e.Item.FindControl("txtTakePhone") as TextBox;
                
                if (!string.IsNullOrEmpty(txtrealityTime.Text))
                {
                    int TakeID = e.CommandArgument.ToString().ToInt32();
                    CS_TakeDisk c_TakeDisk = objTakeDiskBLL.GetByID(TakeID);
                    c_TakeDisk.realityTime = txtrealityTime.Text.ToDateTime();
                    c_TakeDisk.TakeCustomer = txtTakeCustomer.Text;
                    c_TakeDisk.TakePhone = txtTakePhone.Text;

                    c_TakeDisk.State = 3;
                    objTakeDiskBLL.Update(c_TakeDisk);
                    //重新绑定数据源
                    DataBinder();
                }
                else
                {
                    JavaScriptTools.AlertWindow("请你填写完整的信息", this.Page);
                }
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void TalkeDiskPager_PageChanged(object sender, EventArgs e)
        {

            DataBinder();
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/TakeDiskHaveTakeModel.xml"));

            string ObjTempletContent = Objreader.ReadToEnd();
            System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();
            Objreader.Close();

            #region 相关查询
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();

            if (!string.IsNullOrEmpty(txtGroom.Text))
            {
                ObjParameterList.Add(new ObjectParameter("Bride_LIKE", txtGroom.Text));
            }
            if (!string.IsNullOrEmpty(txtGroomCellPhone.Text))
            {
                ObjParameterList.Add(new ObjectParameter("GroomCellPhone_LIKE", txtGroomCellPhone.Text.Trim()));
            }
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
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetDateStr(ObjDataItem.TakePlanTime) + "</Data></Cell>\r\n");
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
    }
}