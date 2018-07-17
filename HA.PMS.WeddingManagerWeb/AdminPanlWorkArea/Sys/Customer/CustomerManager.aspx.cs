using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Customer
{
    public partial class CustomerManager : HA.PMS.Pages.SystemPage
    {
        //新人信息
        Customers ObjCustomerBLL = new Customers();
        //员工
        Employee ObjEmployeeBLL = new Employee();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }

        protected void BinderData(object sender, EventArgs e)
        {
            int resourceCount = 0;
            List<PMSParameters> ObjparList = new List<PMSParameters>();

            //新娘
            if (CstmNameSelector.SelectedValue.ToInt32() == 1)
            {
                ObjparList.Add(CstmNameSelector.Text != string.Empty, "Bride", CstmNameSelector.Text, NSqlTypes.LIKE);
            }
            //新郎
            else if (CstmNameSelector.SelectedValue.ToInt32() == 2)
            {
                ObjparList.Add(CstmNameSelector.Text != string.Empty, "Groom", CstmNameSelector.Text, NSqlTypes.LIKE);
            }
            //婚期  
            ObjparList.Add(PartyDateRanger.IsNotBothEmpty, "PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);
            //酒店
            ObjparList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text, NSqlTypes.LIKE);
            //联系电话
            if (txtCellPhone.Text.ToString().Trim() != string.Empty)
            {
                ObjparList.Add("BrideCellPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
                ObjparList.Add("GroomCellPhone", txtCellPhone.Text, NSqlTypes.OR);
            }

            //是本人创建及下级的(这种用法 可以获取已删除的用户 所创建的客户)
            ObjparList.Add("CreateEmployee", Employee.GetMyManagerEmpLoyee(User.Identity.Name.ToInt32(), string.Empty, true).Replace("{", "").Replace("}", ""), NSqlTypes.IN);

            BLLAssmblly.Flow.Customers customersBLL = new BLLAssmblly.Flow.Customers();
            repTelemarketingManager.DataSource = customersBLL.GetByWhereParameter(ObjparList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out resourceCount);
            CtrPageIndex.RecordCount = resourceCount;
            repTelemarketingManager.DataBind();
        }

        protected void repTelemarketingManager_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int CustomerID = e.CommandArgument.ToString().ToInt32();
            FL_Customers Customer = ObjCustomerBLL.GetByID(CustomerID);
            int result = ObjCustomerBLL.Delete(Customer);
            BinderData(source, e);
            if (result > 0)
            {
                JavaScriptTools.AlertWindow("删除成功", Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("删除失败,请稍后再试...", Page);
            }
        }
    }
}