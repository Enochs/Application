using HA.PMS.Pages;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea
{
    public partial class MenuTree : OtherPage
    {
        /// <summary>
        /// 部门操作
        /// </summary>
        Department ObjDepartmentBLL = new Department();

        /// <summary>
        /// 用户操作
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();

        /// <summary>
        /// 
        /// </summary>
        UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();
        EmployeeJobI objJobBLL = new EmployeeJobI();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderChannel();
                Sys_Employee currentEmployee = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32());
                this.lblLoginUser.Text = currentEmployee.EmployeeName;
                Sys_Department userDepart = ObjDepartmentBLL.GetByID(currentEmployee.DepartmentID);
                this.lblDepartment.Text = userDepart.DepartmentName;
                Sys_EmployeeJob jobs = objJobBLL.GetByID(currentEmployee.JobID);
                this.lblEmpLoyeeJob.Text = jobs.Jobname;

            }
        }


        public string CheckByClassType()
        {
            //单店版
            if (System.Configuration.ConfigurationManager.AppSettings["VSKey"] == "459519")
            {

                if (ObjUserJurisdictionBLL.CheckByClassType("StorehouseAdminPanel", User.Identity.Name.ToInt32()))
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["SiteKey"] != "459519")
                    {

                        return System.Configuration.ConfigurationManager.AppSettings["SiteKey"] + "459519";
                    }
                    return string.Empty;
                }


                if (ObjUserJurisdictionBLL.CheckByClassType("总经理指挥台", User.Identity.Name.ToInt32()))
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["SiteKey"] != "459519")
                    {

                        return System.Configuration.ConfigurationManager.AppSettings["SiteKey"] + "000000";
                    }
                    return string.Empty;
                }


            }

            if (System.Configuration.ConfigurationManager.AppSettings["VSKey"] == "550219")
            {
                return string.Empty;
            }

            return string.Empty;
            //StorehouseAdminPanel
        }

        /// <summary>
        /// 绑定频道
        /// </summary>
        private void BinderChannel()
        {
            Storehouse ObjStorehouseBLL = new Storehouse();
            Channel ObjChannelBll = new Channel();
            if (Request["EmployeeID"].ToInt32() > 0)
            {
                //this.RepChannel.DataSource = ObjUserJurisdictionBLL.GetEmPloyeeChannel(Request["EmployeeID"].ToInt32(), 0).Where(C => C.IsMenu == true && C.IsClose == false).OrderBy(C => C.OrderCode);
                //this.RepChannel.DataBind();


            }
            else
            {

                var DataList = ObjUserJurisdictionBLL.GetEmPloyeeChannel(User.Identity.Name.ToInt32(), 0).Where(C => C.IsMenu == true && C.IsClose == false).OrderBy(C => C.OrderCode).ToList();
                string SiteTitle = System.Configuration.ConfigurationManager.AppSettings["SiteTitle"];
                if (!Request.Url.ToString().Contains(SiteTitle))
                {
                    //仅库房
                    if (CheckByClassType().Contains("459519"))
                    {
                        DataList = DataList.Where(C => C.ChannelID == 102||C.ChannelID == 160).ToList();

                    }

                    //仅总经理指挥塔
                    if (CheckByClassType().Contains("000000"))
                    {
                        DataList = DataList.Where(C => C.ChannelID == 160).ToList();
                    }
                    if ((CheckByClassType() == string.Empty) && System.Configuration.ConfigurationManager.AppSettings["VSKey"] == "459519")
                    {
                        Response.Write("<script>alert('无权操作权限');window.parent.document.location ='/Account/LoginOut.aspx';</script>");
                    }
              
                }

                if (ObjStorehouseBLL.GetKeyByManager(User.Identity.Name.ToInt32()) > 0)
                {
                    this.RepChannel.DataSource = DataList;
                    this.RepChannel.DataBind();
                }
                else
                {
                    this.RepChannel.DataSource = DataList.Where(C => C.ChannelID != 102 && C.ChannelID != 183);
                    this.RepChannel.DataBind();
                }
            }
        }

        /// <summary>
        /// 添加空格
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public string ReturnNbSP(object Value)
        {
            int A = 9 - Value.ToString().Length;
            string Nbsp = string.Empty;
            if (A > 0)
            {
                for (int i = 0; i <= A; i++)
                {
                    Nbsp += "&nbsp;&nbsp;";
                }

                return Value.ToString() + Nbsp;
            }

            return Value.ToString();
        }


        /// <summary>
        /// 绑定二级菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepChannel_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Channel ObjChannelBll = new Channel();
            Repeater Objrep = (Repeater)e.Item.FindControl("repSecond");
            if (Request["EmployeeID"].ToInt32() > 0)
            {
                Objrep.DataSource = ObjUserJurisdictionBLL.GetEmPloyeeChannel(Request["EmployeeID"].ToInt32(), ((HiddenField)e.Item.FindControl("hidekey")).Value.ToInt32()).Where(C => C.IsMenu == true && C.IsClose == false).OrderBy(C => C.OrderCode);
                Objrep.DataBind();
            }
            else
            {


                Objrep.DataSource = ObjUserJurisdictionBLL.GetEmPloyeeChannel(User.Identity.Name.ToInt32(), ((HiddenField)e.Item.FindControl("hidekey")).Value.ToInt32()).Where(C => C.IsMenu == true && C.IsClose == false).OrderBy(C => C.OrderCode);
                Objrep.DataBind();
            }
        }
    }
}