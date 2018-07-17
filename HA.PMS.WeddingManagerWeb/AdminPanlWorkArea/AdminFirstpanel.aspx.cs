using HA.PMS.BLLAssmblly.Sys;
using System;
using System.Linq;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Emnus;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea
{
    public partial class AdminFirstpanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
            var DataList = ObjUserJurisdictionBLL.GetEmPloyeeChannel(User.Identity.Name.ToInt32(), 0).Where(C => C.IsMenu == true && C.IsClose == false).OrderBy(C => C.OrderCode).ToList();
            if (DataList.Count > 0)
            {
                if (!DataList[0].ChannelAddress.Contains("AdminFirstpanel"))
                {
                    Response.Redirect(DataList[0].ChannelAddress);
                }
            }
            BinderDatas();
            //List<int> ObjList = new List<int>();
            //ObjList.Add(0);
            //ObjList.Add(0);
            //ObjList.Add(0);
            //ObjList.Add(0);
            //ObjList.Add(0);
            //ObjList.Add(0);
            //this.reptabstitle.DataSource = ObjList;
            //this.reptabstitle.DataBind();

            //this.reptabContent.DataSource = ObjList;
            //this.reptabContent.DataBind();
        }

        public string GetCheckURI()
        {
            Employee ObjEmployeeBLL = new Employee();
            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
            {
                return "/AdminPanlWorkArea/Flows/Mission/FL_MissionCheckList.aspx?NeedPopu=1&singer=1";
            }
            else
            {
                return "/AdminPanlWorkArea/Flows/Mission/FL_MissionChecking.aspx?NeedPopu=1&singer=1";
            }
        }


        public void BinderDatas()
        {
            //Customers ObjCustomerBLL = new Customers();
            //var DataList = ObjCustomerBLL.GetByAll();
            //foreach (var item in DataList)
            //{
            //    if (item.PartyDate <= DateTime.Now.ToShortDateString().ToDateTime())
            //    {
            //        item.FinishOver = true;
            //        if (item.State != 206)
            //        {
            //            item.State = 206;
            //        }
            //        ObjCustomerBLL.Update(item);
            //    }
            //    if (item.PartyDate >= DateTime.Now.ToShortDateString().ToDateTime())
            //    {
            //        item.FinishOver = false;
            //        ObjCustomerBLL.Update(item);
            //    }
            //}
        }
    }
}