using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskAppraise : SystemPage
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
        /// 四大关联
        /// </summary>
        OrderGuardian ObjOrderGuardianBLL = new OrderGuardian();


        /// <summary>
        /// 内部员工
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();

        /// <summary>
        /// 
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.OrderAppraise ObjOrderAppraiseBLL = new HA.PMS.BLLAssmblly.Flow.OrderAppraise();


        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        /// <summary>
        /// 项目参与人员
        /// </summary>
        DispatchingEmployeeManager ObjDispatchingEmployeeManagerBLL = new DispatchingEmployeeManager();

        string OrderByColumnName = "PartyDate";

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();

            }
        }
        #endregion


        #region 数据绑定
        /// <summary>
        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        private void BinderData()
        {
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            List<PMSParameters> ObjParList = new List<PMSParameters>();
            ObjParList.Add("FinishOver", true, NSqlTypes.Equal);       //已完成的   当前时间 大于婚期
            ObjParList.Add("IsDelete", false, NSqlTypes.Equal);        //未删除的客户    false  未删除
            ObjParList.Add("State", 206, NSqlTypes.Equal);             //206表示 已完结  



            //婚礼策划
            if (MyManager.SelectedValue.ToInt32() == 0)
            {
                if (!(ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32())))
                {
                    this.MyManager.GetEmployeePar(ObjParList, "QuotedEmployee");   //婚礼策划
                }
            }
            else if (MyManager.SelectedValue.ToInt32() > 0)
            {
                this.MyManager.GetEmployeePar(ObjParList, "QuotedEmployee");   //婚礼策划
            }

            //按婚期查询

            if (DateRanger.IsNotBothEmpty)
            {
                ObjParList.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }


            // 按联系电话查询
            if (txtCellPhone.Text != string.Empty)
            {
                ObjParList.Add("ContactPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }
            //按新人姓名查询
            if (txtContactMan.Text != string.Empty)
            {
                ObjParList.Add("ContactMan", txtContactMan.Text, NSqlTypes.LIKE);
            }

            ObjParList.Add("ParentQuotedID", 0, NSqlTypes.Equal);      //为0 只显示一次  否则会有重复的数据
            //ObjParList.Add("ParentQuotedID", null, NSqlTypes.OrInt);      //有可能为NULL
            ObjParList.Add("PartyDate", DateTime.Now.ToShortDateString().ToDateTime(), NSqlTypes.LessThan);     //当前时间大于婚期

            //状态为0 就是未评价
            ObjParList.Add("EvalState", 0, NSqlTypes.OrInt);
            ObjParList.Add("EvalState", null, NSqlTypes.OrInt);

            var DataList = ObjQuotedPriceBLL.GetByCustomerParameters(ObjParList, OrderByColumnName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);      //最后评价修改时间排序

            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();
        }
        #endregion


        /// <summary>
        /// 根据用户ID获取报价单ID
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetQuotedIDByCustomers(object CustomerID)
        {
            return ObjQuotedPriceBLL.GetByCustomerID(CustomerID.ToString().ToInt32()).QuotedID.ToString();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }



        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            BinderData();
        }

        protected string SetAppraiseStyle(object AppraiseOver, object PartyDate, object IsOver)
        {
            if (Convert.ToBoolean(AppraiseOver) == false && (Convert.ToDateTime(PartyDate) >= DateTime.Today.AddDays(1) || Convert.ToBoolean(IsOver) == true))
            {
                return string.Empty;
            }
            return "style='display:none'";
        }

        #region 获取已付款
        // <summary>
        /// 获取已付款
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string GetQuotedDispatchingFinishMoney(object CustomerID)
        {
            HA.PMS.BLLAssmblly.FinancialAffairsbll.OrderEarnestMoney ObjOrderEarnestMoneyBLL = new HA.PMS.BLLAssmblly.FinancialAffairsbll.OrderEarnestMoney();
            HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();
            decimal FinishAmount = 0;

            //预付款
            var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID((CustomerID + string.Empty).ToInt32());
            if (QuotedModel == null)
            {
                return "0.00";
            }
            else
            {
                decimal EarnestMoney = QuotedModel.EarnestMoney.Value;
                FinishAmount += EarnestMoney;

                //获得收款计划的东西
                var ObjList = ObjQuotedCollectionsPlanBLL.GetByOrderID(QuotedModel.OrderID);

                foreach (var Objitem in ObjList)
                {
                    FinishAmount += Objitem.RealityAmount.Value;
                }

                //定金
                var ObjEorder = ObjOrderEarnestMoneyBLL.GetByOrderID(QuotedModel.OrderID);
                if (ObjEorder != null)
                {
                    if (ObjEorder.EarnestMoney.HasValue)
                    {
                        FinishAmount += ObjEorder.EarnestMoney.Value;
                    }
                }
                return FinishAmount.ToString();

            }
        }
        #endregion
    }
}