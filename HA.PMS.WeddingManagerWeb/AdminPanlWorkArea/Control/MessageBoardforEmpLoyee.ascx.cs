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
    public partial class MessageBoardforEmpLoyee : System.Web.UI.UserControl
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
                //if (!ObjEmployeeBLL.IsManager(EmpLoyeeID))
                //{
                //    btnSaveMessage.Visible = false;
                //    txtCreate.Visible = false;


                this.lblEmployeeName.Text=ObjEmployeeBLL.GetByID(EmpLoyeeID).EmployeeName;
                //}

           //     BinderData();
            }
        }


        private void BinderData()
        {
            repMessage.DataSource = ObjMessageBoardBLL.GetMessageByEmpLoyeeID(EmpLoyeeID, 0);
            repMessage.DataBind();
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 获取回复信息
        /// </summary>
        /// <param name="ParentKey"></param>
        /// <returns></returns>

        public string GetReturnMessage(object ParentKey)
        {
            var ReturnMessage=ObjMessageBoardBLL.GetOnlyeyParent(ParentKey.ToString().ToInt32());
            if (ReturnMessage.MessAgeContent != string.Empty)
            {
                return ObjEmployeeBLL.GetByID(ReturnMessage.EmpLoyeeID).EmployeeName + "/" + ReturnMessage.CreateDate.ToString()+"/</br>"+ReturnMessage.MessAgeContent;
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 上级留言
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveMessage_Click(object sender, EventArgs e)
        {
            FL_MessageBoard ObjMessageBoardModel = new DataAssmblly.FL_MessageBoard();
            ObjMessageBoardModel.CreateDate = DateTime.Now;
            ObjMessageBoardModel.EmpLoyeeID = EmpLoyeeID;
            ObjMessageBoardModel.CreateEmpLoyee = CreateEmpLoyeeID;
            ObjMessageBoardModel.Parent = 0;
            ObjMessageBoardModel.MessAgeContent = txtCreate.Text;
            ObjMessageBoardModel.CreateEmpLoyeeName = ObjEmployeeBLL.GetByID(this.CreateEmpLoyeeID).EmployeeName;

            ObjMessageBoardBLL.Insert(ObjMessageBoardModel);
            JavaScriptTools.AlertAndClosefancybox("留言成功！", Page);
         //   BinderData();

        }


        /// <summary>
        /// 下级回复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveReturn_Click(object sender, EventArgs e)
        {
            FL_MessageBoard ObjMessageBoardModel = new DataAssmblly.FL_MessageBoard();
            ObjMessageBoardModel.CreateDate = DateTime.Now;
            ObjMessageBoardModel.EmpLoyeeID = EmpLoyeeID;
            ObjMessageBoardModel.CreateEmpLoyee = CreateEmpLoyeeID;
            ObjMessageBoardModel.Parent =hideReturnKey.Value.ToInt32();
            ObjMessageBoardModel.MessAgeContent = txtReturn.Text;
            ObjMessageBoardModel.CreateEmpLoyeeName = ObjEmployeeBLL.GetByID(this.CreateEmpLoyeeID).EmployeeName;

            ObjMessageBoardBLL.Insert(ObjMessageBoardModel);
            JavaScriptTools.AlertAndClosefancybox("留言成功！", Page);
           // BinderData();
        }
    }
}