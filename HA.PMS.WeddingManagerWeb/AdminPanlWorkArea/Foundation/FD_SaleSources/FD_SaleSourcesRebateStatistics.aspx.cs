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
using HA.PMS.BLLAssmblly.Report;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_SaleSourcesRebateStatistics : SystemPage
    {
        SaleSources objSaleSourcesBLL = new SaleSources();
        PayNeedRabate objPayNeedRabateBLL = new PayNeedRabate();


        /// <summary>
        /// 电话营销
        /// </summary>
        Telemarketing ObjTelemarketingBLL = new Telemarketing();

        //客户信息操作
        Customers ObjCustomersBLL = new Customers();

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();

                //总累计总支付额
                //ltlSumPayAll.Text = objPayNeedRabateBLL.GetByAll().Sum(C => C.PayMoney) + string.Empty;

            }
        }
        #endregion

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

        #region 渠道类型选择变化事件
        /// <summary>
        /// 选择渠道类型 绑定渠道名称
        /// </summary>
        protected void ddlChannelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlReferee.Items.Clear();
            ddlChanne.Items.Clear();
            if (ddlChannelType.SelectedValue.ToInt32() == -1)
            {

                ListItem currentList = ddlChanne.Items.FindByValue("0");
                if (currentList != null)
                {
                    currentList.Selected = true;
                }
            }
            else
            {
                ddlChanne.BindByParent(ddlChannelType.SelectedValue.ToInt32());
            }
        }
        #endregion

        #region 渠道名称选择变化
        /// <summary>
        /// 绑定渠道联系人
        /// </summary>
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
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        protected void DataBinder()
        {
            //页面查询参数
            List<PMSParameters> pars = new List<PMSParameters>();

            //累计支付内部查询
            List<ObjectParameter> ObjSumPayParameterList = new List<ObjectParameter>();
            //当前总支付金额
            List<ObjectParameter> ObjlCurrentSumPayParameterList = new List<ObjectParameter>();

            //渠道类型
            if (ddlChannelType.SelectedValue.ToInt32() > 0)
            {
                pars.Add("ChannelType", ddlChannelType.SelectedValue.ToInt32(), NSqlTypes.Equal);
                ObjSumPayParameterList.Add(new ObjectParameter("ChannelType", ddlChannelType.SelectedValue.ToInt32()));
            }

            //渠道名称
            if (ddlChanne.SelectedValue.ToInt32() > 0)
            {
                pars.Add("Channel", ddlChanne.SelectedItem.Text, NSqlTypes.StringEquals);
                ObjSumPayParameterList.Add(new ObjectParameter("Channel", ddlChanne.SelectedItem.Text));
            }

            //推荐人
            if (ddlReferee.Items.Count != 0)
            {
                if (ddlReferee.SelectedItem.Text != "请选择")
                {
                    pars.Add("MoneyPerson", ddlReferee.Text, NSqlTypes.StringEquals);
                    ObjSumPayParameterList.Add(new ObjectParameter("MoneyPerson", ddlReferee.Text));
                }
            }

            //时间
            if (ddlTimerType.SelectedValue != "-1")
            {
                string dateType = "PartyDate";

                if (ddlTimerType.SelectedValue == "1")
                {
                    dateType = "PayDate";

                }
                pars.Add(dateType, DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }


            //添加参数:获得已经完结的
            pars.Add("IsFinish", true, NSqlTypes.Bit);

            //责任人
            MyManager.GetEmployeePar(pars);

            #region 分页页码
            int startIndex = RebateStatisticsPager.StartRecordIndex;
            int resourceCount = 0;

            //绑定列表
            var query = ObjTelemarketingBLL.GetByWhereParameter1(pars, "PartyDate", RebateStatisticsPager.PageSize, RebateStatisticsPager.CurrentPageIndex, out resourceCount);
            RebateStatisticsPager.RecordCount = resourceCount;

            rptTelemarketingManager.DataSource = query;
            rptTelemarketingManager.DataBind();

            #region 支付金额

            ////当前支付额 ltlCurrentPay
            //ltlCurrentPay.Text = query.Sum(C => C.PayMoney) + string.Empty;
            ////累计支付额 ltlSumPay
            //var SumPay = objPayNeedRabateBLL.GetByAll(ObjSumPayParameterList.ToArray());
            //ltlSumPay.Text = query.Sum(C => C.PayMoney) + string.Empty;
            //// 当前总支付额 ltlCurrentSumPay
            //var CurrentSumPay = objPayNeedRabateBLL.GetByAll(ObjSumPayParameterList.ToArray());
            //ltlCurrentSumPay.Text = CurrentSumPay.Sum(C => C.PayMoney) + string.Empty;
            #endregion

            #endregion
        }
        #endregion

        #region 查询
        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }


        //分页
        protected void RebateStatisticsPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion



    }
}