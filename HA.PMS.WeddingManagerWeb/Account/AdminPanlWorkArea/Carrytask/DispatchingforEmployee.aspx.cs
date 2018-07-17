//执行中的订单
using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using System.Data.Objects;
using System.Linq;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class DispatchingforEmployee : SystemPage
    {
        QuotedPriceSchedule ObjScheduleBLL = new QuotedPriceSchedule();

        ProductforDispatching ObjProductForDispatchingBLL = new ProductforDispatching();

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }
        #endregion

        #region 数据加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void BinderData(object sender, EventArgs e)
        {
            List<PMSParameters> paramsList = new List<PMSParameters>();


            paramsList.Add("StateEmpLoyee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);
            paramsList.Add("IsUse", true, NSqlTypes.Bit);

            //MyManager.GetEmployeePar(paramsList,"QuotedEmployee");
            //新人姓名
            CstmNameSelector.AppandTo(paramsList);
            //婚期
            if (PartyDateRanger.IsNotBothEmpty)
            {
                paramsList.Add("PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);
            }
            else
            {
                paramsList.Add("PartyDate", "2014-10-01,9999-12-31", NSqlTypes.DateBetween);
                paramsList.Add("UseSate", 19 + "," + 24 + "," + 208 + "," + 207, NSqlTypes.IN);
            }
            //酒店

            paramsList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text.Trim(), NSqlTypes.LIKE);
            //状态
            if (ddlStates.SelectedValue.ToInt32() == 1)
            {
                paramsList.Add("UseSate", 206, NSqlTypes.NotEquals);
            }
            else if (ddlStates.SelectedValue.ToInt32() == 2)
            {
                paramsList.Add("UseSate", 206, NSqlTypes.Equal);
            }


            //数据页面列表绑定
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            repCustomer.DataBind(new Dispatching().GetDispatchingPageByWhere(paramsList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount));
            CtrPageIndex.RecordCount = SourceCount;
        }
        #endregion

        #region 隐藏查看预定按钮
        /// <summary>
        /// 隐藏
        /// </summary>
        public string IsShowOrHide(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            var DataList = ObjScheduleBLL.GetByCustomerID(CustomerID);
            if (DataList.Count > 0)
            {
                return "style='display:block;width:72px;height:20px;'";
            }
            else
            {
                return "style='display:none'";
            }
        }
        #endregion

        #region 是否有变更单
        /// <summary>
        /// 是否拥有变更单
        /// </summary>
        public string IsChange(object Source)
        {
            int DispatchingID = Source.ToString().ToInt32();
            var DataList = ObjProductForDispatchingBLL.GetByDispatchingID(DispatchingID).Where(C => C.IsFirstMakes >= 1 && C.IsFirstMakes != null);
            if (DataList.Count() >= 1)
            {
                return "style='color:red;'";
            }
            else
            {
                return "style='display:none;'";
            }
        }
        #endregion
    }
}