using System;
using System.Windows.Forms;
using System.IO;
namespace HA.PMS.EasyMessage
{
    public partial class LoginBox : Form
    {
        string Password = string.Empty;
        string Path = Application.StartupPath + "\\Save.txt";
        public LoginBox()
        {


            InitializeComponent();


            StreamReader objReder = new StreamReader(Path);
            string Word = objReder.ReadToEnd();
            if (Word != "noneemptyHaveEmployeeIDNone")
            {
                chkSaveEmployee.Checked = true;
                txtLoginName.Text = Word.Split(',')[0];
                Password = Word.Split(',')[1];
                txtPassword.Text = "      ";
            }
            objReder.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.btnLogin.Text = "正在登录...";

            Service ObjService = new Service();

            if (txtPassword.Text.Trim() != string.Empty)
            {
                Password = ObjService.GetMd5Password(txtPassword.Text);
            }
            if (chkSaveEmployee.Checked)
            {

                StreamWriter ObjWrite = new StreamWriter(Path, false);
                ObjWrite.Write(txtLoginName.Text + "," +Password);
                ObjWrite.Close();
            }
            this.btnLogin.Enabled = false;
            if (new Service().GetLoginService(txtLoginName.Text, Password))
            {
                this.Hide();
                new EasyMessAgebox() { LoginName = txtLoginName.Text, Password = Password }.Show();
            }
            else
            {
                MessageBox.Show("用户名或密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.btnLogin.Enabled = true;
                this.btnLogin.Text = "登      录";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
