using System;
using System.Collections.Generic;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class DispachingTotalEmployeeManager : HA.PMS.Pages.SystemPage
    {
        protected void BindData(object sender, EventArgs e)
        {
            //临时变量
            List<ObjectParameter> paramList = new List<ObjectParameter>();
            //是否结束
            paramList.Add("Isover", false);
            //新人姓名
            paramList.Add(!string.IsNullOrWhiteSpace(txtBride.Text), "Bride_LIKE", txtBride.Text.Trim());
            //联系方式
            //paramList.Add(!string.IsNullOrWhiteSpace(txtBrideCellPhone.Text), "BrideCellPhone_LIKE", txtBrideCellPhone.Text.Trim());
            //婚期
            paramList.Add(QueryDateRanger.IsNotBothEmpty && ddltype.SelectedValue.Equals("1"), "PartyDate_between", QueryDateRanger.Start, QueryDateRanger.End);
            //派工日期
            paramList.Add(QueryDateRanger.IsNotBothEmpty && ddltype.SelectedValue.Equals("2"), "CreateDate_between", QueryDateRanger.Start, QueryDateRanger.End);
            //酒店
            paramList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop_LIKE", ddlHotel.SelectedItem.Text.Trim());

            //数据页面列表绑定
            int SourceCount = 0;
            repCustomer.DataBind(new Dispatching().GetPagedDispatchingCustomer(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, C => C.DispatchingID > 0, paramList));
            CtrPageIndex.RecordCount = SourceCount;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData(this, e);
            }
        }

        protected void repCustomer_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            int DispatchingID = Convert.ToInt32(e.CommandArgument);
            Dispatching dispatchingBLL = new Dispatching();
            FL_Dispatching fL_Dispatching = dispatchingBLL.FirstOrDefault(C => C.DispatchingID == DispatchingID);
            switch (e.CommandName)
            {
                case "Save":
                    //1.修改执行总表
                    fL_Dispatching.EmployeeID = e.GetTextValue("hiddeEmpLoyeeID").To<Int32>(User.Identity.Name.To<Int32>(1));
                    dispatchingBLL.Update(fL_Dispatching);
                    //2.修改执行状态表
                    //FL_DispatchingState fL_DispatchingState=new DispatchingState().get
                    JavaScriptTools.AlertWindow("修改成功", Page);
                    break;
                default: break;
            }
        }
    }
}