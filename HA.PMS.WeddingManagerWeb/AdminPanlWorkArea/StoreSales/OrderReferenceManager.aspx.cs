using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class OrderReferenceManager :SystemPage
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
            this.txtContent1.Text = ObjOrderReferenceBLL.GetByState(200).StuContent;
            this.txtContent2.Text = ObjOrderReferenceBLL.GetByState(201).StuContent;
            this.txtContent3.Text = ObjOrderReferenceBLL.GetByState(202).StuContent;
            this.txtContent4.Text = ObjOrderReferenceBLL.GetByState(203).StuContent;
        }


        /// <summary>
        /// 维护销售漏斗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            FL_OrderReference ObjOrderReferenceModel = new FL_OrderReference();
            ObjOrderReferenceModel.StuContent = txtContent1.Text;
            ObjOrderReferenceModel.State = 200;

            ObjOrderReferenceBLL.Insert(ObjOrderReferenceModel);

            ObjOrderReferenceModel.StuContent = txtContent2.Text;
            ObjOrderReferenceModel.State = 201;

            ObjOrderReferenceBLL.Insert(ObjOrderReferenceModel);

            ObjOrderReferenceModel.StuContent = txtContent3.Text;
            ObjOrderReferenceModel.State = 202;

            ObjOrderReferenceBLL.Insert(ObjOrderReferenceModel);

            ObjOrderReferenceModel.StuContent = txtContent4.Text;
            ObjOrderReferenceModel.State = 203;

            ObjOrderReferenceBLL.Insert(ObjOrderReferenceModel);
        }
    }
}