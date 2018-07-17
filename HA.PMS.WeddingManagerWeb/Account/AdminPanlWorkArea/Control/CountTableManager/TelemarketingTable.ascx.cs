using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using System.Text;
using System.Web.Providers.Entities;
using HA.PMS.BLLAssmblly.CA;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CountTableManager
{
    public partial class TelemarketingTable : UserControlTools
    {
        Customers objCustomersBLL = new Customers();
        Department objDepartmentBLL = new Department();
        SaleSources objSaleSourcesBLL = new SaleSources();
        MyGoalTarget objMyGoalTargetBLL = new MyGoalTarget();
        TargetType objTargetTypeBLL = new TargetType();
        Telemarketing ObjTelemarketingBLL = new Telemarketing();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new HA.PMS.BLLAssmblly.Flow.QuotedPrice();
        //返利
        PayNeedRabate objPayNeedRabateBLL = new PayNeedRabate();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                CA_TargetType taget = objTargetTypeBLL.GetTargetTypeByTargetName("有效信息数");

                if (taget.DepartmentId.HasValue)
                {
                    DataDropDownList(taget.DepartmentId.Value);
                }
                DataLoad();
            }
        }
        /// <summary>
        /// 加载对应的数据
        /// </summary>
        protected void DataLoad()
        {
            ViewState["customerCount"] = GetTargetDataByTargetName("有效信息数");
            ViewState["validateRate"] = GetTargetDataByTargetName("客源有效率");
            GetFlinish();
        }
        protected void DataDropDownList(int parentId)
        {
            ddlDepartment.DataSource = objDepartmentBLL.GetbyChildenByDepartmetnID(parentId).Where(C => C.DepartmentID != parentId);
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            ddlDepartment.Items.FindByText("请选择").Selected = true;
        }
        /// <summary>
        /// 根据目标名称返回对应的 时间和部门的 目标集合
        /// </summary>
        /// <param name="targetName"></param>
        /// <returns></returns>
        protected string GetTargetDataByTargetName(string targetName)
        {
            string[] chooseDateStr = ddlChooseYear.SelectedValue.Split(',');
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();
            List<ObjectParameter> objListParameter = new List<ObjectParameter>();
            objListParameter.Add(new ObjectParameter("Goal", targetName));
            objListParameter.Add(new ObjectParameter("CreateTime_between", chooseDateStar + "," + chooseDateEnd));
            if (ddlDepartment.Items.Count > 0)
            {
                if (ddlDepartment.SelectedItem.Text != "请选择")
                {
                    objListParameter.Add(new ObjectParameter("DepartmentId", ddlDepartment.SelectedValue.ToInt32()));
                }
            }

            //按照 targetName 返回对应所有的计划目标
            var currentYearQuery = objMyGoalTargetBLL.GetbyParameter(objListParameter.ToArray());

            StringBuilder sb = new StringBuilder();
            //12个月份，每一个月份
            for (int i = 1; i <= 12; i++)
            {
                var singerQuery = currentYearQuery.Where(C => C.CreateTime.Value.Month == i).FirstOrDefault();
                decimal tagetValue = 0;
                if (singerQuery != null)
                {
                    tagetValue = singerQuery.TargetValue.Value;
                }
                sb.AppendFormat("<td>{0}</td>", tagetValue);
            }
            //当年合计
            sb.AppendFormat("<td>{0}</td>", currentYearQuery.Sum(C => C.TargetValue.Value));

            //上年合计
            objListParameter[1].Value = chooseDateStar.AddYears(-1) + "," + chooseDateEnd.AddYears(-1);
            var preYearQuery = objMyGoalTargetBLL.GetbyParameter(objListParameter.ToArray());
            if (preYearQuery != null)
            {
                sb.AppendFormat("<td>{0}</td>", preYearQuery.Sum(C => C.TargetValue.Value));
            }
            else
            {
                sb.AppendFormat("<td>{0}</td>", 0);
            }
            //历史累计
            //移除时间参数，查询对应的部门的所有目标
            objListParameter.RemoveAt(1);
            var queryAll = objMyGoalTargetBLL.GetbyParameter(objListParameter.ToArray());
            if (queryAll != null)
            {
                sb.AppendFormat("<td>{0}</td>", queryAll.Sum(C => C.TargetValue.Value));
            }
            else
            {
                sb.AppendFormat("<td>{0}</td>", 0);
            }
            return sb.ToString();

        }


        protected void GetFlinish()
        {
            string[] chooseDateStr = ddlChooseYear.SelectedValue.Split(',');
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();

            List<ObjectParameter> ObjParameter = new List<ObjectParameter>();
            List<ObjectParameter> objOrderParameterList = new List<ObjectParameter>();
            ObjParameter.Add(new ObjectParameter("CreateDate_between", chooseDateStar + "," + chooseDateEnd));
            if (ddlDepartment.Items.Count > 0)
            {
                if (ddlDepartment.SelectedItem.Text != "请选择")
                {
                    ObjParameter.Add(new ObjectParameter("DepartmentId", ddlDepartment.SelectedValue.ToInt32()));
                    objOrderParameterList.Add(new ObjectParameter("DepartmentId", ddlDepartment.SelectedValue.ToInt32()));
                }
            }

            //客户量
            var query = ObjTelemarketingBLL.GetTelmarketingCustomersByParameter(ObjParameter.ToArray());
            StringBuilder sbCustomersCount = new StringBuilder();
            //有效量
            var queryValid = query.Where(C => C.State>=2&&C.State<=6);
            StringBuilder sbValide = new StringBuilder();
            //邀约成功量
            var querySuccess = query.Where(C => C.State == 6);
            StringBuilder sbSuccess = new StringBuilder();
            //成交量
            var queryClinchaDeal = query.Where(C => C.State >= 13 && C.State <= 23);
            StringBuilder sbClinchaDeal = new StringBuilder();

            //订单金额
            StringBuilder sbAggregateAmount = new StringBuilder();

            //订单总额



            //所有的订单总额
            var objOrderAllResult = objQuotedPriceBLL.GetCustomerQuotedParameter(objOrderParameterList);

            var objCurrentResult = objOrderAllResult.Where(C => C.PartyDate >= chooseDateStar && C.PartyDate <= chooseDateEnd);

            //返利
            StringBuilder sbPay = new StringBuilder();
            List<ObjectParameter> ObjPayParameterList = new List<ObjectParameter>();
            ObjectParameter dates = new ObjectParameter("PartyDay_between", chooseDateStar + "," + chooseDateEnd);
            ObjPayParameterList.Add(dates);
          //  ObjPayParameterList.Add(new ObjectParameter("IsFinish", true));
            
            var payNeed = objPayNeedRabateBLL.GetByParaandIndex(ObjPayParameterList.ToArray());
            for (int i = 1; i <= 12; i++)
            {

                //客户量 （含订单金额）
                var customers = query.Where(C => C.CreateDate.Value.Month == i);

                var currentOrder = objCurrentResult.Where(C => C.PartyDate.Value.Month == i);
                //当月订单金额
                if (currentOrder!=null)
                {
                    sbAggregateAmount.AppendFormat("<td>{0}</td>", currentOrder.Sum(C=>C.AggregateAmount));
                }
                else
                {
                    //订单金额
                    sbAggregateAmount.AppendFormat("<td>{0}</td>", 0);
                }
                //客源量
                if (customers != null)
                {

                    sbCustomersCount.AppendFormat("<td>{0}</td>", customers.Count());
                    
                  
                }
                else
                {
                    sbCustomersCount.AppendFormat("<td>{0}</td>", 0);
                   
                }
                //有效信息
                var customersValide = queryValid.Where(C => C.CreateDate.Value.Month == i);
                if (customersValide != null)
                {
                    sbValide.AppendFormat("<td>{0}</td>", customersValide.Count());
                }
                else
                {
                    sbValide.AppendFormat("<td>{0}</td>", 0);
                }
                //成功量
                var customersSuccess = querySuccess.Where(C => C.CreateDate.Value.Month == i);
                if (customersSuccess != null)
                {
                    sbSuccess.AppendFormat("<td>{0}</td>", customersSuccess.Count());
                }
                else
                {
                    sbSuccess.AppendFormat("<td>{0}</td>", 0);
                }
                //成交量
                var customersClinchaDeal = queryClinchaDeal.Where(C => C.CreateDate.Value.Month == i);
                if (customersClinchaDeal != null)
                {
                    sbClinchaDeal.AppendFormat("<td>{0}</td>", customersClinchaDeal.Count());
                }
                else
                {
                    sbClinchaDeal.AppendFormat("<td>{0}</td>", 0);
                }
                //返利
                var customersPayNeed = payNeed.Where(C =>  C.PartyDay.Value.Month == i);
                if (customersPayNeed!=null)
                {

                    sbPay.AppendFormat("<td>{0}</td>",customersPayNeed.Sum(C=>C.PayMoney) );
                }
                else
                {
                    sbPay.AppendFormat("<td>{0}</td>", 0);
                }

            }

            #region 客户量年份统计
            //客户量当年合计
            sbCustomersCount.AppendFormat("<td>{0}</td>", query.Count);

            //客户量上年合计
            ObjParameter[0].Value = chooseDateStar.AddYears(-1) + "," + chooseDateEnd.AddYears(-1);
            var preYearQuery = ObjTelemarketingBLL.GetTelmarketingCustomersByParameter(ObjParameter.ToArray());
            if (preYearQuery != null)
            {
                sbCustomersCount.AppendFormat("<td>{0}</td>", preYearQuery.Count);
            }
            else
            {
                sbCustomersCount.AppendFormat("<td>{0}</td>", 0);
            }
            //客户量历史累计
            //移除时间参数，查询对应的部门的所有目标
            ObjParameter.RemoveAt(0);
            var queryAll = ObjTelemarketingBLL.GetTelmarketingCustomersByParameter(ObjParameter.ToArray());
            if (queryAll != null)
            {
                sbCustomersCount.AppendFormat("<td>{0}</td>", queryAll.Count);
            }
            else
            {
                sbCustomersCount.AppendFormat("<td>{0}</td>", 0);
            }
            ViewState["Flinish"] = sbCustomersCount.ToString();
            #endregion

            #region 有效量年份统计
            //当前有客户有效量合计
            sbValide.AppendFormat("<td>{0}</td>", queryValid.Count());
            //客户量上年合计
            var preValidate = preYearQuery.Where(C => C.State >= 2 && C.State <= 6);
            if (preValidate != null)
            {
                sbValide.AppendFormat("<td>{0}</td>", preValidate.Count());
            }
            else
            {
                sbValide.AppendFormat("<td>{0}</td>", 0);
            }
            //客户量历史累计
            var queryValidateAll = queryAll.Where(C => C.State != 300);
            if (queryValidateAll != null)
            {
                sbValide.AppendFormat("<td>{0}</td>", queryValidateAll.Count());
            }
            else
            {
                sbValide.AppendFormat("<td>{0}</td>", 0);
            }
            ViewState["sbValide"] = sbValide.ToString();
            #endregion

            #region 成功量的年份统计
            //当前成功量合计
            sbSuccess.AppendFormat("<td>{0}</td>", querySuccess.Count());
            //成功量上年合计
            var preSuccess = preYearQuery.Where(C => C.State == 6);
            if (preSuccess != null)
            {
                sbSuccess.AppendFormat("<td>{0}</td>", preSuccess.Count());
            }
            else
            {
                sbSuccess.AppendFormat("<td>{0}</td>", 0);
            }
            //成功量历史累计
            var querySuccessAll = queryAll.Where(C => C.State == 6);
            if (querySuccessAll != null)
            {
                sbSuccess.AppendFormat("<td>{0}</td>", querySuccessAll.Count());
            }
            else
            {
                sbSuccess.AppendFormat("<td>{0}</td>", 0);
            }
            ViewState["sbSuccess"] = sbSuccess.ToString();
            #endregion

            #region 成交量年份统计
            //成交量当年合计
            sbClinchaDeal.AppendFormat("<td>{0}</td>", queryClinchaDeal.Count());

            //成交量上年合计

            var preClinchaDeal = preYearQuery.Where(C => C.State >= 13 && C.State <= 23);
            if (preClinchaDeal != null)
            {
                sbClinchaDeal.AppendFormat("<td>{0}</td>", preClinchaDeal.Count());
            }
            else
            {
                sbClinchaDeal.AppendFormat("<td>{0}</td>", 0);
            }
            //成交量历史累计
            var queryClinchaDealAll = queryAll.Where(C => C.State >= 13 && C.State <= 23);
            if (queryClinchaDealAll != null)
            {
                sbClinchaDeal.AppendFormat("<td>{0}</td>", queryClinchaDealAll.Count());
            }
            else
            {
                sbClinchaDeal.AppendFormat("<td>{0}</td>", 0);
            }
            ViewState["sbClinchaDeal"] = sbClinchaDeal.ToString();
            #endregion

            #region 订单金额
            sbAggregateAmount.AppendFormat("<td>{0}</td>", objCurrentResult.Sum(C=>C.AggregateAmount));
         
            //订单金额上年合计
            //objOrderAllResult
            var objPreOrder= objOrderAllResult.Where(C => C.PartyDate >= chooseDateStar.AddYears(-1) && C.PartyDate <= chooseDateEnd.AddYears(-1));

            sbAggregateAmount.AppendFormat("<td>{0}</td>", objPreOrder.Sum(C => C.AggregateAmount));
            //订单金额累计

            sbAggregateAmount.AppendFormat("<td>{0}</td>", objOrderAllResult.Sum(C => C.AggregateAmount));
            ViewState["sbAggregateAmount"] = sbAggregateAmount.ToString();

            #endregion


            #region 返利年份统计
            //返利当年合计
            sbPay.AppendFormat("<td>{0}</td>", GetPaySumMoney(payNeed.ToList()));
            //返利上年统计
            ObjPayParameterList[0].Value = chooseDateStar.AddYears(-1) + "," + chooseDateEnd.AddYears(-1);
            var prePayQuery = objPayNeedRabateBLL.GetByParaandIndex(ObjPayParameterList.ToArray());
            if (prePayQuery != null)
            {
                sbPay.AppendFormat("<td>{0}</td>", prePayQuery.Sum(C=>C.PayMoney));
            }
            else
            {
                sbPay.AppendFormat("<td>{0}</td>", 0);
            }
            //返利历史累计
            ObjPayParameterList.RemoveAt(0);
            var payAll = objPayNeedRabateBLL.GetByParaandIndex(ObjPayParameterList.ToArray());
            if (payAll != null)
            {
                sbPay.AppendFormat("<td>{0}</td>", payAll.Sum(C => C.PayMoney));
            }
            else
            {
                sbPay.AppendFormat("<td>{0}</td>", 0);
            }
            ViewState["sbPay"] = sbPay.ToString();
            #endregion
        }

        /// <summary>
        /// 返回返利金额
        /// </summary>
        /// <param name="allPayMoney"></param>
        /// <returns></returns>
        protected string GetPaySumMoney(List<FD_PayNeedRabate> allPayMoney)
        {
            decimal allPaySumMoney = 0;
            foreach (var item in allPayMoney)
            {

                if (item.PayMoney.HasValue)
                {
                    allPaySumMoney += item.PayMoney.Value;
                }

            }
            return allPaySumMoney + string.Empty;
        }
         

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
    }
}