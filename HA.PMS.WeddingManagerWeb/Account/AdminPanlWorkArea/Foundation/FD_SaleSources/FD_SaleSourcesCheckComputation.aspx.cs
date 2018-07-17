/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.18
 Description:返利核算录入
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
using HA.PMS.EditoerLibrary;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_SaleSourcesCheckComputation : SystemPage
    {
        SaleSources objSaleSourcesBLL = new SaleSources();
        PayNeedRabate objPayNeedRabateBLL = new PayNeedRabate();

        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        //客户信息操作
        Customers ObjCustomersBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataBinder();
            }
        }

        public string GetMoney(object CustomerID)
        {
            var ObjModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID.ToString().ToInt32());
            if (ObjModel != null)
            {
                return ObjModel.FinishAmount.ToString();
            }
            else
            {
                return "暂无订单信息";
            }
        }

        /// <summary>
        /// 帮绑定或查询数据
        /// </summary>
        protected void DataBinder()
        {
            FD_PayNeedSales payNood = new FD_PayNeedSales();

            payNood.SourceID = ddlChanne.SelectedValue.ToInt32();
            payNood.ChannelTypeId = ddlChannelType.SelectedValue.ToInt32();

            //构造查询参数
            var objParmList = new List<PMSParameters>();

            if (ddlChannelType.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("ChannelTypeId", ddlChannelType.SelectedValue.ToInt32());

            }
            ////类别
            if (ddlChanne.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("SourceID", ddlChanne.SelectedValue.ToInt32());

            }


            //新人状态
            if (ddlCustomersState.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("State", ddlCustomersState.SelectedValue.ToInt32());

            }

            if (ddlSerchReferee.Items.Count != 0)
            {
                if (!string.IsNullOrEmpty(ddlSerchReferee.SelectedItem.Text))
                {
                    objParmList.Add("MoneyPerson", ddlSerchReferee.Text, NSqlTypes.StringEquals);
                }

            }


            this.MyManager.GetEmployeePar(objParmList);     //录入人

            ////新人姓名
            //objParmList.Add(txtCustomerName.Text.Trim().ToString() != string.Empty,"",txtCustomerName.Text,NSqlTypes.StringEquals);
            ////联系电话
            //objParmList.Add(txtContactPhone.Text.Trim().ToString() != string.Empty, "", txtContactPhone.Text, NSqlTypes.StringEquals);
            ////婚期
            //if (DateRanger.IsNotBothEmpty)
            //{
            //    objParmList.Add("MoneyPerson", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            //}

            objParmList.Add("IsDelete", false, NSqlTypes.Bit);

            #region 分页页码
            int startIndex = CheckComputationPager.StartRecordIndex;
            int SourceCount = 0;


            var query = objPayNeedRabateBLL.GetPayNeedRabateByWhere(objParmList, "PayDate", CheckComputationPager.PageSize, CheckComputationPager.CurrentPageIndex, out SourceCount);
            CheckComputationPager.RecordCount = SourceCount;
            rptTelemarketingManager.DataSource = query;
            rptTelemarketingManager.DataBind();

            #endregion
        }


        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void CheckComputationPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }


        /// <summary>
        /// 选择的时候变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlChannelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSerchReferee.Items.Clear();
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


        /// <summary>
        /// 当选择渠道变更的时候绑定推荐人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlChanne_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ddlChanne.SelectedValue.ToInt32() == 0)
            {

                ddlSerchReferee.Items.Clear();

            }
            else
            {

                ddlSerchReferee.BinderbyChannel(ddlChanne.SelectedValue.ToInt32());
            }



        }

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

        public string GetoperEmpLoyee(object key)
        {
            string name = Convert.ToString(key);
            return string.IsNullOrEmpty(name) ? GetEmployeeName(User.Identity.Name) : name;
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



        /// <summary>
        /// 查询 并且绑定需要录入的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            DataBinder();
        }


        /// <summary>
        /// 保存所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptTelemarketingManager.Items.Count; i++)
            {

                var ObjRefereeddl = (ddlReferee)rptTelemarketingManager.Items[i].FindControl("DdlReferee1");
                if (ObjRefereeddl.SelectedItem.Text != "请选择")
                {
                    var ObjUpdateModel = objPayNeedRabateBLL.GetByID(((HiddenField)rptTelemarketingManager.Items[i].FindControl("hiddKeyValue")).Value.ToInt32());
                    ObjUpdateModel.PayMoney = ((TextBox)rptTelemarketingManager.Items[i].FindControl("txtPayMoney")).Text.ToDecimal();
                    ObjUpdateModel.OperEmployee = ((TextBox)rptTelemarketingManager.Items[i].FindControl("txtOperEmployee")).Text;
                    ObjUpdateModel.EmpLoyeeID = User.Identity.Name.ToInt32();
                    ObjUpdateModel.PayDate = ((TextBox)rptTelemarketingManager.Items[i].FindControl("txtPayDate")).Text.ToDateTime();
                    ObjUpdateModel.MoneyPerson = ObjRefereeddl.SelectedItem.Text;
                    ObjUpdateModel.CostPhone = ObjRefereeddl.SelectedItem.Value;
                    objPayNeedRabateBLL.Update(ObjUpdateModel);
                }

            }

            JavaScriptTools.AlertWindow("保存成功", Page);
            DataBinder();
        }


        /// <summary>
        /// 完结所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFinishall_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptTelemarketingManager.Items.Count; i++)
            {
                var txtPayMoney = (TextBox)rptTelemarketingManager.Items[i].FindControl("txtPayMoney");
                var ObjUpdateModel = objPayNeedRabateBLL.GetByID(((HiddenField)rptTelemarketingManager.Items[i].FindControl("hiddKeyValue")).Value.ToInt32());
                if (txtPayMoney.Text.ToDecimal() > 0)
                {
                    if (txtPayMoney.Text != string.Empty && ((TextBox)rptTelemarketingManager.Items[i].FindControl("txtPayDate")).Text != string.Empty)
                    {
                        ObjUpdateModel.EmpLoyeeID = User.Identity.Name.ToInt32();
                        ObjUpdateModel.IsFinish = true;
                        objPayNeedRabateBLL.Update(ObjUpdateModel);
                    }
                }

            }
            JavaScriptTools.AlertWindow("保存成功", Page);
            DataBinder();
        }




        //绑定人
        protected void rptTelemarketingManager_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ddlReferee Objddl = (ddlReferee)e.Item.FindControl("DdlReferee1");
            if (Objddl != null)
            {


                Objddl.BinderbyChannel(((HiddenField)e.Item.FindControl("hiddSourceKey")).Value.ToInt32());
                var Objitem = Objddl.Items.FindByText(((FD_PayNeedRabate)e.Item.DataItem).MoneyPerson);
                if (Objitem != null)
                {
                    Objddl.ClearSelection();
                    Objddl.Items.FindByText(((FD_PayNeedRabate)e.Item.DataItem).MoneyPerson).Selected = true;
                }

            }
        }


        /// <summary>
        /// 编辑 确认 删除
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptTelemarketingManager_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //确认收款
            if (e.CommandName == "Finish")
            {
                var txtPayMoney = (TextBox)e.Item.FindControl("txtPayMoney");
                var ObjUpdateModel = objPayNeedRabateBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                if (txtPayMoney.Text.ToDecimal() > 0)
                {

                    var ObjRefereeddl = (ddlReferee)e.Item.FindControl("DdlReferee1");
                    if (ObjRefereeddl.SelectedItem.Text != "请选择")
                    {
                        if (txtPayMoney.Text != string.Empty && ((TextBox)e.Item.FindControl("txtPayDate")).Text != string.Empty)
                        {
                            ObjUpdateModel.EmpLoyeeID = User.Identity.Name.ToInt32();
                            ObjUpdateModel.EmpLoyeeID = User.Identity.Name.ToInt32();
                            ObjUpdateModel.PayMoney = ((TextBox)e.Item.FindControl("txtPayMoney")).Text.ToDecimal();
                            ObjUpdateModel.OperEmployee = ((TextBox)e.Item.FindControl("txtOperEmployee")).Text;
                            ObjUpdateModel.PayDate = ((TextBox)e.Item.FindControl("txtPayDate")).Text.ToDateTime();
                            ObjUpdateModel.MoneyPerson = ObjRefereeddl.SelectedItem.Text;
                            ObjUpdateModel.CostPhone = ObjRefereeddl.SelectedItem.Value;
                            ObjUpdateModel.IsFinish = true;
                            objPayNeedRabateBLL.Update(ObjUpdateModel);
                        }
                    }
                    else
                    {
                        JavaScriptTools.AlertWindow("请选择收款人!", Page);
                    }
                }
            }

            //保存收款
            if (e.CommandName == "Save")
            {
                var ObjRefereeddl = (ddlReferee)e.Item.FindControl("DdlReferee1");

                if (ObjRefereeddl.SelectedItem.Text != "请选择")
                {
                    var ObjUpdateModel = objPayNeedRabateBLL.GetByID(((HiddenField)e.Item.FindControl("hiddKeyValue")).Value.ToInt32());
                    ObjUpdateModel.EmpLoyeeID = User.Identity.Name.ToInt32();
                    ObjUpdateModel.PayMoney = ((TextBox)e.Item.FindControl("txtPayMoney")).Text.ToDecimal();
                    ObjUpdateModel.OperEmployee = ((TextBox)e.Item.FindControl("txtOperEmployee")).Text;
                    ObjUpdateModel.PayDate = ((TextBox)e.Item.FindControl("txtPayDate")).Text.ToDateTime();
                    ObjUpdateModel.MoneyPerson = ObjRefereeddl.SelectedItem.Text;
                    ObjUpdateModel.CostPhone = ObjRefereeddl.SelectedItem.Value;
                    objPayNeedRabateBLL.Update(ObjUpdateModel);
                }

                else
                {
                    JavaScriptTools.AlertWindow("请选择收款人!", Page);
                }

            }

            JavaScriptTools.AlertWindow("保存成功!", Page);
            DataBinder();
            //    Save
            //    Edit

        }



    }
}