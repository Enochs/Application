using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLAssmblly.FD;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.EditoerLibrary
{
     public class ddlArea : System.Web.UI.WebControls.DropDownList
    {
         Hotel objHotelBLL = new Hotel();
         public ddlArea() 
         {

             this.DataSource = objHotelBLL.GetByAll().Distinct();
             this.DataTextField = "Area";
             this.DataValueField = "HotelID";
             this.DataBind();
             this.Items.Add(new ListItem("请选择","0"));
         }
    }
}
