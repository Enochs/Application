using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.BugSystem
{
    public partial class BugSystemManager : SystemPage
    {
        HA.PMS.BLLAssmblly.Sys.BugSystem ObjBugSystemBLL = new BLLAssmblly.Sys.BugSystem();

        Employee ObjEmployeeBLL = new Employee();

        string OrderByName = "BugID";
        int SourceCount = 0;

        #region 页面加载
        /// <summary>
        /// 页面初始
        /// </summary>  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void BinderData(object sender, EventArgs e)
        {
            List<PMSParameters> pars = new List<PMSParameters>();
            //上传人
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                pars.Add("CreateEmployee", MyManager.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }

            //上传时间
            if (!DateRanger.IsNotBothEmpty)
            {
                pars.Add("CreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

            var DataList = ObjBugSystemBLL.GetDataByParameter(pars, OrderByName, ctrPager.PageSize, ctrPager.CurrentPageIndex, out SourceCount);        //获取数据集合
            ctrPager.RecordCount = SourceCount;         //获取数据条数
            RepBugSystem.DataBind(DataList);            //数据绑定
        }
        #endregion

        #region 获取状态信息
        /// <summary>
        /// 获取状态
        /// </summary>
        public string GetState(object Source)
        {
            int State = Source.ToString().ToInt32();
            if (State == 1)
            {
                return "未解决";
            }
            else if (State == 2)
            {
                return "处理中";
            }
            else if (State == 3)
            {
                return "已解决";
            }
            else if (State == 4)
            {
                return "无效信息";
            }
            return "";
        }
        #endregion

        #region 处理
        public string Handel(object Source)
        {
            int BugID = Source.ToString().ToInt32();
            var Model = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32());
            var BugModel = ObjBugSystemBLL.GetByID(BugID);
            //1.未解决  2.处理中  3.已解决  4.无效信息
            if (Model.JobID == 1 && Model.EmployeeTypeID == 1 && (BugModel.State == 1 || BugModel.State == 2))       //系统管理员可以处理 (未处理 、 处理中可以处理）
            {
                return "";
            }
            else
            {
                return "style='display:none;'";
            }
        }
        #endregion

        #region 获取这个Bug的创建人
        public string UploadShow(object Source)
        {
            int BugID = Source.ToString().ToInt32();
            var Model = ObjBugSystemBLL.GetByID(BugID);
            if (Model.CreateEmployee != User.Identity.Name.ToInt32())
            {
                return "style='display:none;'";
            }
            return "";
        }
        #endregion
    }
}