//新订单
using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Emnus;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskOfNew : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BinderData(object sender, EventArgs e)
        {

            var objParmList = new List<PMSParameters>();
            string DataTimerStar = DateTime.Today.ToString();
            string DateTimerEnd = NextWeek.ToString();
            objParmList.Add("UseSate", 24, NSqlTypes.Equal);
            //objParmList.Add("StateEmpLoyee", User.Identity.Name.ToInt32());
            objParmList.Add("IsUse", false, NSqlTypes.Bit);

            //按新人姓名查询
            CstmNameSelector.AppandTo(objParmList);

            //按酒店查询
            objParmList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text.Trim(), NSqlTypes.StringEquals);

            //婚期查询
            objParmList.Add(PartyDateRanger.IsNotBothEmpty, "PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);


            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            repCustomer.DataBind(new BLLAssmblly.Flow.Dispatching().GetDispatchingPageByWhere(objParmList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount)); //ObjInvtieBLL.GetInviteCustomerByStateIndex(isAdd, tlCustomerID, GetWhereParList, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;

        }
    }
}