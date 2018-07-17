
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.12
 Description:员工详细页面
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
using System.IO;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel
{
    public partial class Sys_EmployeeDetails : SystemPage
    {
        Employee objEmployeeBLL = new Employee();
        EmployeeJobI objJobBLL = new EmployeeJobI();
        Department objDepartmenttBLL = new Department();
        EmployeeType objEmployeeTypeBLL = new EmployeeType();

        EmployeeDataFile ObjDataFileBLL = new EmployeeDataFile();
        protected void Page_Load(object sender, EventArgs e)
        {
            //employeeId
            if (!IsPostBack)
            {
                //由于员工信息较多，所以本页只提供查看某个员工的所有信息记录
                int employeeId = Request.QueryString["employeeId"].ToInt32();
                Sys_Employee sys_Employee = objEmployeeBLL.GetByID(employeeId);
                ltlName.Text = sys_Employee.EmployeeName;
                ltlPhone.Text = sys_Employee.CellPhone;


                ltlGroup.Text = sys_Employee.GroupID.ToString();

                //获取员工工作名称
                ltlJob.Text = objJobBLL.GetByID(sys_Employee.JobID).Jobname;
                string sexStr = sys_Employee.Sex.ToString();
                ltlSex.Text = sexStr == "0" ? "男" : "女";
                ltlEmail.Text = sys_Employee.Email;
                //获取员工Type类型
                var ObjEmplyoeeTypeModel = objEmployeeTypeBLL.GetByID(sys_Employee.EmployeeTypeID);
                ltlEmplyoeeType.Text = object.ReferenceEquals(ObjEmplyoeeTypeModel, null) ? string.Empty : ObjEmplyoeeTypeModel.Type;
                ltlCellPhone.Text = sys_Employee.TelPhone;
                //获取这个员工所在部门的名称
                ltlDepartment.Text = objDepartmenttBLL.GetByID(sys_Employee.DepartmentID).DepartmentName;
                ltlQQ.Text = sys_Employee.QQ;
                ltlWeiXin.Text = sys_Employee.WeiXin;
                ltlWeiBo.Text = sys_Employee.WeiBo;

                ltlCardId.Text = sys_Employee.CardId;
                ltlBankName.Text = sys_Employee.BankName;
                ltlBankCardId.Text = sys_Employee.BankCard;
                ltlPlanChecks.Text = GetEmployeeName(sys_Employee.PlanChecks);
                ltlEntryTime.Text = sys_Employee.EntryTime.ToString();
                ltlPosiTime.Text = sys_Employee.PosiTime.ToString();
                ltlWorkNumber.Text = sys_Employee.WorkNumber.ToString();
                imgPerson.ImageUrl = sys_Employee.ImageURL;
                imgCardId.ImageUrl = sys_Employee.CardIdUrl;

                rptDataFile.DataSource = ObjDataFileBLL.GetByEmployeeID(employeeId);
                rptDataFile.DataBind();


            }
        }

        #region 下载文件
        /// <summary>
        /// 下载
        /// </summary> 
        protected void rptDataFile_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int DataId = e.CommandArgument.ToString().ToInt32();
            var DataModel = ObjDataFileBLL.GetByID(DataId);         //子级
            TextBox txtDataname = e.Item.FindControl("txtDataname") as TextBox;
            Label lblDataname = e.Item.FindControl("lblDataname") as Label;
            if (e.CommandName == "DownLoad")
            {
                TransmitFile(DataModel.DataUrl, DataModel.DataName);
            }
            else if (e.CommandName == "Delete")
            {
                try
                {
                    File.Delete(Server.MapPath(DataModel.DataUrl));
                    int result = ObjDataFileBLL.Delete(DataModel);
                    if (result > 0)
                    {
                        JavaScriptTools.AlertWindowAndLocation("删除成功", Page.Request.Url.ToString(), Page);
                    }
                    else
                    {
                        JavaScriptTools.AlertWindow("删除失败,请稍后再试...", Page);
                    }
                }
                catch
                {
                    Response.Redirect(Page.Request.Url.ToString());
                }
            }
        }
        #endregion

        #region 文件下载
        /// <summary>
        /// 使用微软的TransmitFile下载文件
        /// </summary>
        /// <param name="filePath">服务器相对路径</param>
        public void TransmitFile(string filePath, string fileName)
        {
            try
            {
                filePath = Server.MapPath(filePath);
                if (File.Exists(filePath))
                {
                    FileInfo info = new FileInfo(filePath);
                    long fileSize = info.Length;
                    HttpContext.Current.Response.Clear();

                    //指定Http Mime格式为压缩包
                    HttpContext.Current.Response.ContentType = "application/x-zip-compressed";

                    // Http 协议中有专门的指令来告知浏览器, 本次响应的是一个需要下载的文件. 格式如下:
                    // Content-Disposition: attachment;filename=filename.txt
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                    //不指明Content-Length用Flush的话不会显示下载进度   
                    HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                    HttpContext.Current.Response.TransmitFile(filePath, 0, fileSize);
                    HttpContext.Current.Response.Flush();
                }
            }
            catch
            { }
            finally
            {
                HttpContext.Current.Response.Close();
            }

        }


        public void DownLoadFile(string fileUrl, string fileName)
        {
            string selectName = Server.MapPath(fileUrl);
            string saveFileName = fileName;              //创建一个文件实体，方便对文件操作             
            FileInfo finfo = new FileInfo(selectName);             //清空输出流              
            Response.Clear();
            Response.Charset = "utf-8";
            Response.Buffer = true;              //关闭输出文件编码及类型和文件名              
            this.EnableViewState = false;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + saveFileName);
            //因为保存的文件类型不限，此处类型选择“unknown”             
            Response.ContentType = "application/unknown";
            Response.WriteFile(selectName);             //清空并关闭输出流 
            Response.Flush();
            Response.Close();
            Response.End();
        }

        #endregion

        public string GetDataName(string DataName)
        {
            string[] name = DataName.Split('.');
            string names = name[0].ToString();
            string suff = name[1].ToString();
            if (name[0].Length > 8)
            {
                return names.Substring(0, 8) + "…" + "." + suff;
            }
            else
            {
                return names + "." + suff;
            }
        }

        #region 点击下载方法
        /// <summary>
        /// 下载
        /// </summary> 
        protected void lbtnDownLoad1_Click(object sender, EventArgs e)
        {
            LinkButton lbtnDownLoad = (sender as LinkButton);
            int employeeId = Request.QueryString["employeeId"].ToInt32();
            var EmployeeModel = objEmployeeBLL.GetByID(employeeId);
            if (lbtnDownLoad.ID == "lbtnDownLoad1")
            {
                //1.获取文件虚拟路径
                string fileLocalPath = GetServerPath(EmployeeModel.ImageURL);
                //2.下载
                try
                {
                    IOTools.DownLoadFile(Server.MapPath(fileLocalPath), EmployeeModel.UploadImageName);
                }
                catch (Exception ex) { JavaScriptTools.AlertWindow("下载失败！该文件可能已被移除", Page); }
            }
            else if (lbtnDownLoad.ID == "lbtnDownLoad2")
            {
                //1.获取文件虚拟路径
                string fileLocalPath = GetServerPath(EmployeeModel.CardIdUrl);
                //2.下载
                try
                {
                    IOTools.DownLoadFile(Server.MapPath(fileLocalPath), EmployeeModel.UploadCardIdName);
                }
                catch (Exception ex) { JavaScriptTools.AlertWindow("下载失败！该文件可能已被移除", Page); }
            }
        }
        #endregion

        #region 头像 身份证复印件 下载方法

        protected string GetServerPath(object source)
        {
            //获取程序根目录
            string tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());
            string imagesurl = (source + string.Empty).Replace(tmpRootDir, "");
            return imagesurl = "/" + imagesurl.Replace(@"\", @"/");
        }
        #endregion
    }
}