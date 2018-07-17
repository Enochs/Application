using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.CustomerSystem;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CustomerProject.JinseBainian
{
    public partial class CustomerActiveCreateUpdate : SystemPage
    {
        PackageReserve ObjPackageReserveBLL = new PackageReserve();

        Employee ObjEmployeeBLL = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                BinderData();
            }
        }

        private void BinderData()
        {

            lblPartyDate.Text = Request["DateTime"];
            lblEmployeeName.Text = GetEmployeeName(User.Identity.Name.ToInt32());
            lblTimerSpan.Text = Request["SourceItem"];
            var ObjModel = ObjPackageReserveBLL.GetOnlyModel(Request["DateTime"].ToDateTime(), Request["SourceItem"], Request["PackageID"].ToInt32());
            if (ObjModel != null)
            {
                btnChange.Visible = false;
                btnDelete.Visible = false;
                lblPartyDate.Text = Request["DateTime"];
                lblEmployeeName.Text = GetEmployeeName(ObjModel.EmployeeID);
                lblTimerSpan.Text = Request["SourceItem"];
                txtNode.Text = ObjModel.Node;
                rdoState.SelectedIndex = ObjModel.State;
                btnSaveChange.Visible = false;

                if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))         //管理层登录
                {

                    if (ObjModel.State == 1)            //暂定
                    {
                        btnChange.Visible = true;
                    }
                    else                                        //预定
                    {
                        btnChange.Visible = true;
                        rdoState.Enabled = false;
                    }

                    btnDelete.Visible = true;
                }

                if (User.Identity.Name.ToInt32() == ObjModel.EmployeeID && !(ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32())))    //录入人和登陆人是本人
                {

                    if (ObjModel.State == 1)            //暂定
                    {
                        btnChange.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else                                     //预定  不能取消 修改
                    {
                        btnChange.Visible = false;
                        rdoState.Enabled = false;
                        btnDelete.Visible = false;
                    }

                }



            }
            else
            {
                lblPartyDate.Text = Request["DateTime"];
                lblEmployeeName.Text = GetEmployeeName(User.Identity.Name.ToInt32());
                lblTimerSpan.Text = Request["SourceItem"];
            }
        }


        /// <summary>
        /// 创建修改数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            CC_PackageReserve ObjCCModel = new CC_PackageReserve();
            ObjCCModel.CreateDate = DateTime.Now;
            ObjCCModel.State = rdoState.SelectedIndex;
            ObjCCModel.Node = txtNode.Text;
            ObjCCModel.IsDelete = false;
            ObjCCModel.DateItem = Request["SourceItem"];
            ObjCCModel.PartyDate = Request["DateTime"].ToDateTime();
            ObjCCModel.PackageID = Request["PackageID"].ToInt32();
            ObjCCModel.EmployeeID = User.Identity.Name.ToInt32();


            ObjPackageReserveBLL.Insert(ObjCCModel);
            JavaScriptTools.ClickParentControl("操作成功!", "btnSerch", Page);

        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var ObjModel = ObjPackageReserveBLL.GetOnlyModel(Request["DateTime"].ToDateTime(), Request["SourceItem"], Request["PackageID"].ToInt32());
            ObjPackageReserveBLL.Delete(ObjModel);
            JavaScriptTools.ClickParentControl("取消成功!", "btnSerch", Page);
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            var ObjCCModel = ObjPackageReserveBLL.GetOnlyModel(Request["DateTime"].ToDateTime(), Request["SourceItem"], Request["PackageID"].ToInt32());
            ObjCCModel.CreateDate = DateTime.Now;
            ObjCCModel.State = rdoState.SelectedIndex;
            ObjCCModel.Node = txtNode.Text;
            ObjCCModel.IsDelete = false;
            ObjCCModel.DateItem = Request["SourceItem"];
            ObjCCModel.PartyDate = Request["DateTime"].ToDateTime();
            ObjCCModel.PackageID = Request["PackageID"].ToInt32();
            ObjCCModel.UpdateEmployee = User.Identity.Name.ToInt32();
            ObjCCModel.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
            ObjPackageReserveBLL.Update(ObjCCModel);
            JavaScriptTools.ClickParentControl("更改成功!", "btnSerch", Page);
        }
    }
}