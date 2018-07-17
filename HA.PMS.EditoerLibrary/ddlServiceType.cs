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
    /// 会员服务类型
    /// </summary>
     public class ddlServiceType : DropDownList
    {
         public ddlServiceType()
         {
             MemberServiceTypeResult objMemberServiceTypeResultBLL = new MemberServiceTypeResult();
             this.DataSource = objMemberServiceTypeResultBLL.GetByAll();
             this.DataTextField = "ServiceTypeName";
             this.DataValueField = "ServiceTypeId";
             this.DataBind();
             this.Items.Add(new ListItem("请选择", "-1"));
             this.SelectedIndex = this.Items.Count - 1;
         }
    }
}
