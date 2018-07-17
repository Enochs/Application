using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Sys;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceDispatchingUpdate : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        /// <summary>
        /// 客户
        /// </summary>
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();

        StorehouseSourceProduct storehouseSourceProductBLL = new StorehouseSourceProduct();
        /// <summary>
        /// 报价单
        /// </summary>
        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();

        int QuotedID = 0;
        int OrderID = 0;
        int CustomerID = 0;
        int QuotedEmployee = 0;
        string Type = "";

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            QuotedID = Request["QuotedID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();

            QuotedEmployee = Request["QuotedEmployee"].ToInt32();
            if (!IsPostBack)
            {
                BinderData();
            }
        }
        #endregion

        #region 数据绑定 BinderData
        /// <summary>
        /// 绑定
        /// </summary>
        public void BinderData()
        {

            QuotedID = Request["QuotedID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();

            QuotedEmployee = Request["QuotedEmployee"].ToInt32();
            if (QuotedEmployee == User.Identity.Name.ToInt32())
            {
                Type = "Dispatching";
            }
            else
            {
                Type = "Look";
            }

            var ObjList = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, 0);

            this.repfirst.DataSource = ObjList;
            this.repfirst.DataBind();

            IsVisible();

        }
        #endregion

        #region 外围数据绑定 ItemDataBound
        /// <summary>
        /// 内部Repeater绑定
        /// </summary>
        protected void repfirst_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField ObjHiddCategKey = (HiddenField)e.Item.FindControl("hidefirstCategoryID");
            Repeater ObjRep = (Repeater)e.Item.FindControl("repdatalist");
            var ObjItemList = new List<FL_QuotedPriceItems>();

            var DataList = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, ObjHiddCategKey.Value.ToInt32(), 2).OrderByDescending(C => C.ParentCategoryID).ToList();
            //如果没有二级 则只有一级项目
            if (DataList.Count == 0)
            {
                var NewList = ObjQuotedPriceItemsBLL.GetOnlyByCatageID(QuotedID, ObjHiddCategKey.Value.ToInt32(), 1);
                DataList.Add(NewList);
            }

            //获取产品级项目
            foreach (var ObjItem in DataList)
            {
                var ItemList = ObjQuotedPriceItemsBLL.GetByCategoryID(QuotedID, ObjItem.CategoryID, 3);
                if (ItemList.Count == 0)
                {
                    ObjItemList.Add(ObjItem);
                }
                else
                {
                    ItemList[0].ItemLevel = 2;
                    ObjItemList.AddRange(ItemList);
                }
            }

            ObjRep.DataSource = ObjItemList;
            ObjRep.DataBind();
        }
        #endregion

        #region 点击保存按钮
        /// <summary>
        /// 保存
        /// </summary>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            SaveChange();
        }
        #endregion

        #region 保存数据 保存分项
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repfirst_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SaveItem")
            {
                SaveChange();
            }
        }
        #endregion

        #region 点击确认按钮
        /// <summary>
        /// 确认
        /// </summary>
        protected void btn_Confirm_Click(object sender, EventArgs e)
        {
            SaveChange();
            QuotedID = Request["QuotedID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();

            Report ObjReportBLL = new Report();
            var ObjReportModel = ObjReportBLL.GetByCustomerID(CustomerID, User.Identity.Name.ToInt32());    //修改签约时间
            ObjReportModel.QuotedDateSucessDate = DateTime.Now;
            ObjReportBLL.Update(ObjReportModel);

            //操作日志
            CreateHandle(2);        //1.代表打回订单 2.代表确认派工

            string UrlPar = "QuotedID=" + QuotedID + "&OrderID=" + OrderID + "&CustomerID=" + CustomerID;
            JavaScriptTools.AlertWindowAndLocation("保存成功,请选择总调度!", "/AdminPanlWorkArea/QuotedPrice/QuotedPriceSplit/QuotedPriceFinishNextPage.aspx?" + UrlPar, Page);
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repdatalist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                QuotedID = Request["QuotedID"].ToInt32();
                int ChangeID = e.CommandArgument.ToString().ToInt32();
                var ObjQuotedItem = ObjQuotedPriceItemsBLL.GetByID(ChangeID);
                var DataList = ObjQuotedPriceItemsBLL.GetByCategoryID(QuotedID, ObjQuotedItem.CategoryID);
                if (DataList.Count == 2)
                {
                    var QuotedModel = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, ObjQuotedItem.ParentCategoryID);
                    if (QuotedModel == null || QuotedModel.Count == 0)          //删除一级项目
                    {
                        ObjQuotedPriceItemsBLL.Delete(ObjQuotedPriceItemsBLL.GetByID(ObjQuotedItem.ParentCategoryID));
                    }

                    foreach (var item in DataList)      //删除二级项目
                    {
                        ObjQuotedPriceItemsBLL.Delete(item);
                    }
                }
                else
                {
                    ObjQuotedPriceItemsBLL.Delete(ObjQuotedItem);   //删除三级项目
                }
            }
            BinderData();
        }
        #endregion

        #region 页面状态



        public string GetKindImage(object Kind)
        {
            var ObjImageList = ObjQuotedPriceBLL.GetImageByKind(QuotedID, Kind.ToString().ToInt32(), 1);
            string ImageList = string.Empty;
            foreach (var ObjImage in ObjImageList)
            {
                ImageList += "<img alt='' src='" + ObjImage.FileAddress + "' />";
            }
            //<img alt="" src="../../Images/Appraise/3.gif" />
            return ImageList;
        }

        public string HideforNoneImage(object Kind)
        {
            var ObjImageList = ObjQuotedPriceBLL.GetImageByKind(QuotedID, Kind.ToString().ToInt32(), 1);
            if (ObjImageList.Count > 0)
            {
                return string.Empty;
            }
            else
            {
                return "style='display: none;'";

            }

        }


        /// <summary>
        /// 隐藏选择项目
        /// </summary>
        /// <returns></returns>
        public string HideSelectItem(object Level)
        {
            //return string.Empty;
            if (Level != null)
            {
                if (Level.ToString() == "0")
                {
                    return "style='color:#0b0fee;'";
                }
                else
                {
                    return "style='display:none;'";
                }
            }
            else
            {
                return "style='display:none;'";
            }

        }


        /// <summary>
        /// 隐藏选择产品
        /// </summary>
        /// <returns></returns>
        public string HideSelectProduct(object Level)
        {
            //return string.Empty;
            if (Level != null)
            {
                if (Level.ToString() == "2")
                {
                    return string.Empty; //"style='color:#0b0fee;'";
                }
                else
                {
                    return "style='display:none;'";
                }
            }
            else
            {
                return "style='display:none;'";
            }
        }


        /// <summary>
        /// 根据类型ID获取类型名称
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetProductByID(object Key)
        {
            if (Key != null)
            {
                return ObjAllProductsBLL.GetByID(Key.ToString().ToInt32()).ProductName;
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region 获取数量等页面状态

        protected void ddlfengge_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.ddlPackgeName.DataSource = ObjQuotedPriceBLL.GetByType(2).Where(C=>C.PakegTyper==ddlfengge.SelectedItem.Text);
            //this.ddlPackgeName.DataTextField = "QuotedTitle";
            //this.ddlPackgeName.DataValueField = "QuotedID";
            //this.ddlPackgeName.DataBind();
        }

        public string GetAvailableCount(object productid, object rowType)
        {
            return Convert.ToInt32(rowType) == 2 ? storehouseSourceProductBLL.GetAvailableCount(Convert.ToInt32(productid), Convert.ToInt32(Request["CustomerID"])).ToString() : string.Empty;
        }



        public string GetChangeSummoney(object ChangeID)
        {
            QuotedSourceItem ObjItemBLL = new QuotedSourceItem();
            return ObjItemBLL.GetByChangeID(ChangeID.ToString().ToInt32()).Sum(C => C.Money).ToString();

        }
        #endregion

        #region 保存 SaveChange方法
        /// <summary>
        /// 保存
        /// </summary>
        public void SaveChange()
        {

            for (int P = 0; P < repfirst.Items.Count; P++)
            {
                Repeater ObjrepList = (Repeater)repfirst.Items[P].FindControl("repdatalist");
                FL_QuotedPriceItems ObjItem;
                decimal? ItemSumMoney = 0;
                //保存主体
                for (int I = 0; I < ObjrepList.Items.Count; I++)
                {
                    ObjItem = ObjQuotedPriceItemsBLL.GetByID(((HiddenField)ObjrepList.Items[I].FindControl("hidePriceKey")).Value.ToInt32());
                    ObjItem.ImageUrl = string.Empty;
                    ObjItem.Requirement = ((TextBox)ObjrepList.Items[I].FindControl("txtRequirement")).Text;
                    ObjItem.Quantity = ((TextBox)ObjrepList.Items[I].FindControl("txtQuantity")).Text.ToInt32();
                    ObjItem.Remark = ((TextBox)ObjrepList.Items[I].FindControl("txtRemark")).Text;
                    ObjItem.UnitPrice = ((TextBox)ObjrepList.Items[I].FindControl("txtSalePrice")).Text.ToDecimal();
                    ObjItem.IsChange = false;
                    ObjItem.IsDelete = false;
                    ObjItem.IsSvae = true;
                    ObjItem.Subtotal = (ObjItem.UnitPrice * ObjItem.Quantity).ToString().ToDecimal();
                    ItemSumMoney += ObjItem.Subtotal;
                    ObjQuotedPriceItemsBLL.Update(ObjItem);
                }
                //保存分项合计
                HiddenField ObjHiddKey = (HiddenField)repfirst.Items[P].FindControl("hideKey");
                ObjItem = ObjQuotedPriceItemsBLL.GetByID(ObjHiddKey.Value.ToInt32());
                ObjItem.ItemSaleAmount = ((TextBox)repfirst.Items[P].FindControl("txtSaleItem")).Text.ToDecimal();
                ObjItem.ItemAmount = ItemSumMoney;
                ObjQuotedPriceItemsBLL.Update(ObjItem);
                ItemSumMoney = 0;
                //更新报价单主体
            }
            BinderData();
        }
        #endregion

        #region 打回订单
        /// <summary>
        /// 订单打回
        /// </summary>
        protected void btn_BackOrder_Click(object sender, EventArgs e)
        {
            QuotedID = Request["QuotedID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();

            ///修改客户状态
            var ObjCustomerModel = ObjCustomerBLL.GetByID(CustomerID);
            ObjCustomerModel.State = (int)CustomerStates.SucessOrder;
            ObjCustomerBLL.Update(ObjCustomerModel);

            //还原订单信息
            FL_QuotedPrice ObjQuotePriceModel = ObjQuotedPriceBLL.GetByQuotedID(QuotedID);
            ObjQuotePriceModel.IsChecks = false;        //未审核
            ObjQuotePriceModel.IsDispatching = 0;       //相当于状态()
            ObjQuotePriceModel.FinishAmount = 0;        //由于日报表原因 所以修改价格
            ObjQuotePriceModel.RealAmount = 0;
            ObjQuotedPriceBLL.Update(ObjQuotePriceModel);

            //修改下单时间为null（或者为空）
            Report ObjReportBLL = new Report();
            SS_Report ObjReportModel = ObjReportBLL.GetByCustomerID(CustomerID);
            WorkReport ObjWorkRportBLL = new WorkReport();
            //修改日报表
            var ObjWorkReportModel = ObjWorkRportBLL.GetEntityByTimeCustomerID(User.Identity.Name.ToInt32(), ObjReportModel.QuotedDateSucessDate.ToString().ToDateTime().ToString("yyyy-MM-dd").ToDateTime());
            ObjWorkReportModel.OrderAmount = 0;
            ObjWorkReportModel.QuotedCheckNum = 0;
            ObjWorkRportBLL.Update(ObjWorkReportModel);

            ObjReportModel.QuotedDateSucessDate = null;
            ObjReportBLL.Update(ObjReportModel);

            //操作日志
            CreateHandle(1);

            JavaScriptTools.AlertWindowAndLocation("打回订单成功", "QuotedPriceListCreateEdit.aspx?SaleEmployee=" + ObjQuotePriceModel.SaleEmployee + "&OrderID=" + OrderID + "&QuotedID=" + QuotedID + "&CustomerID=" + CustomerID + "&PartyDate=" + ObjQuotedPriceBLL.GetByViewQuotedID(QuotedID).PartyDate, Page);


        }
        #endregion

        #region 相应的隐藏功能
        /// <summary>
        /// 隐藏
        /// </summary>
        public void IsVisible()
        {
            if (Type == "Look")       //不是本人 或者点击查看
            {
                for (int i = 0; i < repfirst.Items.Count; i++)
                {
                    (repfirst.Items[i].FindControl("btnSaveItem") as Button).Visible = false;
                    Repeater repDataList = repfirst.Items[i].FindControl("repdatalist") as Repeater;
                    for (int j = 0; j < repDataList.Items.Count; j++)
                    {
                        (repDataList.Items[j].FindControl("txtRemark") as TextBox).ReadOnly = true;
                    }
                }
                btn_Confirm.Visible = false;
                btn_Save.Visible = false;
                btn_BackOrder.Visible = false;
            }
        }
        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle(int Type)
        {
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();
            var Model = ObjCustomerBLL.GetByCustomerID(Request["CustomerID"].ToInt32());
            if (Type == 1)
            {
                HandleModel.HandleContent = "策划报价-派工明细,客户姓名:" + Model.Bride + "/" + Model.Groom + ",打回订单";
            }
            else
            {
                HandleModel.HandleContent = "策划报价-派工明细,客户姓名:" + Model.Bride + "/" + Model.Groom + ",确认派工";
            }
            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 3;     //策划报价
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion
    }
}