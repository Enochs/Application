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
using HA.PMS.BLLAssmblly.CS;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company
{
    public partial class CompanySystemUpdate : SystemPage
    {

        CompanySystem objCompanySystemBLL = new CompanySystem();
        int SystemId = 0;
        string Type = "";
        string SystemType = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SystemId = Request["SystemId"].ToInt32();
            Type = Request["Type"].ToString();
            SystemType = Request["SystemType"].ToString();
            if (!IsPostBack)
            {
                CA_CompanySystem sys = objCompanySystemBLL.GetByID(SystemId);
                string suff = "." + sys.SystemTitle.Split('.')[1];
                txtFileName.Text = sys.SystemTitle.Replace(suff, "");

                DataBinder();

                if (Type == "ParentId")
                {
                    tr_Title.Visible = true;
                    tr_Parent.Visible = false;
                    tr_Name.Visible = false;
                    txtSystemTitle.Text = sys.SystemTitle;
                }
                else if (Type == "ChildId")
                {

                    tr_Title.Visible = false;
                    tr_Parent.Visible = true;
                    tr_Name.Visible = true;

                    //找到父级节点
                    ddlParent.Items.FindByValue(sys.ParentID + string.Empty).Selected = true;

                }

            }
        }

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        protected void DataBinder()
        {
            ddlParent.DataSource = objCompanySystemBLL.GetByParentID(0).Where(C => C.Type == SystemType.ToInt32()).ToList();
            ddlParent.DataTextField = "SystemTitle";
            ddlParent.DataValueField = "SystemId";
            ddlParent.DataBind();
        }

        #region 点击保存
        /// <summary>
        /// 保存
        /// </summary> 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CA_CompanySystem SystemModel = new CA_CompanySystem();

            if (Type == "ParentId")             //修改父级   顶级
            {
                var Model = objCompanySystemBLL.GetByName(txtSystemTitle.Text.ToString(), 0);        //查询该名称是否存在
                var Models = objCompanySystemBLL.GetByID(SystemId);

                if (Model != null)
                {
                    if (Models.SystemTitle == Model.SystemTitle)      //没做修改  还是提示
                    {
                        JavaScriptTools.AlertAndClosefancybox("修改成功", Page);
                    }
                    else            //重复 请更换其他名称类别
                    {
                        JavaScriptTools.AlertWindow("该类别已存在,请更换", Page);
                        return;
                    }
                }
                else
                {
                    string OldeFileName = Models.SystemTitle;
                    string NewFileName = txtSystemTitle.Text.Trim().ToString();
                    string DirName = Server.MapPath(Models.SystemPureRoute);      //旧文件夹
                    string NewDirName = Server.MapPath((Models.SystemPureRoute.Replace(Models.SystemTitle, txtSystemTitle.Text.Trim().ToString())));     //新文件夹
                    Directory.Move(DirName, NewDirName);        //移动到新文件夹(修改文件夹名称)

                    ///修改父级
                    Models.SystemTitle = txtSystemTitle.Text.Trim().ToString();
                    Models.SystemPureRoute = Models.SystemPureRoute.Replace(OldeFileName, NewFileName);
                    Models.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                    objCompanySystemBLL.Update(Models);

                    ///修改子级
                    var SystemList = objCompanySystemBLL.GetByParentID(SystemId);
                    if (SystemList.Count > 0)
                    {
                        foreach (var item in SystemList)
                        {
                            item.SystemTitle = item.SystemURL.Replace(OldeFileName, NewFileName);
                            item.SystemPureRoute = item.SystemPureRoute.Replace(OldeFileName, NewFileName);
                            item.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                            objCompanySystemBLL.Update(item);
                        }
                    }
                    JavaScriptTools.AlertAndClosefancybox(Page);
                }
            }
            else if (Type == "ChildId")             //修改子级
            {
                SystemModel = objCompanySystemBLL.GetByID(SystemId);         //数据实体(旧)
                var OldSystemModel = objCompanySystemBLL.GetByID(SystemModel.ParentID);      //父级数据实体(旧)
                string NewSystemTitle = objCompanySystemBLL.GetByID(ddlParent.SelectedValue.ToInt32()).SystemTitle;        //新文件夹名称

                string DirName = Server.MapPath(objCompanySystemBLL.GetByID(SystemId).SystemURL);
                string NewDirName = Server.MapPath(objCompanySystemBLL.GetByID(SystemId).SystemURL.Replace(OldSystemModel.SystemTitle, NewSystemTitle));
                string NewPureRoute = Server.MapPath(objCompanySystemBLL.GetByID(SystemId).SystemPureRoute.Replace(OldSystemModel.SystemTitle, NewSystemTitle));

                //文件夹 不存在  就新建文件夹
                if (!Directory.Exists(NewPureRoute))            //文件夹没有存过文件  纯路径为空 文件夹要创建
                {
                    Directory.CreateDirectory(NewPureRoute);    //创建文件夹  纯路径
                }
                //修改纯路径
                var NewSystemModel = objCompanySystemBLL.GetByName(NewSystemTitle, 0);
                NewSystemModel.SystemPureRoute = objCompanySystemBLL.GetByID(SystemId).SystemPureRoute.Replace(OldSystemModel.SystemTitle + "/", NewSystemTitle);      // 加上‘/’ 是为保证统一格式  可以不加
                objCompanySystemBLL.Update(NewSystemModel);
                ///移动
                File.Move(DirName, NewDirName);      //直接修改文件夹的名称

                ///修改数据
                string Title = txtFileName.Text.Trim().ToString();
                SystemModel.ParentID = ddlParent.SelectedValue.ToInt32();
                if (Title != "")
                {
                    string suff = "." + SystemModel.SystemTitle.Split('.')[1];
                    if (objCompanySystemBLL.GetByTitle(Title) == null)
                    {
                        if (!(Title.Contains(suff)))
                        {
                            SystemModel.SystemTitle = Title + suff;
                        }
                        else
                        {
                            SystemModel.SystemTitle = Title;
                        }
                    }
                    else
                    {
                        if (SystemModel.SystemTitle == Title + suff)         //当前文件名称等于修改后的名称   就是说明没修改文件名称
                        {

                        }
                        else
                        {
                            JavaScriptTools.AlertWindow("该文件名称已经存在", Page);
                            return;
                        }
                        JavaScriptTools.AlertWindow("该文件名称已经存在", Page);
                        return;
                    }
                }
                else
                {
                    JavaScriptTools.AlertWindow("文件名称不能为空", Page);
                    return;
                }
                SystemModel.SystemURL = SystemModel.SystemURL.Replace(OldSystemModel.SystemTitle, NewSystemTitle);
                SystemModel.SystemPureRoute = SystemModel.SystemPureRoute.Replace(OldSystemModel.SystemTitle, NewSystemTitle);
                SystemModel.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                objCompanySystemBLL.Update(SystemModel);

                JavaScriptTools.AlertAndClosefancybox(Page);

            }
        }
        #endregion
    }
}