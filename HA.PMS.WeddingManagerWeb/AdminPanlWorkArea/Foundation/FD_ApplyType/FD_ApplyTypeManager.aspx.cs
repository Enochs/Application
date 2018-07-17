using HHLWedding.BLLAssmbly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_ApplyType
{
    public partial class FD_ApplyTypeManager :  HA.PMS.Pages.SystemPage
    {
        BaseService<HA.PMS.DataAssmblly.FD_ApplyType> ObjApplyTypeBLL = new BaseService<HA.PMS.DataAssmblly.FD_ApplyType>();


        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>

        public void BinderData()
        {
            var list = ObjApplyTypeBLL.FindAll();
            repApplyType.DataSource = list;
            repApplyType.DataBind();
        }
        #endregion


        #region 添加邀约类型
        /// <summary>
        /// 添加邀约类型
        /// </summary>
        protected void bntAddApplyType_Click(object sender, EventArgs e)
        {
            HA.PMS.DataAssmblly.FD_ApplyType m_applyType = new HA.PMS.DataAssmblly.FD_ApplyType()
            {
                ApplyName = txtTypeName.Value,
                CreateDate = DateTime.Now,
                Status = 1
            };
            ObjApplyTypeBLL.Add(m_applyType);
            BinderData();
        }
        #endregion


        #region Repeater绑定事件
        /// <summary>
        /// 修改/删除事件
        /// </summary>
        protected void repApplyType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var ID = e.CommandArgument.ToString().ToInt32();
            var m_applyType = ObjApplyTypeBLL.GetById(ID);
            if (e.CommandName == "ModifyStatus")        //修改
            {
                TextBox txtName = (TextBox)repApplyType.Items[e.Item.ItemIndex].FindControl("txtApplyName");
                //HtmlInputText htmlInput = (HtmlInputText)e.Item.FindControl("txtName");
                var name = txtName.Text;
                m_applyType.ApplyName = name;
            }
            else if (e.CommandName == "EnableStatus")       //禁用/启用
            {
                if (m_applyType.Status == 1)
                {
                    m_applyType.Status = 0;
                }
                else
                {
                    m_applyType.Status = 1;
                }
            }
            ObjApplyTypeBLL.Update(m_applyType);        //修改
            BinderData();
        }
        #endregion
    }
}