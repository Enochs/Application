using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.CS;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class Index : System.Web.UI.Page
    {
        FourGuardian ObjFourGuardianBLL = new FourGuardian();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.repContent.DataSource = ObjFourGuardianBLL.GetByType(1);
                this.repContent.DataBind();
                rbtnContent.DataSource = ObjFourGuardianBLL.GetByType(2);
                rbtnContent.DataBind();
                lblSums.Text = DateTime.Now.ToString("1949-10-01").ToString() + "时间";
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            BinderDatas();
        }

        public void BinderDatas()
        {
            Customers ObjCustomerBLL = new Customers();
            FL_Customers CustomerModel = new FL_Customers();
            var DataList = ObjCustomerBLL.GetByAll().Where(C => C.PartyDate <= DateTime.Now.ToShortDateString().ToDateTime() && C.PartyDate != DateTime.Now.ToString("1949-10-01").ToDateTime() && C.State != 206 && C.State != 29);


            foreach (var item in DataList)
            {
                CustomerModel = ObjCustomerBLL.GetByID(item.CustomerID);
                CustomerModel.FinishOver = true;
                CustomerModel.State = 206;
                CustomerModel.CustomerID = item.CustomerID;
                ObjCustomerBLL.UpdateCustomer(CustomerModel);
                //ObjCustomerBLL.Update(CustomerModel);

                #region 注释

                //if (item.State != 206)
                //{
                //    CustomerModel = ObjCustomerBLL.GetByID(item.CustomerID);
                //    CustomerModel.FinishOver = true;
                //    CustomerModel.State = 206;
                //    ObjCustomerBLL.Update(CustomerModel);
                //}

                //if (item.PartyDate <= DateTime.Now.ToShortDateString().ToDateTime())
                //{
                //    CustomerModel.FinishOver = true;
                //    if (item.State != 206)
                //    {
                //        CustomerModel.State = 206;
                //    }

                //}
                //if (item.PartyDate >= DateTime.Now.ToShortDateString().ToDateTime())
                //{
                //    CustomerModel.FinishOver = false;
                //}
                //ObjCustomerBLL.Update(CustomerModel);
                #endregion
            }

        }

        public void BindSatisfaction()
        {
            DegreeOfSatisfaction ObjDegreeBLL = new DegreeOfSatisfaction();
            Customers ObjCustomerBLL = new Customers();
            var DataList = ObjCustomerBLL.GetByAll().Where(C => C.FinishOver == true && C.State == 206 && C.PartyDate <= DateTime.Now.ToShortDateString().ToDateTime() && C.PartyDate != DateTime.Now.ToString("1949-10-01").ToDateTime());
            foreach (var item in DataList)
            {
                var ObjDegreeModel = ObjDegreeBLL.GetByCustomersID(item.CustomerID);
                if (ObjDegreeModel == null)
                {
                    ObjDegreeBLL.Insert(new CS_DegreeOfSatisfaction()
                    {
                        CustomerID = item.CustomerID,
                        SumDof = "",
                        DofContent = "",
                        DofDate = null,
                        IsDelete = false,
                        DegreeResult = null,
                        State = 0,
                        UpdateTime = DateTime.Now.ToString().ToDateTime(),
                        UpdateEmployeeID = User.Identity.Name.ToInt32(),
                        PlanDate = null,
                    });
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BindSatisfaction();
        }

        public void BindDown()
        {
            this.repContent.DataSource = ObjFourGuardianBLL.GetByType(1);
            this.repContent.DataBind();
        }
    }
}