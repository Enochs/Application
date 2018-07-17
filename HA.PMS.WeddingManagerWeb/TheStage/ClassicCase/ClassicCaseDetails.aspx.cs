using System;
using System.Linq;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace HA.PMS.WeddingManagerWeb.TheStage.ClassicCase
{
    public partial class ClassicCaseDetails : System.Web.UI.Page
    {
        TheCase objTheCaseBLL = new TheCase();
        CaseFile objCaseFileBLL = new CaseFile();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int CaseID = Request.QueryString["CaseID"].ToInt32();
                FD_TheCase objTheCase = objTheCaseBLL.GetByID(CaseID);

                ltlHotel.Text = objTheCase.CaseHotel;
                ltlName.Text = objTheCase.CaseName;
                string notes = RemoveHTML(objTheCase.CaseDetails.ToString());
                if (notes.Length > 100)
                {
                    lblNotes.Text = notes.Substring(0, 100) + "……";
                }
                else
                {
                    lblNotes.Text = notes;

                }
                lblNotes.ToolTip = notes;
                ltlGroom.Text = objTheCase.CaseGroom;  
                ltlStyle.Text = objTheCase.CaseStyle;

                var query = objCaseFileBLL.GetByAll().Where(C => C.FileType == 2 && C.CaseId == CaseID);
                var firstData=query.FirstOrDefault();

                if (objTheCase == null)
                {
                    ViewState["imgTop"] = firstData.CaseFilePath.Replace("~", string.Empty);
                }
                else
                {
                    string path = objTheCase.CasePath.ToString();
                    ViewState["imgTop"] = objTheCase.CasePath.Replace("~", "/.");
                }

                rptList.DataSource = query.Skip(1);
                rptList.DataBind();

                List<FD_CaseFile> objCaseFileList = objCaseFileBLL.GetByCaseID(objTheCase.CaseID);
                int sum = objCaseFileList.Count;
                if (objCaseFileList.Count > 3)
                {
                    objCaseFileList = objCaseFileList.Take(3).ToList();
                    div_Details.Visible = true;
                    lblDetails.Text = "(还有" + (sum - 3).ToString() + "条没显示)";
                }
                else
                {
                    div_Details.Visible = false;
                }

 
                
                rptMovieList.DataSource = objCaseFileList;
                rptMovieList.DataBind();
            }
        }

        protected string GetImgPath(object source) 
        {
            string newPath = (source + string.Empty);
            return newPath.Replace("~", "/.");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            
            Employee ObjEmployee = new Employee();
            //login 首页导航，不加入验证中

            if (string.IsNullOrEmpty(txtLoginName.Text) || string.IsNullOrEmpty(txtpassword.Text))
            {
                Response.Redirect("/Account/AdminWorklogin.html");
            }
            int userId = 0;
            if (Request.Cookies["userName"] != null)
            {
                userId = Server.HtmlEncode(Request.Cookies["userName"].Value).ToInt32();
            }

            var ObjSysEmpLoyee = ObjEmployee.EmpLoyeeLogin(txtLoginName.Text.Trim(), txtpassword.Text.Trim().MD5Hash());
            if (ObjSysEmpLoyee != null)
            {

                 


                Response.Cookies["userName"].Value = ObjSysEmpLoyee.EmployeeID + string.Empty;
                Response.Cookies["HAEmployeeID"].Value = ObjSysEmpLoyee.EmployeeID + string.Empty;

                Response.Cookies["userName"].Expires = DateTime.Now.AddMinutes(15);
                System.Web.Security.FormsAuthentication.SetAuthCookie(ObjSysEmpLoyee.EmployeeID.ToString(), false);

                Response.Redirect("/AdminPanlWorkArea/AdminMain.aspx");
             
            }
            else
            {
                Response.Redirect("/Account/AdminWorklogin.html?error=true");
                 
            }

        }

        #region =====过滤html标签 RemoveHTML(string html)=====
        /// <summary>
        /// 过滤html
        /// </summary>
        /// <param name="html">需要过滤的字符串</param>
        /// <returns>过滤html后的字符串</returns>
        public static string RemoveHTML(string html)
        {
            html = Regex.Replace(html, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"-->", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<!--.*", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<img[^>]*>;", "", RegexOptions.IgnoreCase);
            html.Replace("<", "");
            html.Replace(">", "");
            html.Replace("\r\n", "");
            html=html.Replace("&ldquo", "");
            html=html.Replace("&rdquo", "");
            //html = HttpContext.Current.Server.HtmlEncode(html).Trim();
            //html = HttpContext.Current.Server.HtmlDecode(html).Trim();
            return html;
        }
        #endregion
    }
}