using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company
{
    public partial class CompanyFileCreate : SystemPage
    {
        Department ObjDepartmentBLL = new Department();
        CompanyFile ObjFileBLL = new CompanyFile();

        string Type = string.Empty;
        int FileId = 0;

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type = Request["Type"].ToString();
            FileId = Request["FileID"].ToInt32();
            if (!IsPostBack)
            {
                if (Type == "Add" || Type == "Modify")
                {
                    txtTopName.Visible = true;
                    ddlParentFileName.Visible = false;
                    tr_FielName.Visible = false;

                    if (Type == "Modify")
                    {
                        var FileModel = ObjFileBLL.GetByID(FileId);
                        txtTopName.Text = FileModel.FileName;
                    }
                }
                else if (Type == "ModifyChild")
                {
                    DDLBind();
                    txtTopName.Visible = false;
                    ddlParentFileName.Visible = true;
                    tr_FielName.Visible = true;

                    var Model = ObjFileBLL.GetByID(FileId);
                    ddlParentFileName.SelectedValue = Model.ParentFileId.ToString();
                    string suff = "." + Model.FileName.Split('.')[1];
                    txtFileName.Text = Model.FileName.Replace(suff, "");
                }
            }
        }
        #endregion

        #region 下拉框绑定
        public void DDLBind()
        {
            var DataList = ObjFileBLL.GetByParentId(0);
            ddlParentFileName.DataTextField = "FileName";
            ddlParentFileName.DataValueField = "FileId";
            ddlParentFileName.DataSource = DataList;
            ddlParentFileName.DataBind();
        }
        #endregion

        #region 点击确定
        /// <summary>
        /// 确定
        /// </summary>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            CA_CompanyFile FileModel = new CA_CompanyFile();

            #region 修改顶级
            if (Type == "Modify")
            {
                var Model = ObjFileBLL.GetByName(txtTopName.Text.ToString(), 0);        //查询该名称是否存在
                var Models = ObjFileBLL.GetByID(FileId);

                if (Model != null)          //不是null  就说明改名称已经存在
                {
                    if (Models.FileName == Models.FileName)      //没做修改  还是提示
                    {
                        JavaScriptTools.AlertAndClosefancybox("修改成功", Page);
                    }
                    else            //重复 请更换其他名称类别
                    {
                        JavaScriptTools.AlertWindow("该类别已存在,请更换", Page);
                        return;
                    }
                }
                else            //model等于null  就说明是新的名称
                {
                    string OldeFileName = Models.FileName;
                    string NewFileName = txtTopName.Text.Trim().ToString();
                    string DirName = Server.MapPath(Models.PureRoute);      //旧文件夹
                    string NewDirName = Server.MapPath((Models.PureRoute.Replace(Models.FileName, txtTopName.Text.Trim().ToString())));     //新文件夹
                    Directory.Move(DirName, NewDirName);        //移动到新文件夹(修改文件夹名称)

                    ///修改父级
                    Models.FileName = txtTopName.Text.Trim().ToString();
                    Models.PureRoute = Models.PureRoute.Replace(OldeFileName, NewFileName);
                    Models.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                    ObjFileBLL.Update(Models);

                    ///修改子级
                    var FileList = ObjFileBLL.GetByParentId(FileId);
                    if (FileList.Count > 0)
                    {
                        foreach (var item in FileList)
                        {
                            item.FileURL = item.FileURL.Replace(OldeFileName, NewFileName);
                            item.PureRoute = item.PureRoute.Replace(OldeFileName, NewFileName);
                            item.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                            ObjFileBLL.Update(item);
                        }
                    }
                    JavaScriptTools.AlertAndClosefancybox(Page);
                }
            }
            #endregion

            #region 添加 顶级
            if (Type == "Add")
            {
                FileModel.FileURL = "";
                FileModel.FileName = txtTopName.Text.ToString();
                FileModel.ItemLevel = 1;
                FileModel.ParentFileId = 0;
                FileModel.IsDelete = false;
                FileModel.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                FileModel.CreateEmployee = User.Identity.Name.ToInt32();
                FileModel.Remark = "";
                int result = ObjFileBLL.Insert(FileModel);
                JavaScriptTools.AlertAndClosefancybox(Page);
            }
            #endregion


            #region 修改子级 修改子级的上级

            if (Type == "ModifyChild")
            {

                FileModel = ObjFileBLL.GetByID(FileId);         //数据实体(旧)
                var OldFileModel = ObjFileBLL.GetByID(FileModel.ParentFileId);      //父级数据实体(旧)
                string NewFileName = ObjFileBLL.GetByID(ddlParentFileName.SelectedValue.ToInt32()).FileName;        //新文件夹名称



                string DirName = Server.MapPath(ObjFileBLL.GetByID(FileId).FileURL);
                string NewDirName = Server.MapPath(ObjFileBLL.GetByID(FileId).FileURL.Replace(OldFileModel.FileName, NewFileName));
                string NewPureRoute = Server.MapPath(ObjFileBLL.GetByID(FileId).PureRoute.Replace(OldFileModel.FileName, NewFileName));

                //文件夹 不存在  就新建文件夹
                if (!Directory.Exists(NewPureRoute))
                {
                    Directory.CreateDirectory(NewPureRoute);    //创建文件夹  纯路径
                    //修改纯路径
                    var NewFileModel = ObjFileBLL.GetByName(NewFileName, 0);
                    NewFileModel.PureRoute = ObjFileBLL.GetByID(FileId).PureRoute.Replace(OldFileModel.FileName + "/", NewFileName);      // 加上‘/’ 是为保证统一格式  可以不加
                    ObjFileBLL.Update(NewFileModel);
                }


                ///移动
                File.Move(DirName, NewDirName);      //直接修改文件夹的名称

                ///修改数据
                string FileName = txtFileName.Text.Trim().ToString();
                if (FileName != "")
                {
                    string suff = "." + FileModel.FileName.Split('.')[1];
                    if (ObjFileBLL.GetByName(FileName) == null)
                    {
                        if (!(Title.Contains(suff)))
                        {
                            FileModel.FileName = FileName + suff;
                        }
                        else
                        {
                            FileModel.FileName = FileName;
                        }
                    }
                    else
                    {
                        if (FileModel.FileName == Title + suff)         //当前文件名称等于修改后的名称   就是说明没修改文件名称
                        {

                        }
                        else
                        {
                            JavaScriptTools.AlertWindow("该文件名称已经存在", Page);
                            return;
                        }
                    }
                }
                else
                {
                    JavaScriptTools.AlertWindow("文件名称不能为空", Page);
                    return;
                }
                FileModel.ParentFileId = ddlParentFileName.SelectedValue.ToInt32();
                FileModel.FileURL = FileModel.FileURL.Replace(OldFileModel.FileName, NewFileName);
                FileModel.PureRoute = FileModel.PureRoute.Replace(OldFileModel.FileName, NewFileName);
                FileModel.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                ObjFileBLL.Update(FileModel);

                JavaScriptTools.AlertAndClosefancybox(Page);


            }
            #endregion
        }
        #endregion
    }
}