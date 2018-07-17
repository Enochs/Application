
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer
{
    public partial class RenewInvite : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.Invite ObjInvtieBLL = new BLLAssmblly.Flow.Invite();
        Customers ObjCustomersBLL = new Customers();
        LoseContent ObjLoseContentBLL = new LoseContent();

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Load加载时绑定数据源
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
                    return "具体原因不明";
                }
            }
            else
            {
                return string.Empty;
            }
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
            if (MyManager.SelectedValue.ToInt32() == 0)
            {
                MyManager.GetEmployeePar(objParmList, "InviteEmployee");
            }
            else
            {
                objParmList.Add("InviteEmployee", MyManager.SelectedValue, NSqlTypes.Equal);
            }

            objParmList.Add("State", "29,7", NSqlTypes.IN);

            //按流失时间查询
            if (txtStar.Text != string.Empty && txtEnd.Text != string.Empty)
            {
                objParmList.Add("LastFollowDate", txtStar.Text.ToDateTime() + "," + txtEnd.Text.ToDateTime(), NSqlTypes.DateBetween);
            }

            //按渠道名称查询
            if (ddlChannelname.SelectedValue.ToInt32() != 0 && ddlChannelname.SelectedItem != null)
            {
                objParmList.Add("Channel", ddlChannelname.SelectedItem.Text, NSqlTypes.DateBetween);

            }

            //按流失原因查询
            if (ddlLoseContent.SelectedValue.ToInt32() != 0)
            {
                objParmList.Add("ContentID", ddlLoseContent.SelectedValue.ToInt32(), NSqlTypes.Equal);

            }


            //按渠道类型查询
            if (ddlChanneltype.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("ChannelType", ddlChanneltype.SelectedValue.ToInt32(), NSqlTypes.Equal);

            }

            //按新人姓名查询
            if (txtName.Text != string.Empty)
            {
                objParmList.Add("ContactMan", txtName.Text, NSqlTypes.LIKE);
                objParmList.Add("Bride", txtName.Text, NSqlTypes.ORLike);
                objParmList.Add("Groom", txtName.Text, NSqlTypes.ORLike);
            }

            //按联系电话查询
            if (txtCellphone.Text != string.Empty)
            {
                objParmList.Add("ContactPhone", txtCellphone.Text, NSqlTypes.LIKE);

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

        #region 渠道类型选择变化事件
        /// <summary>
        /// 渠道类型选择
        /// </summary>    
        protected void ddlChanneltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChannelname.Items.Clear();
            ddlChannelname.BindByParent(ddlChanneltype.SelectedValue.ToInt32());
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询数据
        /// </summary>
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 恢复邀约
        /// <summary>
        /// 恢复邀约
        /// </summary> 
        protected void repTelemarketingManager_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            WorkReport ObjWorkBLL = new WorkReport();
            //获取流失时间
            DateTime LoseDate = (e.Item.FindControl("lblLastFollowDate") as Label).Text.ToDateTime();
            var Model = ObjWorkBLL.GetEntityByTimeCustomerID(User.Identity.Name.ToInt32(), LoseDate);
            Model.LoseInviteNum -= 1;       //生成时间当天的流失量减1 电销量不用手动加1  沟通之后 日报表中会自动增加
            ObjWorkBLL.Update(Model);

            int CustomerID = (e.CommandArgument + string.Empty).ToInt32();
            FL_Customers ObjCustomersUpdateModel = ObjCustomersBLL.GetByID(CustomerID);
            switch (e.CommandName)
            {
                case "ReInvite":
                    ObjCustomersUpdateModel.State = (int)CustomerStates.DoInvite;//邀约中
                    ObjCustomersBLL.Update(ObjCustomersUpdateModel);
                    JavaScriptTools.AlertWindowAndLocation("已将新人恢复到邀约中", "InviteCommunicationContent.aspx?CustomerID=" + CustomerID, Page);
                    break;
                default: break;
            }
        }
        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle(FL_Customers ObjCustomersUpdateModel)
        {
            HandleLog ObjHandleBLL = new HandleLog();
            string Customername = ObjCustomersUpdateModel.Bride.ToString() != "" ? ObjCustomersUpdateModel.Bride.ToString() : ObjCustomersUpdateModel.Groom.ToString();
            sys_HandleLog HandleModel = new sys_HandleLog();
            HandleModel.HandleContent = "电话邀约,客户姓名:" + Customername + ",恢复邀约";
            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 1;     //电话邀约
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion
    }
}