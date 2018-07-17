using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.TheStage.Hotel
{
    public partial class HotelSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderDropDownList();
                BinderData();
            }
        }

        protected void BinderDropDownList()
        {
            var query = new BLLAssmblly.FD.Hotel().GetAreaList();
            foreach (var item in query)
            {
                ddlArea.Items.Add(new ListItem(item));
            }
            ddlArea.Items.Insert(0, new ListItem("请选择", "0"));
        }

        protected void BinderData()
        {
            List<ObjectParameter> parameters = new List<ObjectParameter>();
            parameters.Add(ddlArea.SelectedItem.Text != "请选择", "Area", ddlArea.SelectedItem.Text.Trim());
            parameters.Add(ddlStarLevel.SelectedValue.ToInt32() > 0, "StarLevel", ddlStarLevel.SelectedValue.ToInt32());
            parameters.Add(ddlStarLevel.SelectedValue.ToInt32() <= 0, "StarLevel_NumOr", "1,2,3,4,5,6");
            parameters.Add(new ObjectParameter("IsDelete", false));

            int starCount = 0;
            int endCount = 1000;
            if (!string.IsNullOrEmpty(txtDeskStar.Text))
            {
                starCount = txtDeskStar.Text.ToInt32();
            }
            if (!string.IsNullOrEmpty(txtDeskEnd.Text))
            {
                endCount = txtDeskEnd.Text.ToInt32();
            }
            if (starCount != 0)
            {
                parameters.Add(new ObjectParameter("DeskCount_between", starCount + "," + endCount));
            }

            int resourceCount = 0;
            var query = new BLLAssmblly.FD.Hotel().GetByIndex(HotelPager.PageSize, HotelPager.CurrentPageIndex, out resourceCount, parameters);
            HotelPager.RecordCount = resourceCount;
            rptList.DataBind(query);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            Employee ObjEmployee = new Employee();
            //login 首页导航，不加入验证中

            if (string.IsNullOrEmpty(txtLoginName.Text) || string.IsNullOrEmpty(txtpassword.Text))
            {
                Response.Redirect("/Account/AdminWorklogin.html");
            }
            int userId = 0;
            if (Request.Cookies["userName"] != null)
            {
                userId = Server.HtmlEncode(Request.Cookies["userName"].Value).ToInt32();
            }

            var ObjSysEmpLoyee = ObjEmployee.EmpLoyeeLogin(txtLoginName.Text.Trim(), txtpassword.Text.Trim().MD5Hash());
            if (ObjSysEmpLoyee != null)
            {




                Response.Cookies["userName"].Value = ObjSysEmpLoyee.EmployeeID + string.Empty;
                Response.Cookies["HAEmployeeID"].Value = ObjSysEmpLoyee.EmployeeID + string.Empty;

                Response.Cookies["userName"].Expires = DateTime.Now.AddMinutes(15);
                System.Web.Security.FormsAuthentication.SetAuthCookie(ObjSysEmpLoyee.EmployeeID.ToString(), false);

                Response.Redirect("/AdminPanlWorkArea/AdminMain.aspx");

            }
            else
            {
                Response.Redirect("/Account/AdminWorklogin.html?error=true");

            }

        }

        protected void HotelPager_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            HotelPager.CurrentPageIndex = 1;
            BinderData();
        }

        protected string GetStarLevelString(object StarLevel)
        {
            switch (Convert.ToInt32(StarLevel))
            {
                case 1: return "一星级";
                case 2: return "二星级";
                case 3: return "三星级";
                case 4: return "四星级";
                case 5: return "五星级";
                case 6: return "其他";
                default: return string.Empty;
            }
        }
    }
}