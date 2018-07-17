
/**
 Version :HaoAi22
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.21
 Description:庆典文库修改页面
 History:修改日志

 Author:杨洋
 date:2013.3.21
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
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationKnowledge
{
    public partial class FD_CelebrationKnowledgeUpdate : SystemPage
    {
        CelebrationKnowledge objCelebrationKnowledgeBLL = new CelebrationKnowledge();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                int KnowledgeID = Request.QueryString["KnowledgeID"].ToInt32();
                HA.PMS.DataAssmblly.FD_CelebrationKnowledge fd= objCelebrationKnowledgeBLL.GetByID(KnowledgeID);
                txtKnowContent.Text = fd.KnowledgeContent;
                txtKnowledgeTitle.Text = fd.KnowledgeTitle;
                DataBinder();
                //如果父级节点为0的话，就代表它本身就是顶级栏目
                if (fd.ParentID==0)
                {
                    //就默认是它自己
                    ddlParent.Items.FindByValue(fd.KnowledgeID + string.Empty).Selected = true;
                }
                else
                {
                    //相反找到父级节点
                    ddlParent.Items.FindByValue(fd.ParentID + string.Empty).Selected = true;
                }
               
            }
        }

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        protected void DataBinder()
        {
            ddlParent.DataSource = objCelebrationKnowledgeBLL.GetByAll();
            ddlParent.DataTextField = "KnowledgeTitle";
            ddlParent.DataValueField = "KnowledgeID";
            ddlParent.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int KnowledgeID = Request.QueryString["KnowledgeID"].ToInt32();
            HA.PMS.DataAssmblly.FD_CelebrationKnowledge fdKnow = objCelebrationKnowledgeBLL.GetByID(KnowledgeID); ;
            fdKnow.KnowledgeContent = txtKnowContent.Text;
            if (chkTop.Checked)
            {
                fdKnow.ParentID = 0;//默认为父级
            }
            else
            {
                fdKnow.ParentID = ddlParent.SelectedValue.ToInt32();

            }
            fdKnow.IsDelete = false;
            fdKnow.KnowledgeTitle = txtKnowledgeTitle.Text;
            int result = objCelebrationKnowledgeBLL.Update(fdKnow);
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("修改失败,请重新尝试", this.Page);

            }
        }
    }
}