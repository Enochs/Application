using HA.PMS.BLLAssmblly.CS;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskforGivefile : SystemPage
    {
        TakeDisk ObjTakeDiskBLL = new TakeDisk();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        /// <summary>
        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        private void BinderData()
        {
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            List<ObjectParameter> ObjParList = new List<ObjectParameter>();
            //ObjParList.Add(new ObjectParameter("PartyDate_between", DateTime.Now.ToShortDateString() + "," + DateTime.Now.AddDays(7).ToShortDateString()));
            ObjParList.Add(new ObjectParameter("IsBegin", true));
            ObjParList.Add(new ObjectParameter("TakeStateID", 2));
            ObjParList.Add(new ObjectParameter("EmpLoyeeID", User.Identity.Name.ToInt32()));

            //if (txtStarDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            //{
            //    ObjParList.Add(new ObjectParameter("PartyDate_between", txtStarDate.Text.ToDateTime() + "," + txtEndDate.Text.ToDateTime()));
            //}

            var DataList = ObjTakeDiskBLL.GetTakeDiskByParameter(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex - 1, out SourceCount, ObjParList.ToArray());
            rptTalkeDisk.DataSource = DataList;
            rptTalkeDisk.DataBind();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

    }
}