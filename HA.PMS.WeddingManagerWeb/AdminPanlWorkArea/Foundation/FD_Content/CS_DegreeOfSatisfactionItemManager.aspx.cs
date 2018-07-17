using HA.PMS.BLLAssmblly.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class CS_DegreeOfSatisfactionItemManager : SystemPage
    {
        DegreeOfSatisfactionItem ObjDegreeOfSatisfactionItemBLL = new DegreeOfSatisfactionItem();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        private void BinderData()
        {
            this.repItem.DataSource = ObjDegreeOfSatisfactionItemBLL.GetByAll();
            this.repItem.DataBind();
        }

        protected void repItem_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                ObjDegreeOfSatisfactionItemBLL.Delete(new DataAssmblly.CS_DegreeOfSatisfactionItem() { ItemKey=e.CommandArgument.ToString().ToInt32()});
                BinderData();
                JavaScriptTools.AlertWindow("删除成功",Page);
            }

            if (e.CommandName == "Edit")
            {
                var ObjUpdateModel=ObjDegreeOfSatisfactionItemBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                ObjUpdateModel.Title = (e.Item.FindControl("txtName") as TextBox).Text;
                ObjDegreeOfSatisfactionItemBLL.Update(ObjUpdateModel);

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (txtTitle.Text != string.Empty)
            {
                ObjDegreeOfSatisfactionItemBLL.Insert(new CS_DegreeOfSatisfactionItem() {UpdateDate=DateTime.Now,Title=txtTitle.Text });
                JavaScriptTools.AlertWindow("添加成功", Page);
                txtTitle.Text = string.Empty;
                BinderData();
            }

           
        }
    }
}