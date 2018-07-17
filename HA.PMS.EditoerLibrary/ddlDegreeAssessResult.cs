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
    /// 满意度
    /// </summary>
    public class ddlDegreeAssessResult:DropDownList
    {

        public ddlDegreeAssessResult() 
        {
            DegreeAssessResult objDegreeAssessResultBLL = new DegreeAssessResult();
            this.DataSource = objDegreeAssessResultBLL.GetByAll();
            this.DataTextField = "AssessName";
            this.DataValueField = "AssessId";
            this.DataBind();
            this.Items.Add(new ListItem("请选择", "-1"));
            this.SelectedIndex = this.Items.Count - 1;
        
        }
    }
}
