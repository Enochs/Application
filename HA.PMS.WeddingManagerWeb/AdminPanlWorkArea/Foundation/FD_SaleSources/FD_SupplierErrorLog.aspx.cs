/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.6
 Description:供应差错记录界面
 History:修改日志

 Author:杨洋
 date:2013.4.6
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


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_SupplierErrorLog : SystemPage
    {
        SupplierErrorLog objSupplierErrorLogBLL = new SupplierErrorLog();
        OrderAppraise objOrderAppraiseBLL = new OrderAppraise();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             

                DataBinder();
            }
        }
        
        
        protected void DataBinder() 
        {
            int KindID=Request.QueryString["KindID"].ToInt32();
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            ObjParameterList.Add(new ObjectParameter("KindID", KindID));
            ObjParameterList.Add(new ObjectParameter("Type", 3));
            #region 分页页码
            int startIndex = SupplierErrorLogPager.StartRecordIndex;
            int resourceCount = 0;
            var query = objOrderAppraiseBLL.GetbyParameter(ObjParameterList.ToArray(), SupplierErrorLogPager.PageSize, 
                SupplierErrorLogPager.CurrentPageIndex, out resourceCount);
            SupplierErrorLogPager.RecordCount = resourceCount;
            rptSupplierErrorLog.DataSource = query;
            rptSupplierErrorLog.DataBind();
            #endregion
        }
        protected void SupplierErrorLogPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}