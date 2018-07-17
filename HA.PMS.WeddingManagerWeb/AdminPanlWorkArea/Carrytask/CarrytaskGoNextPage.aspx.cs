using HA.PMS.Pages;
using System;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Emnus;



namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskGoNextPage : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["DispatchingID"].ToInt32() > 0)
            {
                Dispatching ObjDispatchingBLL = new Dispatching();
                var ObjDispatchingModel = ObjDispatchingBLL.GetByID(Request["DispatchingID"].ToInt32());
                if (ObjDispatchingModel.EmployeeID == User.Identity.Name.ToInt32())
                {

                    hideOpen.Value = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/Notassignedtsks.aspx?StateKey=" + Request["StateKey"] + "&New=1&DispatchingID=" + Request["DispatchingID"] + "&CustomerID=" + Request["CustomerID"] + "&OrderID=" + Request["OrderID"] + "&NeedPopu=1";
                    hideLocation.Value = Request.UrlReferrer.ToString();
                    //hideLocation.Value = hideOpen.Value;
                    //Response.Redirect(hideOpen.Value.ToString());
                }
                else
                {
                    Customers ObjCustomersBLL = new Customers();
                    //Response.Redirect("/AdminPanlWorkArea/Carrytask/CarrytaskTab.aspx?StateKey="+Request["StateKey"]+"&New=1&DispatchingID=" + Request["DispatchingID"] + "&NeedPopu=1&CustomerID=" + Request["CustomerID"] + "&PageName=ProductList");
                    hideOpen.Value = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/Notassignedtsks.aspx?StateKey=" + Request["StateKey"] + "&New=1&DispatchingID=" + Request["DispatchingID"] + "&NeedPopu=1&CustomerID=" + Request["CustomerID"] + "&PageName=ProductList";
                    hideLocation.Value = Request.UrlReferrer.ToString();
                    //hideLocation.Value = hideOpen.Value;
                    //Response.Redirect(hideOpen.Value.ToString());
                }
            }
        }
    }
}