using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Emnus;
using System.Linq;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer
{
    public partial class InviteList : SystemPage
    {
        /// <summary>
        /// 用户操作
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();
        Order objOrderBLL = new Order();

        HA.PMS.BLLAssmblly.Flow.Invite ObjInvtieBLL = new BLLAssmblly.Flow.Invite();
        Telemarketing objTelemarketingBLL = new Telemarketing();
        /// <summary>
        /// 部门
        /// </summary>
        Department ObjDepartmentBLL = new Department();

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
                ddlChannelname.Items.Clear();
            }
        }
        #endregion

        #region 渠道类型选择 绑定渠道名称
        /// <summary>
        /// 选择渠道类型 绑定渠道名称
        /// </summary>
        protected void ddlChanneltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlreferrr.Items.Clear();
            ddlChannelname.Items.Clear();
            if (ddlChanneltype.SelectedValue.ToInt32() == -1)
            {
                ListItem currentList = ddlChannelname.Items.FindByValue("0");
                if (currentList != null)
                {
                    currentList.Selected = true;
                }
            }
            else
            {
                ddlChannelname.BindByParent(ddlChanneltype.SelectedValue.ToInt32());
            }

            //ddlChannelname.BindByParent(ddlChanneltype.SelectedValue.ToInt32());
        }
        #endregion

        #region 选择渠道 绑定联系人
        /// <summary>
        /// 绑定渠道联系人
        /// </summary>
        protected void ddlChannelname_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlChannelname.SelectedValue.ToInt32() == 0)
            {
                ddlreferrr.Items.Clear();
            }
            else
            {
                ddlreferrr.BinderbyChannel(ddlChannelname.SelectedValue.ToInt32());
            }
            // ddlreferrr.BinderbyChannel(ddlChannelname.SelectedValue.ToInt32());
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据到Repeater控件中
        /// </summary>
        private void DataBinder()
        {
            int startIndex = CtrPageIndex.StartRecordIndex;

            int SourceCount = 0;
            var objParmList = new List<PMSParameters>();


            // var GetWhereParList = new List<ObjectParameter>();
            bool isAdd = false;
            //开始时间
            DateTime startTime = new DateTime();
            //如果没有选择结束时间就默认是当前时间
            string dateStr = "2100-1-1";


            DateTime endTime = dateStr.ToDateTime();
            //电话营销表ID
            List<int> tlCustomerID = new List<int>();
            if (ddlDate.SelectedItem.Text != "请选择")
            {
                objParmList.Add(DateRanger.IsNotBothEmpty, ddlDate.SelectedValue, DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }


            //渠道类型
            if (ddlChanneltype.SelectedItem != null)
            {
                if (ddlChanneltype.SelectedItem.Text != "无")
                {

                    objParmList.Add("ChannelType", ddlChanneltype.SelectedValue.ToString().ToInt32(), NSqlTypes.Equal);
                }
            }

            //渠道
            if (ddlChannelname.SelectedItem != null)
            {
                if (ddlChannelname.SelectedItem.Text != "请选择")
                {
                    objParmList.Add("Channel", ddlChannelname.SelectedItem.Text.ToString(), NSqlTypes.StringEquals);
                }
            }

            //按照推荐查询
            if (ddlreferrr.Items.Count != 0)
            {
                if (string.IsNullOrEmpty(ddlreferrr.SelectedItem.Text) || ddlreferrr.SelectedItem.Text != "请选择")
                {
                    objParmList.Add("Referee", ddlreferrr.SelectedItem.Text.ToString(), NSqlTypes.StringEquals);
                }
            }


            //按状态查询
            if (ddlCustomersState1.SelectedItem != null)
            {
                if (ddlCustomersState1.SelectedItem.Text != "无")
                {

                    // GetWhereParList.Add(new ObjectParameter("State", ddlCustomersState1.SelectedValue.ToInt32()));
                    objParmList.Add("State", ddlCustomersState1.SelectedValue);
                }
            }


            //按照责任人查询
            if (MyManager.SelectedValue.ToInt32() == 0)
            {
                MyManager.GetEmployeePar(objParmList);
            }
            else
            {
                objParmList.Add("EmployeeID", MyManager.SelectedValue, NSqlTypes.Equal);
            }

            //联系电话
            if (txtCellPhone.Text != string.Empty)
            {
                objParmList.Add("ContactPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }

            //按新人姓名查询
            objParmList.Add(txtContactMan.Text != "", "ContactMan", txtContactMan.Text, NSqlTypes.LIKE);


            var DataList = ObjInvtieBLL.GetByWhereParameter(objParmList, "CreateDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);

            CtrPageIndex.RecordCount = SourceCount;
            repTelemarketingManager.DataSource = DataList;
            repTelemarketingManager.DataBind();
            lblCustomerCount.Text = SourceCount.ToString();

            ////统计分析开始
            #region k线图
            //#region 当期


            //var SumTotalParmeters = new List<ObjectParameter>();
            //SumTotalParmeters = MyManager.GetEmployeePar(SumTotalParmeters);
            //if (txtStar.Text != string.Empty && txtEnd.Text != string.Empty)
            //{
            //    SumTotalParmeters.Add(new ObjectParameter("CreateDate_between", txtStar.Text.ToDateTime() + "," + txtEnd.Text.ToDateTime()));
            //}

            //#region 当期
            ////当期客源量
            //ltlCustomersCount.Text = ObjInvtieBLL.GetInviteSumTotal(SumTotalParmeters, 1);
            ////当期订单总额
            //ltlOrderMoney.Text = ObjInvtieBLL.GetInviteSumTotal(SumTotalParmeters, 2);

            ////有效信息数量
            //ltlMsgCount.Text = ObjInvtieBLL.GetInviteSumTotal(SumTotalParmeters, 3);

            ////有效率
            //if (ltlMsgCount.Text.ToDecimal() > 0)
            //{
            //    ltlMsgRate.Text = (ltlMsgCount.Text.ToDecimal() / ltlCustomersCount.Text.ToDecimal()).ToString("0.00%");
            //}
            //else
            //{
            //    ltlMsgRate.Text = "0.00%";
            //}

            ////邀约中数量
            //var StateParmeter = new List<ObjectParameter>();
            //StateParmeter = MyManager.GetEmployeePar(StateParmeter);
            //StateParmeter.Add(new ObjectParameter("State", 5));
            //if (txtStar.Text != string.Empty && txtEnd.Text != string.Empty)
            //{
            //    StateParmeter.Add(new ObjectParameter("CreateDate_between", txtStar.Text.ToDateTime() + "," + txtEnd.Text.ToDateTime()));
            //}
            //ltlInviteIng.Text = ObjInvtieBLL.GetInviteSumTotal(StateParmeter, 4);

            ////StateParmeter = new List<ObjectParameter>();
            ////StateParmeter = SumTotalParmeters;
            ////邀约成功数
            //ltlInviteSuccess.Text = ObjInvtieBLL.GetInviteSumTotal(SumTotalParmeters, 5);
            ////邀约中率
            //if (ltlCustomersCount.Text.ToDecimal() > 0)
            //{
            //    ltlInviteRate.Text = (ltlInviteIng.Text.ToDecimal() / ltlCustomersCount.Text.ToDecimal()).ToString("0.00%");
            //}
            //else
            //{
            //    ltlInviteRate.Text = "0.00%";
            //}


            ////邀约成功

            ////邀约成功率
            //if (ltlCustomersCount.Text.ToDecimal() > 0)
            //{
            //    ltlSuccessRate.Text = (ltlInviteSuccess.Text.ToDecimal() / ltlCustomersCount.Text.ToDecimal()).ToString("0.00%");
            //}





            ////流失量
            ////int loseCount = currentList.Where(C => C.State == 29).Count();
            //StateParmeter = new List<ObjectParameter>();
            //StateParmeter = MyManager.GetEmployeePar(StateParmeter);
            //StateParmeter.Add(new ObjectParameter("State", 29));
            //if (txtStar.Text != string.Empty && txtEnd.Text != string.Empty)
            //{
            //    StateParmeter.Add(new ObjectParameter("CreateDate_between", txtStar.Text.ToDateTime() + "," + txtEnd.Text.ToDateTime()));
            //}
            //ltlLose.Text = ObjInvtieBLL.GetInviteSumTotal(StateParmeter, 6);
            //////流失率
            //if (ltlLose.Text.ToDecimal() > 0)
            //{
            //    ltlLoseRate.Text = (ltlLose.Text.ToDecimal() / ltlCustomersCount.Text.ToDecimal()).ToString("0.00%");
            //}
            //else
            //{
            //    ltlLoseRate.Text = "0.00%";
            //}
            //////未邀约


            //StateParmeter = new List<ObjectParameter>();
            //StateParmeter = MyManager.GetEmployeePar(StateParmeter);
            //StateParmeter.Add(new ObjectParameter("State", 3));
            //ltlDont.Text = ObjInvtieBLL.GetInviteSumTotal(StateParmeter, 7);
            ////未邀约率
            //if (ltlCustomersCount.Text.ToDecimal() > 0)
            //{

            //    ltlDontRate.Text = (ltlDont.Text.ToDecimal() / ltlCustomersCount.Text.ToDecimal()).ToString("0.00%");
            //}
            //else
            //{
            //    ltlInviteRate.Text = "0.00%";
            //}
            //#endregion

            //#endregion



            //#region 历史
            //if (!IsPostBack)
            //{
            //    BinderHistorym();
            //}
            //#endregion

            #endregion
        }
        #endregion

        ///// <summary>
        ///// 绑定历史累计统计
        ///// </summary>
        //private void BinderHistorym()
        //{

        //    var SumTotalParmeters = new List<ObjectParameter>();

        //    MyManager.GetEmployeePar(SumTotalParmeters);

        //    #region 历史

        //    //当期客源量
        //    ltlAllCustomers.Text = ObjInvtieBLL.GetInviteSumTotal(SumTotalParmeters, 1);
        //    //当期订单总额
        //    ltlAllOrderMoney.Text = ObjInvtieBLL.GetInviteSumTotal(SumTotalParmeters, 2);

        //    //有效信息数量
        //    ltlAllMsgCount.Text = ObjInvtieBLL.GetInviteSumTotal(SumTotalParmeters, 3);


        //    //有效率
        //    if (ltlAllCustomers.Text.ToDecimal() > 0)
        //    {
        //        ltlAllMsgRate.Text = (ltlAllMsgCount.Text.ToDecimal() / ltlAllCustomers.Text.ToDecimal()).ToString("0.00%");
        //    }
        //    else
        //    {
        //        ltlAllMsgRate.Text = "0.00%";
        //    }

        //    //邀约中数量
        //    var StateParmeter = new List<ObjectParameter>();
        //    StateParmeter = MyManager.GetEmployeePar(StateParmeter);
        //    StateParmeter.Add(new ObjectParameter("State", 5));

        //    ltlAllInviteIng.Text = ObjInvtieBLL.GetInviteSumTotal(StateParmeter, 4);

        //    if (ltlAllCustomers.Text.ToDecimal() > 0)
        //    {
        //        ltlAllInviteRate.Text = (ltlAllInviteIng.Text.ToDecimal() / ltlAllCustomers.Text.ToDecimal()).ToString("0.00%");
        //    }
        //    else
        //    {
        //        ltlAllMsgRate.Text = "0.00%";
        //    }


        //    ////历史邀约成功
        //    ltlAllInviteSuccess.Text = ObjInvtieBLL.GetInviteSumTotal(SumTotalParmeters, 5);
        //    //邀约成功率
        //    if (ltlCustomersCount.Text.ToDecimal() > 0)
        //    {
        //        ltlAllSuccessRate.Text = (ltlAllInviteSuccess.Text.ToDecimal() / ltlAllCustomers.Text.ToDecimal()).ToString("0.00%");
        //    }

        //    StateParmeter = new List<ObjectParameter>();
        //    StateParmeter = MyManager.GetEmployeePar(StateParmeter);
        //    StateParmeter.Add(new ObjectParameter("State", 29));
        //    ltlAllLose.Text = ObjInvtieBLL.GetInviteSumTotal(StateParmeter, 6);
        //    ////流失率
        //    if (ltlAllCustomers.Text.ToDecimal() > 0)
        //    {
        //        ltlAllLoseRate.Text = (ltlAllLose.Text.ToDecimal() / ltlAllCustomers.Text.ToDecimal()).ToString("0.00%");
        //    }
        //    else
        //    {
        //        ltlAllLoseRate.Text = "0.00%";
        //    }
        //    ////未邀约




        //    StateParmeter = new List<ObjectParameter>();
        //    StateParmeter = MyManager.GetEmployeePar(StateParmeter);
        //    StateParmeter.Add(new ObjectParameter("State", 3));
        //    ltlAllDont.Text = ObjInvtieBLL.GetInviteSumTotal(StateParmeter, 7);
        //    //未邀约率
        //    if (ltlAllCustomers.Text.ToDecimal() > 0)
        //    {

        //        ltlAllDontRate.Text = (ltlDont.Text.ToDecimal() / ltlAllCustomers.Text.ToDecimal()).ToString("0.00%");
        //    }
        //    else
        //    {
        //        ltlAllDontRate.Text = "0.00%";
        //    }
        //    //ltlAllLose.Text = allLoseCount + string.Empty;
        //    ////历史流失率
        //    //ltlAllLoseRate.Text = GetDoubleFormat((double)allLoseCount / allCustomersCount);
        //    ////历史为邀约

        //    //ltlAllDont.Text = allDont + string.Empty;
        //    ////历史未邀约率
        //    //ltlAllDontRate.Text = GetDoubleFormat((double)allDont / allCustomersCount);
        //    #endregion
        //}


        #region 算出订单总金额
        /// <summary>
        /// 算出订单总金额 
        /// </summary>
        protected string GetSumOrderMoneyByCustomerId(List<View_GetInviteCustomers> currentCustomer)
        {
            decimal currentSumMoney = 0;
            foreach (var item in currentCustomer)
            {
                currentSumMoney += GetAggregateAmount(item.CustomerID).ToDecimal();
            }
            return currentSumMoney + string.Empty;
        }
        #endregion

        #region 根据ID获取部门
        /// <summary>
        /// 根据ID获取部门
        /// </summary>
        public string GetDepartmentByID(object ID)
        {
            if (ID == null)
            {
                return string.Empty;
            }
            else
            {
                var ObjModel = ObjDepartmentBLL.GetByID(ID.ToString().ToInt32());
                if (ObjModel == null)
                {
                    return string.Empty;
                }
                else
                {
                    return ObjModel.DepartmentName;
                }
            }
        }
        #endregion

        #region 根据ID获取用户名
        /// <summary>
        /// 根据ID获取用户名
        /// </summary>
        public string GetEmpLoyeeNameByID(object ID)
        {
            if (ID == null)
            {
                return string.Empty;
            }
            else
            {
                var ObjModel = ObjEmployeeBLL.GetByID(ID.ToString().ToInt32());
                if (ObjModel == null)
                {
                    return string.Empty;
                }
                else
                {
                    return ObjModel.EmployeeName;
                }
            }
        }
        #endregion

        #region 点击查询   页数
        /// <summary>
        /// 点击
        /// </summary>   
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void ddlChooseYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 获取下次沟通时间
        /// <summary>
        /// 下次沟通时间
        /// </summary>
        public string GetNextDate(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            InvtieContent ObjContentBLL = new InvtieContent();
            var DataList = ObjContentBLL.GetByCustomerID(CustomerID);
            DateTime NextPlanDate = DataList.Max(C => C.NextPlanDate).ToString().ToDateTime();
            return NextPlanDate.ToShortDateString();
        }
        #endregion

        #region 屏蔽导出


        protected void btnExportoExcel_Click(object sender, EventArgs e)
        {

            //StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/IntiveSumModel3.xml"));

            //string ObjTempletContent = Objreader.ReadToEnd();
            //Objreader.Close();
            //ObjTempletContent = ObjTempletContent.Replace("<=1ByNow>", ltlCustomersCount.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=2ByNow>", ltlOrderMoney.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=3ByNow>", ltlMsgCount.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=4ByNow>", ltlMsgRate.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=5ByNow>", ltlInviteIng.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=6ByNow>", ltlInviteRate.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=7ByNow>", ltlInviteSuccess.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=8ByNow>", ltlSuccessRate.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=9ByNow>", ltlLose.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=10ByNow>", ltlLoseRate.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=11ByNow>", ltlDont.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=12ByNow>", ltlDontRate.Text);


            //ObjTempletContent = ObjTempletContent.Replace("<=1ByOther>", ltlAllCustomers.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=2ByOther>", ltlAllOrderMoney.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=3ByOther>", ltlAllMsgCount.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=4ByOther>", ltlAllMsgRate.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=5ByOther>", ltlAllInviteIng.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=6ByOther>", ltlAllInviteRate.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=7ByOther>", ltlAllInviteSuccess.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=8ByOther>", ltlAllSuccessRate.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=9ByOther>", ltlAllLose.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=10ByOther>", ltlAllLoseRate.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=11ByOther>", ltlAllDont.Text);
            //ObjTempletContent = ObjTempletContent.Replace("<=12ByOther>", ltlAllDontRate.Text);
            //IOTools.DownLoadByString(ObjTempletContent, "xls");
        }
        #endregion
    }
}