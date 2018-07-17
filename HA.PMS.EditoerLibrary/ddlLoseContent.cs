using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.EditoerLibrary
{
    public class ddlLoseContent : System.Web.UI.WebControls.DropDownList
    {
        public void BinderByType(int? Type)
        {
            this.Width = 75;
            LoseContent objLoseContentBLL = new LoseContent();
            this.DataSource = objLoseContentBLL.GetByAll().Where(C=>C.Type==Type);
            this.DataTextField = "Title";
            this.DataValueField = "ContentID";
            this.DataBind();
            this.Items.Add(new System.Web.UI.WebControls.ListItem("渠道信息无效", "-3"));
 
            this.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            this.Items.FindByText("请选择").Selected = true;
        }

        public ddlLoseContent()
        {
            this.Width = 75;
            LoseContent objLoseContentBLL = new LoseContent();
            this.DataSource = objLoseContentBLL.GetByAll();
            this.DataTextField = "Title";
            this.DataValueField = "ContentID";
            this.DataBind();

            this.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            this.Items.FindByText("请选择").Selected = true;
        }
    }
}
