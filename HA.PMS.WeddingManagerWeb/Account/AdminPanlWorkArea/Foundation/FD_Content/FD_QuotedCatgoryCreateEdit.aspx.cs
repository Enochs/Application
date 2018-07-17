using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class FD_QuotedCatgoryCreateEdit : SystemPage
    {
        QuotedCatgory ObjQuotedCatgoryBLL = new QuotedCatgory();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request["Paretnt"].ToInt32() > 0)
            //{
            //    rdolist.Visible = false;
            //}           

            if (!IsPostBack)
            {
                if (Request["Action"] == "Update")
                {
                    var ObjModel = ObjQuotedCatgoryBLL.GetByID(Request["Paretnt"].ToInt32());
                    this.txtDeparName.Text = ObjModel.Title;
                    rdolist.ClearSelection();
                    rdolist.Visible = true;
                    rdolist.Items.FindByValue(ObjModel.Productproperty.ToString()).Selected=true;

                }
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            if (Request["Paretnt"].ToInt32() == 0)
            {
                ObjQuotedCatgoryBLL.Insert(new DataAssmblly.FD_QuotedCatgory()
                {
                    Title = txtDeparName.Text,
                    Parent = 0,
                    SortOrder = ObjQuotedCatgoryBLL.GetFirstSortOrder(0, 0),
                    ItemLevel = ObjQuotedCatgoryBLL.GetItemLevel(0),
                    Productproperty=rdolist.SelectedValue.ToInt32()
                });
            }
            else
            {

                if (Request["Action"] == "Update")
                {
                    var ObjUpdateModel=ObjQuotedCatgoryBLL.GetByID(Request["Paretnt"].ToInt32());
                  
                    ObjUpdateModel.Title = txtDeparName.Text;
                    ObjUpdateModel.Productproperty = rdolist.SelectedValue.ToInt32();
                    ObjQuotedCatgoryBLL.Update(ObjUpdateModel);
                }
                else
                {
                    var OnlyeModel=ObjQuotedCatgoryBLL.GetByID(Request["Paretnt"].ToInt32());
                    ObjQuotedCatgoryBLL.Insert(new DataAssmblly.FD_QuotedCatgory()
                    {
                        Title = txtDeparName.Text,
                        Parent = Request["Paretnt"].ToInt32(),
                        SortOrder = ObjQuotedCatgoryBLL.GetFirstSortOrder(-1, Request["Paretnt"].ToInt32()),
                 
                        ItemLevel=ObjQuotedCatgoryBLL.GetItemLevel(Request["Paretnt"].ToInt32()),
                        Productproperty=OnlyeModel.Productproperty
                    });
                }

            }
            JavaScriptTools.AlertWindowAndReaload("保存成功", Page);
        }
    }
}