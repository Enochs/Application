/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.18
 Description:渠道返利明细
 History:修改日志

 Author:杨洋
 Date:2013.4.18
 version:好爱1.0
 description:修改描述
 
 
 
 */
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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_SaleSourcesRebateStatistics : SystemPage
    {
        SaleSources objSaleSourcesBLL = new SaleSources();
        PayNeedRabate objPayNeedRabateBLL = new PayNeedRabate();

        //客户信息操作
        Customers ObjCustomersBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                DataBinder();
                //总累计总支付额
                ltlSumPayAll.Text = objPayNeedRabateBLL.GetByAll().Sum(C => C.PayMoney) + string.Empty;

            }
        }


        #region 通用查询
        /// <summary>
        /// 获取渠道名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetSourceName(object source)
        {
            if (source != null)
            {
                HA.PMS.DataAssmblly.FD_SaleSources saleSource = objSaleSourcesBLL.GetByID((int)source);
                if (saleSource != null)
                {
                    return saleSource.Sourcename;
                }
                else
                {
                    return "直接到店";
                }
            }
            else
            {
                return string.Empty;
            }
        }



        /// <summary>
        /// 获取新人姓名
        /// </summary>
        /// <returns></returns>
        public string GetGoomByID(object Key)
        {
            var ObjModel = ObjCustomersBLL.GetByID(Key.ToString().ToInt32());
            if (ObjModel != null)
            {
                return ObjModel.Bride;
            }
            else
            {
                return string.Empty;
            }
        }


        #endregion

        /// <summary>
        /// 选择渠道类型 绑定渠道名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlChannelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlReferee.Items.Clear();
            ddlChanne.Items.Clear();
            if (ddlChannelType.SelectedValue.ToInt32() == -1)
            {
               
               ListItem currentList= ddlChanne.Items.FindByValue("0");
               if (currentList!=null)
               {
                   currentList.Selected = true;
               }
               

            }
            else
            {
                ddlChanne.BindByParent(ddlChannelType.SelectedValue.ToInt32());
            }

        }


        /// <summary>
        /// 绑定渠道联系人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlChanne_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChanne.SelectedValue.ToInt32() == 0)
            {

                ddlReferee.Items.Clear();

            }
            else
            {
                ddlReferee.BinderbyChannel(ddlChanne.SelectedValue.ToInt32());
            }

       
        }


        protected void DataBinder()
        {
            FD_PayNeedSales payNood = new FD_PayNeedSales();

            payNood.SourceID = ddlChanne.SelectedValue.ToInt32();
            payNood.ChannelTypeId = ddlChannelType.SelectedValue.ToInt32();
            //payNood.MoneyPerson = ddlMoneyPerson.Text;
            //页面查询参数
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();

            //累计支付内部查询
            List<ObjectParameter> ObjSumPayParameterList = new List<ObjectParameter>();

            //当前总支付金额
            List<ObjectParameter> ObjlCurrentSumPayParameterList = new List<ObjectParameter>();

            if (ddlChannelType.SelectedValue.ToInt32() > 0)
            {
                ObjParameterList.Add(new ObjectParameter("ChannelTypeId", payNood.ChannelTypeId));
                ObjSumPayParameterList.Add(new ObjectParameter("ChannelTypeId", payNood.ChannelTypeId));
            }

            if (ddlChanne.SelectedValue.ToInt32() > 0)
            {
                ObjParameterList.Add(new ObjectParameter("SourceID", payNood.SourceID));
                ObjSumPayParameterList.Add(new ObjectParameter("SourceID", payNood.SourceID));
            }
            if (ddlReferee.Items.Count != 0)
            {
                if (ddlReferee.SelectedItem.Text!="请选择")
                {
                 
                    ObjParameterList.Add(new ObjectParameter("MoneyPerson", ddlReferee.Text));
                    ObjSumPayParameterList.Add(new ObjectParameter("MoneyPerson", ddlReferee.Text));
                }
            }

            //开始时间
            DateTime startTime = new DateTime();
           // 如果没有选择结束时间就默认是当前时间
            string dateStr = "2100-1-1";
            DateTime endTime = dateStr.ToDateTime();

            if (!string.IsNullOrEmpty(txtStar.Text))
            {
                startTime = txtStar.Text.ToDateTime();
            }
            if (!string.IsNullOrEmpty(txtEnd.Text))
            {
                endTime = txtEnd.Text.ToDateTime();
            }
            if (ddlTimerType.SelectedValue != "-1")
            {
                string dateType = "PartyDay";
               
                if (ddlTimerType.SelectedValue == "1")
                {
                    dateType = "PayDate";
                  
                }
                ObjectParameter dates = new ObjectParameter(dateType + "_between", startTime + "," + endTime);
                ObjParameterList.Add(dates);
            }


            //添加参数:获得已经完结的
            ObjParameterList.Add(new ObjectParameter("IsFinish", true));
            //ObjParameterList.Add(new ObjectParameter("EmpLoyeeID", User.Identity.Name.ToInt32()));

            MyManager.GetEmployeePar(ObjParameterList);
            //ObjlCurrentSumPayParameterList.Add(dates);

            #region 分页页码
            int startIndex = RebateStatisticsPager.StartRecordIndex;
            int resourceCount = 0;

            //绑定列表
            var query = objPayNeedRabateBLL.GetByParaandIndex(ObjParameterList.ToArray(), RebateStatisticsPager.PageSize, RebateStatisticsPager.CurrentPageIndex, out resourceCount);
            RebateStatisticsPager.RecordCount = resourceCount;

            rptTelemarketingManager.DataSource = query;
            rptTelemarketingManager.DataBind();

            //当前支付额 ltlCurrentPay
            ltlCurrentPay.Text = query.Sum(C => C.PayMoney) + string.Empty;
            //累计支付额 ltlSumPay

            var SumPay = objPayNeedRabateBLL.GetByAll(ObjSumPayParameterList.ToArray());
            ltlSumPay.Text = query.Sum(C => C.PayMoney) + string.Empty;
            // 当前总支付额 ltlCurrentSumPay
            var CurrentSumPay = objPayNeedRabateBLL.GetByAll(ObjSumPayParameterList.ToArray());
            ltlCurrentSumPay.Text = CurrentSumPay.Sum(C => C.PayMoney) + string.Empty;
            #endregion
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void RebateStatisticsPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }


         
    }
}