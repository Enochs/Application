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
    public partial class CS_TakeforNoneNotice : SystemPage
    {

        /// <summary>
        /// 明细操作
        /// </summary>
        MissionDetailed objMissionDetailsedBLL = new MissionDetailed();


        /// <summary>
        /// 分组操作
        /// </summary>
        MissionManager ObjMissionManagerBLL = new MissionManager();

        Employee ObjEmployeeBLL = new Employee();


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

                ddlHotel.ClearSelection();
                ddlHotel.Items.FindByText("未选择").Selected = true;
                DataBinder();

            }
        }
        #endregion

        #region 数据绑定
        protected void DataBinder()
        {

            #region 相关的查询
            List<PMSParameters> ObjParameterList = new List<PMSParameters>();
            //新人姓名
            if (!string.IsNullOrEmpty(txtGroom.Text))
            {
                ObjParameterList.Add("Bride", txtGroom.Text, NSqlTypes.LIKE);
                ObjParameterList.Add("Bride", txtGroom.Text, NSqlTypes.ORLike);
            }
            //新人联系电话查询
            if (!string.IsNullOrEmpty(txtGroomCellPhone.Text))
            {
                ObjParameterList.Add("BrideCellPhone", txtGroomCellPhone.Text, NSqlTypes.StringEquals);
                ObjParameterList.Add("GroomCellPhone", txtGroomCellPhone.Text, NSqlTypes.OR);

            }

            ObjParameterList.Add("State", 1);
            ObjParameterList.Add("IsCheck", true, NSqlTypes.Bit);
            //ObjParameterList.Add("CustomerState", 206);


            //开始时间
            DateTime startTime = "1949-10-1".ToDateTime();
            //如果没有选择结束时间就默认是当前时间

            DateTime endTime = "2100-1-1".ToDateTime();
            if (!string.IsNullOrEmpty(txtStart.Text) && !string.IsNullOrEmpty(txtEnd.Text))
            {
                startTime = txtStart.Text.ToDateTime();
                endTime = txtEnd.Text.ToDateTime();
                ObjParameterList.Add("PartyDate", startTime.ToShortDateString().ToDateTime() + "," + endTime.ToShortDateString().ToDateTime(), NSqlTypes.DateBetween);

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

        #region 点击确定


        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 分页 上一页/下一页

        protected void TalkeDiskPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 点击保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void rptTalkeDisk_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "Edit")
            {
                //取件时间
                TextBox txtrealityTime = e.Item.FindControl("txtRealityTime") as TextBox;

                //备注
                TextBox txtRemark = e.Item.FindControl("txtRemark") as TextBox;

                //通知
                TextBox txtNoticeTime = e.Item.FindControl("txtNoticeTime") as TextBox;

                int TakeID = e.CommandArgument.ToString().ToInt32();
                CS_TakeDisk c_TakeDisk = objTakeDiskBLL.GetByID(TakeID);

                if (c_TakeDisk.IsCheck.Value && c_TakeDisk.State == 1)
                {
                    c_TakeDisk.TakePlanTime = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();

                    if (txtrealityTime.Text != "")      //取件时间
                    {
                        c_TakeDisk.realityTime = txtrealityTime.Text.ToString().ToDateTime();
                    }
                    else
                    {
                        c_TakeDisk.realityTime = null;
                    }

                    if (txtNoticeTime.Text != "")       //通知时间
                    {
                        c_TakeDisk.NoticeTime = txtNoticeTime.Text.ToString().ToDateTime();
                    }
                    else
                    {
                        c_TakeDisk.NoticeTime = null;
                    }

                    //置为已取件
                    if (c_TakeDisk.realityTime.ToString() != string.Empty && c_TakeDisk.NoticeTime.ToString() != string.Empty)
                    {
                        c_TakeDisk.ThirdEmployee = User.Identity.Name.ToInt32();
                        c_TakeDisk.State = 2;
                    }
                    c_TakeDisk.Remark = txtRemark.Text.Trim().ToString();
                    c_TakeDisk.FinishTime = DateTime.Now.ToString().ToDateTime();
                    objTakeDiskBLL.Update(c_TakeDisk);
                    JavaScriptTools.AlertWindow("保存成功！", Page);
                    //重新绑定数据源
                    DataBinder();
                }
                else
                {
                    JavaScriptTools.AlertWindow("请先审核!", this.Page);
                }
            }
            else
            {
                TextBox txtRemark = e.Item.FindControl("txtRemark") as TextBox;
                int TakeID = e.CommandArgument.ToString().ToInt32();
                CS_TakeDisk c_TakeDisk = objTakeDiskBLL.GetByID(TakeID);

                c_TakeDisk.IsCheck = true;
                c_TakeDisk.Remark = txtRemark.Text.Trim().ToString();

                objTakeDiskBLL.Update(c_TakeDisk);
                JavaScriptTools.AlertWindow("保存成功！", Page);
                //重新绑定数据源
                DataBinder();
            }
        }
        #endregion
    }
}