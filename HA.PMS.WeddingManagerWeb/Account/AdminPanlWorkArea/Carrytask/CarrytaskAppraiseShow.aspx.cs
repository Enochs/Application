using System;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskAppraiseShow : SystemPage
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
            this.repOrderAppraise.DataSource = OrderModelList.Where(C => C.Type == 1);
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



        protected void repSupplily_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var ObjItem = (FL_OrderAppraise)e.Item.DataItem;
            var Objddl = (DropDownList)e.Item.FindControl("ddlPoint");
            var Objddl1 = (DropDownList)e.Item.FindControl("ddErro");


            if (ObjItem.Point != null && Objddl.Items.FindByValue(ObjItem.Point.ToString()) != null)
            {
                Objddl.Items.FindByValue(ObjItem.Point.ToString()).Selected = true;
            }

            if (ObjItem.ErroState != null && Objddl1.Items.FindByValue(ObjItem.ErroState.ToString()) != null)
            {
                Objddl1.Items.FindByValue(ObjItem.ErroState.ToString()).Selected = true;
            }
        }

        protected void repOrderAppraise_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var ObjItem = (FL_OrderAppraise)e.Item.DataItem;
            var Objddl = (DropDownList)e.Item.FindControl("ddlPoint");
            var Objddl1 = (DropDownList)e.Item.FindControl("ddErro");


            if (ObjItem.Point != null && Objddl.Items.FindByValue(ObjItem.Point.ToString()) != null)
            {
                Objddl.Items.FindByValue(ObjItem.Point.ToString()).Selected = true;
            }

            if (ObjItem.ErroState != null && Objddl1.Items.FindByValue(ObjItem.ErroState.ToString()) != null)
            {
                Objddl1.Items.FindByValue(ObjItem.ErroState.ToString()).Selected = true;
            }
        }
    }
}