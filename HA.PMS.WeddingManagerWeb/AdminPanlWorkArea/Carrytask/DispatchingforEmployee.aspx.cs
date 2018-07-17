//执行中的订单
using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using System.Data.Objects;
using System.Linq;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class DispatchingforEmployee : SystemPage
    {
        QuotedPriceSchedule ObjScheduleBLL = new QuotedPriceSchedule();

        ProductforDispatching ObjProductForDispatchingBLL = new ProductforDispatching();

        //派工人 StateEmployee
        DispatchingState ObjState = new DispatchingState();

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }
        #endregion

        #region 数据加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void BinderData(object sender, EventArgs e)
        {
            List<PMSParameters> paramsList = new List<PMSParameters>();


            //paramsList.Add("StateEmpLoyee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);
            //paramsList.Add("IsUse", true, NSqlTypes.Bit);

            //MyManager.GetEmployeePar(paramsList,"QuotedEmployee");
            //新人姓名
            CstmNameSelector.AppandTo(paramsList);
            //婚期
            if (PartyDateRanger.IsNotBothEmpty)
            {
                paramsList.Add("PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);
            }
            else
            {
                paramsList.Add("PartyDate", "2014-10-01,9999-12-31", NSqlTypes.DateBetween);
                paramsList.Add("UseSate", 19 + "," + 208 + "," + 207, NSqlTypes.IN);
            }
            //酒店

            paramsList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text.Trim(), NSqlTypes.LIKE);
            //状态
            if (ddlStates.SelectedValue.ToInt32() == 1)
            {
                paramsList.Add("UseSate", 206, NSqlTypes.NotEquals);
            }
            else if (ddlStates.SelectedValue.ToInt32() == 2)
            {
                paramsList.Add("UseSate", 206, NSqlTypes.Equal);
            }

            if (User.Identity.Name.ToInt32() != 6)
            {
                //派工人
                if (MyManager1.SelectedValue.ToInt32() > 0)
                {
                    paramsList.Add("StateEmpLoyee", MyManager1.SelectedValue.ToInt32(), NSqlTypes.Equal);
                }
                else
                {
                    paramsList.Add("StateEmpLoyee", User.Identity.Name.ToInt32(), NSqlTypes.Equal, true);
                }
            }


            //数据页面列表绑定
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            repCustomer.DataBind(new Dispatching().GetDispatchingPageByWhere(paramsList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount));
            CtrPageIndex.RecordCount = SourceCount;
        }
        #endregion

        #region 隐藏查看预定按钮
        /// <summary>
        /// 隐藏
        /// </summary>
        public string IsShowOrHide(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            var DataList = ObjScheduleBLL.GetByCustomerID(CustomerID);
            if (DataList.Count > 0)
            {
                return "style='display:block;width:72px;height:20px;'";
            }
            else
            {
                return "style='display:none'";
            }
        }
        #endregion

        #region 是否有变更单
        /// <summary>
        /// 是否拥有变更单
        /// </summary>
        public string IsChange(object Source)
        {
            int DispatchingID = Source.ToString().ToInt32();
            var DataList = ObjProductForDispatchingBLL.GetByDispatchingID(DispatchingID).Where(C => C.IsFirstMakes >= 1 && C.IsFirstMakes != null);
            if (DataList.Count() >= 1)
            {
                return "style='color:red;'";
            }
            else
            {
                return "style='display:none;'";
            }
        }
        #endregion

        #region 改派派工人
        /// <summary>
        /// 改派
        /// </summary> 
        protected void repCustomer_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            //改派
            if (e.CommandName == "SaveChange")
            {
                HiddenField HideEmployee = e.Item.FindControl("hiddeEmpLoyeeID") as HiddenField;
                if (HideEmployee.Value.ToInt32() > 0)
                {
                    int StateKey = e.CommandArgument.ToString().ToInt32();
                    var Model = ObjState.GetByID(StateKey);
                    if (Model != null)
                    {
                        Model.StateEmpLoyee = HideEmployee.Value.ToInt32();
                        ObjState.Update(Model);
                    }
                    BinderData();
                }
                else
                {
                    JavaScriptTools.AlertWindow("请选择派工人", Page);
                }
            }
        }
        #endregion


        #region 数据加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void BinderData()
        {
            List<PMSParameters> paramsList = new List<PMSParameters>();


            paramsList.Add("StateEmpLoyee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);
            paramsList.Add("IsUse", true, NSqlTypes.Bit);

            //MyManager.GetEmployeePar(paramsList,"QuotedEmployee");
            //新人姓名
            CstmNameSelector.AppandTo(paramsList);
            //婚期
            if (PartyDateRanger.IsNotBothEmpty)
            {
                paramsList.Add("PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);
            }
            else
            {
                paramsList.Add("PartyDate", "2014-10-01,9999-12-31", NSqlTypes.DateBetween);
                paramsList.Add("UseSate", 19 + "," + 24 + "," + 208 + "," + 207, NSqlTypes.IN);
            }
            //酒店

            paramsList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text.Trim(), NSqlTypes.LIKE);
            //状态
            if (ddlStates.SelectedValue.ToInt32() == 1)
            {
                paramsList.Add("UseSate", 206, NSqlTypes.NotEquals);
            }
            else if (ddlStates.SelectedValue.ToInt32() == 2)
            {
                paramsList.Add("UseSate", 206, NSqlTypes.Equal);
            }

            //派工人
            if (MyManager1.SelectedValue.ToInt32() > 0)
            {
                paramsList.Add("EmpLoyeeID", MyManager1.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }
            else
            {
                paramsList.Add("EmpLoyeeID", User.Identity.Name.ToInt32(), NSqlTypes.Equal, true);
            }


            //数据页面列表绑定
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            repCustomer.DataBind(new Dispatching().GetDispatchingPageByWhere(paramsList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount));
            CtrPageIndex.RecordCount = SourceCount;
        }
        #endregion

        #region 会员标志
        /// <summary>
        /// 会员标志显示
        /// </summary> 
        protected void repCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Customers ObjCustomersBLL = new Customers();
            Image imgIcon = e.Item.FindControl("ImgIcon") as Image;
            int CustomerID = (e.Item.FindControl("HideCustomerID") as HiddenField).Value.ToString().ToInt32();
            var CustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            if (CustomerModel.IsVip == true)            //该客户是会员
            {
                imgIcon.Visible = true;
            }
            else     //不是会员
            {
                imgIcon.Visible = false;
            }
        }
        #endregion
    }
}