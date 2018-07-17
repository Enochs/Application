
/**
 Version :HaoAi22
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.21
 Description:庆典文库管理页面
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
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationKnowledge
{
    public partial class FD_CelebrationKnowledgeManager : SystemPage
    {
        CelebrationKnowledge objCelebrationKnowledge = new CelebrationKnowledge();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        protected void DataBinder()
        {


            HA.PMS.DataAssmblly.FD_CelebrationKnowledge know = new DataAssmblly.FD_CelebrationKnowledge();
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            int userId = User.Identity.Name.ToInt32();
            //52
               List<ObjectParameter> UserParameterList = new List<ObjectParameter>();
               UserParameterList.Add(new ObjectParameter("EmployeeID", userId));
               UserParameterList.Add(new ObjectParameter("ChannelID", 52));
               var queryJurisdic = PublicDataTools<Sys_UserJurisdiction>.GetDataByParameter(new Sys_UserJurisdiction(),
                userId, 52, UserParameterList.ToArray());
              //if (queryJurisdic.Count==0)
              //{
              //    phContent.Visible = false;
              //}
               
            #region 分页页码
            int startIndex = KnowPager.StartRecordIndex;
            int resourceCount = 0;
            var query = objCelebrationKnowledge.GetbyFD_CelebrationKnowledgeParameter(
            ObjParameterList.ToArray(), KnowPager.PageSize, KnowPager.CurrentPageIndex, out resourceCount);
            KnowPager.RecordCount = resourceCount;

            rptKnow.DataSource = query;
            rptKnow.DataBind();

            #endregion
             
        }

        protected void rptKnow_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int KnowledgeID = e.CommandArgument.ToString().ToInt32();
               
                HA.PMS.DataAssmblly.FD_CelebrationKnowledge know = objCelebrationKnowledge.GetByID(KnowledgeID);

                objCelebrationKnowledge.Delete(know);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }

        protected void KnowPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }





    }
}