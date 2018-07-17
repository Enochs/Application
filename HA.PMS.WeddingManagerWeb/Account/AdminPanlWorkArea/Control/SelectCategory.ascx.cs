using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class SelectCategory : System.Web.UI.UserControl
    {
        //PMS_WeddingEntities Objentity = new PMS_WeddingEntities();
        //Category ObjCategoryBLL = new Category();
        QuotedCatgory ObjQuotedCatgoryBLL = new QuotedCatgory();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }


        /// <summary>
        /// 绑定一级分类数据
        /// </summary>
        private void BinderData()
        {


            if (Request["ParentID"] != null)
            {
                int ParentID = Request["ParentID"].ToInt32();
                if (ParentID.ToString().Length >= 4)
                {
                    ParentID = ParentID.ToString().Substring(2, 3).ToString().ToInt32();
                }
                else
                {
                    ParentID = Request["ParentID"].ToInt32();
                }

                if (Request["ParentID"].ToInt32() == 0)
                {
                    var DataList = ObjQuotedCatgoryBLL.GetByParentID(ParentID);

                    this.repPGlist.DataSource = ObjQuotedCatgoryBLL.GetByParentID(ParentID);
                    this.repPGlist.DataBind();
                }
                else
                {
                    var DataList = ObjQuotedCatgoryBLL.GetByParentID(ParentID);
                    this.repPGlist.DataSource = DataList;
                    this.repPGlist.DataBind();
                }
            }
        }


        /// <summary>
        /// 回调绑定相应的类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lstpg_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.repPGlist.DataSource = ObjQuotedCatgoryBLL.GetByParentID(lstpg.SelectedItem.Value.ToInt32()); ;
            //this.repPGlist.DataBind();

        }


        /// <summary>
        /// 获取选择的类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveSelect_Click(object sender, EventArgs e)
        {
            string SelectValue = string.Empty;
            for (int i = 0; i < repPGlist.Items.Count; i++)
            {
                CheckBox Objchk = (CheckBox)this.repPGlist.Items[i].FindControl("chkpg");
                if (Objchk != null && Objchk.Checked)
                {
                    HiddenField ObjHideKey = (HiddenField)this.repPGlist.Items[i].FindControl("hideKey");
                    SelectValue += ObjHideKey.Value + ",";

                }
            }
            SelectValue = SelectValue.Trim(',');
            if (SelectValue != string.Empty)
            {
                if (Request["Callback"] != null)
                {
                    JavaScriptTools.SetValueByParentControl(Request["ControlKey"], SelectValue, Request["Callback"], this.Page);
                }
                else
                {
                    JavaScriptTools.SetValueByParentControl(Request["ControlKey"], SelectValue, this.Page);
                }
            }
            else
            {
                JavaScriptTools.AlertWindow("请至少选择一个项目!", Page);
            }
        }


        /// <summary>
        /// 点击类型显示产品
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repPGlist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            QuotedProduct ObjQuotedProduct = new QuotedProduct();
            AllProducts ObjProductBLL = new AllProducts();
            var Keys = ObjQuotedProduct.GetByQcKey(e.CommandArgument.ToString().ToInt32()).Keys;

            if (Keys != string.Empty)
            {
                hideSelectProduct.Value = "," + Keys;
            }

            var KeyList = Keys.Split(',');
            List<FD_AllProducts> ObjBinderList = new List<FD_AllProducts>();
            for (int z = 0; z < KeyList.Length; z++)
            {
                ObjBinderList.Add(ObjProductBLL.GetByID(KeyList[z].ToInt32()));
            }


            this.repSaleProduct.DataSource = ObjBinderList;
            this.repSaleProduct.DataBind();
        }
    }
}