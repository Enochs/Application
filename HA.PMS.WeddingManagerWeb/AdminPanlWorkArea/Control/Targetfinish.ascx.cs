using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.SysTarget;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using System.Configuration;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class Targetfinish : System.Web.UI.UserControl
    {
        Employee ObjEmployeeBLL = new Employee();
        Target ObjTargetBLL = new Target();
        FinishTargetSum ObjFinishTargetSumBLL = new FinishTargetSum();
        Department ObjDepartmentBLL = new Department();

        #region 头部绑定方法
        /// <summary>
        /// 绑定
        /// </summary>
        private void BinderDepartmentSum(int months)
        {

            string KeyList = string.Empty;
            var ObjDepartMentList = ObjEmployeeBLL.GetMyManagerEmpLoyees(Request.Cookies["HAEmployeeID"].Value.ToInt32(), ref KeyList);

            KeyList = KeyList.Trim(',');
            if (KeyList == string.Empty)
            {
                KeyList = ObjEmployeeBLL.GetByID(Request.Cookies["HAEmployeeID"].Value.ToInt32()).DepartmentID.ToString();
            }


            List<int> EmployeeKey = new List<int>();


            EmployeeKey.Add(Request.Cookies["HAEmployeeID"].Value.ToInt32());

            var ObjNeed = ObjFinishTargetSumBLL.GetinEmployeeKeyListNeedCreate(EmployeeKey, 1);
            if (ObjNeed.Count > 0)
            {

                //int Month = DateTime.Now.Month;
                int Month = months;
                var ObjList = new List<View_DepartmentTarget>();
                FL_FinishTargetSum FinishModel = new FL_FinishTargetSum();

                if (ObjEmployeeBLL.IsManager(Request.Cookies["HAEmployeeID"].Value.ToInt32()))
                {
                    ObjList = ObjFinishTargetSumBLL.GetDepartmentTarget(KeyList, " and Year=" + DateTime.Now.Year + " and IsActive=1 and TargetTitle='" + ObjNeed[0].TargetTitle + "'");
                }
                else
                {
                    ObjList = ObjFinishTargetSumBLL.GetEmployeetargetbyID(Request.Cookies["HAEmployeeID"].Value, " and Year=" + DateTime.Now.Year + " and IsActive=1 and TargetTitle='" + ObjNeed[0].TargetTitle + "'");
                }

                if (ObjList.Count > 0)
                {
                    lblTargetName.Text = ObjList[0].TargetTitle;
                    decimal? YearSum = ObjList[0].MonthPlan1 + ObjList[0].MonthPlan2 + ObjList[0].MonthPlan3 + ObjList[0].MonthPlan4 + ObjList[0].MonthPlan5 + ObjList[0].MonthPlan6 + ObjList[0].MonthPlan7 + ObjList[0].MonthPlan8 + ObjList[0].MonthPlan9 + ObjList[0].MonthPlan10 + ObjList[0].MonthPlan11 + ObjList[0].MonthPlan12; ;
                    decimal? YaerSumFinish = ObjList[0].MonthFinsh1 + ObjList[0].MonthFinish2 + ObjList[0].MonthFinish3 + ObjList[0].MonthFinish4 + ObjList[0].MonthFinish5 + ObjList[0].MonthFinish6 + ObjList[0].MonthFinish7 + ObjList[0].MonthFinish8 + ObjList[0].MonthFinish9 + ObjList[0].MonthFinish10 + ObjList[0].MonthFinish11 + ObjList[0].MonthFinish12; ;
                    lblYearPlan.Text = YearSum.ToString();
                    lblYearfinish.Text = YaerSumFinish.ToString();
                    if (Month <= 3)
                    {
                        lblQPlan.Text = (ObjList[0].MonthPlan1 + ObjList[0].MonthPlan2 + ObjList[0].MonthPlan3).ToString();
                        lblQfinish.Text = (ObjList[0].MonthFinsh1 + ObjList[0].MonthFinish2 + ObjList[0].MonthFinish3).ToString();

                    }

                    if (Month > 3 && Month <= 6)
                    {
                        lblQPlan.Text = (ObjList[0].MonthPlan4 + ObjList[0].MonthPlan5 + ObjList[0].MonthPlan6).ToString();
                        lblQfinish.Text = (ObjList[0].MonthFinish4 + ObjList[0].MonthFinish5 + ObjList[0].MonthFinish6).ToString();
                    }


                    if (Month > 6 && Month <= 9)
                    {
                        lblQPlan.Text = (ObjList[0].MonthPlan7 + ObjList[0].MonthPlan8 + ObjList[0].MonthPlan9).ToString();
                        lblQfinish.Text = (ObjList[0].MonthFinish7 + ObjList[0].MonthFinish8 + ObjList[0].MonthFinish9).ToString();
                    }

                    if (Month > 9 && Month <= 12)
                    {
                        lblQPlan.Text = (ObjList[0].MonthPlan10 + ObjList[0].MonthPlan12 + ObjList[0].MonthPlan11).ToString();
                        lblQfinish.Text = (ObjList[0].MonthFinish10 + ObjList[0].MonthFinish11 + ObjList[0].MonthFinish12).ToString();
                    }


                    switch (Month)
                    {
                        case 1:
                            lblMonthFinish.Text = ObjList[0].MonthFinsh1.ToString();
                            lblMonthPlan.Text = ObjList[0].MonthPlan1.ToString();
                            if (lblMonthFinish.Text.ToDecimal() != 0)
                            {
                                lblMonth.Text = (lblMonthPlan.Text.ToDecimal() / lblMonthFinish.Text.ToDecimal()).ToString();
                            }
                            break;
                        case 2:
                            lblMonthFinish.Text = ObjList[0].MonthFinish2.ToString();
                            lblMonthPlan.Text = ObjList[0].MonthPlan2.ToString();

                            break;
                        case 3:
                            lblMonthFinish.Text = ObjList[0].MonthFinish3.ToString();
                            lblMonthPlan.Text = ObjList[0].MonthPlan3.ToString();
                            break;
                        case 4:
                            lblMonthFinish.Text = ObjList[0].MonthFinish4.ToString();
                            lblMonthPlan.Text = ObjList[0].MonthPlan4.ToString();
                            break;
                        case 5:
                            lblMonthFinish.Text = ObjList[0].MonthFinish5.ToString();
                            lblMonthPlan.Text = ObjList[0].MonthPlan5.ToString();
                            break;
                        case 6:
                            lblMonthFinish.Text = ObjList[0].MonthFinish6.ToString();
                            lblMonthPlan.Text = ObjList[0].MonthPlan6.ToString();
                            break;
                        case 7:
                            lblMonthFinish.Text = ObjList[0].MonthFinish7.ToString();
                            lblMonthPlan.Text = ObjList[0].MonthPlan7.ToString();
                            break;
                        case 8:
                            lblMonthFinish.Text = ObjList[0].MonthFinish8.ToString();
                            lblMonthPlan.Text = ObjList[0].MonthPlan8.ToString();
                            break;
                        case 9:
                            lblMonthFinish.Text = ObjList[0].MonthFinish9.ToString();
                            lblMonthPlan.Text = ObjList[0].MonthPlan9.ToString();
                            break;
                        case 10:
                            lblMonthFinish.Text = ObjList[0].MonthFinish10.ToString();
                            lblMonthPlan.Text = ObjList[0].MonthPlan10.ToString();
                            break;
                        case 11:
                            lblMonthFinish.Text = ObjList[0].MonthFinish11.ToString();
                            lblMonthPlan.Text = ObjList[0].MonthPlan11.ToString();
                            break;
                        case 12:
                            lblMonthFinish.Text = ObjList[0].MonthFinish12.ToString();
                            lblMonthPlan.Text = ObjList[0].MonthPlan12.ToString();
                            break;

                    }
                }

                if (lblQPlan.Text.ToDecimal() > 0)
                {

                    lblQ.Text = (lblQfinish.Text.ToDecimal() / lblQPlan.Text.ToDecimal()).ToString("0.0000%");
                }
                else
                {
                    lblQ.Text = "0%";
                }

                if (lblMonthPlan.Text.ToDecimal() > 0)
                {
                    lblMonth.Text = (lblMonthFinish.Text.ToDecimal() / lblMonthPlan.Text.ToDecimal()).ToString("0.0000%");
                }
                else
                {
                    lblMonth.Text = "0%";
                }

                if (lblYearPlan.Text.ToDecimal() > 0)
                {
                    lblYearQ.Text = (lblYearfinish.Text.ToDecimal() / lblYearPlan.Text.ToDecimal()).ToString("0.0000%");
                }
                else
                {
                    lblYearQ.Text = "0%";
                }
            }

        }
        #endregion



        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary> 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlMonthSelect.Text = DateTime.Now.Month.ToString();
                ddlMonthSelect.Items[DateTime.Now.Month - 1].Selected = true;
                string conn_strs = ConfigurationManager.AppSettings["PMS_WeddingEntities"].ToString();
                if (conn_strs != "server=sql.m45.vhostgo.com;uid=timewedding;pwd=weddinglove;database=timewedding")     //外网服务器
                {
                    BinderDepartmentSum(DateTime.Now.Month);
                }
            }
        }
        #endregion

        protected void ddlMonthSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            int month = ddlMonthSelect.SelectedValue.ToInt32();
            BinderDepartmentSum(month);
        }
    }
}