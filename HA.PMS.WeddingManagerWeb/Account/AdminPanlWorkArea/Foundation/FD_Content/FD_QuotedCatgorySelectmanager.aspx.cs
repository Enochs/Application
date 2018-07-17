using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;

using HA.PMS.Pages;
using System.Web.UI.HtmlControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class FD_QuotedCatgorySelectmanager : System.Web.UI.Page
    {

        QuotedCatgory ObjQuotedCatgoryBLL = new QuotedCatgory();


        QuotedProduct ObjQuotedProduct = new QuotedProduct();
        /// <summary>
        /// 产品
        /// </summary>
        AllProducts ObjProductBLL = new AllProducts();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var Keys = ObjQuotedProduct.GetByQcKey(Request["Qckey"].ToInt32()).Keys;
                List<FD_AllProducts> ObjProductlist = new List<FD_AllProducts>();

                var KeyList = Keys.Split(',');
                for (int z = 0; z < KeyList.Length; z++)
                {
                    if (KeyList[z].ToInt32() > 0)
                    {
                        ObjProductlist.Add(ObjProductBLL.GetByID(KeyList[z].ToInt32()));
                    }
                }
                this.repProductByCatogryforWarehouseList.DataBind(ObjProductlist);
            }
        }


        public string GetCount(object ItemIndex)
        {
            try
            {
                var ProductCount = ObjQuotedProduct.GetByQcKey(Request["Qckey"].ToInt32()).ProductCount;

                return ProductCount.Split(',')[ItemIndex.ToString().ToInt32()];
            }
            catch
            {
                return string.Empty;
            }
        }


        protected void btnSaveSelect_Click(object sender, EventArgs e)
        {
            string Count = "";
            for (int z = 0; z < repProductByCatogryforWarehouseList.Items.Count; z++)
            {

                var ObjItem = repProductByCatogryforWarehouseList.Items[z].FindControl("txtSaleCount") as TextBox;//数量
                Count += ObjItem.Text + ",";

            }
            var ObjupdateModel = ObjQuotedProduct.GetByQcKey(Request["Qckey"].ToInt32());
            ObjupdateModel.ProductCount = Count.Trim(',');
            ObjQuotedProduct.Update(ObjupdateModel);
            JavaScriptTools.AlertAndClosefancybox("保存完毕！", Page);
        }
    }
}