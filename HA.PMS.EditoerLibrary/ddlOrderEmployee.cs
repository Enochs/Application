using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Sys;
using System.Web;

namespace HA.PMS.EditoerLibrary
{
    public class ddlOrderEmployee:DropDownList
    {
        public ddlOrderEmployee()
        {
           





        }


        public void BinderByCustomerID(int CustomerID)
        {
            Invite ObjInviteBLL = new Invite();
            
            Order ObjOrderBLL = new Order();
            Dispatching ObjDispatchingBLL = new Dispatching();
            QuotedPrice ObjQuotedPriceBLL = new QuotedPrice();
            Employee ObjEmployeeBLL = new Employee();

            var objInvuteModel = ObjInviteBLL.GetByCustomerID(CustomerID);
            if (objInvuteModel != null)
            {

                this.Items.Add(new ListItem(ObjEmployeeBLL.GetByID(objInvuteModel.EmpLoyeeID).EmployeeName + "(邀约人)", objInvuteModel.EmpLoyeeID.ToString()));
            }

            var ObjOrderModel = ObjOrderBLL.GetbyCustomerID(CustomerID);
            if (ObjOrderModel != null)
            {

                this.Items.Add(new ListItem(ObjEmployeeBLL.GetByID(ObjOrderModel.EmployeeID).EmployeeName + "(跟单人)", ObjOrderModel.EmployeeID.ToString()));
            }

            var ObjQuotedModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
            if (ObjQuotedModel != null)
            {
                this.Items.Add(new ListItem(ObjEmployeeBLL.GetByID(ObjQuotedModel.EmpLoyeeID).EmployeeName + "(策划师)", ObjQuotedModel.EmpLoyeeID.ToString()));
            }


            var DisModel=ObjDispatchingBLL.GetDispatchingByCustomerID(CustomerID);
            if (DisModel != null)
            {
                this.Items.Add(new ListItem(ObjEmployeeBLL.GetByID(DisModel.EmployeeID).EmployeeName + "(总派工人)", DisModel.EmployeeID.ToString()));
            }

        }

    }
}
