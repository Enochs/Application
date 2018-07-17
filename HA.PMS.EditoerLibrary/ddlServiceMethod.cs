using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.FD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;


namespace HA.PMS.EditoerLibrary
{
    /// <summary>
    /// 会员服务方式
    /// </summary>
    public class ddlServiceMethod : DropDownList
    {
        public ddlServiceMethod() 
        {
            MemberServiceMethodResult objMemberServiceMethodResultBLL = new MemberServiceMethodResult();
            this.DataSource = objMemberServiceMethodResultBLL.GetByAll();
            this.DataTextField = "ServiceName";
            this.DataValueField = "ServiceId";
            this.DataBind();
            this.Items.Add(new ListItem("请选择", "-1"));
            this.SelectedIndex = this.Items.Count - 1;
        }
    }
}
