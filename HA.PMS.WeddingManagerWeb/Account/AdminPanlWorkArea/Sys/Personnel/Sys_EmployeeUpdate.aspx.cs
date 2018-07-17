
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.13
 Description:雇员
 History: 员工修改页面
 
 Author:杨洋
 date:2013.3.13
 version:好爱1.0
 description:修改描述
 
 
 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.SysTarget;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel
{
    public partial class Sys_EmployeeUpdate : SystemPage
    {
        Employee objEmployeeBLL = new Employee();
        EmployeeJobI objJobBLL = new EmployeeJobI();
        Department objDepartmenttBLL = new Department();
        EmployeeType objEmployeeTypeBLL = new EmployeeType();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int employeeId = StringControl.ToInt32(Request.QueryString["employeeId"]);
                Sys_Employee sys_Employee = objEmployeeBLL.GetByID(employeeId);
                txtName.Text = sys_Employee.EmployeeName;
                txtPhone.Text = sys_Employee.CellPhone;
                //判断用员工当前的信息如果是男的radiobutton按钮为选中状态
                if (sys_Employee.Sex == 0)
                {
                    rdoMan.Checked = true;
                }
                else
                {
                    rdoWoman.Checked = false;
                }

                txtBirthday.Text = sys_Employee.BornDate.ToShortDateString();       //生日
                txtImage.Text = sys_Employee.UploadImageName == null ? "" : sys_Employee.UploadImageName;
                txtLoginName.Text = sys_Employee.LoginName.ToString();
                txtPhone.Text = sys_Employee.CellPhone.ToString();
                txtTellPhone.Text = sys_Employee.TelPhone.ToString();
                txtCurrentLocation.Text = sys_Employee.CurrentLocation;
                txtQQ.Text = sys_Employee.QQ.ToString();
                txtWeiXin.Text = sys_Employee.WeiXin;
                txtWeiBo.Text = sys_Employee.WeiBo;
                txtEmail.Text = sys_Employee.Email;
                txtCardId.Text = sys_Employee.CardId == null ? "" : sys_Employee.CardId.ToString();
                txtBankName.Text = sys_Employee.BankName;
                txtBankCard.Text = sys_Employee.BankCard;
                txtFileCardId.Text = sys_Employee.UploadCardIdName == null ? "" : sys_Employee.UploadCardIdName.ToString();
                txtEmpLoyee.Text = GetEmployeeName(sys_Employee.PlanChecks.Value.ToString());
                txtEntryTime.Text = sys_Employee.EntryTime.ToString();
                txtPositiveTime.Text = sys_Employee.PosiTime != null ? "" : sys_Employee.PosiTime.ToString();
                txtWorkNumber.Text = sys_Employee.WorkNumber.ToString();
                txtBackUps.Text = sys_Employee.BackUps;
                txtRemark.Text = sys_Employee.Remark;

                if (Convert.ToBoolean(sys_Employee.IsDelete))
                {
                    trLoginName.Visible = false;
                    trPassWord.Visible = false;
                }

                if (sys_Employee.Sex == 0)
                {
                    rdoMan.Checked = true;
                }
                else
                {
                    rdoMan.Checked = false;
                    rdoWoman.Checked = true;
                }


                //默认
                sys_Employee.Employeekey = "默认";
                //txtCoach.Text = sys_Employee.Coach.ToString();
                //txtLook.Text = sys_Employee.Look.ToString();
                //在页面初始化加载之前的绑定对应的下拉框
                DataBinder(sys_Employee.JobID.Value, sys_Employee.DepartmentID, sys_Employee.EmployeeTypeID);
            }
        }
        /// <summary>
        /// 绑定员工的工作，部门，员工类型
        /// </summary>
        protected void DataBinder(int jobId, int departmentId, int employeeTypeId)
        {
            #region 员工工作 绑定
            ddlJob.DataSource = objJobBLL.GetByAll();
            ddlJob.DataTextField = "Jobname";
            ddlJob.DataValueField = "JobID";
            ddlJob.DataBind();
            //之前信息默认为第一项
            ListItem jobListItem = ddlJob.Items.FindByValue(jobId.ToString());
            if (jobListItem != null)
            {
                jobListItem.Selected = true;
            }
            #endregion

            #region 部门数据源绑定
            ddlDepartment.DataSource = objDepartmenttBLL.GetByAll();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();
            ListItem departmentListItem = ddlDepartment.Items.FindByValue(departmentId.ToString());
            if (departmentListItem != null)
            {
                departmentListItem.Selected = true;
            }
            #endregion
            //之前信息默认为第一项

            //暂时保留
            //ddlGroup.DataSource = objJobBLL.GetByAll();
            //ddlGroup.DataTextField = "Jobname";
            //ddlGroup.DataValueField = "JobID";
            //ddlGroup.DataBind();

            #region 员工类型绑定
            ddlEmployeeType.DataSource = objEmployeeTypeBLL.GetByAll();
            ddlEmployeeType.DataTextField = "Type";
            ddlEmployeeType.DataValueField = "EmployeeTypeID";
            ddlEmployeeType.DataBind();
            //之前信息默认为第一项
            ListItem employeeListItem = ddlEmployeeType.Items.FindByValue(employeeTypeId.ToString());
            if (employeeListItem != null)
            {
                employeeListItem.Selected = true;
            }
            #endregion
        }

        #region 点击保存
        /// <summary>
        /// 保存功能
        /// </summary>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (objEmployeeBLL.IsLoginNameExistExceptSelf(txtLoginName.Text.Trim(), Request.QueryString["employeeId"].ToInt32()))
            {
                JavaScriptTools.AlertWindow("该用户名已存在，请更换用户名", Page);
                return;
            }
            int employeeId = Request.QueryString["employeeId"].ToInt32();
            Sys_Employee sys_Employee = objEmployeeBLL.GetByID(employeeId);

            string FileAddress = string.Empty;

            #region 上传个人头像  身份证复印件


            if (imageUpload.HasFile)
            {
                FileAddress = "/AdminPanlWorkArea/Sys/Personnel/PersonnelImage/" + Guid.NewGuid().ToString() + ".jpg";

                if (System.IO.Directory.Exists(Server.MapPath("/AdminPanlWorkArea/Sys/Personnel/PersonnelImage/")))
                {
                    imageUpload.SaveAs(Server.MapPath(FileAddress));
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("/AdminPanlWorkArea/Sys/Personnel/PersonnelImage/"));
                    imageUpload.SaveAs(Server.MapPath(FileAddress));
                }
                sys_Employee.ImageURL = FileAddress;
                sys_Employee.UploadImageName = imageUpload.FileName.ToString();
            }
            else
            {
                if (txtImage.Text.Trim().ToString() == string.Empty)    //未上传文件
                {
                    JavaScriptTools.AlertWindow("不能上传这种类型的文件", Page);
                }
            }


            //身份证号

            if (FileUpCardId.HasFile)
            {
                FileAddress = "/AdminPanlWorkArea/Sys/Personnel/PersonIdCard/" + Guid.NewGuid().ToString() + ".jpg";

                if (System.IO.Directory.Exists(Server.MapPath("/AdminPanlWorkArea/Sys/Personnel/PersonIdCard/")))
                {
                    FileUpCardId.SaveAs(Server.MapPath(FileAddress));
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("/AdminPanlWorkArea/Sys/Personnel/PersonIdCard/"));
                    FileUpCardId.SaveAs(Server.MapPath(FileAddress));
                }
                sys_Employee.CardIdUrl = FileAddress;
                sys_Employee.UploadCardIdName = FileUpCardId.FileName.ToString();
            }
            else
            {
                if (txtFileCardId.Text.Trim().ToString() == string.Empty)    //未上传文件
                {
                    JavaScriptTools.AlertWindow("不能上传这种类型的文件", Page);
                }
            }

            #endregion


            sys_Employee.EmployeeName = txtName.Text;
            sys_Employee.JobID = ddlJob.SelectedValue.ToInt32();
            sys_Employee.DepartmentID = ddlDepartment.SelectedValue.ToInt32();
            FinishTargetSum ObjFinishTargetBLL = new FinishTargetSum();
            FL_FinishTargetSum TargetModel = ObjFinishTargetBLL.GetByEmployeeID(Request["employeeId"].ToInt32());
            if (TargetModel != null)
            {
                TargetModel.DepartmentID = ddlDepartment.SelectedValue.ToInt32();
                ObjFinishTargetBLL.Update(TargetModel);
            }
            sys_Employee.GroupID = 1;

            if (ddlEmployeeType.SelectedValue.ToInt32() > 0)
            {
                sys_Employee.EmployeeTypeID = ddlEmployeeType.SelectedValue.ToInt32();
            }
            sys_Employee.LoginName = txtLoginName.Text;
            sys_Employee.PassWord = txtPassWord.Text.Trim().ToString() == "" ? sys_Employee.PassWord : txtPassWord.Text.Trim().MD5Hash();
            //判断性别男选中之后 为0
            sys_Employee.Sex = rdoMan.Checked ? 0 : 1;
            sys_Employee.BornDate = txtBirthday.Text.ToDateTime();
            sys_Employee.CellPhone = txtPhone.Text.Trim().ToString();
            sys_Employee.TelPhone = txtTellPhone.Text.Trim().ToString();
            sys_Employee.CurrentLocation = txtCurrentLocation.Text.Trim().ToString();
            sys_Employee.QQ = txtQQ.Text.Trim().ToString();
            sys_Employee.WeiXin = txtWeiXin.Text.Trim().ToString();
            sys_Employee.WeiBo = txtWeiBo.Text.Trim().ToString();
            sys_Employee.Email = txtEmail.Text.Trim().ToString();
            sys_Employee.CardId = txtCardId.Text.Trim().ToString();
            sys_Employee.BankName = txtBankName.Text.Trim().ToString();
            sys_Employee.BankCard = txtBankCard.Text.Trim().ToString();
            sys_Employee.PlanChecks = hideEmpLoyeeID.Value.ToInt32();
            sys_Employee.EntryTime = txtEntryTime.Text.Trim().ToString().ToDateTime();
            sys_Employee.PosiTime = txtPositiveTime.Text.Trim().ToString().ToDateTime();
            sys_Employee.WorkNumber = txtWorkNumber.Text.Trim().ToInt32();
            sys_Employee.BackUps = txtBackUps.Text.Trim().ToString();
            sys_Employee.Remark = txtRemark.Text.Trim().ToString();
            if (!Convert.ToBoolean(sys_Employee.IsDelete))
            {
                sys_Employee.LoginName = txtLoginName.Text.Trim();
                if (txtPassWord.Text != string.Empty)
                {
                    sys_Employee.PassWord = txtPassWord.Text.MD5Hash();
                }
            }
            sys_Employee.IsDelete = false;
            DateTime nowDate = DateTime.Now;
            sys_Employee.CreateDate = nowDate;
            sys_Employee.IsClose = true;
            sys_Employee.LoginYear = nowDate.Year;
            sys_Employee.LoginMonth = nowDate.Month;
            sys_Employee.LoginDay = nowDate.Day;
            //默认
            sys_Employee.Employeekey = User.Identity.Name;
            sys_Employee.CreateEmployee = User.Identity.Name.ToInt32();

            int result = objEmployeeBLL.Update(sys_Employee);
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("修改失败,请重新尝试", this.Page);

            }
        }
        #endregion

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEmpLoyee.Text = GetEmployeeName(objDepartmenttBLL.GetByID(ddlDepartment.SelectedValue.ToInt32()).DepartmentManager);
        }

    }
}