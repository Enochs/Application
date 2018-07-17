/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.16
 Description:渠道浏览页面
 History:修改日志

 Author:杨洋
 date:2013.3.16
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


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_SaleSourcesManager : SystemPage
    {
        SaleSources ObjSaleSourcesBLL = new SaleSources();
        Employee objEmployeeBLL = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        /// <summary>
        /// 解析返利为文字
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetNeedRebate(object source)
        {
            string NeedRebate = source.ToString();
            return NeedRebate == "True" ? "是" : "否";
        }
        /// <summary>
        /// 绑定
        /// </summary>
        protected void DataBinder()
        {
            int SourceCount = 0;
            var ObjParList = new List<PMSParameters>();
            if (txtSourceName.Text != string.Empty)
            {
                ObjParList.Add("Sourcename", txtSourceName.Text, NSqlTypes.LIKE);
            }

            if (ddlChannelType1.SelectedValue.ToInt32() > 0)
            {
                ObjParList.Add("ChannelTypeId", ddlChannelType1.SelectedValue);
            }
            ObjParList.Add("ProlongationEmployee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);

            rptSaleSources.DataSource = ObjSaleSourcesBLL.GetByWhereParameter(ObjParList, "Sourcename", SaleSourcesPager.PageSize, SaleSourcesPager.CurrentPageIndex, out SourceCount);
            SaleSourcesPager.RecordCount = SourceCount;
            rptSaleSources.DataBind();

        }



        protected void rptSaleSources_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int sourceID = e.CommandArgument.ToString().ToInt32();
                //创建渠道浏览类
                HA.PMS.DataAssmblly.FD_SaleSources fd_SaleSources = new DataAssmblly.FD_SaleSources();

                fd_SaleSources.SourceID = sourceID;
                ObjSaleSourcesBLL.Delete(fd_SaleSources);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }

        protected void SaleSourcesPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void benSerch_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #region 获取渠道类型名称
        public string GetsChannelTypeName(object Source)
        {
            ChannelType ObjChannelTypeBLL = new ChannelType();
            int ChannelTypeID = Source.ToString().ToInt32();
            if (ChannelTypeID != 0)
            {

                var TypeMode = ObjChannelTypeBLL.GetByID(ChannelTypeID);
                if (TypeMode != null)
                {
                    return TypeMode.ChannelTypeName;
                }
            }
            return "";
        }
        #endregion
    }
}