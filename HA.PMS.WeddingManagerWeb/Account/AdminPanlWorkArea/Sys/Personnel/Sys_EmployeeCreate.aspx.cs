
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.12
 Description:员工创建页面
 History:修改日志
 
 Author:杨洋
 date:2013.3.12
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
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel
{
    public partial class Sys_EmployeeCreate : SystemPage
    {
        Employee objEmployeeBLL = new Employee();
        EmployeeJobI objJobBLL = new EmployeeJobI();
        Department objDepartmenttBLL = new Department();
        EmployeeType objEmployeeTypeBLL = new EmployeeType();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定用于员工选择的相关下拉框的数据方法
                DataBinder();
            }
        }

        /// <summary>
        /// 绑定员工的工作，部门，员工类型
        /// </summary>
        protected void DataBinder()
        {
            #region 员工对应的工作绑定
            ddlJob.DataSource = objJobBLL.GetByAll();
            ddlJob.DataTextField = "Jobname";
            ddlJob.DataValueField = "JobID";
            ddlJob.DataBind();


            #endregion

            #region 部门下拉框的绑定
            lblDepartment.Text = objDepartmenttBLL.GetByID(Request["DepartmentID"].ToInt32()).DepartmentName;
            hideEmpLoyeeID.Value = objDepartmenttBLL.GetByID(Request["DepartmentID"].ToInt32()).DepartmentManager.ToString();
            txtEmpLoyee.Text = GetEmployeeName(objDepartmenttBLL.GetByID(Request["DepartmentID"].ToInt32()).DepartmentManager.ToString());
            #endregion
            //暂时保留
            //ddlGroup.DataSource = objJobBLL.GetByAll();
            //ddlGroup.DataTextField = "Jobname";
            //ddlGroup.DataValueField = "JobID";
            //ddlGroup.DataBind();

            #region 员工类型的绑定
            ddlEmployeeType.DataSource = objEmployeeTypeBLL.GetByAll();
            ddlEmployeeType.DataTextField = "Type";
            ddlEmployeeType.DataValueField = "EmployeeTypeID";
            ddlEmployeeType.DataBind();
            #endregion


        }
        /// <summary>
        /// 创建员工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            #region 创建


            //构建员工实体类对象
            Sys_Employee sys_Employee = new Sys_Employee();
            if (objEmployeeBLL.IsLoginNameExist(txtLoginName.Text.Trim()))
            {
                JavaScriptTools.AlertWindow("你的登陆账号已重复,请更换", this.Page);
                return;
            }
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
                JavaScriptTools.AlertWindow("不能上传这种类型的文件", Page);
            }

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
                JavaScriptTools.AlertWindow("不能上传这种类型的文件", Page);
            }
            #endregion

            sys_Employee.EmployeeName = txtName.Text;
            sys_Employee.JobID = ddlJob.SelectedValue.ToInt32();
            sys_Employee.DepartmentID = Request["DepartmentID"].ToInt32();
            sys_Employee.GroupID = 1;

            if (ddlEmployeeType.SelectedValue.ToInt32() > 0)
            {
                sys_Employee.EmployeeTypeID = ddlEmployeeType.SelectedValue.ToInt32();
            }
            sys_Employee.LoginName = txtLoginName.Text;
            sys_Employee.PassWord = txtPwd.Text.Trim().ToString() == "" ? "123456".MD5Hash() : txtPwd.Text.Trim().MD5Hash();
            //判断性别男选中之后 为0
            sys_Employee.Sex = rdoMan.Checked ? 0 : 1;
            sys_Employee.BornDate = txtBirthday1.Text.ToDateTime();
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

            //sys_Employee.GroupID = StringControl.ToInt32(ddlGroup.SelectedValue);
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


            //sys_Employee.Coach = txtCoach.Text.ToInt32();
            //sys_Employee.Look = txtLook.Text.ToInt32();

            int result = objEmployeeBLL.Insert(sys_Employee);
            if (result > 0)
            {
                UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();

                JurisdictionforButton ObjJurisdictionforButtonBLL = new JurisdictionforButton();
                if (ObjUserJurisdictionBLL.StarUserJurisdiction(sys_Employee.EmployeeID))
                {
                    ObjJurisdictionforButtonBLL.StarJurisdictionforButton(sys_Employee.EmployeeID);
                }

                //保存操作日志
                CreateHandle();
                JavaScriptTools.AlertAndClosefancybox("添加成功,初始化权限完毕，请立即为用户授权!", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("添加失败,请重新尝试", this.Page);

            }
            #endregion
        }

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();

            HandleModel.HandleContent = "系统设置-添加新员工,员工姓名:" + txtName.Text.Trim().ToString() + "部门：" + lblDepartment.Text.Trim().ToString();

            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 9;     //系统设置
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion
    }
}