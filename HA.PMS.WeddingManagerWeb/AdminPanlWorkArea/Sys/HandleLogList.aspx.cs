using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys
{
    public partial class HandleLogList : SystemPage
    {
        /// <summary>
        /// 操作日志
        /// </summary>
        HandleLog ObjHandleBLL = new HandleLog();

        /// <summary>
        /// 操作类型
        /// </summary>
        HandleType ObjHandleTypeBLL = new HandleType();

        string OrderByColumnName = "HandleID";
        int SourceCount = 0;

        #region 页面初始化
        /// <summary>
        /// 加载页面
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderType();
                BinderData(sender, e);

            }
        }
        #endregion

        #region 操作日志类型 绑定 下拉框
        /// <summary>
        /// 绑定
        /// </summary>
        public void BinderType()
        {
            var query = ObjHandleTypeBLL.GetByAll().OrderBy(C => C.Sort);
            ddlHandleType.DataSource = query;
            ddlHandleType.DataBind();
            ddlHandleType.Items.Insert(0, new ListItem { Text = "请选择", Value = "0" });
            ddlHandleType.Items.FindByText("请选择").Selected = true;
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定
        /// </summary>
        public void BinderData(object sender, EventArgs e)
        {
            List<PMSParameters> pars = new List<PMSParameters>();
            //操作人
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                MyManager.GetEmployeePar(pars, "HandleEmployeeID");
            }

            //日志类型
            pars.Add(ddlHandleType.SelectedValue.ToInt32() > 0, "HandleType", ddlHandleType.SelectedValue.ToInt32(), NSqlTypes.Equal);

            //操作日期
            pars.Add(DateRanger.IsNotBothEmpty, "HandleCreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);

            //关键字查询
            pars.Add(txtPrimaryKey.Text.Trim() != string.Empty, "HandleContent", txtPrimaryKey.Text.Trim().ToString(), NSqlTypes.LIKE);

            var DataList = ObjHandleBLL.GetDataByParameter(pars, OrderByColumnName, CtrPager.PageSize, CtrPager.CurrentPageIndex, out SourceCount);
            CtrPager.RecordCount = SourceCount;
            lblDetailsCount.Text = "总共" + SourceCount + "条数据";
            rptHandleLog.DataBind(DataList);
        }
        #endregion

        #region 获取类型名称
        /// <summary>
        /// 根据ID获取类型名称
        /// </summary>
        public string GetTypeByID(object Source)
        {
            int TypeID = Source.ToString().ToInt32();
            var Model = ObjHandleTypeBLL.GetByID(TypeID);
            if (Model != null)
            {
                return Model.TypeName;
            }
            return "";
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询
        /// </summary>
        protected void btnLook_Click(object sender, EventArgs e)
        {
            BinderData(sender, e);
        }
        #endregion
    }
}