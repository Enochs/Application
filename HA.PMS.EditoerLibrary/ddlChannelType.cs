using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLAssmblly.FD;
using System.Web.UI;

namespace HA.PMS.EditoerLibrary
{
    /// <summary>
    /// 渠道类型下拉控件
    /// </summary>
    public class ddlChannelType : System.Web.UI.WebControls.DropDownList
    {
        public ddlChannelType()
        {
            this.Width = 75;
            ///渠道类型
            ChannelType ObjChannelTypeBLL = new ChannelType();
            this.DataTextField = "ChannelTypeName";
            this.DataValueField = "ChannelTypeId";
            this.DataSource = ObjChannelTypeBLL.GetByAll().OrderBy(c => c.ChannelTypeId);
            this.DataBind();
            this.Items.Add(new System.Web.UI.WebControls.ListItem("无", "-1"));
            this.Items.FindByText("无").Selected = true;
        }
    }
}
