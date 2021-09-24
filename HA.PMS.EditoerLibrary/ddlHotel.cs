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
    public class ddlHotel : DropDownList
    {
        public ddlHotel()
        {
            this.Width = 75;
            Hotel objHotelBLL = new Hotel();
            this.DataSource = objHotelBLL.GetByAll().OrderBy(C => C.HotelName.Trim());
            this.DataTextField = "HotelName";
            this.DataValueField = "HotelID";
            this.DataBind();
            this.Items.Insert(0, new ListItem("未选择", "0"));
            //this.ClearSelection();
            // this.Items.FindByValue("0").Selected = true;
            //this.SelectedIndex = this.Items.Count - 1;
            //this.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            //this.Items.FindByText("请选择").Selected = true;

        }


    }
}

