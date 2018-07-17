using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.EditoerLibrary;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceScheduleManager : SystemPage
    {

        QuotedPriceSchedule ObjScheduleBLL = new QuotedPriceSchedule();
        FourGuardian ObjGuardianBLL = new FourGuardian();
        GuardianType ObjGuardianTypeBLL = new GuardianType();

        string OrderByColumnName = "PartyDate";
        int SourceCount = 0;

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 预定婚期
        /// </summary>

        public void DataBinder()
        {
            List<PMSParameters> pars = new List<PMSParameters>();
            pars.Add("State", "15, 19, 24, 204", NSqlTypes.IN);           //审核中 新订单 执行中 确认
            if (ddlPreSchedule.SelectedValue.ToInt32() == 1)        // 已预订
            {
                pars.Add("ScheState", 0, NSqlTypes.Equal);
            }
            else if (ddlPreSchedule.SelectedValue.ToInt32() == 2)   //未预定
            {
                pars.Add("ScheState", null, NSqlTypes.IsNull);
            }
            else
            {
                pars.Add("ScheState", 0, NSqlTypes.OrInt);
                pars.Add("ScheState", null, NSqlTypes.OrInt);
            }
            MyManager.GetEmployeePar(pars);         //按策划师查询
            CstmNameSelector.AppandTo(pars);        //按新人姓名查询
            pars.Add(ddlHotel1.SelectedValue.ToInt32() > 0, "WineShop", ddlHotel1.SelectedValue.ToInt32(), NSqlTypes.Equal);   //酒店
            CellphoneSelector.AppandTo(pars);       //按新人联系电话查询
            pars.Add(DateRanger.IsNotBothEmpty, "PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);


            var DataList = ObjScheduleBLL.GetByAllParameter(pars, OrderByColumnName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            rptSchedule.DataBind(DataList);
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 点击上一页 下一页 分页查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 数据绑定完成时间 ItemDataBound
        /// <summary>
        /// 绑定
        /// </summary> 
        protected void rptSchedule_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int ScheId = (e.Item.FindControl("HideScheID") as HiddenField).Value.ToInt32();
            if (ScheId > 0)
            {
                FL_QuotedPriceSchedule ObjScheduleModel = ObjScheduleBLL.GetByID(ScheId);
                Label lblGuardianName = e.Item.FindControl("lblGuardianName") as Label;
                Label lblGuardianType = e.Item.FindControl("lblGuardianType") as Label;

                lblGuardianName.Text = ObjGuardianBLL.GetByID(ObjScheduleModel.ScheGuardianID.ToString().ToInt32()).GuardianName.ToString();
                lblGuardianType.Text = ObjGuardianTypeBLL.GetByID(ObjScheduleModel.ScheGuardianType.ToString().ToInt32()).TypeName.ToString();
            }
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询
        /// </summary> 
        protected void btnLookFor_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

    }
}