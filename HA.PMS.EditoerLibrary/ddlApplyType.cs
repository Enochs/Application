using HA.PMS.DataAssmblly;
using HHLWedding.BLLAssmbly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace HA.PMS.EditoerLibrary
{
    public class ddlApplyType : System.Web.UI.WebControls.DropDownList
    {
        BaseService<FD_ApplyType> ObjApplyTypeBLL = new BaseService<FD_ApplyType>();
        public ddlApplyType()
        {

            List<Expression<Func<FD_ApplyType, bool>>> parsList = new List<Expression<Func<FD_ApplyType, bool>>>();
            parsList.Add(c => c.Status == 1);
            this.DataSource = ObjApplyTypeBLL.GetListBy(parsList).Distinct();
            this.DataTextField = "ApplyName";
            this.DataValueField = "ID";
            this.DataBind();
            this.Items.Add(new ListItem("请选择", "0"));
            this.Items.FindByText("请选择").Selected = true;

        }
    }
}
