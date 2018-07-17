
/**
 Version :HaoAi 1.0
 File Name :Customers
 Author:黄晓可
 Date:2013.3.17
 Description:沟通流失列表
 **/
//

using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;
using System.Data.Objects;
using System.Web.UI.WebControls;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer
{
    public partial class LoseInvite : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.Invite ObjInvtieBLL = new BLLAssmblly.Flow.Invite();
        Customers ObjCustomersBLL = new Customers();
        LoseContent ObjLoseContentBLL = new LoseContent();

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Load加载时绑定数据源
                ddlDataBind();
                DataBinder();
            }
        }
        #endregion


        #region 获取流失原因
        /// <summary>
        /// 获取流失原因
        /// </summary>
        public string GetLoseContent(object ContentKey)
        {
            if (ContentKey != null)
            {
                var Objmodel = ObjLoseContentBLL.GetByID(ContentKey.ToString().ToInt32());

                if (Objmodel != null)
                {
                    return Objmodel.Title;
                }
                else
                {
                    if (ContentKey.ToString() == "-3")
                    {
                        return "渠道信息无效";
                    }
                    return "未选择流失原因";
                }
            }
            else
            {
                return "未选择流失原因";
            }
        }
        #endregion

        #region 获取流失原因
        /// <summary>
        /// 获取流失详细原因
        /// </summary>
        public string GetLoseContentsDetails(object ContentKey)
        {

            int customerId = (ContentKey + string.Empty).ToInt32();
            FL_Invite currentInvite = ObjInvtieBLL.GetByCustomerID(customerId);
            if (currentInvite != null)
            {
                return ObjInvtieBLL.GetLoseContentByInviteID(currentInvite.InviteID);
            }
            return "未填写";

        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据到Repeater控件中
        /// </summary>
        private void DataBinder()
        {
            int startIndex = CtrPageIndex.StartRecordIndex;
            var objParmList = new List<PMSParameters>();

            int SourceCount = 0;
            //是否按照责任人查询
            //MyManager.GetEmployeePar(GetWhereParList);
            objParmList.Add("State", "7,29", NSqlTypes.IN);

            //按渠道名称查询
            if (ddlChanneltype.SelectedValue.ToInt32() != 0 && ddlChanneltype.SelectedItem != null)
            {
                if (ddlChanneltype.SelectedItem.Text != "无")
                {
                    objParmList.Add("ChannelType", ddlChanneltype.SelectedValue.ToString().ToInt32(), NSqlTypes.Equal);
                }
            }


            //按渠道名称查询
            if (ddlChannelname.SelectedValue.ToInt32() != 0 && ddlChannelname.SelectedItem != null)
            {
                objParmList.Add("Channel", ddlChannelname.SelectedItem.Text, NSqlTypes.StringEquals);

            }

            //按流失时间查询
            if (txtStar.Text != string.Empty && txtEnd.Text != string.Empty)
            {
                objParmList.Add("LastFollowDate", txtStar.Text.ToDateTime() + "," + txtEnd.Text.ToDateTime(), NSqlTypes.DateBetween);
            }

            //按流失原因查询
            if (ddlLoseContent.SelectedValue.ToInt32() != 0)
            {
                objParmList.Add("ContentID", ddlLoseContent.SelectedValue.ToInt32());

            }

            //按新人姓名查询
            if (txtName.Text != string.Empty)
            {
                objParmList.Add("ContactMan", txtName.Text, NSqlTypes.LIKE);
                objParmList.Add("Bride", txtName.Text, NSqlTypes.ORLike);
                objParmList.Add("Groom", txtName.Text, NSqlTypes.ORLike);
            }

            //按联系电话查询
            if (txtCellPhone.Text != string.Empty)
            {
                objParmList.Add("ContactPhone", txtCellPhone.Text, NSqlTypes.LIKE);
            }

            //按邀约人查询
            if (MyManager1.SelectedValue.ToInt32() == 0)
            {
                objParmList.Add("InviteEmployee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);
            }
            else
            {
                objParmList.Add("InviteEmployee", MyManager1.SelectedValue, NSqlTypes.Equal);

            }

            var DataList = ObjInvtieBLL.GetByWhereParameter(objParmList, "CreateDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            repTelemarketingManager.DataSource = DataList;
            repTelemarketingManager.DataBind();
            ddlChannelname.Items.Clear();

            //repTelemarketingManager.DataSource = ObjCustomersBLL.GetInviteCustomerByStateIndex();
            //repTelemarketingManager.DataBind();
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 渠道类型 选择变化事件
        /// <summary>
        /// 渠道类型
        /// </summary>
        protected void ddlChanneltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChannelname.Items.Clear();
            ddlChannelname.BindByParent(ddlChanneltype.SelectedValue.ToInt32());
        }
        #endregion

        #region 下拉框绑定 流失原因
        /// <summary>
        /// 下拉框绑定
        /// </summary>
        public void ddlDataBind()
        {
            ddlLoseContent.Width = 75;
            LoseContent objLoseContentBLL = new LoseContent();
            ddlLoseContent.DataSource = objLoseContentBLL.GetByType(1);
            ddlLoseContent.DataTextField = "Title";
            ddlLoseContent.DataValueField = "ContentID";
            ddlLoseContent.DataBind();

            ddlLoseContent.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            ddlLoseContent.Items.FindByText("请选择").Selected = true;
        }
        #endregion

        #region 查询功能
        /// <summary>
        /// 查询数据
        /// </summary>
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 会员标志显示
        /// <summary>
        /// 会员标志
        /// </summary>
        protected void repTelemarketingManager_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
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