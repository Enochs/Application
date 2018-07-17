using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class DateRanger : System.Web.UI.UserControl
    {
        #region Fields

        private string title = string.Empty;
        private string dateFormat = "yyyy/MM/dd";
        private DateTime minDate = DateTime.Parse("1753/01/01 00:00:00");
        private DateTime maxDate = DateTime.Parse("9999/12/30 23:59:59");

        #endregion

        #region Properties

        /// <summary>
        /// 获取或设置显示的标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// 获取或设置日期格式。
        /// </summary>
        public string DateFormat
        {
            get { return dateFormat; }
            set { dateFormat = value; }
        }

        /// <summary>
        /// 获取或设置的默认最小日期。
        /// </summary>
        public DateTime MinDate
        {
            get { return minDate; }
            set { minDate = value; }
        }

        /// <summary>
        /// 获取或设置的默认最大日期。
        /// </summary>
        public DateTime MaxDate
        {
            get { return maxDate.AddDays(-1); }
            set { maxDate = value; }
        }

        /// <summary>
        /// 获取或设置起始日期文本框中的日期。若转换为日期失败，返回设置的最小日期
        /// </summary>
        [Obsolete("已过时，请使用StartoEnd")]
        public DateTime Start
        {
            get
            {
                DateTime result;
                if (DateTime.TryParse(txtDateStart.Text, out result))
                {
                    return result.Date;
                }
                return MinDate;
            }
            set
            {
                txtDateStart.Text = value.ToString(DateFormat);
            }
        }

        /// <summary>
        /// 获取或设置截止日期文本框中的日期。若转换为日期失败，返回设置的最大日期

        [Obsolete("已过时，请使用StartoEnd")]
        public DateTime End
        {
            get
            {
                DateTime result;
                if (DateTime.TryParse(txtDateEnd.Text, out result))
                {
                    return result.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
                }
                return MaxDate;
            }
            set
            {
                txtDateEnd.Text = value.ToString(DateFormat);
            }
        }


        public string StartoEnd
        {
            get
            {
                DateTime StarDate = DateTime.Now, EndDate = DateTime.Now;
                if (DateTime.TryParse(txtDateStart.Text, out StarDate))
                {
                    //StarDate=StarDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
                    StarDate = StarDate.Date;
                }
                else
                {
                    StarDate = DateTime.MinValue.AddYears(1900);
                }

                if (DateTime.TryParse(txtDateEnd.Text, out EndDate))
                {
                    //EndDate = EndDate.Date.AddDays(1);
                    EndDate = EndDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

                }
                else
                {
                    EndDate = DateTime.MaxValue.AddDays(-1);
                }



                return StarDate + "," + EndDate;
            }
            set
            {

            }
        }

        /// <summary>
        /// 获取或设置起始日期控件的文本。
        /// </summary>
        public string StartText
        {
            get { return txtDateStart.Text; }
            set { txtDateStart.Text = value; }
        }

        /// <summary>
        /// 获取或设置截止日期控件的文本。
        /// </summary>
        public string EndText
        {
            get { return txtDateEnd.Text; }
            set { txtDateEnd.Text = value; }
        }

        /// <summary>
        /// 指示起始和截止日期文本框是否至少有一个不为空。
        /// </summary>
        public bool IsNotBothEmpty
        {
            get
            {
                return !string.Empty.Equals(txtDateStart.Text.Trim()) || !string.Empty.Equals(txtDateEnd.Text.Trim());
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            lblTitle.Text = Title;
            txtDateStart.MaxLength = txtDateEnd.MaxLength = 16;
            txtDateStart.Width = txtDateEnd.Width = 64;
        }
    }
}