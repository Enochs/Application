using HA.PMS.BLLAssmblly.FD;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class FD_MaterialManager : SystemPage
    {
        Material ObjMaterialBLL = new Material();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        private void BinderData()
        {
            List<System.Data.Objects.ObjectParameter> parameters = new List<System.Data.Objects.ObjectParameter>();

            var objParmList = new List<PMSParameters>();

            //按材质名称查找
            objParmList.Add(txtMaterialName.Text != string.Empty, "MaterialName", txtMaterialName.Text, NSqlTypes.LIKE);

            int resourceCounts = 0;
            var query = ObjMaterialBLL.GetByWhereParameter(objParmList, "MaterialId", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out resourceCounts);
            CtrPageIndex.RecordCount = resourceCounts;
            rptMaterial.DataBind(query);
        }

        /// <summary>
        /// 点击查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
    }
}