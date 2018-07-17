using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using System.Data;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.SysTarget;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Jurisdiction
{
    public partial class Sys_ChannelManager : SystemPage
    {
        Channel ObjChannel = new Channel();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();

                JavaScriptTools.ChecksPageControl("Sys_ChannelManager", this.Page);
            }
        }

        /// <summary>
        /// 绑定频道数据
        /// </summary>
        private void BinderData()
        {
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            ObjParameterList.Add(new ObjectParameter("Parent", 0));
            this.RepChannelList.DataSource = ObjChannel.GetbyParameter(ObjParameterList.ToArray()).OrderBy(C => C.OrderCode);//.Where(C => C.Parent == 0);
            this.RepChannelList.DataBind();
        }


        /// <summary>
        /// 删除顶级项目
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RepChannelList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                ObjChannel.Delete(new DataAssmblly.Sys_Channel() { ChannelID = e.CommandArgument.ToString().ToInt32() });
                BinderData();
            }
        }

        protected void RepChannelList_ItemCreated(object sender, RepeaterItemEventArgs e)
        {

        }



        protected void RepChannelList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Channel ObjChannelBll = new Channel();
            Repeater Objrep = (Repeater)e.Item.FindControl("repSecond");
            Objrep.DataSource = ObjChannelBll.GetByParent(((HiddenField)e.Item.FindControl("hidekey")).Value.ToInt32()).OrderBy(C => C.OrderCode);
            Objrep.DataBind();
        }




        protected void btnCreateBase_Click(object sender, EventArgs e)
        {
            Target ObjTargetBLL = new Target();
            var ObjList = ObjChannel.GetByAll();
            System.Text.StringBuilder objBulider = new System.Text.StringBuilder();

            System.Text.StringBuilder objTargetBulider = new System.Text.StringBuilder();



            System.Text.StringBuilder objFirstBulider = new System.Text.StringBuilder();
            foreach (var Objitem in ObjList)
            {
                string Ismenu = "0";
                if (Objitem.IsMenu.Value)
                {
                    Ismenu = "1";
                }
                string Ispublic = "0";
                if (Objitem.IsPublic.Value)
                {
                    Ispublic = "1";
                }
                else
                {
                    Ispublic = "0";
                }

                string InserString = "INSERT INTO [PMS_Wedding].[dbo].[Sys_Channel]([ChannelID],[ChannelName] ,[ChannelAddress],[CreateDate],[Parent],[IsMenu],[ChannelGetType],[StyleSheethem],[OrderCode],[IsPublic])";
                InserString = InserString + "VALUES(" + Objitem.ChannelID + ",'" + Objitem.ChannelName + "','" + Objitem.ChannelAddress + "','" + DateTime.Now.ToShortDateString() + "'," + Objitem.Parent + "," + Ismenu + "," + "'" + Objitem.ChannelGetType + "'," + "'" + Objitem.StyleSheethem
                    + "','" + Objitem.OrderCode + "'," + Ispublic + ")\r\n";
                if (Objitem.StyleSheethem == null)
                {
                    Objitem.StyleSheethem = string.Empty;
                }
                if (Objitem.ChannelGetType == null)
                {
                    Objitem.ChannelGetType = string.Empty;
                }
               
                string FirstString = Objitem.ChannelID + "$" + Objitem.Parent + "$" + Objitem.ChannelName + "$" + Objitem.ChannelAddress + "$" + Objitem.ChannelGetType.ToString() + "$" + Objitem.StyleSheethem + "$" + Objitem.IsMenu.ToString() + "$" + Objitem.IsPublic.ToString() + "\r\n";
                objFirstBulider.Append(FirstString);

                objBulider.Append(InserString);
            }

            var ObjTargetList = ObjTargetBLL.GetByAll();
            foreach (var ObjTargetItem in ObjTargetList)
            {
                string InserSting = "INSERT INTO [PMS_Wedding].[dbo].[FL_Target]([ChannelID],[TargetTitle] ,[Remark] ,[TargetType] ,[ChannelName],[Unite])VALUES(" + ObjTargetItem.ChannelID + ",'" + ObjTargetItem.TargetTitle + "','" + ObjTargetItem.Remark + "'," + ObjTargetItem.TargetType + ",'" + ObjTargetItem.ChannelName + "','" + ObjTargetItem.Unite + "')\r\n";
                objTargetBulider.Append(InserSting);
            }

            System.IO.StreamWriter OBjWrite = new System.IO.StreamWriter(Server.MapPath("/ChannelList.txt"), false);
            OBjWrite.Write(objBulider.ToString());
            OBjWrite.Close();


            System.IO.StreamWriter OBjTargetWrite = new System.IO.StreamWriter(Server.MapPath("/Target.txt"), false);
            OBjTargetWrite.Write(objTargetBulider.ToString());
            OBjTargetWrite.Close();


            System.IO.StreamWriter OBjFirstTargetWrite = new System.IO.StreamWriter(Server.MapPath("/ChannelData.txt"), false);
            OBjFirstTargetWrite.Write(objFirstBulider.ToString());
            OBjFirstTargetWrite.Close();

        }

        protected void repSecond_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                ObjChannel.Delete(new DataAssmblly.Sys_Channel() { ChannelID = e.CommandArgument.ToString().ToInt32() });
                BinderData();
            }
        }
    }
}