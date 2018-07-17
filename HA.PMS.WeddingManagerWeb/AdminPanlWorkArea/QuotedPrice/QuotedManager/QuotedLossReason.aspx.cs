using HA.PMS.BLLAssmblly.FD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.SysTarget;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedManager
{
    public partial class QuotedLossReason : System.Web.UI.Page
    {
        LoseContent ObjLoseBlLL = new LoseContent();
        QuotedLoss ObjLossBLL = new QuotedLoss();
        HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();

        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        Customers ObjCustomerBLL = new Customers();

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DDLDataBind();
                if (Request["Type"].ToString() == "Look")
                {
                    var Model = ObjQuotedPriceBLL.GetByCustomerID(Request["CustomerID"].ToInt32());
                    if (Model != null)
                    {
                        txtLostContent.Text = Model.LoseContent;
                        ddlLoseReason.SelectedValue = Model.LoseContentID.ToString();
                    }

                    txtLostContent.Enabled = false;
                    ddlLoseReason.Enabled = false;
                    btnSave.Visible = false;
                }
            }
        }
        #endregion


        #region 下拉框绑定 流失原因
        /// <summary>
        /// 流失原因
        /// </summary>
        public void DDLDataBind()
        {
            var LoseContentList = ObjLoseBlLL.GetByAll();
            ddlLoseReason.DataTextField = "Title";
            ddlLoseReason.DataValueField = "ContentID";
            ddlLoseReason.DataSource = LoseContentList;
            ddlLoseReason.DataBind();

            ddlLoseReason.Items.Insert(0, new ListItem("请选择", "0"));
        }
        #endregion

        #region 点击保存  确认之后

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var Model = ObjQuotedPriceBLL.GetByCustomerID(Request["CustomerID"].ToInt32());
            Model.LoseContentID = ddlLoseReason.SelectedValue.ToInt32();
            Model.LoseContent = txtLostContent.Text.ToString();
            int result = ObjQuotedPriceBLL.Update(Model);

            var Customer = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
            Customer.State = (int)CustomerStates.BackOrder;        //修改状态  退单
            int results = ObjCustomerBLL.Update(Customer);

            if (result > 0 && results > 0)
            {
                //保存操作日志
                CreateHandle();
                JavaScriptTools.AlertAndClosefancybox("退单成功", Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("退单失败,请稍候再试...", Page);
            }
        }
        #endregion


        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();
            var Model = ObjCustomerBLL.GetByID(4878);
            HandleModel.HandleContent = "策划报价-订单退订,客户姓名:" + Model.Bride + "/" + Model.Groom + ",退订成功";

            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 3;     //策划报价
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion

    }
}