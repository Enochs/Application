using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.EditoerLibrary
{
    public class ddlSysChannel : DropDownList
    {
        Channel ObjChannelBLL = new Channel();
        public ddlSysChannel()
        {
            this.DataSource = ObjChannelBLL.GetByParent(0);
            this.DataTextField = "ChannelName";
            this.DataValueField = "ChannelID";
            this.DataBind();
            
        }
    }
}
