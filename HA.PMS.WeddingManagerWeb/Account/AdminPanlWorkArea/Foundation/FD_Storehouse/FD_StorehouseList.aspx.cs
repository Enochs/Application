using HA.PMS.BLLAssmblly.FD;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_StorehouseList : SystemPage
    {
        //库房基础设置
        Storehouse ObjStorehouseBLL = new Storehouse();

        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            repStoreHouse.DataSource = ObjStorehouseBLL.GetByAll();
            repStoreHouse.DataBind();
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptDepartment_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "SaveChange")
            {
                HiddenField ObjddlEmployee = (HiddenField)e.Item.FindControl("hideEmpLoyeeID");
                Employee ObjEmployeeBLL = new Employee();


                HA.PMS.DataAssmblly.FD_Storehouse ObjUpdateModel = ObjStorehouseBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                ObjUpdateModel.EmpLoyeeID = ObjddlEmployee.Value.ToInt32();

                ObjUpdateModel.DepartmentID=ObjEmployeeBLL.GetByID(ObjUpdateModel.EmpLoyeeID).DepartmentID;
                ObjStorehouseBLL.Update(ObjUpdateModel);
            }
        }
    }
}