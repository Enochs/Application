using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class OrderReferenceShow : System.Web.UI.Page
    {
        OrderReference ObjOrderReferenceBLL = new OrderReference();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            this.lblContent1.Text = ObjOrderReferenceBLL.GetByState(200).StuContent;
            this.lblContent2.Text = ObjOrderReferenceBLL.GetByState(201).StuContent;
            this.lblContent3.Text = ObjOrderReferenceBLL.GetByState(202).StuContent;
            this.lblContent4.Text = ObjOrderReferenceBLL.GetByState(203).StuContent;
        }

    }
}