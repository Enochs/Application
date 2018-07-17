using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using System.Text;
using HA.PMS.DataAssmblly;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member
{
    public partial class SendMessage : SystemPage
    {

        Customers ObjCustomersBLL = new Customers();
        List<FL_Customers> Customers = null;
        protected int LimitCount = 200;

        protected void Page_Load(object sender, EventArgs e)
        {
            string instr = Request["CustomerID"];
            List<int> idlist = ToIntList(Request["CustomerID"], 0, LimitCount);
            if (idlist != null && idlist.Count > LimitCount)
            {
                Response.Write(string.Format("<script>alert('已超过批量操作的上线{0},超过部分将被忽略！')</script>", LimitCount));
            }
            Customers = GetCustomers(idlist);
            if (IsPostBack)
            {
                BinderMessage();
            }
        }


        /// <summary>
        /// 绑定需要发布的短信
        /// </summary>
        private void BinderMessage()
        {
            if (Request["CustomerID"] != null)
            {
            }
        }

        public string GetCustomerName()
        {
            return GetCustomerNamesString(Customers);
        }

        public string GetPhone()
        {
            return GetCustomerBrideCellPhonesString(Customers);
        }

        public string GetCdKey()
        {
            return System.Configuration.ConfigurationManager.AppSettings["SpServiceName"];
        }

        public string GetCdPwd()
        {
            return System.Configuration.ConfigurationManager.AppSettings["SpPwd"];
        }

        public string GetMessAge()
        {
            if (Customers != null && Customers.Count == 1)
            {
                /// <summary>
                /// 服务方式 
                /// </summary>
                MemberServiceMethodResult ObjMemberServiceMethodResultBLL = new MemberServiceMethodResult();
                var ObjModel = ObjMemberServiceMethodResultBLL.GetByID(Request["TypeID"].ToInt32());
                if (ObjModel != null)
                {
                    return ObjModel.SpTemplete.Replace("&Name&", ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32()).Bride);
                }
            }
            return string.Empty;
        }



        protected List<int> ToIntList(string idstr, int startIndex, int length)
        {
            if (!string.IsNullOrWhiteSpace(idstr))
            {
                List<int> result = new List<int>();
                string[] TempArray = idstr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (TempArray != null && TempArray.Length > 0)
                {
                    for (int i = startIndex; i < (length > TempArray.Length ? TempArray.Length : length); i++)
                    {
                        result.Add(TempArray[i].ToInt32());
                    }
                    return result;
                }
            }
            return null;
        }

        protected List<FL_Customers> GetCustomers(List<int> idlist)
        {
            if (idlist != null && idlist.Count > 0)
            {
                BLLAssmblly.Flow.Customers ObjCustomersBLL = new BLLAssmblly.Flow.Customers();
                List<FL_Customers> result = new List<FL_Customers>();
                foreach (int Item in idlist)
                {
                    result.Add(ObjCustomersBLL.GetByID(Item));
                }
                return result;
            }
            return null;
        }

        protected string GetCustomerNamesString(List<FL_Customers> list)
        {
            if (list != null && list.Count > 0)
            {
                StringBuilder result = new StringBuilder();
                foreach (FL_Customers Item in list)
                {
                    result.Append(Item.Bride).Append(",");
                }
                return result.Remove(result.Length - 1, 1).ToString();
            }
            return string.Empty;
        }

        protected string GetCustomerBrideCellPhonesString(List<FL_Customers> list)
        {
            if (list != null && list.Count > 0)
            {
                StringBuilder result = new StringBuilder();
                foreach (FL_Customers Item in list)
                {
                    result.Append(Item.BrideCellPhone).Append(",");
                }
                return result.Remove(result.Length - 1, 1).ToString();
            }
            return string.Empty;
        }
    }
}