using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases
{
    public partial class FD_TheCaseMovieChoose : SystemPage
    {
        CaseFile objCaseFileBLL = new CaseFile();
        //视频文件路径
        string CaseMovieFilePath = "~/Files/TheCase/TheCaseMovie/";

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }
        #endregion

        #region 点击保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            int checkCount = 0;//选择文件的个数
            int CaseID = Request.QueryString["CaseID"].ToInt32();//获取传递过来的 案例编号ID
            for (int i = 0; i < rptGuradian.Items.Count; i++)
            {
                CheckBox chSinger = rptGuradian.Items[i].FindControl("chSinger") as CheckBox;
                if (chSinger.Checked)
                {
                    //取出文件名
                    Literal ltlGuradianFileName = rptGuradian.Items[i].FindControl("ltlGuradianFileName") as Literal;
                    //取出文件路径
                    Literal ltlGuradianFilePath = rptGuradian.Items[i].FindControl("ltlGuradianFilePath") as Literal;
                    //声明
                    FD_CaseFile objCaseFile = new FD_CaseFile();
                    checkCount++;//选择文件个数加1
                    //保存视频文件信息到实体对象
                    objCaseFile.FileType = 1;
                    objCaseFile.CaseId = CaseID;
                    objCaseFile.CaseFileName = ltlGuradianFileName.Text;
                    objCaseFile.CaseFilePath = CaseMovieFilePath + ltlGuradianFileName.Text;
                    //保存到数据库
                    objCaseFileBLL.Insert(objCaseFile);
                }
            }
            if (checkCount > 0)//如果有选择文件
            {
                JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("请你选择文件", this.Page);
            }
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询功能
        /// </summary>
        protected void btnLook_Click(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定
        /// </summary>
        public void BinderData()
        {
            //获取存储视频文件的目录的绝对路径
            string FilePath = Server.MapPath(CaseMovieFilePath);
            //获得该目录的信息
            DirectoryInfo ObjDirInfo = new DirectoryInfo(FilePath);
            //获得当前目录以及其所有子目录的文件信息
            FileInfo[] ObjAllFileInfo = ObjDirInfo.GetFiles("*", SearchOption.AllDirectories);
            //声明List
            List<FD_GuradianFile> ListGuradianFile = new List<FD_GuradianFile>();
            //声明
            FD_GuradianFile ObjGruadianFile = null;
            //遍历所有文件
            foreach (FileInfo ItemFileInfo in ObjAllFileInfo)
            {
                if (txtCasename.Text.Trim() != "")      //条件查询
                {
                    string name = txtCasename.Text.Trim().ToString();
                    ObjGruadianFile = new FD_GuradianFile();
                    //保存文件全路径
                    ObjGruadianFile.GuradianFilePath = ItemFileInfo.FullName;
                    //保存文件名
                    ObjGruadianFile.GuradianFileName = ItemFileInfo.Name;
                    //添加到List
                    if (ObjGruadianFile.GuradianFilePath.Contains(name))
                    {
                        ListGuradianFile.Add(ObjGruadianFile);
                    }
                }
                else
                {    //默认显示
                    ObjGruadianFile = new FD_GuradianFile();
                    //保存文件全路径
                    ObjGruadianFile.GuradianFilePath = ItemFileInfo.FullName;
                    //保存文件名
                    ObjGruadianFile.GuradianFileName = ItemFileInfo.Name;
                    //添加到List
                    ListGuradianFile.Add(ObjGruadianFile);
                }
            }
            //设置数据源
            rptGuradian.DataSource = ListGuradianFile;
            //绑定数据源
            rptGuradian.DataBind();
        }
        #endregion
    }
}