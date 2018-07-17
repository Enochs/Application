using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.GeneralManagerPodium
{
    public partial class WorkCheckGuide : SystemPage
    {
        Employee objEmployeeBLL = new Employee();

        Department ObjDepartmentBLL = new Department();

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.MessageBoardforEmpLoyee.CreateEmpLoyeeID = User.Identity.Name.ToInt32();
            this.MessageBoardforEmpLoyee.EmpLoyeeID = User.Identity.Name.ToInt32();
            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        protected void DataBinder()
        {
            int userID = User.Identity.Name.ToInt32();

            var FirstEmployeeList = objEmployeeBLL.GetMyManagerEmpLoyee(userID, true);
            DataList1.DataSource = FirstEmployeeList;
            DataList1.DataBind();

            var SecondEmplOyeeList = objEmployeeBLL.GetMyManagerEmpLoyee(userID, false);
            foreach (var ObjFirstItem in FirstEmployeeList)
            {
                var RemoveItem = SecondEmplOyeeList.FirstOrDefault(C => C.EmployeeID == ObjFirstItem.EmployeeID);
                if (RemoveItem != null)
                {
                    SecondEmplOyeeList.Remove(RemoveItem);
                }
            }
            DataList2.DataSource = SecondEmplOyeeList;
            DataList2.DataBind();
        }
        #endregion

        #region 获取直接下属
        /// <summary>
        /// 直接下属
        /// </summary>
        public string GetURL(object EmpLoyeeID)
        {
            WarningMessage ObjWarningMessageBLL = new WarningMessage();
            var ObjEmpLoyeeModel = objEmployeeBLL.GetByID(EmpLoyeeID.ToString().ToInt32());
            var ObjDepartmetnModel = ObjDepartmentBLL.GetByID(ObjEmpLoyeeModel.DepartmentID);
            string ImgUrl = string.Empty;

            if (ObjEmpLoyeeModel.ImageURL != null && ObjEmpLoyeeModel.ImageURL != string.Empty)
            {
                ImgUrl = ObjEmpLoyeeModel.ImageURL;
            }
            else
            {
                ImgUrl = "/AdminPanlWorkArea/Commandanddispatch/GeneralManagerPodium/defaultImage.jpg";
            }
            if (ObjWarningMessageBLL.HaveWaring(EmpLoyeeID.ToString().ToInt32()))
            {
                var ObjWareModel = ObjWarningMessageBLL.GetOnlyEmployeeID(EmpLoyeeID.ToString().ToInt32());

                string ReturnString = "<a Title='" + ObjWareModel.MessAgeTitle + "' href='/AdminPanlWorkArea/Commandanddispatch/WarningMessageforEmployee.aspx?EmployeeID=" + EmpLoyeeID.ToString() + "&NeedPopu=1'><img style='border:solid;border-color:red;width: 80px; height: 80px;' src='" + ImgUrl + "' /></a></br>";
                ReturnString += "<a Title='" + ObjWareModel.MessAgeTitle + "'  href='/AdminPanlWorkArea/Commandanddispatch/WarningMessageforEmployee.aspx?EmployeeID=" + EmpLoyeeID.ToString() + "&NeedPopu=1'>" + ObjEmpLoyeeModel.EmployeeName + "</a></br>";
                ReturnString += "<div style='width:100px'>今日去向：" + objEmployeeBLL.GetCurrentLocation(Convert.ToInt32(EmpLoyeeID)) + "</div>";

                if (ObjDepartmetnModel.DepartmentManager == User.Identity.Name.ToInt32())
                {

                    ReturnString += "<a class='btn btn-primary  btn-mini' href='/AdminPanlWorkArea/Flows/Mission/FL_MissionDetailedCreate.aspx?EmployeeID=" + EmpLoyeeID.ToString() + "&NeedPopu=1'>下达任务</a>&nbsp;";
                    ReturnString += "<a class='btn btn-primary  btn-mini'  onclick='MessageBorde(" + EmpLoyeeID + ")'  href='#'>留言</a>";
                }
                else
                {
                    ReturnString += "<a class='btn btn-primary  btn-mini' href='/AdminPanlWorkArea/Flows/Mission/FL_MissionDetailedCreate.aspx?EmployeeID=" + EmpLoyeeID.ToString() + "&NeedPopu=1'>下达任务</a>&nbsp;";
                    ReturnString += "<a class='btn btn-primary  btn-mini'  onclick='MessageBorde(" + EmpLoyeeID + ")'  href='#'>留言</a>";
                }
                return ReturnString;
            }
            else
            {
                string ReturnString = "<img style='width: 80px; height: 80px;border:solid;border-color:green;'   src='" + ImgUrl + "' alt='" + ObjEmpLoyeeModel.EmployeeName + "' /></br>";
                ReturnString += "<a href='/AdminPanlWorkArea/ControlPage/MessageBoardPage.aspx?EmployeeID=" + EmpLoyeeID.ToString() + "&NeedPopu=1'>" + ObjEmpLoyeeModel.EmployeeName + "</a></br>";
                ReturnString += "<div style='width:100px'>今日去向：" + objEmployeeBLL.GetCurrentLocation(Convert.ToInt32(EmpLoyeeID)) + "</div>";
                if (ObjDepartmetnModel.DepartmentManager == User.Identity.Name.ToInt32())
                {

                    ReturnString += "<a class='btn btn-primary  btn-mini' href='/AdminPanlWorkArea/Flows/Mission/FL_MissionDetailedCreate.aspx?EmployeeID=" + EmpLoyeeID.ToString() + "&NeedPopu=1'>下达任务</a>&nbsp;";
                    ReturnString += "<a class='btn btn-primary  btn-mini' onclick='MessageBorde(" + EmpLoyeeID + ")'  href='#'>留言</a>";
                }
                else
                {
                    ReturnString += "<a class='btn btn-primary  btn-mini' href='/AdminPanlWorkArea/Flows/Mission/FL_MissionDetailedCreate.aspx?EmployeeID=" + EmpLoyeeID.ToString() + "&NeedPopu=1'>下达任务</a>&nbsp;";
                    ReturnString += "<a class='btn btn-primary  btn-mini' onclick='MessageBorde(" + EmpLoyeeID + ")' href='#'>留言</a>";
                }
                return ReturnString;
            }
        }
        #endregion

        #region 获取间接下属
        /// <summary>
        /// 间接下属
        /// </summary>
        public string GetSecondURL(object EmpLoyeeID)
        {
            WarningMessage ObjWarningMessageBLL = new WarningMessage();
            var ObjEmpLoyeeModel = objEmployeeBLL.GetByID(EmpLoyeeID.ToString().ToInt32());
            var ObjDepartmetnModel = ObjDepartmentBLL.GetByID(ObjEmpLoyeeModel.DepartmentID);
            string ImgUrl = string.Empty;
            if (ObjEmpLoyeeModel.ImageURL != null && ObjEmpLoyeeModel.ImageURL != string.Empty)
            {
                ImgUrl = ObjEmpLoyeeModel.ImageURL;
            }
            else
            {
                ImgUrl = "/AdminPanlWorkArea/Commandanddispatch/GeneralManagerPodium/defaultImage.jpg";
            }

            string Address = objEmployeeBLL.GetCurrentLocation(Convert.ToInt32(EmpLoyeeID)).ToString();
            string location = Address;
            //string location = Address.Length >= 12 ? Address : Address.Substring(0, 11) + "……";
            if (Address.Length > 6)
            {
                location = Address.Substring(0, 6) + "……";

            }
            else
            {
                location = Address;
            }

            if (ObjWarningMessageBLL.HaveWaring(EmpLoyeeID.ToString().ToInt32()))
            {
                var ObjWareModel = ObjWarningMessageBLL.GetOnlyEmployeeID(EmpLoyeeID.ToString().ToInt32());
                string ReturnString = "<a Title='" + ObjWareModel.MessAgeTitle + "' href='/AdminPanlWorkArea/Commandanddispatch/WarningMessageforEmployee.aspx?EmployeeID=" + EmpLoyeeID.ToString() + "&NeedPopu=1'><img style='border:solid;border-color:red;width: 80px; height: 80px;' src='" + ImgUrl + "' alt='" + ObjEmpLoyeeModel.EmployeeName + "' /></a></br>";
                ReturnString += "<a Title='" + ObjWareModel.MessAgeTitle + "'  href='/AdminPanlWorkArea/Commandanddispatch/WarningMessageforEmployee.aspx?EmployeeID=" + EmpLoyeeID.ToString() + "&NeedPopu=1'>" + ObjEmpLoyeeModel.EmployeeName + "</a></br>";
                ReturnString += "<div style='width:100px;'>今日去向：" + location + "</div>";
                if (ObjDepartmetnModel.DepartmentManager == User.Identity.Name.ToInt32())
                {

                    ReturnString += "<a class='btn btn-primary  btn-mini'  onclick='MessageBorde(" + EmpLoyeeID + ")'  href='#'>留言</a>";
                }
                else
                {
                    ReturnString += "<a class='btn btn-primary  btn-mini'  onclick='MessageBorde(" + EmpLoyeeID + ")'  href='#'>留言</a>";
                }
                return ReturnString;
            }
            else
            {
                string ReturnString = "<img style='width: 80px; height: 80px;border:solid;border-color:green;'   src='" + ImgUrl + "' alt='" + ObjEmpLoyeeModel.EmployeeName + "' /></br>";
                ReturnString += "<a href='/AdminPanlWorkArea/ControlPage/MessageBoardPage.aspx?EmployeeID=" + EmpLoyeeID.ToString() + "&NeedPopu=1'>" + ObjEmpLoyeeModel.EmployeeName + "</a></br>";
                ReturnString += "<div style='width:100px;'>今日去向：" + location + "</div>";

                if (ObjDepartmetnModel.DepartmentManager == User.Identity.Name.ToInt32())
                {

                    ReturnString += "<a class='btn btn-primary  btn-mini' onclick='MessageBorde(" + EmpLoyeeID + ")'  href='#'>留言</a>";
                }
                else
                {
                    ReturnString += "<a class='btn btn-primary  btn-mini' onclick='MessageBorde(" + EmpLoyeeID + ")' href='#'>留言</a>";
                }
                return ReturnString;
            }
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询功能
        /// </summary>
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            if (DepartmentDropdownList1.SelectedItem != null)
            {
                int EmployeeID = User.Identity.Name.ToInt32();

                var FirstEmployeeList = objEmployeeBLL.GetMyManagerEmpLoyee(EmployeeID, true);


                //var SecondEmplOyeeList = objEmployeeBLL.GetMyManagerEmpLoyee(EmployeeID, false);
                //foreach (var ObjFirstItem in FirstEmployeeList)
                //{
                //    var RemoveItem = SecondEmplOyeeList.FirstOrDefault(C => C.EmployeeID == ObjFirstItem.EmployeeID);
                //    if (RemoveItem != null)
                //    {
                //        SecondEmplOyeeList.Remove(RemoveItem);
                //    }
                //}

                //DataList2.DataSource = SecondEmplOyeeList.Where(C => C.DepartmentID == DepartmentDropdownList1.SelectedValue.ToInt32());
                //DataList2.DataBind();

                var ObjEmployeeeList = objEmployeeBLL.GetByALLDepartmetnID(DepartmentDropdownList1.SelectedValue.ToInt32());
                DataList2.DataSource = ObjEmployeeeList;
                DataList2.DataBind();
            }
        }
        #endregion


    }
}