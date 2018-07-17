using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskTab : SystemPage
    {
        /// <summary>
        /// 
        /// </summary>
        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["New"] != null)
            {
                DispatchingState ObjDispatchingStateBLL = new DispatchingState();
                ObjDispatchingStateBLL.CheckState(Request["DispatchingID"].ToInt32(), Request["StateKey"].ToInt32(), User.Identity.Name.ToInt32());
                // ObjProductforDispatchingBLL.UpdateIsGetforDispatchingID(Request["DispatchingID"].ToInt32(), User.Identity.Name.ToInt32());
            }
            else
            {
                DispatchingState ObjDispatchingStateBLL = new DispatchingState();
                ObjDispatchingStateBLL.CheckState(Request["DispatchingID"].ToInt32(), Request["StateKey"].ToInt32(), User.Identity.Name.ToInt32());
            }
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <returns></returns>
        public string HideCreate()
        {
            Dispatching ObjDispatchingBLL = new Dispatching();
            if (ObjDispatchingBLL.GetByID(Request["DispatchingID"].ToInt32()).EmployeeID.ToString() != User.Identity.Name)
            {
                return "style='display:none;'";
            }
            else
            {
                return string.Empty;
            }
        }

        public string ReadOnly()
        {
            if (Request["ReadOnly"] != null)
            {
                return "display:none;";
            }
            else

            {
                return string.Empty;
            }
        }


        public string BinderPage()
        {

            return Request["PageName"];
        }
    }
}