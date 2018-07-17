using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.Designclass
{
    public partial class DesignImageShowList : SystemPage
    {
        DesignUpload ObjDesignUploadBLL = new DesignUpload();
        int DesignID = 0;
        int Type = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DesignID = Request["DesignClassID"].ToInt32();
                BinderData();
            }
        }

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>

        public void BinderData()
        {
            DesignID = Request["DesignClassID"].ToInt32();
            Type = Request["Type"].ToInt32();
            repfilelist.DataSource = ObjDesignUploadBLL.GetByDesignClassID(DesignID, Type);
            repfilelist.DataBind();

        }
        #endregion

        protected void repfilelist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int DuId = e.CommandArgument.ToString().ToInt32();
            FL_DesignUpload ObjUploadModel = ObjDesignUploadBLL.GetByID(DuId);
            if (e.CommandName == "DownLoad")
            {
                //1.获取文件虚拟路径
                string fileLocalPath = GetServerPath(ObjUploadModel.FileAddress);
                //2.下载
                try
                {
                    IOTools.DownLoadFile(Server.MapPath(fileLocalPath), ObjUploadModel.FileName);
                }
                catch (Exception ex) { JavaScriptTools.AlertWindow("下载失败！该文件可能已被移除", Page); }
            }
            if (e.CommandName == "Delete")
            {
                int result = ObjDesignUploadBLL.Delete(ObjUploadModel);
                if (result > 0)
                {
                    //JavaScriptTools.AlertWindow("删除成功", Page);
                    BinderData();
                }
                else
                {
                    JavaScriptTools.AlertWindow("删除失败,请稍后再试...", Page);
                }
            }
        }

        protected string GetServerPath(object source)
        {
            //获取程序根目录
            string tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());
            string imagesurl = (source + string.Empty).Replace(tmpRootDir, "");
            return imagesurl = "/" + imagesurl.Replace(@"\", @"/");
        }

    }
}