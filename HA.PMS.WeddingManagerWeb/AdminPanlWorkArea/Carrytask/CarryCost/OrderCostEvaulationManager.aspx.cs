using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryCost
{
    public partial class OrderCostEvaulationManager : SystemPage
    {
        Dispatching ObjDispatchingBLL = new Dispatching();

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder(sender,e);
            }
        }
        #endregion

        #region 数据绑定
        public void DataBinder(object sender, EventArgs e)
        {
            var paramsList = new List<PMSParameters>();
            string EmployeeName = GetEmployeeName(User.Identity.Name.ToInt32());        //本次登录的姓名
            paramsList.Add("IsUse", true, NSqlTypes.Bit);
            if (MyManager1.SelectedValue.ToInt32() == 0)
            {
                MyManager1.GetEmployeePar(paramsList, "QuotedEmpLoyee");
            }
            else
            {
                paramsList.Add("QuotedEmpLoyee", MyManager1.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }
            //新人姓名
            CstmNameSelector.AppandTo(paramsList);
            //婚期
            paramsList.Add("PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);
            //酒店

            paramsList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text.Trim(), NSqlTypes.LIKE);


            //数据页面列表绑定
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            //获取集合绑定
            List<View_DispatchingCustomers> ObjDiCustomerModel = ObjDispatchingBLL.GetDispatchingPageByWhere(paramsList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            //ObjDiCustomerModel = ObjDiCustomerModel.Where(C => C.QuotedEmpLoyee == User.Identity.Name.ToInt32()).ToList();
            //SourceCount = ObjDiCustomerModel.Count;
            repCustomer.DataBind(ObjDiCustomerModel);
            CtrPageIndex.RecordCount = SourceCount;
            lblPersonSum.Text = "当前查询总人数为:" + SourceCount;
        }
        #endregion

        #region 会员标志
        /// <summary>
        /// 会员标志
        /// </summary>
        protected void repCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Customers ObjCustomersBLL = new Customers();
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