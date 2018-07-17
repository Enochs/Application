using HA.PMS.BLLAssmblly.CustomerSystem;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CustomerProject.JinseBainian
{
    public partial class CustomerActives : SystemPage
    {
        CelebrationPackage objCelebrationPackage = new CelebrationPackage();
        List<FD_CelebrationPackage> ObjList = new List<FD_CelebrationPackage>();
        List<CC_PackageReserve> ObjResList = new List<CC_PackageReserve>();
        /// <summary>
        /// 预订
        /// </summary>
        PackageReserve ObjPackageReserveBLL = new PackageReserve();
        public class Timers
        {
            public string Day { get; set; }

            public string Datetime { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
               // BinderData(DateTime.Now, DateTime.Now.AddDays(7));
                BinderData(DateTime.Now.ToShortDateString().ToDateTime(), DateTime.Now.AddDays(7).ToShortDateString().ToDateTime());
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="Star"></param>
        /// <param name="End"></param>
        private void BinderData(DateTime Star, DateTime End)
        {

            if (Star == DateTime.MinValue)
            {
                JavaScriptTools.AlertWindow("请选择开始时间!", Page);
                return;
            }


            if (End == DateTime.MinValue)
            {
                JavaScriptTools.AlertWindow("请选择结束时间!", Page);
                return;
            }

            if (End < Star)
            {
                JavaScriptTools.AlertWindow("结束时间过小!", Page);
                return;
            }

            //根据时间段查询出的预订列表
            ObjResList = ObjPackageReserveBLL.GetByTimerSpan(Star, End);

            ObjList = objCelebrationPackage.GetByAll();


            repDataList.DataBind(ObjList);
            List<Timers> objDataList = new List<Timers>();
            int D = (End.DayOfYear + 1) - (Star.DayOfYear);
            for (int i = 0; i < D; i++)
            {
                objDataList.Add(new Timers { Day = Star.AddDays(i).ToShortDateString(), Datetime = DateTime.Now.ToShortDateString() });
            }
            repDataTimeList.DataBind(objDataList);
        }

        protected void repDataTimeList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater Objrep = (Repeater)e.Item.FindControl("repColn");
            Objrep.DataSource = ObjList;
            Objrep.DataBind();


        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            if (DateRanger1.IsNotBothEmpty)
            {
                BinderData(DateRanger1.Start.ToShortDateString().ToDateTime(), DateRanger1.End.ToShortDateString().ToDateTime());
            }
            else
            {
                BinderData(DateTime.Now.ToShortDateString().ToDateTime(), DateTime.Now.AddDays(7).ToShortDateString().ToDateTime());
            }
        }


        /// <summary>
        /// 绑定预订暂定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repColn_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var DataTimer = ((HiddenField)e.Item.FindControl("hideDay")).Value.ToDateTime();
            var ParkegKey = (e.Item.DataItem as FD_CelebrationPackage).PackageID;
            DateTime NowTime = DateTime.Now.ToShortDateString().ToDateTime();

            CC_PackageReserve ObjModel = ObjResList.FirstOrDefault(C => C.PackageID == ParkegKey && C.PartyDate == DataTimer && C.DateItem == "午宴");

            if (ObjModel != null)
            {
                ObjModel.IsDelete = true;
                if (ObjModel.State == 0)
                {
                    if (ObjModel.PartyDate >= NowTime)
                    {
                        ((e.Item.FindControl("lblItem1")) as Label).BackColor = Color.Red;
                    }
                    else
                    {
                        ObjPackageReserveBLL.Update(ObjModel);
                    }
                }
                else
                {
                    if (ObjModel.PartyDate.AddDays(3) >= NowTime)
                    {
                        ((e.Item.FindControl("lblItem1")) as Label).BackColor = Color.Blue;
                    }
                    else
                    {
                        ObjPackageReserveBLL.Update(ObjModel);
                    }
                }
            }


            var ObjModel1 = ObjResList.FirstOrDefault(C => C.PackageID == ParkegKey && C.PartyDate == DataTimer && C.DateItem == "晚宴");

            if (ObjModel1 != null)
            {
                ObjModel1.IsDelete = true;
                if (ObjModel1.State == 0)
                {
                    if (ObjModel1.PartyDate >= NowTime)
                    {
                        ((e.Item.FindControl("lblItem2")) as Label).BackColor = Color.Red;
                    }
                    else
                    {
                        ObjPackageReserveBLL.Update(ObjModel1);
                    }
                }
                else
                {
                    if (ObjModel1.PartyDate.AddDays(3) >= NowTime)
                    {
                        ((e.Item.FindControl("lblItem2")) as Label).BackColor = Color.Blue;
                    }
                    else
                    {
                        ObjPackageReserveBLL.Update(ObjModel1);
                    }
                }
            }
        }


    }
}