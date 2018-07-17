using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.Pages;
using HA.PMS.EditoerLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceSplit
{
    public partial class OrderMissionCreateManager : SystemPage
    {
        /// <summary>
        /// 用户操作
        /// </summary>
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();

        /// <summary>
        /// 
        /// </summary>
        Dispatching ObjDispatchingBLL = new Dispatching();


        WeddingPlanning ObjWeddingPlanningBLL = new WeddingPlanning();

        HA.PMS.BLLAssmblly.Sys.WeddingPlanning ObjSysWeddingPlanningBLL = new BLLAssmblly.Sys.WeddingPlanning();
        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPrice = new BLLAssmblly.Flow.QuotedPrice();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
                if (Request["Dis"] != null)
                {
                    hideIsDis.Value = "1";

                    txtEmpLoyee.Value = GetEmployeeName(User.Identity.Name.ToInt32());
                }
            }
        }

        /// <summary>
        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        private void BinderData()
        {

            int startIndex = WeddingPlanningPager.StartRecordIndex;
            //如果是最后一页，则重新设置起始记录索引，以使最后一页的记录数与其它页相同，如总记录有101条，每页显示10条，如果不使用此方法，则第十一页即最后一页只有一条记录，使用此方法可使最后一页同样有十条记录。

            int resourceCount = 0;

            var query = ObjWeddingPlanningBLL.GetByOrderIdIndex(Request["OrderID"].ToInt32(), WeddingPlanningPager.PageSize, WeddingPlanningPager.CurrentPageIndex, "未完成", out resourceCount); ;

            WeddingPlanningPager.RecordCount = resourceCount;

            repWeddingPlanning.DataSource = query;
            repWeddingPlanning.DataBind();
            //var DataList = ObjWeddingPlanningBLL.GetByOrderID(Request["OrderID"].ToInt32());
            ////DataList.Add(new FL_WeddingPlanning());
            ////DataList.Reverse();
            //this.repWeddingPlanning.DataSource = DataList.OrderByDescending(C=>C.CreateDate.Value);
            //this.repWeddingPlanning.DataBind();
            ddlCategory.DataSource = ObjSysWeddingPlanningBLL.GetByparent(0);
            ddlCategory.DataTextField = "PlanningName";
            ddlCategory.DataValueField = "PlanningID";
            ddlCategory.DataBind();

            if (ddlCategory.Items.Count > 0)
            {
                ddlCategoryItem.Items.Clear();
                ddlCategoryItem.DataSource = ObjSysWeddingPlanningBLL.GetByparent(ddlCategory.Items[0].Value.ToInt32());
                ddlCategoryItem.DataTextField = "PlanningName";
                ddlCategoryItem.DataValueField = "PlanningID";
                ddlCategoryItem.DataBind();
            }

        }


        /// <summary>
        /// 根据用户ID获取报价单ID
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetQuotedIDByCustomers(object CustomerID)
        {
            return ObjQuotedPrice.GetByCustomerID(CustomerID.ToString().ToInt32()).QuotedID.ToString();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }


        /// <summary>
        /// 保存添加的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            if (txtRequirement.Text != string.Empty && txtServiceContent.Text != string.Empty)
            {
                if (txtRemark.Text.Length > 75)
                {
                    JavaScriptTools.AlertWindow("备注不能超过70个汉子", Page);
                    return;
                }
                FL_WeddingPlanning ObjWeddingPlanningModel;

                ObjWeddingPlanningModel = new FL_WeddingPlanning();
                ObjWeddingPlanningModel.ServiceContent = txtServiceContent.Text;
                ObjWeddingPlanningModel.Requirement = txtRequirement.Text;
                ObjWeddingPlanningModel.Remark = txtRemark.Text;
                ObjWeddingPlanningModel.PlanFinishDate = txtPlanFinishDate.Text.ToDateTime();
                ObjWeddingPlanningModel.EmpLoyeeID = hideEmpLoyeeID.Value.ToInt32();
                ObjWeddingPlanningModel.CreateDate = DateTime.Now;
                ObjWeddingPlanningModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
                ObjWeddingPlanningModel.IsDelete = false;
                ObjWeddingPlanningModel.OrderID = Request["OrderID"].ToInt32();

                ObjWeddingPlanningModel.OrderCoder = ObjOrderBLL.GetByID(Request["OrderID"].ToInt32()).OrderCoder;
                ObjWeddingPlanningModel.Category = ddlCategory.SelectedItem.Text;
                ObjWeddingPlanningModel.CategoryItem = ddlCategoryItem.SelectedItem.Text;
                ObjWeddingPlanningModel.State = "未完成";
                string FileName = Guid.NewGuid().ToString() + FileImage.FileName;
                string FileAddress = "/Files/WeddingPlanning/" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "/" + FileName;
                if (FileImage.HasFile)
                {

                    string Dir = string.Empty;
                    if (!System.IO.Directory.Exists(Server.MapPath("/Files/WeddingPlanning/" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "/")))
                    {
                        Dir = Server.MapPath("/Files/WeddingPlanning/" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "/");
                        System.IO.Directory.CreateDirectory(Dir);
                        FileImage.SaveAs(Dir + FileName);
                    }
                    else
                    {
                        Dir = Server.MapPath("/Files/WeddingPlanning/" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "/");
                        FileImage.SaveAs(Dir + FileName);
                    }
                }


                ObjWeddingPlanningModel.ImageAddress = FileAddress;
                ObjWeddingPlanningBLL.Insert(ObjWeddingPlanningModel);

                var CustomerID = ObjOrderBLL.GetByID(Request["OrderID"].ToInt32()).CustomerID;
                MissionManager ObjMissManagerBLL = new MissionManager();

                string Titles = "(" + ddlCategory.SelectedItem.Text + "-" + ddlCategoryItem.SelectedItem.Text + ")";
                if (FileImage.HasFile)
                {
                    ObjMissManagerBLL.WeddingMissionCreate(CustomerID.Value, 2, (int)MissionTypes.Plan, ObjWeddingPlanningModel.PlanFinishDate.Value, ObjWeddingPlanningModel.EmpLoyeeID.Value, "?CustomerID=" + CustomerID.ToString(), MissionChannel.DispatchingManager, User.Identity.Name.ToInt32(), ObjWeddingPlanningModel.PlanningID, txtServiceContent.Text, txtRequirement.Text, FileAddress, Titles);
                }
                else
                {
                    ObjMissManagerBLL.WeddingMissionCreate(CustomerID.Value, 2, (int)MissionTypes.Plan, ObjWeddingPlanningModel.PlanFinishDate.Value, ObjWeddingPlanningModel.EmpLoyeeID.Value, "?CustomerID=" + CustomerID.ToString(), MissionChannel.DispatchingManager, User.Identity.Name.ToInt32(), ObjWeddingPlanningModel.PlanningID, txtServiceContent.Text, txtRequirement.Text, string.Empty, Titles);
                }
                Response.Redirect(this.Request.Url.ToString());
            }
            else
            {
                JavaScriptTools.AlertWindow("任务要求不能为空！", Page);
            }
            // var DataList = ObjWeddingPlanningBLL.GetByOrderID(Request["OrderID"].ToInt32());
            //DataList.Add(new FL_WeddingPlanning());
            //DataList.Reverse();
            //this.repWeddingPlanning.DataSource = DataList;
            //this.repWeddingPlanning.DataBind();
            // Response.Redirect(Request.Url.ToString());

        }


        /// <summary>
        /// 保存所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            FL_WeddingPlanning ObjWeddingPlanningModel = new FL_WeddingPlanning();
            ObjWeddingPlanningBLL.Insert(ObjWeddingPlanningModel);
            JavaScriptTools.AlertAndClosefancybox("保存成功", this.Page);

        }

        protected void repWeddingPlanning_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var CustomerID = ObjOrderBLL.GetByID(Request["OrderID"].ToInt32()).CustomerID;
            WeddingPlanFile ObjWeddingPlanFileBLL = new WeddingPlanFile();
            if (e.CommandName == "Delete")
            {
                int PlanningID = ((HiddenField)e.Item.FindControl("hideKey")).Value.ToInt32();
                ObjWeddingPlanningBLL.Delete(new FL_WeddingPlanning() { PlanningID = PlanningID });

                var WeddingPlanFileList = ObjWeddingPlanFileBLL.GetByPlanningID(PlanningID);
                foreach (FL_WeddingPlanFile Item in WeddingPlanFileList)
                {
                    //1.删除文件
                    System.IO.File.Delete(Server.MapPath(Item.FileAddress));
                    //2.删除记录
                    ObjWeddingPlanFileBLL.Delete(Item);
                }
            }
            else
            {
                var ObjWeddingModel = (HiddenField)e.Item.FindControl("hideKey");
                var ObjUpdateModel = ObjWeddingPlanningBLL.GetByID(ObjWeddingModel.Value.ToInt32());
                ObjUpdateModel.EmpLoyeeID = ((HiddenField)e.Item.FindControl("hideEmpLoyeeID")).Value.ToInt32();
                ObjUpdateModel.PlanFinishDate = ((DateEditTextBox)e.Item.FindControl("txtPlanFinishDate")).Text.ToDateTime();
                ObjUpdateModel.Remark = ((TextBox)e.Item.FindControl("txtRemark")).Text;
                ObjUpdateModel.ServiceContent = ((TextBox)e.Item.FindControl("txtServiceContent")).Text;
                ObjUpdateModel.Requirement = ((TextBox)e.Item.FindControl("txtRequirement")).Text;
                ObjWeddingPlanningBLL.Update(ObjUpdateModel);

                MissionManager ObjMissManagerBLL = new MissionManager();
                ObjMissManagerBLL.WeddingMissionCreate(CustomerID.Value, 2, (int)MissionTypes.Plan, ObjUpdateModel.PlanFinishDate.Value, ObjUpdateModel.EmpLoyeeID.Value, "?CustomerID=" + CustomerID.ToString(), MissionChannel.FL_TelemarketingManager, User.Identity.Name.ToInt32(), ObjUpdateModel.PlanningID);
            }

            BinderData();
            JavaScriptTools.AlertWindow("保存成功", Page);
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategoryItem.Items.Clear();
            ddlCategoryItem.DataSource = ObjSysWeddingPlanningBLL.GetByparent(ddlCategory.SelectedItem.Value.ToInt32());
            ddlCategoryItem.DataTextField = "PlanningName";
            ddlCategoryItem.DataValueField = "PlanningID";
            ddlCategoryItem.DataBind();
        }

        protected void WeddingPlanningPager_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void btnReturnList_Click(object sender, EventArgs e)
        {
            Response.Redirect("CarrytaskWeddingList.aspx");
        }

    }
}