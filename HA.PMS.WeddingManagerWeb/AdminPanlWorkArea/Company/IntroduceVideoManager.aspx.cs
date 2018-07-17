using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company
{
    public partial class IntroduceVideoManager : SystemPage
    {
        //string IntroMovieFilePath = "~/Files/IntroduceVideo/IntroduceVideo.mov"; //视频地址
        string ConfigPath = "~/Files/IntroduceVideo/config.ini";//配置文件路径
        string BackPath = "~/bin/Login.dll";//备份文件路径
        string LoginPath = "~/Account/AdminWorklogin.html"; //登录导航页面文件虚拟路径
        string DefaultUrl = "/TheStage/CompanyIntroduction/CompanyIntroVideoPlay.html";//导航页面默认导航地址（公司介绍）

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //读取配置文件内容
                txtVideoPath.Text = new IOTools().ReadFileText(Server.MapPath(ConfigPath), System.Text.Encoding.UTF8).Replace(DefaultUrl, string.Empty);
            }
        }

        protected void ButSave_Click(object sender, EventArgs e)
        {

            string Url = txtVideoPath.Text.Trim();//用户输入的网络地址
            IOTools ObjIOTool = new IOTools();
            //读取备份文件
            string BackLoginHtml = ObjIOTool.ReadFileText(Server.MapPath(BackPath), System.Text.Encoding.UTF8);
            //读取源数据
            string OriLoginHtml = ObjIOTool.ReadFileText(Server.MapPath(LoginPath), System.Text.Encoding.UTF8);

            if (!string.IsNullOrWhiteSpace(Url))    /*用户有输入*/
            {
                //如果备份文件不存在或没有数据
                if (string.IsNullOrWhiteSpace(BackLoginHtml))
                {
                    //将源数据写入备份文件
                    ObjIOTool.WriteFileText(Server.MapPath(BackPath), OriLoginHtml, System.Text.Encoding.UTF8);
                    //替换默认网址，并写入源文件
                    ObjIOTool.WriteFileText(Server.MapPath(LoginPath), OriLoginHtml.Replace(DefaultUrl, Url), System.Text.Encoding.UTF8);
                }
                else
                {
                    //写入源文件
                    ObjIOTool.WriteFileText(Server.MapPath(LoginPath), BackLoginHtml.Replace(DefaultUrl, Url), System.Text.Encoding.UTF8);
                }
                //写入配置文件
                ObjIOTool.WriteFileText(Server.MapPath(ConfigPath), Url + DefaultUrl, System.Text.Encoding.UTF8);
                JavaScriptTools.AlertWindow("保存成功，已设置为网络地址！", Page);
            }
            else /*如果用户没有输入数据，点击了保存，则取消网络地址*/
            {
                //如果备份文件不存在或没有数据
                if (string.IsNullOrWhiteSpace(BackLoginHtml))
                {
                    //将源数据写入备份文件
                    ObjIOTool.WriteFileText(Server.MapPath(BackPath), OriLoginHtml, System.Text.Encoding.UTF8);
                }
                else
                {
                    //如果备份文件和源文件不相同
                    if (!OriLoginHtml.Equals(BackLoginHtml))
                    {
                        //将备份文件写入源文件
                        ObjIOTool.WriteFileText(Server.MapPath(LoginPath), BackLoginHtml, System.Text.Encoding.UTF8);
                    }
                }
                //写入配置文件
                ObjIOTool.WriteFileText(Server.MapPath(ConfigPath), DefaultUrl, System.Text.Encoding.UTF8);
                JavaScriptTools.AlertWindow("保存成功，已设置为默认地址！", Page);
            }
        }
    }
}