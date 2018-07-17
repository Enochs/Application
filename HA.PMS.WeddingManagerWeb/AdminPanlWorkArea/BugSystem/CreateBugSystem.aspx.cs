using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.BugSystem
{
    public partial class CreateBugSystem : SystemPage
    {
        HA.PMS.BLLAssmblly.Sys.BugSystem ObjBugSystemBLL = new HA.PMS.BLLAssmblly.Sys.BugSystem();
        BugFile ObjBugFileBLL = new BugFile();

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["BugID"] != null)       //获取BugID  显示
                {
                    IsVisible();        //隐藏文本框 显示Label
                    lblGetTitle.Text = "查看Bug";
                    int BugID = Request["BugID"].ToInt32();
                    var Model = ObjBugSystemBLL.GetByID(BugID);     //获取Bug信息
                    lblTitle.Text = Model.BugTitle.ToString();      //Bug标题
                    lblContent.Text = Model.BugContent.ToString();  //Bug说明
                    lblCreateEmployee.Text = GetEmployeeName(Model.CreateEmployee);
                    lblCreateDate.Text = Model.CreateDate.ToString();
                    if (Request["Type"] == "处理")
                    {
                        rdoState.SelectedValue = Model.State.ToString();
                        txtSuggest.Text = Model.SolRemark;
                        if (Model.State == 2)           //如果状态是处理中  就不能恢复成未解决
                        {
                            rdoState.Items.FindByValue("1").Enabled = false;
                        }
                    }
                    else if (Request["Type"] == "查看")
                    {
                        lblState.Visible = true;
                        lblSuggest.Visible = true;
                        btnBack.Visible = true;
                        txtSuggest.Visible = false;
                        rdoState.Visible = false;
                        btnFinish.Visible = false;
                        btnSaveFinish.Visible = false;
                        lblSuggest.Text = Model.SolRemark.ToString();
                        lblState.Text = GetState(Model.State);
                    }

                    var DataList = ObjBugFileBLL.GetByBugID(BugID);     //获取集合
                    repBugFile.DataSource = DataList;       //绑定
                    repBugFile.DataBind();
                }
                else
                {
                    tblFinish.Visible = false;
                }
            }
        }
        #endregion

        #region 提交
        /// <summary>
        /// 提交
        /// </summary>
        protected void btnGetConfirm_Click(object sender, EventArgs e)
        {
            sys_BugSystem BugModel = new sys_BugSystem();
            BugModel.BugTitle = txtTitle.Text.Trim().ToString();
            BugModel.BugContent = txtContent.Text.Trim().ToString();
            BugModel.CreateDate = DateTime.Now;
            BugModel.CreateEmployee = User.Identity.Name.ToInt32();
            BugModel.State = 1;     //状态 1.未解决  2.以解决  3.无效信息(就是正常页面 没有错误)

            BugModel.SolRemark = "";
            BugModel.IsDelete = false;
            int result = ObjBugSystemBLL.Insert(BugModel);
            if (result > 0)
            {
                JavaScriptTools.AlertWindowAndLocation("提交成功", "BugSystemManager.aspx", Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("提交失败,请稍候再试...", Page);
            }
        }
        #endregion

        #region 查看  隐藏文本框
        public void IsVisible()
        {
            txtTitle.Visible = false;
            txtContent.Visible = false;

            tr_Get.Visible = false;
            tr_FileShow.Visible = true;

            lblTitle.Visible = true;
            lblContent.Visible = true;
            tblFinish.Visible = true;
        }
        #endregion

        #region 点击确认完成/保存
        /// <summary>
        /// 保存事件(完成)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnFinish_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            sys_BugSystem BugModel = ObjBugSystemBLL.GetByID(Request["BugID"].ToInt32());
            BugModel.State = rdoState.SelectedValue.ToInt32();
            BugModel.SolRemark = txtSuggest.Text.Trim().ToString();
            int result = ObjBugSystemBLL.Update(BugModel);
            if (result > 0)
            {
                if (btn.Text == "确认完成")
                {
                    JavaScriptTools.AlertWindowAndLocation("已确认完成", "BugSystemManager.aspx", Page);
                }
                else if (btn.Text == "保存")
                {
                    JavaScriptTools.AlertWindowAndLocation("保存成功", "BugSystemManager.aspx", Page);
                }
            }
            else
            {
                JavaScriptTools.AlertWindow("提交失败,请稍候再试...", Page);
            }
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
    }
}