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
using HA.PMS.BLLAssmblly.FD;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.Other
{
    public partial class Otherall : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }

        protected CS_Member GetMember(object CustomerID)
        {
            return new BLLAssmblly.CS.Member().GetByCustomerID(CustomerID.ToString().ToInt32());
        }

        protected void BinderData(object sender, EventArgs e)
        {
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            List<ObjectParameter> paramList = new List<ObjectParameter>();

            //新人姓名
            paramList.Add(!string.IsNullOrWhiteSpace(txtBride.Text), "Bride_LIKE", txtBride.Text.Trim());
            //新人手机
            paramList.Add(!string.IsNullOrWhiteSpace(txtBrideCellPhone.Text), "BrideCellPhone_LIKE", txtBrideCellPhone.Text.Trim());
            //酒店
            paramList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop_LIKE", ddlHotel.SelectedItem.Text.Trim());
            //婚期
            paramList.Add(QueryDateRanger.IsNotBothEmpty && ddlDateType.SelectedValue.Equals("1"), "PartyDate_between", QueryDateRanger.Start, QueryDateRanger.End);
            //生日
            paramList.Add(QueryDateRanger.IsNotBothEmpty && ddlDateType.SelectedValue.Equals("2"), "BrideBirthday", QueryDateRanger.Start, QueryDateRanger.End);

            int TypeID = Request["TypeID"].ToInt32();
            switch (TypeID)
            {
                //本周完成
                case 1: paramList.Add("CreateDate_between", string.Format("{0},{1}", Monday, Sunday)); break;
                //下周完成
                case 2: paramList.Add("CreateDate_between", string.Format("{0},{1}", NextMonday, NextSunday)); break;
                //本月完成
                case 3: paramList.Add("CreateDate_between", string.Format("{0},{1}", MonthStart, MonthEnd)); break;
                //下月完成
                case 4: paramList.Add("CreateDate_between", string.Format("{0},{1}", NextMonthStart, NextMonthEnd)); break;
                //延伸服务明细
                default: break;
            };

            var query = new BLLAssmblly.CS.Member().GetOtherMember(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, paramList);
            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataBind(query);
        }
    }
}