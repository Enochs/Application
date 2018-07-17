using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web;
using HA.PMS.BLLAssmblly.Sys;
namespace HA.PMS.EditoerLibrary
{
    public class btnManager : System.Web.UI.WebControls.Button
    {
        public btnManager()
        {
            this.Text = "查询";
            this.CssClass = "btn btn-primary ";
            this.Height = 27;
            //Employee ObjEmployeeBLL = new Employee();
            //if (!ObjEmployeeBLL.IsManager( int.Parse(HttpContext.Current.Request.Cookies["HAEmployeeID"].Value)))
            //{
            //    this.Visible = false;
            //}
        }
    }
}
