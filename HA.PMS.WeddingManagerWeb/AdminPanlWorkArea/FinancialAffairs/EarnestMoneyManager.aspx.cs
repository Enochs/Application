using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using HA.PMS.ToolsLibrary;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.FinancialAffairsbll;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs
{
    public partial class EarnestMoneyManager :SystemPage
    {
        Customers ObjCustomerBLL = new Customers();
        Order ObjOrderBLL = new Order();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        /// <summary>
        /// 绑定成功预定的需要派单的
        /// </summary>
        private void BinderData()
        {
            List<ObjectParameter> ObjParList = new List<ObjectParameter>();
            ObjParList.Add(new ObjectParameter("State_NumOr", (int)CustomerStates.SucessOrder + "," + (int)CustomerStates.DoingChecksQuotedPrice + "," + (int)CustomerStates.DoingCarrytask));
            if (rdoTimerSpan.SelectedItem != null)
            {
                if (rdoTimerSpan.SelectedItem.Text == "婚期")
                {
                    if (txtTimerStar.Text != string.Empty && txtTimerEnd.Text != string.Empty)
                    {
                        ObjParList.Add(new ObjectParameter("PartyDate_between", txtTimerStar.Text + "," + txtTimerEnd.Text));
                    }
                }
                else
                {
                    if (txtTimerStar.Text != string.Empty && txtTimerEnd.Text != string.Empty)
                    {
                        ObjParList.Add(new ObjectParameter("LastFollowDate_between", txtTimerStar.Text + "," + txtTimerEnd.Text));
                    }
                }
            }
            //ObjParList.Add(new ObjectParameter("EmpLoyeeID", User.Identity.Name.ToInt32()));
            //ObjParList.Add(new ObjectParameter("EarnestFinish",false));
 


            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = ObjOrderBLL.GetOrderCustomerByIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, ObjParList);
            CtrPageIndex.RecordCount = SourceCount;

            repCustomer.DataSource = DataList;
            repCustomer.DataBind();
        }



        /// <summary>
        /// 确认定金
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repCustomer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            OrderEarnestMoney ObjOrderEarnestMoneyBLL = new OrderEarnestMoney();
            var OrderModel=ObjOrderBLL.GetByID(e.CommandArgument.ToString().ToInt32());
            if (OrderModel.EarnestFinish == false)
            {
                var OrderID = e.CommandArgument.ToString().ToInt32();

                //保存数据
                FL_OrderEarnestMoney ObjInsertModel = ObjOrderEarnestMoneyBLL.GetByOrderID(e.CommandArgument.ToString().ToInt32());
                //ObjInsertModel.EarnestMoney = OrderModel.EarnestMoney;
                ObjInsertModel.Isfinish = true;
                //ObjInsertModel.CreateDate = DateTime.Now;
                ObjInsertModel.FinishEmpLoyee = User.Identity.Name.ToInt32();
                ObjInsertModel.OrderID = OrderModel.OrderID;
          
                OrderModel.EarnestFinish = true;
                ObjOrderBLL.Update(OrderModel);
                ObjOrderEarnestMoneyBLL.Update(ObjInsertModel);
                JavaScriptTools.AlertWindow("定金确认成功",Page);


                FL_Message ObjMessageModel = new FL_Message();
                ObjMessageModel.EmployeeID = OrderModel.EmployeeID;
                ObjMessageModel.MissionID = 0;
                ObjMessageModel.IsDelete = false;
                ObjMessageModel.IsLook = false;
                ObjMessageModel.Message = "定金确认";
                ObjMessageModel.MessAgeTitle = "财务已经确认定金";
                ObjMessageModel.KeyWords = "";
                ObjMessageModel.CreateEmployeename = "系统";
                ObjMessageModel.CreateDate = DateTime.Now;
                Message ObjMessageBLL = new Message();
                ObjMessageBLL.Insert(ObjMessageModel);
            }

            BinderData();
        }

        protected void btnserch_Click(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void btnSaveDate_Click(object sender, EventArgs e)
        {

        }
    }
}