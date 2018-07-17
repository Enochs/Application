using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.Birthday
{
    public partial class BirthdayForAll : SystemPage
    {
        //客户 新人信息
        Customers ObjCustomersBLL = new Customers();
        //策划报价
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new HA.PMS.BLLAssmblly.Flow.QuotedPrice();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }

        protected CS_Member GetMember(object CustomerID)
        {
            return new BLLAssmblly.CS.Member().GetByCustomerID(CustomerID.ToString().ToInt32());
        }

        protected void BinderData(object sender, EventArgs e)
        {
            #region 查询参数
            List<PMSParameters> ObjParList = new List<PMSParameters>();
            ObjParList.Add("ParentQuotedID", 0, NSqlTypes.Equal);
            ObjParList.Add("IsDelete", false, NSqlTypes.Bit);
            //新人姓名
            ObjParList.Add(!string.IsNullOrWhiteSpace(txtBride.Text), "Bride", txtBride.Text.Trim(), NSqlTypes.LIKE);
            ObjParList.Add(!string.IsNullOrWhiteSpace(txtBride.Text), "Groom", txtBride.Text.Trim(), NSqlTypes.ORLike);
            //新人手机
            ObjParList.Add(!string.IsNullOrWhiteSpace(txtBrideCellPhone.Text), "BrideCellPhone", txtBrideCellPhone.Text.Trim(), NSqlTypes.OrInts);
            ObjParList.Add(!string.IsNullOrWhiteSpace(txtBrideCellPhone.Text), "GroomCellPhone", txtBrideCellPhone.Text.Trim(), NSqlTypes.OrInts);
            //酒店
            ObjParList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "WineShop", ddlHotel.SelectedItem.Text, NSqlTypes.StringEquals);

            //责任人
            if (MyManager1.SelectedValue.ToInt32() > 0)
            {
                ObjParList.Add("EmployeeID", MyManager1.SelectedValue, NSqlTypes.Equal);
            }

            //婚期
            ObjParList.Add(DateRanger.IsNotBothEmpty, "PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);

            #endregion

            #region 分页绑定
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;

            //生日查询
            if (txtStartBirthday.Text.Trim().ToString() != string.Empty || txtEndBirthday.Text.Trim().ToString() != string.Empty)
            {
                var DataList = ObjQuotedPriceBLL.GetByWhereParameter(ObjParList, "PartyDate", 10000, 1, out SourceCount);
                int StartMonth = txtStartBirthday.Text.Trim().ToInt32();
                int EndMonth = txtEndBirthday.Text.Trim().ToInt32();
                //选择月份判断  开始日期为空
                if (txtStartBirthday.Text.Trim().ToString() == string.Empty && txtEndBirthday.Text.Trim().ToString() != string.Empty)
                {
                    StartMonth = EndMonth;
                }
                //结束日期为空
                else if (txtStartBirthday.Text.Trim().ToString() != string.Empty && txtEndBirthday.Text.Trim().ToString() == string.Empty)
                {
                    EndMonth = StartMonth;
                }
                DataList = DataList.Where(C => C.GroomBirthday != null || C.BrideBirthday != null).ToList();
                DataList = ObjCustomersBLL.GetBirthCustomers(DataList, StartMonth, EndMonth);
                CtrPageIndex.RecordCount = DataList.Count;
                repCustomer.DataBind(DataList.Take(CtrPageIndex.PageSize).Skip((CtrPageIndex.CurrentPageIndex - 1) * CtrPageIndex.PageSize));
            }
            else
            {
                var DataList = ObjQuotedPriceBLL.GetByWhereParameter(ObjParList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
                CtrPageIndex.RecordCount = SourceCount;
                repCustomer.DataSource = DataList;
                repCustomer.DataBind();
            }

            #endregion
            //repCustomer.DataBind(ObjCustomersBLL.GetBirthCustomer());
        }

        #region 切换日期
        /// <summary>
        /// 切换日起
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>

        protected void repCustomer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            int CustomerID = e.CommandArgument.ToString().ToInt32();
            if (e.CommandName == "Change")
            {
                var Model = ObjCustomersBLL.GetByID(CustomerID);
                Label lblBirthDay = e.Item.FindControl("lblBirthDay") as Label;
                if (lblBirthDay.Text == GetShortDateString(Model.GroomBirthday))
                {
                    lblBirthDay.Text = GetShortDateString(Model.BrideBirthday);
                }
                else
                {
                    lblBirthDay.Text = GetShortDateString(Model.GroomBirthday);
                }
            }
        }
        #endregion

        protected void lbtnShowOrHide_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < repCustomer.Items.Count; i++)
            //{
            //    var ObjItem = repCustomer.Items[i];
            //    LinkButton lbtnChange = ObjItem.FindControl("lbtnChange") as LinkButton;
            //    if (lbtnShowOrHide.Text == "隐藏")
            //    {
            //        lbtnChange.Visible = false;
            //        if (i == repCustomer.Items.Count - 1)
            //        {
            //            lbtnShowOrHide.Text = "显示";
            //        }
            //    }
            //    else if (lbtnShowOrHide.Text == "显示")
            //    {
            //        lbtnChange.Visible = true;
            //        if (i == repCustomer.Items.Count - 1)
            //        {
            //            lbtnShowOrHide.Text = "隐藏";
            //        }
            //    }
            //}
        }
    }
}