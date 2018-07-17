using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork
{
    public partial class TaskWorkPanel : SystemPage
    {
        ProductforDispatching ObjProductForDispatchingBLL = new ProductforDispatching();
        Dispatching ObjDispatchingBLL = new Dispatching();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region 隐藏变更单(没有做过变更单)
        public string HideChange()
        {
            var ObjList = ObjProductForDispatchingBLL.GetByDispatchingID(Request["DispatchingID"].ToInt32());
            if (ObjList.Where(C => C.IsFirstMakes >= 1 && C.IsFirstMakes != null).ToList().Count >= 1)      //做过变更单
            {
                return string.Empty;
            }
            else            //没做过变更单
            {
                return "style='display:none;'";
            }
        }
        #endregion

        #region 派设计师
        /// <summary>
        /// 改派设计师
        /// </summary>
        protected void btnSaveDesigner_Click(object sender, EventArgs e)
        {

            int EmployeeID = hideEmpLoyeeID.Value.ToString().ToInt32();
            int DispatchingID = Request["DispatchingID"].ToInt32();
            var DataList = ObjProductForDispatchingBLL.GetByDispatchingID(DispatchingID.ToString().ToInt32());
            var Model = ObjDispatchingBLL.GetByID(DispatchingID);
            if (Model != null)
            {
                Model.DesignEmployee = EmployeeID;
                ObjDispatchingBLL.Update(Model);
                if (DataList.Count > 0)
                {
                    foreach (var item in DataList)
                    {
                        item.DesignEmployee = EmployeeID;
                        ObjProductForDispatchingBLL.Update(item);
                    }
                }
            }
            JavaScriptTools.AlertWindow("成功分派设计师", Page);
            //JavaScriptTools.OpenWindown("../DesignTaskWorkManager.aspx", Page);

        }
        #endregion
    }
}