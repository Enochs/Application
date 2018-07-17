using HA.PMS.BLLAssmblly.FD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace HA.PMS.EditoerLibrary
{
    public class ddlReferee : System.Web.UI.WebControls.DropDownList
    {
        SaleSources ObjSaleSourcesBLL = new SaleSources();
        public ddlReferee()
        {

        }


        /// <summary>
        /// 根据渠道绑定
        /// </summary>
        /// <param name="ChannelID"></param>
        public void BinderbyChannel(int? ChannelID)
        {
            this.Items.Clear();

            var ObjDataList = ObjSaleSourcesBLL.GetByID(ChannelID);



            if (ObjDataList != null)
            {
                ///收款人1
                if (!string.IsNullOrEmpty(ObjDataList.Tactcontacts1))
                {


                    this.Items.Add(new ListItem(ObjDataList.Tactcontacts1.Trim(), "1"));
                }

                ///收款人2
                if (!string.IsNullOrEmpty(ObjDataList.Tactcontacts2)
                    )
                {
                    this.Items.Add(new ListItem(ObjDataList.Tactcontacts2.Trim(), "2"));

                }

                ///收款人3
                if (!string.IsNullOrEmpty(ObjDataList.Tactcontacts3)
                   )
                {
                    this.Items.Add(new ListItem(ObjDataList.Tactcontacts3.Trim(), "3"));

                }


                //请选择
                this.Items.Add(new ListItem("请选择", "0"));
                this.Items[this.Items.Count - 1].Selected = true;

            }
            else
            {
                this.Items.Add(new ListItem("无", "0"));
            }

        }


        /// <summary>
        /// 根据渠道绑定
        /// </summary>
        /// <param name="ChannelID"></param>
        public void BinderbyChannel(string Sourcename)
        {
            this.Items.Clear();

            var ObjDataList = ObjSaleSourcesBLL.GetByName(Sourcename);



            if (ObjDataList != null)
            {
                ///收款人1
                if (!string.IsNullOrEmpty(ObjDataList.Tactcontacts1))
                {


                    this.Items.Add(new ListItem(ObjDataList.Tactcontacts1.Trim(), "1"));
                }

                ///收款人2
                if (!string.IsNullOrEmpty(ObjDataList.Tactcontacts2)
                    )
                {
                    this.Items.Add(new ListItem(ObjDataList.Tactcontacts2.Trim(), "2"));

                }

                ///收款人3
                if (!string.IsNullOrEmpty(ObjDataList.Tactcontacts3)
                   )
                {
                    this.Items.Add(new ListItem(ObjDataList.Tactcontacts3.Trim(), "3"));

                }


                //请选择
                this.Items.Add(new ListItem("请选择", "0"));
                this.Items[this.Items.Count - 1].Selected = true;

            }
            else
            {
                this.Items.Add(new ListItem("无", "0"));
            }

        }
    }
}
