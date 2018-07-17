using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HA.PMS.EditoerLibrary
{
    public class lblMissionfortodaysum : System.Web.UI.WebControls.Label
    {
        /// <summary>
        /// 明细操作
        /// </summary>
        MissionDetailed objMissionDetailsedBLL = new MissionDetailed();
        public lblMissionfortodaysum()
        {
            
            var LoginEmpLoyeeID = int.Parse(HttpContext.Current.Request.Cookies["HAEmployeeID"].Value);
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            //if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
            //{
            //    ObjParameterList.Add(new ObjectParameter("EmployeeID_CreateEmployeeID_PVP", User.Identity.Name.ToInt32() + "," + User.Identity.Name.ToInt32()));

            //}
            //else
            //{
            ObjParameterList.Add(new ObjectParameter("EmployeeID", LoginEmpLoyeeID));
            // }
            ObjParameterList.Add(new ObjectParameter("IsDelete", false));
            ObjParameterList.Add(new ObjectParameter("IsOver", false));
            // ObjParameterList.Add(new ObjectParameter("Type", 9));
            ObjParameterList.Add(new ObjectParameter("PlanDate", DateTime.Parse(DateTime.Now.ToShortDateString())));

            int sourceCount = 0;
            objMissionDetailsedBLL.GetMissionDetailedByWhere(100000, 0, out sourceCount, ObjParameterList);
            this.Text = sourceCount.ToString();
        }

    }
}
