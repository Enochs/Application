using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HA.PMS.SnManager
{
    public partial class SnIndex : Form
    {
        public SnIndex()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            Sys_SndatabaseEntities ObjEntity = new Sys_SndatabaseEntities();

            var DataSource = ObjEntity.Table_SnManager;
            this.GrewSnList.DataSource = DataSource.ToList();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sn ObjSn = new Sn();
            ObjSn.ShowDialog();
        }


        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Login ObjLogin = new Login();
            ObjLogin.ShowDialog();
            BinderData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BinderData();
        }


        /// <summary>
        /// 生成更新文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreatefileList_Click(object sender, EventArgs e)
        {

            List<string> ObjFloderList = new List<string>();


            DiguiFloder(string.Empty);
            //var FiLeItem=Directory.GetFiles("E:\\FileAutoUpdate\\HoldLove");
            //StringBuilder Objsb = new StringBuilder();
            //foreach (var Item in FiLeItem)
            //{
            //    Objsb.Append(Item.Substring(Item.LastIndexOf("\\")+1).Replace("\"","") + "|" + GetFileByte(Item) +"|"+""+",");
            //}
            //Objsb = Objsb.Replace("\"", "");
            //StreamWriter ObjWrite = new StreamWriter("E:\\FileAutoUpdate\\HoldLove\\FileList.txt");
            //ObjWrite.Write(Objsb.ToString().Trim(','));
            //ObjWrite.Close();
        }


        private void DiguiFloder(string Path)
        {

            if (Path == string.Empty)
            {
                var DirList = Directory.GetDirectories("D:\\FileAutoUpdate\\" + textBox1.Text + "\\");
                foreach (var Objdir in DirList)
                {

                    GetFileStringByFloder(Objdir);
                    DiguiFloder(Objdir);

                }

            }
            else
            {
                var DirList = Directory.GetDirectories(Path);
                foreach (var Objdir in DirList)
                {

                    GetFileStringByFloder(Objdir);
                    DiguiFloder(Objdir);

                }
            }



        }

        /// <summary>
        /// 获取拼凑文件路径 大小
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        private void GetFileStringByFloder(string Path)
        {
            var FiLeItem = Directory.GetFiles(Path);
            StringBuilder Objsb = new StringBuilder();
            string BinPath = string.Empty;

            BinPath = Path.Substring(20, Path.Length-20);

            //D:\FileAutoUpdate\5

            //D:\FileAutoUpdate\4

            foreach (var Item in FiLeItem)
            {
                Objsb.Append(Item.Substring(Item.LastIndexOf("\\") + 1).Replace("\"", "") + "|" + GetFileByte(Item) + "|" + BinPath + ",\r\n");
            }
            Objsb = Objsb.Replace("\"", "");
            StreamWriter ObjWrite = new StreamWriter("D:\\FileAutoUpdate\\" + textBox1.Text + "\\ItemFileList.txt", true);
            ObjWrite.Write(Objsb.ToString().Trim(','));
            ObjWrite.Close();

        }

        public decimal GetFileByte(string Path)
        {
            //根据图片文件的路径使用文件流打开，并保存为byte[]    
            FileStream fs = new FileStream(Path, FileMode.Open);
            decimal Length = fs.Length;
            fs.Close();
            return Length;
        }
    }
}
