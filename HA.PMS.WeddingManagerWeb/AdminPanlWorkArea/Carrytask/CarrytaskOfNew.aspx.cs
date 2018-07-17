//新订单
using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Emnus;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskOfNew : SystemPage
    {
        DispatchingState ObjState = new DispatchingState();

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BinderData(object sender, EventArgs e)
        {

            var objParmList = new List<PMSParameters>();
            string DataTimerStar = DateTime.Today.ToString();
            string DateTimerEnd = NextWeek.ToString();
            objParmList.Add("UseSate", 24, NSqlTypes.Equal);
            objParmList.Add("StateEmpLoyee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);
            objParmList.Add("IsUse", false, NSqlTypes.Bit);

            //按新人姓名查询
            CstmNameSelector.AppandTo(objParmList);

            //按酒店查询
            objParmList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text.Trim(), NSqlTypes.StringEquals);

            //婚期查询
            objParmList.Add(PartyDateRanger.IsNotBothEmpty, "PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);


            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            repCustomer.DataBind(new BLLAssmblly.Flow.Dispatching().GetDispatchingPageByWhere(objParmList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount)); //ObjInvtieBLL.GetInviteCustomerByStateIndex(isAdd, tlCustomerID, GetWhereParList, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
        }
        #endregion

        #region 进行改派
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
                    DataBinder();
                }
                else
                {
                    JavaScriptTools.AlertWindow("请选择派工人", Page);
                }
            }

        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void DataBinder()
        {

            var objParmList = new List<PMSParameters>();
            string DataTimerStar = DateTime.Today.ToString();
            string DateTimerEnd = NextWeek.ToString();
            objParmList.Add("UseSate", 24, NSqlTypes.Equal);
            objParmList.Add("StateEmpLoyee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);
            objParmList.Add("IsUse", false, NSqlTypes.Bit);

            //按新人姓名查询
            CstmNameSelector.AppandTo(objParmList);

            //按酒店查询
            objParmList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text.Trim(), NSqlTypes.StringEquals);

            //婚期查询
            objParmList.Add(PartyDateRanger.IsNotBothEmpty, "PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);


            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            repCustomer.DataBind(new BLLAssmblly.Flow.Dispatching().GetDispatchingPageByWhere(objParmList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount)); //ObjInvtieBLL.GetInviteCustomerByStateIndex(isAdd, tlCustomerID, GetWhereParList, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
        }
        #endregion

        #region 会员标志
        /// <summary>
        /// 会员标志
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