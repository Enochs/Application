/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.17
 Description:供应商管理页面
 History:修改日志

 Author:杨洋
 date:2013.3.17
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
    public class FL_CustomersChannelComparer : IEqualityComparer<FD_Supplier>
    {

        public bool Equals(FD_Supplier x, FD_Supplier y)
        {


            if (Object.ReferenceEquals(x, y)) return true;


            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;


            return x.Name == y.Name;
        }



        public int GetHashCode(FD_Supplier c)
        {

            if (Object.ReferenceEquals(c, null)) return 0;


            int hashName = c.Name == null ? 0 : c.Name.GetHashCode();


            //  int hashProductCode = c.Channel.GetHashCode();


            return hashName;
        }

    }
    public partial class FD_SupplierManager :SystemPage
    {
        Supplier ObjSupplierBLL = new Supplier();
        Category objCategoryBLL = new Category();

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataDropDownList();
                DataBinder();
            }
        }

        protected void DataDropDownList() 
        {
            ddlCategory.DataSource = objCategoryBLL.GetByAll();
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();
            ddlCategory.Items.Add(new ListItem("请选择", "0"));
            ddlCategory.SelectedIndex = ddlCategory.Items.Count - 1;
            var query = ObjSupplierBLL.GetByAll();

            ddlSupplierName.DataSource = query;
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataValueField = "SupplierID";
            ddlSupplierName.DataBind();
            ddlSupplierName.Items.Add(new ListItem("请选择", "0"));
            ddlSupplierName.SelectedIndex = ddlSupplierName.Items.Count - 1;
        }
        protected void DataBinder() 
        {

            #region 相关的查询
       
            FD_Supplier fD_Supplier = new FD_Supplier();
            fD_Supplier.Name = ddlSupplierName.SelectedItem.Text;
            fD_Supplier.CategoryID = ddlCategory.SelectedValue.ToInt32();
      
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            if (ddlCategory.SelectedValue.ToInt32() != 0)
            {
                ObjParameterList.Add(new ObjectParameter("CategoryID", fD_Supplier.CategoryID));
            }

            if (ddlSupplierName.SelectedItem.Text != "请选择")
            {
                ObjParameterList.Add(new ObjectParameter("Name", fD_Supplier.Name));
            }
            
            //开始时间
            DateTime startTime = new DateTime();
            //如果没有选择结束时间就默认是当前时间
            string endTimeStr = "2100-10-10";
            DateTime endTime = endTimeStr.ToDateTime();
            if (!string.IsNullOrEmpty(txtStarDate.Text))
            {
                startTime = txtStarDate.Text.ToDateTime();
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text))
            {
                endTime = txtEndDate.Text.ToDateTime();
            }
            ObjParameterList.Add(new ObjectParameter("StarDate_between", startTime + "," + endTime));
            #endregion

            #region 分页页码
            //int startIndex = SupplierPager.StartRecordIndex;
            //int resourceCount = 0;
            //var query = ObjSupplierBLL.GetbyParameter(ObjParameterList.ToArray(), SupplierPager.PageSize, SupplierPager.CurrentPageIndex, out resourceCount);
            //SupplierPager.RecordCount = resourceCount;

            //rptSupplier.DataSource = query;
            //rptSupplier.DataBind();
           
            #endregion


           
        
        }

        protected void rptSupplier_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int SupplierID = e.CommandArgument.ToString().ToInt32();
                //创建供应商类
                FD_Supplier fd_Supplier = new FD_Supplier() 
                {
                  SupplierID=SupplierID
                
                };
                ObjSupplierBLL.Delete(fd_Supplier);
                //删除之后重新绑定数据源
                DataBinder();
                DataDropDownList();
            }
        }

        

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void SupplierPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}