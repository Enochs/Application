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
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_StrorehouseInuseProduct : SystemPage
    {
        ProductOrderInuse objFD_ProductOrderInuseBLL = new ProductOrderInuse();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        protected void DataBinder() 
        {
            #region 查询参数
            HA.PMS.DataAssmblly.FD_Storehouse ff = new HA.PMS.DataAssmblly.FD_Storehouse();
            /*
             找到 orderCoder 在FL_Dispatching 表中到到DispatchingID  通过FL_ProductforDispatching 找到  ProductID
             通过ProductID 比对FD_AllProducts中的Keys  和Type=2 (库房),查询出对应的明细
             */


            FD_ProductOrderInuse fD_ProductOrderInuse = new DataAssmblly.FD_ProductOrderInuse();
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            //开始时间
            DateTime startTime = new DateTime();
            //如果没有选择结束时间就默认是当前时间

            DateTime endTime = DateTime.Now;
            if (!string.IsNullOrEmpty(txtStar.Text))
            {
                startTime = txtStar.Text.ToDateTime();
            }
            if (!string.IsNullOrEmpty(txtEnd.Text))
            {
                endTime = txtEnd.Text.ToDateTime();
            }
            ObjParameterList.Add(new ObjectParameter("PartyDate_between", startTime + "," + endTime));
            #endregion

            #region 分页页码
            int startIndex = InusePager.StartRecordIndex;
            int resourceCount = 0;
            var query = objFD_ProductOrderInuseBLL.GetbyFD_ProductOrderInuseParameter(ObjParameterList.ToArray(), InusePager.PageSize, InusePager.CurrentPageIndex, out resourceCount);
            InusePager.RecordCount = resourceCount;

            rptStroe.DataSource = query;
            rptStroe.DataBind();
            #endregion
        }
        protected void InusePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}