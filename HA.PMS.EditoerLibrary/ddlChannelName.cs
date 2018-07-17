using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLAssmblly.FD;
using System.Web.UI;


namespace HA.PMS.EditoerLibrary
{
    public class ddlChannelName : System.Web.UI.WebControls.DropDownList
    {

        public ddlChannelName()
        {
            //渠道名称
            SaleSources objSaleSourcesBLL = new SaleSources();
            this.DataSource = objSaleSourcesBLL.GetByAll().OrderBy(C => C.Letter);
            this.DataTextField = "Sourcename";
            this.DataValueField = "SourceID";
            this.DataBind();
            this.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            this.Items.FindByText("请选择").Selected = true;
            this.Width = 75;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        public void BindByParent(int? Type)
        {
            this.Width = 75;
            this.Items.Clear();
            SaleSources objSaleSourcesBLL = new SaleSources();
            this.DataSource = objSaleSourcesBLL.GetByType(Type).OrderBy(C => C.Letter);
            this.DataTextField = "Sourcename";
            this.DataValueField = "SourceID";
            this.DataBind();
            this.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            this.Items.FindByText("请选择").Selected = true;
        }

        /// <summary>
        /// 通过 EmployeeID 过滤
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="EmployeeID"></param>
        public void BindByParent(int? Type, int EmployeeID)
        {
            this.Width = 75;
            this.Items.Clear();
            SaleSources objSaleSourcesBLL = new SaleSources();
            this.DataSource = objSaleSourcesBLL.GetByType(Type).Where(C => C.ProlongationEmployee.Equals(EmployeeID)).OrderBy(C => C.Letter);
            this.DataTextField = "Sourcename";
            this.DataValueField = "SourceID";
            this.DataBind();
            this.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            this.Items.FindByText("请选择").Selected = true;
        }

        /// <summary>
        /// 绑定本人以及下属的频道
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="EmployeeID"></param>
        public void BindSubordinateByParent(int? Type, int EmployeeID)
        {
            this.Items.Clear();
            this.Width = 75;
            SaleSources objSaleSourcesBLL = new SaleSources();
            List<int> EmployeeIDList = new HA.PMS.BLLAssmblly.Sys.Employee().GetMyManagerEmpLoyee(EmployeeID).Select(C => C.EmployeeID).ToList();
            EmployeeIDList.Add(EmployeeID);
            EmployeeIDList.Distinct();
            this.DataSource = objSaleSourcesBLL.GetByType(Type).Where(C => EmployeeIDList.Contains(C.ProlongationEmployee.Value)).OrderBy(C => C.Letter);
            this.DataTextField = "Sourcename";
            this.DataValueField = "SourceID";
            this.DataBind();
            this.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            this.Items.FindByText("请选择").Selected = true;
        }

    }
}
