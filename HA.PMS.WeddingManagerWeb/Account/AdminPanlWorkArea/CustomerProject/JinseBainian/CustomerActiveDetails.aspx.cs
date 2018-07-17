using HA.PMS.BLLAssmblly.CustomerSystem;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CustomerProject.JinseBainian
{
    public partial class CustomerActiveDetails : SystemPage
    {
        PackageReserve ObjPackageBLL = new PackageReserve();

        int SourceKey = 0;

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["SourceKey"] == null)
                {
                    SourceKey = Request["SourceKey"].ToInt32();
                }
                else
                {
                    SourceKey = 1;
                }
                DataBinder();
            }
        }
        #endregion


        #region 数据绑定
        public void DataBinder()
        {
            CC_PackageReserve ObjPackageModel = ObjPackageBLL.GetByID(SourceKey);
            lblPackage.Text = GetByPackgetID(ObjPackageModel.PackageID);
            lblEmployee.Text = GetEmployeeName(ObjPackageModel.EmployeeID);
            lblDateItem.Text = ObjPackageModel.DateItem;
            lblState.Text=ObjPackageModel.State == 0 ? "预定" : "暂定";
            lblCreateDate.Text=ObjPackageModel.CreateDate.ToString();
            lblPartyDate.Text=ObjPackageModel.PartyDate.ToString();
            if (ObjPackageModel.UpdateEmployee != null || ObjPackageModel.UpdateEmployee.ToString() != string.Empty)
            {
                lblupdatePerson.Text = GetEmployeeName(ObjPackageModel.UpdateEmployee);
            }
            else
            {
                lblupdatePerson.Text = "";
            }
            lblupdateTime.Text = ObjPackageModel.UpdateDate.ToString();
            lblRemark.Text = ObjPackageModel.Node.ToString();
        }
        #endregion
    }
}