using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea
{
    public partial class AllTest : SystemPage
    {
        Hotel ObjHotelBLL = new Hotel();
        CompanyFile ObjFileBLL = new CompanyFile();
        int SourceCount = 0;
        string SortName = "FileID";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DDLDataBind();
                List<PMSParameters> pars = new List<PMSParameters>();
                rptCompanyFile.DataSource = ObjFileBLL.GetDataByParameter(pars, SortName, 1500, 1, out SourceCount);
                rptCompanyFile.DataBind();

                System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
                //sp.SoundLocation = "后会无期.mp3";
                //sp.PlayLooping();

            }
        }

        public void DDLDataBind()
        {
            var DataList = ObjHotelBLL.GetByAll().OrderBy(C => C.HotelName);
            ddlHotel.DataSource = DataList;
            ddlHotel.DataTextField = "HotelName";
            ddlHotel.DataValueField = "HotelID";
            ddlHotel.DataBind();
            ddlHotel.Items.Insert(0, new ListItem { Value = "0", Text = "请选择" });
        }

        #region 点击复制 事件
        /// <summary>
        ///  复制  粘贴功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            if (button.Text == "复制")
            {
                if (txtCopyText.Text != "")
                {
                    //Clipboard.SetDataObject(txtCopyText.Text);
                    string copyText = txtCopyText.Text.Trim().ToString();
                    string.Copy(txtCopyText.Text.Trim().ToString());

                }
            }
            else if (button.Text == "粘贴")
            {
            }
            else if (button.Text == "添加收藏夹")
            {
                string urls = Page.Request.Url.AbsoluteUri.ToString();
                string url = "http://wwww.baidu.com";
                string url1 = "http://localhost:9941/AdminPanlWorkArea/AllTest.aspx";
                addFavorites(urls, "婚庆管理", "C:/Users/Administrator/AppData/Local/Googe/Chrome/User Data/Default");
            }
        }
        #endregion


        #region 添加到收藏夹
        public void addFavorites(string url, string saveName, string folderName)
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri(url));
            request.Method = "GET";
            request.Timeout = 10000;
            try
            {
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //获取当前用户的收藏夹的物理文件夹位置  
                    String favoritesPath = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
                    String savePath = favoritesPath;
                    if (!String.IsNullOrEmpty(folderName))
                    {
                        savePath += @"/" + folderName;
                        if (!Directory.Exists(savePath))
                            Directory.CreateDirectory(savePath);
                    }
                    //IWshRuntimeLibrary.WshShell shell_class = new IWshRuntimeLibrary.WshShellClass();
                    IWshRuntimeLibrary.WshShell shell_class = new WshShell();
                    IWshRuntimeLibrary.IWshShortcut shortcut = null;
                    try
                    {
                        shortcut = shell_class.CreateShortcut(favoritesPath + @"/" + saveName + ".lnk") as IWshRuntimeLibrary.IWshShortcut;
                        shortcut.TargetPath = url;
                        shortcut.Save();
                        JavaScriptTools.AlertWindow("添加成功", Page);
                    }
                    catch (Exception ex)
                    {
                        JavaScriptTools.AlertWindow("添加失败", Page);
                    }
                }
                else
                {
                    JavaScriptTools.AlertWindow("请求失败", Page);
                }
            }
            catch (Exception ex)
            {
                JavaScriptTools.AlertWindow(ex.Message, Page);
            }
        }
        #endregion

        protected void btnPaste_Click(object sender, EventArgs e)
        {
            //有取件时录入取件系统

            HA.PMS.BLLAssmblly.CS.TakeDisk objTakeDiskBLL = new BLLAssmblly.CS.TakeDisk();
            CS_TakeDisk ObjTakeDiskModel = new CS_TakeDisk();


            ObjTakeDiskModel.HaveFile = true;


            ObjTakeDiskModel.HavePhoto = false;

            ObjTakeDiskModel.CustomerID = 1760;
            ObjTakeDiskModel.IsDelete = false;
            ObjTakeDiskModel.IsCheck = false;

            ObjTakeDiskModel.State = 0;
            ObjTakeDiskModel.UpdateEmployee = User.Identity.Name.ToInt32();
            ObjTakeDiskModel.QuotedEmployee = 42;
            ObjTakeDiskModel.IsFinish = false;


            ObjTakeDiskModel.OrderID = Request["OrderID"].ToInt32();
            objTakeDiskBLL.Insert(ObjTakeDiskModel);
        }

        #region 文本变化检索


        protected void txtPasteText_TextChanged(object sender, EventArgs e)
        {
            txtPasteText.Text = GetChineseSpell(txtPasteText.Text.Trim().ToString());
        }
        #endregion

        #region 拼音检索

        public string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += getSpell(strText.Substring(i, 1));
            }
            return myStr;
        }

        public string getSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            else return cnChar;
        }
        #endregion

        #region 查看
        /// <summary>
        /// 查看
        /// </summary>
        protected void rptCompanyFile_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int FileID = e.CommandArgument.ToString().ToInt32();
            if (e.CommandName == "Look")
            {
                var Model = ObjFileBLL.GetByID(FileID);
                string url = "http://officeweb365.com/o/?i=1737&furl=http://timid.wicp.net:54735" + Model.FileURL.ToString();
                Response.Write("<script>window.open('" + url + "')</script>");

            }
        }
        #endregion

        protected void btnOpen_Click(object sender, EventArgs e)
        {
            emPlayer.Src = "Music/喜欢你.mp3";
        }



    }
}