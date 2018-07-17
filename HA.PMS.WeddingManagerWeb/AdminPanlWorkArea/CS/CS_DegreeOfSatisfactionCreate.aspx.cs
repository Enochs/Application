/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.25
 Description:客户满意度添加页面
 History:修改日志

 Author:杨洋
 date:2013.3.25
 version:好爱1.0
 description:修改描述
 
 
 
 */
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
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.EditoerLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS
{
    public partial class CS_DegreeOfSatisfactionCreate : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        InvestigateState objInvestigateStateBLL = new InvestigateState();
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        DegreeOfSatisfactionItem ObjDegreeOfSatisfactionItemBLL = new DegreeOfSatisfactionItem();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            this.repItemList.DataSource = ObjDegreeOfSatisfactionItemBLL.GetByAll();
            this.repItemList.DataBind();
        }
        #endregion


        #region 点击保存事件
        /// <summary>
        /// 保存功能
        /// </summary>    
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlSumDof.SelectedItem.Text == "请选择")
            {
                JavaScriptTools.AlertWindow("请选择总体满意度!", Page);
                return;
            }
            var ObjUpdateModel = objDegreeOfSatisfactionBLL.GetByCustomerID(Request["CustomerID"].ToInt32());

            ObjUpdateModel.SumDof = ddlSumDof.SelectedItem.Text;
            ObjUpdateModel.DofContent = txtDofContent.Text;
            ObjUpdateModel.DofDate = DateTime.Now.ToString().ToDateTime();
            ObjUpdateModel.State = 2;
            objDegreeOfSatisfactionBLL.Update(ObjUpdateModel);
            DegreeOfSatisfactionContent ObjDegreeOfSatisfactionContentBLL = new DegreeOfSatisfactionContent();
            for (int i = 0; i < repItemList.Items.Count; i++)
            {
                ddlDegreeAssessResult Objddl = (ddlDegreeAssessResult)repItemList.Items[i].FindControl("DdlDegreeAssessResult1");
                TextBox txtContent = (TextBox)repItemList.Items[i].FindControl("txtContent");
                HiddenField hideKey = (HiddenField)repItemList.Items[i].FindControl("HideKey");
                ObjDegreeOfSatisfactionContentBLL.Insert(new CS_DegreeOfSatisfactionContent()
                {
                    AssessId = Objddl.SelectedValue.ToInt32(),
                    StationContent = txtContent.Text,
                    DofKey = ObjUpdateModel.DofKey,
                    ItemKey = hideKey.Value.ToInt32()
                });
            }

            //保存操作日志
            CreateHandle();

            JavaScriptTools.AlertWindowAndLocation("保存调查结果成功！", "/AdminPanlWorkArea/CS/CS_DegreeOfSatisfactionShow.aspx?DofKey=" + ObjUpdateModel.DofKey, Page);

            //CS_DegreeOfSatisfaction cS_DegreeOfSatisfaction = new CS_DegreeOfSatisfaction();

            //cS_DegreeOfSatisfaction.DofDate = txtDofDate.Text.ToDateTime();
            //cS_DegreeOfSatisfaction.DofContent = txtDofContent.Text;
            //cS_DegreeOfSatisfaction.CustomerID = ddlCustomer.SelectedValue.ToInt32();
            //cS_DegreeOfSatisfaction.InvestigateStateID = ddlInvestigateStateID.SelectedValue.ToInt32();
            //cS_DegreeOfSatisfaction.IsDelete = false;
            //cS_DegreeOfSatisfaction.DegreeResult = txtDegreeResult.Text;
            //cS_DegreeOfSatisfaction.SumDof = txtSumDof.Text.ToInt32();
            //int result = objDegreeOfSatisfactionBLL.Insert(cS_DegreeOfSatisfaction);
            ////根据返回判断添加的状态
            //if (result > 0)
            //{
            //    JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
            //}
            //else
            //{
            //    JavaScriptTools.AlertAndClosefancybox("添加失败,请重新尝试", this.Page);

            //}
        }

        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();

            var Model = objCustomersBLL.GetByID(Request["CustomerID"].ToInt32());
            HandleModel.HandleContent = "满意度调查-保存满意度,客户姓名:" + Model.Bride + "/" + Model.Groom + ",总体满意度：" + ddlSumDof.SelectedItem.Text;

            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 12;     //满意度调查
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion

    }
}