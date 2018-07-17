using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class MessageBoardforall : System.Web.UI.UserControl
    {
        /// <summary>
        /// 部门员工操作
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();


        HA.PMS.BLLAssmblly.Flow.MessageBoard ObjMessageBoardBLL = new HA.PMS.BLLAssmblly.Flow.MessageBoard();
        /// <summary>
        /// 员工
        /// </summary>
        public int EmpLoyeeID
        { get; set; }


        public int CreateEmpLoyeeID
        { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (EmpLoyeeID == 0)
                {
                    EmpLoyeeID =Request.Cookies["HAEmployeeID"].Value.ToInt32();
                    CreateEmpLoyeeID = Request.Cookies["HAEmployeeID"].Value.ToInt32();
                }
                BinderData();
            }
        }

        private void BinderData()
        {
    
 
            int startIndex = MessagePager.StartRecordIndex;
            //如果是最后一页，则重新设置起始记录索引，以使最后一页的记录数与其它页相同，如总记录有101条，每页显示10条，如果不使用此方法，则第十一页即最后一页只有一条记录，使用此方法可使最后一页同样有十条记录。

            int resourceCount = 0;
            EmpLoyeeID = Request.Cookies["HAEmployeeID"].Value.ToInt32();
            var query = ObjMessageBoardBLL.GetMineMessage(EmpLoyeeID, 0, MessagePager.PageSize, MessagePager.CurrentPageIndex, out resourceCount); ;

            MessagePager.RecordCount = resourceCount;

            repMessage.DataSource = query;
            repMessage.DataBind();

        }


        /// <summary>
        /// 获取回复信息
        /// </summary>
        /// <param name="ParentKey"></param>
        /// <returns></returns>

        public string GetReturnMessage(object ParentKey)
        {
            var ReturnMessage = ObjMessageBoardBLL.GetOnlyeyParent(ParentKey.ToString().ToInt32());
            if (ReturnMessage.MessAgeContent != string.Empty)
            {
                return ObjEmployeeBLL.GetByID(ReturnMessage.EmpLoyeeID).EmployeeName + "/" + ReturnMessage.CreateDate.ToString() + "/</br>" + ReturnMessage.MessAgeContent;
            }
            else
            {
                return string.Empty;
            }
        }

        protected void btnSaveMessage_Click(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void btnSaveReturn_Click(object sender, EventArgs e)
        {
            FL_MessageBoard ObjMessageBoardModel = new DataAssmblly.FL_MessageBoard();
            ObjMessageBoardModel.CreateDate = DateTime.Now;
            ObjMessageBoardModel.EmpLoyeeID = EmpLoyeeID;
            ObjMessageBoardModel.CreateEmpLoyee = CreateEmpLoyeeID;
            ObjMessageBoardModel.Parent = hideReturnKey.Value.ToInt32();
            ObjMessageBoardModel.MessAgeContent = txtReturn.Text;
            ObjMessageBoardModel.CreateEmpLoyeeName = ObjEmployeeBLL.GetByID(this.CreateEmpLoyeeID).EmployeeName;

            ObjMessageBoardBLL.Insert(ObjMessageBoardModel);
            JavaScriptTools.AlertWindowAndReload("留言成功！", Page);
            BinderData();

        }

        protected void MessagePager_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void repMessage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            { 
                ObjMessageBoardBLL.Delete(ObjMessageBoardBLL.GetByID(e.CommandArgument.ToString().ToInt32()));
                BinderData();
            }
            JavaScriptTools.AlertWindow("删除完毕！",Page);
        }


    }
}