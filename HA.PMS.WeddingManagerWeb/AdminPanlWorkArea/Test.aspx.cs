using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;
using System.Drawing;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea
{
    public partial class Test : SystemPage
    {
        Customers ObjCustomerBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime date = DateTime.Now.ToShortDateString().ToDateTime();
                var DataList = ObjCustomerBLL.GetAllByPartyDate(date);
                RepCustomer.DataBind(DataList);
            }
        }

        protected void btnFull_Click(object sender, EventArgs e)
        {
            txtLetter.Text = PinYin.GetFullPinYin(txtName.Text.Trim().ToString());
        }

        protected void btnShou_Click(object sender, EventArgs e)
        {
            //txtLetter.Text = PinYin.GetFirstPinYin(txtName.Text.Trim().ToString());
            SaleSources ObjSourceBLL = new SaleSources();
            var DataList = ObjSourceBLL.GetByAll();
            foreach (var item in DataList)
            {
                item.Letter = PinYin.GetFirstLetter(item.Sourcename.Trim().ToString()).ToUpper();
                ObjSourceBLL.Update(item);
            }
        }

        protected void BtnFirstLetter_Click(object sender, EventArgs e)
        {
            Hotel ObjHotelBLL = new Hotel();
            var DataList = ObjHotelBLL.GetByAll();
            foreach (var item in DataList)
            {
                item.Letter = PinYin.GetFirstLetter(item.HotelName.Trim().ToString()).ToUpper();
                ObjHotelBLL.Update(item);
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            DateTime date = Calendar1.SelectedDate;
            var DataList = ObjCustomerBLL.GetAllByPartyDate(date);
            RepCustomer.DataBind(DataList);
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            //if (e.Day.ToString() == "2015-01-05")
            //{
            JavaScriptTools.AlertWindow(e.Day.ToString(), Page);
            //}
            
        }




    }
}