using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskOfWeek : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BinderData(object sender, EventArgs e)
        {
            var objParmList = new List<PMSParameters>();
            string DataTimerStar = DateTime.Today.ToString();
            string DateTimerEnd = DateTime.Now.AddDays(7).ToString();
            objParmList.Add("PartyDate", DataTimerStar + "," + DateTimerEnd, NSqlTypes.DateBetween);
            //objParmList.Add("EmpLoyeeID", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            repCustomer.DataBind(new BLLAssmblly.Flow.Dispatching().GetDispatchingPageByWhere(objParmList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount)); //ObjInvtieBLL.GetInviteCustomerByStateIndex(isAdd, tlCustomerID, GetWhereParList, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
        }
    }
}