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
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using System.Text;
using System.Web.Providers.Entities;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CountMananger
{
    public partial class DispatchingOrderCount : UserControlTools
    {
        Dispatching objDispatchingBLL = new Dispatching();
        protected void Page_Load(object sender, EventArgs e)
        {
            int resource = 0;
            int employeeId  = Server.HtmlEncode(Request.Cookies["userName"].Value).ToInt32();
            List<ObjectParameter> objParameterList = new List<ObjectParameter>();
            DateTime starTime=new DateTime(DateTime.Now.Year,1,1);
             DateTime endTime=new DateTime(DateTime.Now.Year,12,31);
            objParameterList.Add(new ObjectParameter("isover", false));
            objParameterList.Add(new ObjectParameter("PartyDate_between", starTime + "," + endTime));

            objParameterList.Add(new ObjectParameter("EmpLoyeeID", employeeId));
            var objResult = objDispatchingBLL.GetDispatchingByWhere(objParameterList);
            StringBuilder sbAggregateAmount = new StringBuilder();

            for (int i = 1; i <= 12; i++)
            {
                var monthCurrent = objResult.Where(C => C.PartyDate.Value.Month == i);
                if (monthCurrent != null)
                {
                    sbAggregateAmount.AppendFormat("{0},", monthCurrent.Sum(C => C.FinishAmount));
                }
                else
                {
                    sbAggregateAmount.AppendFormat("{0},", 0);
                }
            }
            ViewState["allAggregateAmount"] = GetSubString(sbAggregateAmount.ToString());
            
        }
        
        
         
    }
}