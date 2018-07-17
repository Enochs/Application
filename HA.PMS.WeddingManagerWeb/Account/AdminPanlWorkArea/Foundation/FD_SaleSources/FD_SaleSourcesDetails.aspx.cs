using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_SaleSourcesDetails : SystemPage
    {
        SaleSources ObjSaleSourcesBLL = new SaleSources();
        ChannelType ObjChannelTypeBLL = new ChannelType();
        Employee objEmployeeBLL = new Employee();

        protected bool IsSaleSourcePrivateOpening = false;   //指示自己录入的渠道只有自己和主管可以看见功能是否处于开启状态

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int SourceID = Request.QueryString["SourceID"].ToInt32();
                int cEmployeeID = User.Identity.Name.ToInt32();

                //频道权限
                IsSaleSourcePrivateOpening = new SysConfig().IsSaleSourcePrivateOpening(cEmployeeID, false);
                //如果开启了频道限制功能
                if (IsSaleSourcePrivateOpening)
                {
                    var ObjSaleSourcesModel = ObjSaleSourcesBLL.GetByID(SourceID);
                    if (ObjSaleSourcesModel != null && ObjSaleSourcesModel.ProlongationEmployee.HasValue)
                    {
                        //如果该频道是本人创建，或是其主管则可以查看
                        var EmployeeList = new Employee().GetMyManagerEmpLoyee(cEmployeeID).Select(C => C.EmployeeID).ToList();
                        if (EmployeeList.Count.Equals(0))
                        {
                            EmployeeList.Add(cEmployeeID);
                        }
                        if (EmployeeList.Contains(ObjSaleSourcesBLL.GetByID(SourceID).ProlongationEmployee.Value))
                        {
                            GetSaleSources(SourceID);
                        }
                        else
                        {
                            JavaScriptTools.AlertWindow("您无权查看该渠道！", Page);
                        }
                    }
                }
                else
                {
                    GetSaleSources(SourceID);
                }

            }
        }
        /// <summary>
        /// 返回渠道页面的实体类,然后按照对应控件赋值
        /// </summary>
        /// <param name="sourcesId"></param>
        protected void GetSaleSources(int sourcesId)
        {
            HA.PMS.DataAssmblly.FD_SaleSources fd_SaleSources = ObjSaleSourcesBLL.GetByID(sourcesId);
            if (fd_SaleSources != null)
            {
                ltlSourcename.Text = fd_SaleSources.Sourcename;

                ltlProlongationDate.Text = GetShortDateString(fd_SaleSources.ProlongationDate);
                ltlProlongationEmployee.Text = objEmployeeBLL.GetByID(fd_SaleSources.ProlongationEmployee).EmployeeName;

                ltlMaintenanceEmployee.Text = objEmployeeBLL.GetByID(fd_SaleSources.MaintenanceEmployee).EmployeeName;

                ltlChannelType.Text = ObjChannelTypeBLL.GetByID(fd_SaleSources.ChannelTypeId).ChannelTypeName;
                ltlAddress.Text = fd_SaleSources.Address;
                ltlFax.Text = fd_SaleSources.Fax;
                ltlSynopsis.Text = fd_SaleSources.Synopsis;
                ltlPreferentialpolicy.Text = fd_SaleSources.Preferentialpolicy;
                ltlRebatepolicy.Text = fd_SaleSources.Rebatepolicy;
                ltlNeedRebate.Text = fd_SaleSources.NeedRebate == true ? "是" : "否";

                ltlTactcontacts1.Text = fd_SaleSources.Tactcontacts1;
                ltlTactcontactsType1.Text = fd_SaleSources.TactcontactsType1;
                ltlTactcontactsJob1.Text = fd_SaleSources.TactcontactsJob1;
                ltlTactcontactsPhone1.Text = fd_SaleSources.TactcontactsPhone1;
                ltlEmail1.Text = fd_SaleSources.Email1;
                ltlQQ1.Text = fd_SaleSources.QQ1;
                ltlWeibo1.Text = fd_SaleSources.Weibo1;
                ltlWenXin1.Text = fd_SaleSources.WenXin1;

                ltlTactcontacts2.Text = fd_SaleSources.Tactcontacts2;
                ltlTactcontactsType2.Text = fd_SaleSources.TactcontactsType2;
                ltlTactcontactsJob2.Text = fd_SaleSources.TactcontactsJob2;
                ltlTactcontactsPhone2.Text = fd_SaleSources.TactcontactsPhone2;
                ltlEmail2.Text = fd_SaleSources.Email2;
                ltlQQ2.Text = fd_SaleSources.QQ2;
                ltlWeibo2.Text = fd_SaleSources.Weibo2;
                ltlWenXin2.Text = fd_SaleSources.WenXin2;

                ltlTactcontacts3.Text = fd_SaleSources.Tactcontacts3;
                ltlTactcontactsType3.Text = fd_SaleSources.TactcontactsType3;
                ltlTactcontactsJob3.Text = fd_SaleSources.TactcontactsJob3;
                ltlTactcontactsPhone3.Text = fd_SaleSources.TactcontactsPhone3;
                ltlEmail3.Text = fd_SaleSources.Email3;
                ltlQQ3.Text = fd_SaleSources.QQ3;
                ltlWeibo3.Text = fd_SaleSources.Weibo3;
                ltlWenXin3.Text = fd_SaleSources.WenXin3;
            }
        }
    }
}