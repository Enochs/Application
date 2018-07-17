using HA.PMS.BLLAssmblly.FD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.EditoerLibrary
{
    public class ddlEvaluationResult : DropDownList
    {
        public ddlEvaluationResult()
        {
            this.Width = 75;
            WeddingSceneEvaluationResult ObjEvaluationBLL = new WeddingSceneEvaluationResult();
            this.DataSource = ObjEvaluationBLL.GetByAll();
            this.DataTextField = "EvaluationName";
            this.DataValueField = "EvaluationID";
            this.DataBind();
            ListItem li = new ListItem("--请选择--", "");
            this.Items.Insert(0, li);
            //Hotel objHotelBLL = new Hotel();
            //this.DataSource = objHotelBLL.GetByAll().OrderBy(C => C.HotelName);
            //this.DataTextField = "HotelName";
            //this.DataValueField = "HotelID";
            //this.DataBind();
            //this.Items.Insert(0, new ListItem("未选择", "0"));
           
        }
    }
}
