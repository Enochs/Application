using System;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_ChannelTypeManager : SystemPage
    {
        ChannelType ObjChannelTypeBLL = new ChannelType();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        protected void BinderData()
        {
            int SourceCount;
            repChanneType.DataSource = ObjChannelTypeBLL.GetByIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            repChanneType.DataBind();
        }

        /// <summary>
        /// 保存添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveAdd_Click(object sender, EventArgs e)
        {
            if (!ObjChannelTypeBLL.IsChannelTypeNameExist(txtChannelTypeName.Text.Trim()))
            {
                ObjChannelTypeBLL.Insert(new FD_ChannelType()
                {
                    ChannelTypeName = txtChannelTypeName.Text.Trim(),
                    IsDelete = false
                });
                JavaScriptTools.AlertWindowAndReload("保存成功", Page);

                txtChannelTypeName.Text = string.Empty;
                BinderData();
            }
            else
            {
                JavaScriptTools.AlertWindow("该类别名称已存在", Page);
            }
        }
 

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repChanneType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int channelTypeId = Convert.ToInt32(e.CommandArgument);
            TextBox txtName = ((TextBox)e.Item.FindControl("txtName"));
            string channelTypeName = txtName.Text.Trim();
            FD_ChannelType channelType = ObjChannelTypeBLL.GetByID(channelTypeId);
            if (e.CommandName == "Edit")
            {
                if (!ObjChannelTypeBLL.IsChannelTypeNameExist(channelTypeName))
                {
                    channelType.ChannelTypeName = channelTypeName;
                    ObjChannelTypeBLL.Update(channelType);
                    txtName.BorderColor = System.Drawing.Color.Green;

                }
                else if (channelType.ChannelTypeName != channelTypeName)
                {
                    txtName.BorderColor = System.Drawing.Color.Yellow;
                    JavaScriptTools.AlertWindow("该类别名称已存在", Page);
                }
            }
            else
            {
                channelType.IsDelete = true;
                ObjChannelTypeBLL.Update(channelType);
                JavaScriptTools.AlertWindow("删除完毕", Page);
                BinderData();
            }
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBind();
        }
    }
}