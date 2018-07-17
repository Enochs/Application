using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using System.Data.Objects;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using System.Text;
using HA.PMS.BLLAssmblly.CA;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Emnus;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CountTableManager
{
    public partial class InviteTable : UserControlTools
    {
        Department objDepartmentBLL = new Department();
        SaleSources objSaleSourcesBLL = new SaleSources();
        MyGoalTarget objMyGoalTargetBLL = new MyGoalTarget();
        TargetType objTargetTypeBLL = new TargetType();
        Telemarketing ObjTelemarketingBLL = new Telemarketing();
        Dispatching ObjDispatchingBLL = new Dispatching();
        Order objOrderBLL = new Order();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new HA.PMS.BLLAssmblly.Flow.QuotedPrice();
        HA.PMS.BLLAssmblly.Flow.Invite ObjInvtieBLL = new BLLAssmblly.Flow.Invite();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CA_TargetType taget = objTargetTypeBLL.GetTargetTypeByTargetName("邀约成功数");
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
            ViewState["TargetInviteSuccess"] = GetTargetDataByTargetName("邀约成功数");
            ViewState["TargetInviteRate"] = GetTargetDataByTargetName("邀约成功率");
            GetFlinish();
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

        protected void DataDropDownList(int parentId)
        {
            ddlDepartment.DataSource = objDepartmentBLL.GetbyChildenByDepartmetnID(parentId).Where(C => C.DepartmentID != parentId);
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            ddlDepartment.Items.FindByText("请选择").Selected = true;
        }

        protected void GetFlinish()
        {
            string[] chooseDateStr = ddlChooseYear.SelectedValue.Split(',');
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();




            //客源量 参数集合
            var customerList = new List<ObjectParameter>();
            //订单参数对象集合
            List<ObjectParameter> objOrderParameterList = new List<ObjectParameter>();
            //邀约记录
            var GetWhereParList = new List<ObjectParameter>();


            if (ddlDepartment.Items.Count > 0)
            {
                if (ddlDepartment.SelectedItem.Text != "请选择")
                {
                    GetWhereParList.Add(new ObjectParameter("ToDepartMent", ddlDepartment.SelectedValue.ToInt32()));
                    customerList.Add(new ObjectParameter("DepartmentId", ddlDepartment.SelectedValue.ToInt32()));
                    objOrderParameterList.Add(new ObjectParameter("DepartmentId", ddlDepartment.SelectedValue.ToInt32()));

                }
            }
            //返回所有邀约记录表
            var objInviteAllResult = ObjInvtieBLL.GetInviteCustomerByStateIndex(GetWhereParList);
            //当前的邀约记录
            var objCurrentResult = objInviteAllResult.Where(C => C.CreateDate >= chooseDateStar && C.CreateDate <= chooseDateEnd);
            //上年的邀约记录
            var objPreResult = objInviteAllResult.Where(C => C.CreateDate >= chooseDateStar.AddYears(-1) && C.CreateDate <= chooseDateEnd.AddYears(-1));


            //客源量所有数据不包含 年份的参数
            var queryCustomer = ObjTelemarketingBLL.GetTelmarketingCustomersByParameter(customerList.ToArray());
            //当前的客源记录数据集合
            var chooseCustomerYear = queryCustomer.Where(C => C.CreateDate >= chooseDateStar && C.CreateDate <= chooseDateEnd);
            StringBuilder sbCustomer = new StringBuilder();

            // 订单总数
            StringBuilder sbOrder = new StringBuilder();
            var chooseYearOrder = objQuotedPriceBLL.GetCustomerQuotedParameter(objOrderParameterList);
            //当前年份的订单情况
            var currentYearOrder = chooseYearOrder.Where(C => C.CreateDate >= chooseDateStar && C.CreateDate <= chooseDateEnd);

            //有效信息
            var valideAll = queryCustomer.Where(C => C.State >= 2 && C.State <= 6);
            StringBuilder sbValid = new StringBuilder();
            var chooseYearValid = valideAll.Where(C => C.CreateDate >= chooseDateStar && C.CreateDate <= chooseDateEnd);


            //获取不含年份所有成功数 
            var DataList = ObjInvtieBLL.GetInviteCustomerByStateIndex(GetWhereParList);

            //邀约成功
            var InviteSuccess = objCurrentResult.Where(C => C.State == 6);
            StringBuilder sbInviteSuccess = new StringBuilder();

            //邀约中

            var OngoingInvite = objCurrentResult.Where(C => C.State == 5);

            StringBuilder sbOngoing = new StringBuilder();

            var LoseInvite = objCurrentResult.Where(C => C.State == 29);

            StringBuilder sbLose = new StringBuilder();
            //未邀约


            var NotInvite = objCurrentResult.Where(C => C.State == 2);

            StringBuilder sbNot = new StringBuilder();


            //12个月份，每一个月份
            for (int i = 1; i <= 12; i++)
            {
                //未邀约
                var singerNot = NotInvite.Where(C => C.CreateDate.Value.Month == i);
                if (singerNot != null)
                {
                    sbNot.AppendFormat("<td>{0}</td>", singerNot.Count());
                }
                else
                {
                    sbNot.AppendFormat("<td>{0}</td>", 0);
                }

                //流失
                var singerLose = LoseInvite.Where(C => C.CreateDate.Value.Month == i);
                if (singerLose != null)
                {
                    sbLose.AppendFormat("<td>{0}</td>", singerLose.Count());
                }
                else
                {
                    sbLose.AppendFormat("<td>{0}</td>", 0);
                }

                //邀约中
                var singerOngoing = OngoingInvite.Where(C => C.CreateDate.Value.Month == i);
                if (singerOngoing != null)
                {
                    sbOngoing.AppendFormat("<td>{0}</td>", singerOngoing.Count());
                }
                else
                {
                    sbOngoing.AppendFormat("<td>{0}</td>", 0);
                }

                //有效量
                var singerValid = chooseYearValid.Where(C => C.CreateDate.Value.Month == i);
                if (singerValid != null)
                {

                    sbValid.AppendFormat("<td>{0}</td>", singerValid.Count());
                }
                else
                {
                    sbValid.AppendFormat("<td>{0}</td>", 0);
                }
                //邀约成功数
                var singerQuery = InviteSuccess.Where(C => C.CreateDate.Value.Month == i);
                if (singerQuery != null)
                {
                    sbInviteSuccess.AppendFormat("<td>{0}</td>", singerQuery.Count());
                }
                else
                {
                    sbInviteSuccess.AppendFormat("<td>{0}</td>", 0);
                }
                //客源量
                var singerCustomers = chooseCustomerYear.Where(C => C.CreateDate.Value.Month == i);
                if (singerCustomers != null)
                {
                    sbCustomer.AppendFormat("<td>{0}</td>", singerCustomers.Count());
                }
                else
                {
                    sbCustomer.AppendFormat("<td>{0}</td>", 0);
                }
                //订单
                var singerOrder = currentYearOrder.Where(C => C.CreateDate.Month == i);
                if (singerOrder != null)
                {
                    sbOrder.AppendFormat("<td>{0}</td>", singerOrder.Count());
                }
                else
                {
                    sbOrder.AppendFormat("<td>{0}</td>", 0);
                }
            }

            #region  未邀约年份统计
            sbNot.AppendFormat("<td>{0}</td>", NotInvite.Count());

            //上年未邀约统计
            var preYearNot = objPreResult.Where(C => C.State == 2);
            if (preYearNot != null)
            {
                sbNot.AppendFormat("<td>{0}</td>", preYearNot.Count());
            }
            else
            {
                sbNot.AppendFormat("<td>{0}</td>", 0);
            }
            //所有未邀约统计
            sbNot.AppendFormat("<td>{0}</td>", objInviteAllResult.Where(C => C.State == 2).Count());
            ViewState["sbNot"] = sbNot.ToString();

            #endregion

            #region   流失年份统计
            sbLose.AppendFormat("<td>{0}</td>", LoseInvite.Count());

            //上年流失统计
            var preYearLose = objPreResult.Where(C => C.State == 29);
            if (preYearLose != null)
            {
                sbLose.AppendFormat("<td>{0}</td>", preYearLose.Count());
            }
            else
            {
                sbLose.AppendFormat("<td>{0}</td>", 0);
            }
            //所有流失统计
            sbLose.AppendFormat("<td>{0}</td>", objInviteAllResult.Where(C => C.State == 29).Count());
            ViewState["sbLose"] = sbLose.ToString();

            #endregion

            #region 邀约中年份统计
            sbOngoing.AppendFormat("<td>{0}</td>", OngoingInvite.Count());

            //邀约中邀约统计
            var preYearOngoing = objPreResult.Where(C => C.State == 5);
            if (preYearOngoing != null)
            {
                sbOngoing.AppendFormat("<td>{0}</td>", preYearOngoing.Count());
            }
            else
            {
                sbOngoing.AppendFormat("<td>{0}</td>", 0);
            }
            //所有邀约中统计
            sbOngoing.AppendFormat("<td>{0}</td>", objInviteAllResult.Where(C => C.State == 5).Count());
            ViewState["sbOngoing"] = sbOngoing.ToString();


            #endregion
            #region 有效量年份统计
            //有效量当年合计
            sbValid.AppendFormat("<td>{0}</td>", chooseYearValid.Count());

            //上年有效量统计
            var preYearValid = valideAll.Where(C => C.CreateDate >= chooseDateStar.AddYears(-1)
               && C.CreateDate <= chooseDateEnd.AddYears(-1));
            if (preYearValid != null)
            {
                sbValid.AppendFormat("<td>{0}</td>", preYearValid.Count());
            }
            else
            {
                sbValid.AppendFormat("<td>{0}</td>", 0);
            }
            //所有有效量统计
            sbValid.AppendFormat("<td>{0}</td>", valideAll.Count());
            ViewState["sbValid"] = sbValid.ToString();


            #endregion
            #region 订单年份统计
            //订单当年合计
            sbOrder.AppendFormat("<td>{0}</td>", chooseYearOrder.Count());
            //上年订单统计
            var preYearOrder = chooseYearOrder.Where(C => C.CreateDate >= chooseDateStar.AddYears(-1)
               && C.CreateDate <= chooseDateEnd.AddYears(-1));
            if (preYearOrder != null)
            {
                sbOrder.AppendFormat("<td>{0}</td>", preYearOrder.Count());
            }
            else
            {
                sbOrder.AppendFormat("<td>{0}</td>", 0);
            }
            //所有历史统计
            sbOrder.AppendFormat("<td>{0}</td>", chooseYearOrder.Count);
            ViewState["sbOrder"] = sbOrder.ToString();

            #endregion
            #region 邀约成功量年份统计
            //邀约成功量当年合计

            sbInviteSuccess.AppendFormat("<td>{0}</td>", InviteSuccess.Count());
            //上年邀约成功量统计
            var preYearSuccess = objPreResult.Where(C => C.State == 6);
            if (preYearSuccess != null)
            {
                sbInviteSuccess.AppendFormat("<td>{0}</td>", preYearSuccess.Count());
            }
            else
            {
                sbInviteSuccess.AppendFormat("<td>{0}</td>", 0);
            }
            //所有历史统计
            sbInviteSuccess.AppendFormat("<td>{0}</td>", objInviteAllResult.Where(C => C.State == 6).Count());

            ViewState["sbInviteSuccess"] = sbInviteSuccess.ToString();
            #endregion

            #region 客源量年份统计
            //客源量当年合计
            sbCustomer.AppendFormat("<td>{0}</td>", chooseCustomerYear.Count());
            //上年客源量统计
            var preYearCustomer = queryCustomer.Where(C => C.CreateDate >= chooseDateStar.AddYears(-1)
                && C.CreateDate <= chooseDateEnd.AddYears(-1));
            if (preYearCustomer != null)
            {
                sbCustomer.AppendFormat("<td>{0}</td>", preYearCustomer.Count());
            }
            else
            {
                sbCustomer.AppendFormat("<td>{0}</td>", 0);
            }
            //所有历史统计
            sbCustomer.AppendFormat("<td>{0}</td>", queryCustomer.Count);

            ViewState["sbCustomer"] = sbCustomer.ToString();
            #endregion
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
    }
}