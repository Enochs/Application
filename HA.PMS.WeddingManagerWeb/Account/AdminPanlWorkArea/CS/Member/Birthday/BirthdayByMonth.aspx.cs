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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.Birthday
{
    public partial class BirthdayByMonth : SystemPage
    {
        Customers ObjCustomersBLL = new Customers();
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
            //int startIndex = CtrPageIndex.StartRecordIndex;
            //int SourceCount = 0;
            //List<ObjectParameter> paramList = new List<ObjectParameter>();

            ////新人姓名
            //paramList.Add(!string.IsNullOrWhiteSpace(txtBride.Text), "Bride_LIKE", txtBride.Text.Trim());
            ////新人手机
            //paramList.Add(!string.IsNullOrWhiteSpace(txtBrideCellPhone.Text), "BrideCellPhone_LIKE", txtBrideCellPhone.Text.Trim());
            ////酒店
            //paramList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop_LIKE", ddlHotel.SelectedItem.Text.Trim());
            ////婚期
            //paramList.Add(DateRanger.IsNotBothEmpty, "PartyDate_between", DateRanger.Start, DateRanger.End);
            ////生日
            ////paramList.Add(new ObjectParameter("BrideBirthday_GroomBirthday_OTTimerSpan", DateTime.Now.Month + "," + (7 - (int)DateTime.Now.DayOfWeek)));

            //List<View_CustomerQuoted> query = null;
            //int TypeID = Request["Typer"].ToInt32();
            //switch (TypeID)
            //{
            //    //一周内
            //    case 1: query = new Dispatching().GetCsCustomersByBirthdayRange(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, User.Identity.Name.ToInt32(), paramList, Monday, Sunday); break;
            //    //下周
            //    case 2: query = new Dispatching().GetCsCustomersByBirthdayRange(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, User.Identity.Name.ToInt32(), paramList, NextMonday, NextSunday); break;
            //    //所有
            //    case 3: query = new Dispatching().GetCsCustomersByWhere(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, User.Identity.Name.ToInt32(), paramList); break;
            //    //延伸服务明细
            //    default: break;
            //};

           
            //CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataBind(ObjCustomersBLL.GetBirthCustomer());
        }
    }
}