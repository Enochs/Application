using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class OrderAppraise : SystemPage
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

        /// <summary>
        /// 
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.OrderAppraise ObjOrderAppraiseBLL = new HA.PMS.BLLAssmblly.Flow.OrderAppraise();
    


        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPrice = new BLLAssmblly.Flow.QuotedPrice();

        Employee ObjEmployeeBLL = new Employee();
 


        Supplier ObjSupplierBLL = new Supplier();
        int OrderId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderId = Request["OrderId"].ToInt32();
            if (!IsPostBack)
            {
                //FirstCreate();
                BinderData();
                var ObjUpdateModel = ObjDispatchingBLL.GetByOrder(OrderId);
                ObjUpdateModel.IsAppraise = true;

                if (ObjUpdateModel.AppraiseOver == true)
                {
                    btnLock.Visible = false;
                    Button1.Visible = false;
                }

                ObjDispatchingBLL.Update(ObjUpdateModel);
              
            }

        }



        /// <summary>
        /// 首次创建
        /// </summary>
        public void FirstCreate()
        {
          
            var ObjUpdateModel = ObjDispatchingBLL.GetByOrder(OrderId);
            ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();
            if (ObjUpdateModel.IsAppraise == false || ObjUpdateModel.IsAppraise == null)
            {

                var EmployeeIDList = ObjDispatchingBLL.GetEmployeeByOrderID(OrderId);


                var SupperList = ObjProductforDispatchingBLL.GetSupperKeyListByDispatchingID(ObjDispatchingBLL.GetByOrder(OrderId).DispatchingID);

                foreach (var ObjEmpLoyeeID in EmployeeIDList)
                {
                    ObjOrderAppraiseBLL.Insert(new FL_OrderAppraise()
                    {
                        OrderID = OrderId,
                        KindID = ObjEmpLoyeeID,
                        Point = 0,
                        Remark = string.Empty,
                        Type = 1
                    });
                }




                foreach (var ObjSK in SupperList)
                {
                    ObjOrderAppraiseBLL.Insert(new FL_OrderAppraise()
                    {
                        OrderID = OrderId,
                        KindID = ObjSK,
                        Point = 0,
                        Remark = string.Empty,
                        Type = 3
                    });
                }


                ObjUpdateModel.IsAppraise = true;
                ObjDispatchingBLL.Update(ObjUpdateModel);


            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            var OrderModelList = ObjOrderAppraiseBLL.GetByOrder(Request["OrderID"].ToInt32());
            this.repOrderAppraise.DataSource = OrderModelList.Where(C=>C.Type==1);
            this.repOrderAppraise.DataBind();

            this.repSupplily.DataSource = OrderModelList.Where(C => C.Type == 3);
            this.repSupplily.DataBind();
        }



        /// <summary>
        /// 获取评价的元素
        /// </summary>
        /// <param name="KindID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string GetAppraiseItem(object KindID, object Type)
        {

            //if (Type.ToString() == "1")
            //{
            //    return ObjEmployeeBLL.GetByID(KindID.ToString().ToInt32()).EmployeeName;
            //}
 

            //if (Type.ToString() == "3")
            //{
            //  return  ObjSupplierBLL.GetByID(KindID.ToString().ToInt32()).Name;
            //}

            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            DropDownList ObjDDLPoint;
            OrderfinalCost ObjOrderfinalCostBLL = new OrderfinalCost();
            for (int i = 0; i < repOrderAppraise.Items.Count; i++)
            {
                ObjDDLPoint = (DropDownList)repOrderAppraise.Items[i].FindControl("ddlPoint");
                var ObjHideKey = (HiddenField)repOrderAppraise.Items[i].FindControl("hideKey");
                var OrderAppraiseModel = ObjOrderAppraiseBLL.GetByID(ObjHideKey.Value.ToInt32());
                OrderAppraiseModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
                OrderAppraiseModel.OrderID = Request["OrderID"].ToInt32();
                OrderAppraiseModel.Point = ObjDDLPoint.SelectedValue.ToInt32();
                OrderAppraiseModel.PointTitle = ObjDDLPoint.SelectedItem.Text;
                OrderAppraiseModel.Remark = ((TextBox)repOrderAppraise.Items[i].FindControl("txtRemark")).Text;
                OrderAppraiseModel.Suggest = ((TextBox)repOrderAppraise.Items[i].FindControl("txtSuggest")).Text;
                OrderAppraiseModel.ErroState = ((DropDownList)repOrderAppraise.Items[i].FindControl("ddErro")).SelectedValue.ToInt32();
                OrderAppraiseModel.KindID = ObjHideKey.Value.ToInt32();
                OrderAppraiseModel.CreateDate = DateTime.Now;
                ObjOrderAppraiseBLL.Update(OrderAppraiseModel);


                var ObjUpdateModel = ObjOrderfinalCostBLL.GetByPremissionnalName(OrderAppraiseModel.AppraiseTitle, ObjDispatchingBLL.GetByOrder(OrderId).DispatchingID);
                if (ObjUpdateModel != null)
                {
                    ObjUpdateModel.InsideRemark = OrderAppraiseModel.Remark;
                    ObjOrderfinalCostBLL.Update(ObjUpdateModel);
                }
            }


            for (int i = 0; i < repSupplily.Items.Count; i++)
            {
                ObjDDLPoint = (DropDownList)repSupplily.Items[i].FindControl("ddlPoint");
                var ObjHideKey = (HiddenField)repSupplily.Items[i].FindControl("hideKey");
                var OrderAppraiseModel = ObjOrderAppraiseBLL.GetByID(ObjHideKey.Value.ToInt32());
                OrderAppraiseModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
                OrderAppraiseModel.OrderID = Request["OrderID"].ToInt32();
                OrderAppraiseModel.Point = ObjDDLPoint.SelectedValue.ToInt32();
                OrderAppraiseModel.PointTitle = ObjDDLPoint.SelectedItem.Text;
                OrderAppraiseModel.Remark = ((TextBox)repSupplily.Items[i].FindControl("txtRemark")).Text;
                OrderAppraiseModel.Suggest = ((TextBox)repSupplily.Items[i].FindControl("txtSuggest")).Text;
                OrderAppraiseModel.ErroState = ((DropDownList)repSupplily.Items[i].FindControl("ddErro")).SelectedValue.ToInt32();
                OrderAppraiseModel.KindID = ObjHideKey.Value.ToInt32();

                var ObjSupplierModel=ObjSupplierBLL.GetByName(OrderAppraiseModel.AppraiseTitle);
                if (ObjSupplierModel != null)
                {
                    OrderAppraiseModel.SupplierID = ObjSupplierModel.SupplierID;
                }
                OrderAppraiseModel.CreateDate = DateTime.Now;
                ObjOrderAppraiseBLL.Update(OrderAppraiseModel);

               var ObjUpdateModel= ObjOrderfinalCostBLL.GetBySupplilyName(OrderAppraiseModel.AppraiseTitle, ObjDispatchingBLL.GetByOrder(OrderId).DispatchingID);
               if (ObjUpdateModel != null)
               {
                   ObjUpdateModel.InsideRemark = OrderAppraiseModel.Remark;
                   ObjOrderfinalCostBLL.Update(ObjUpdateModel);
               }

            }

            JavaScriptTools.AlertWindow("保存成功!",Page);
           // Response.Redirect("CarrytaskAppraise.aspx");

        }

        protected void btnLock_Click(object sender, EventArgs e)
        {
            //更新状态
            var ObjDisUpdateModel = ObjDispatchingBLL.GetByOrder(OrderId);
            ObjDisUpdateModel.Isover = true;
            ObjDisUpdateModel.IsAppraise = true;
            ObjDisUpdateModel.AppraiseOver = true;
            
            Button1.Visible = false;
            btnLock.Visible = false;
            ObjDispatchingBLL.Update(ObjDisUpdateModel);
        }

        protected void repSupplily_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var ObjItem=(FL_OrderAppraise)e.Item.DataItem;
            var Objddl=(DropDownList)e.Item.FindControl("ddlPoint");
            var Objddl1=(DropDownList)e.Item.FindControl("ddErro");


            if (ObjItem.Point != null)
            {
                Objddl.ClearSelection();
                if (Objddl.Items.FindByValue(ObjItem.Point.ToString()) != null)
                {
                    Objddl.Items.FindByValue(ObjItem.Point.ToString()).Selected = true;
                }
            }

            if (ObjItem.ErroState != null)
            {
                Objddl1.ClearSelection();
                if (Objddl1.Items.FindByValue(ObjItem.ErroState.ToString()) != null)
                {
                    Objddl1.Items.FindByValue(ObjItem.ErroState.ToString()).Selected = true;
                }
            }
        }

        protected void repOrderAppraise_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var ObjItem = (FL_OrderAppraise)e.Item.DataItem;
            var Objddl = (DropDownList)e.Item.FindControl("ddlPoint");
            var Objddl1 = (DropDownList)e.Item.FindControl("ddErro");

            

            if (ObjItem.Point != null)
            {
                Objddl.ClearSelection();
                if (Objddl.Items.FindByValue(ObjItem.Point.ToString()) != null)
                {
                    Objddl.Items.FindByValue(ObjItem.Point.ToString()).Selected = true;
                }
            }

            if (ObjItem.ErroState != null)
            {
                Objddl1.ClearSelection();
                if (Objddl1.Items.FindByValue(ObjItem.ErroState.ToString()) != null)
                {
                    Objddl1.Items.FindByValue(ObjItem.ErroState.ToString()).Selected = true;
                }
            }
        }
    }
}