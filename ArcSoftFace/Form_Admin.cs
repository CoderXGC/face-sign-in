using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace
{
    public partial class Form_Admin : Form
    {
        public Form_Admin()
        {
            InitializeComponent();

        }

        private void 录入员工信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form_Admin_insert From_admin_insert = new Form_Admin_insert(); //实例化一个子窗口

            //设置子窗口不显示为顶级窗口

            From_admin_insert.TopLevel = false;

            //设置子窗口的样式，没有上面的标题栏

            From_admin_insert.FormBorderStyle = FormBorderStyle.None;

            //填充

            From_admin_insert.Dock = DockStyle.Fill;

            //清空Panel里面的控件

            this.panel1.Controls.Clear();

            //加入控件

            this.panel1.Controls.Add(From_admin_insert);

            //让窗体显示

            From_admin_insert.Show();

        }

        private void 修改员工信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Admin_update From_admin_update = new Form_Admin_update(); //实例化一个子窗口

            //设置子窗口不显示为顶级窗口

            From_admin_update.TopLevel = false;

            //设置子窗口的样式，没有上面的标题栏

            From_admin_update.FormBorderStyle = FormBorderStyle.None;

            //填充

            From_admin_update.Dock = DockStyle.Fill;

            //清空Panel里面的控件

            this.panel1.Controls.Clear();

            //加入控件

            this.panel1.Controls.Add(From_admin_update);

            //让窗体显示

            From_admin_update.Show();
        }

        private void 查看签到ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Admin_query From_admin_query = new Form_Admin_query(); //实例化一个子窗口

            //设置子窗口不显示为顶级窗口

            From_admin_query.TopLevel = false;

            //设置子窗口的样式，没有上面的标题栏

            From_admin_query.FormBorderStyle = FormBorderStyle.None;

            //填充

            //  From_admin_query.Dock = DockStyle.Fill;

            //清空Panel里面的控件

            this.panel1.Controls.Clear();

            //加入控件

            this.panel1.Controls.Add(From_admin_query);

            //让窗体显示

            From_admin_query.Show();
        }

        private void 关于系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
        }

        private void Form_Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("真的要退出程序吗？", "退出程序", MessageBoxButtons.OKCancel) == DialogResult.Cancel)

            {

                e.Cancel = true;

            }
        }

        private void Form_Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
            Application.Exit();
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Left -= 5;
            if (label1.Right < 0)
            {
                label1.Left = this.Width;
            }
        }

        private void Form_Admin_Load(object sender, EventArgs e)
        {
            label1.Left = this.Width;
            timer1.Interval = 100;
            timer1.Enabled = true;
            //pictureBox1.Image = "";
            //string url = "http://b.hiphotos.baidu.com/image/pic/item/03087bf40ad162d93b3a196f1fdfa9ec8b13cde9.jpg";
            //try
            //{
            //    pictureBox1.Load(url);
            //}
            //catch (Exception ex)
            //{
            //    //显示本地默认图片
            //}

            //string url = "http://img.zcool.cn/community/01635d571ed29832f875a3994c7836.png@900w_1l_2o_100sh.jpg";
            //this.pictureBox.Image = Image.FromStream(System.Net.WebRequest.Create(url).GetResponse().GetResponseStream());
            string url = "https://www.ylesb.com/csimg/bg.jpg";
            this.pictureBox1.Image = Image.FromStream(System.Net.WebRequest.Create(url).GetResponse().GetResponseStream());//背景图片
            this.pictureBox1.SendToBack();//将背景图片放到最下面
            this.panel1.BackColor = Color.Transparent;//将Panel设为透明
                                                      // this.panel1.Parent = this.pictureBox1;//将panel父控件设为背景图片控件
            this.panel1.BringToFront();//将panel放在前面


        }

        private void 技术支持ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "http://wpa.qq.com/msgrd?v=3&uin=" + 466534434 + "&site=qq&menu=yes";
            ///新开线程，已请求该网址
            System.Diagnostics.Process.Start(url);
        }

        private void 发送反馈ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://mail.qq.com/cgi-bin/qm_share?t=qm_mailme&email=wKOvpKWyuKejgKavuK2hqazuo6_ts");

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.ylesb.com/");
        }


        private void 检查更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            WebClient client = new WebClient();
            byte[] buffer = client.DownloadData("https://www.ylesb.com/csimg/updateversion.txt");
            string version = Encoding.GetEncoding("GB2312").GetString(buffer);
            if (System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Equals(version))
            {
                MessageBox.Show("暂无更新哦~", "温馨提示");
            }
            else
            {
            
                byte[] bufferl = client.DownloadData("https://www.ylesb.com/csimg/updatelog.txt");
                string log = Encoding.GetEncoding("GB2312").GetString(bufferl);
                if (MessageBox.Show("发现新版本\n" + log + "\n是否现在更新?", "温馨提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    try
                    {
                        Task.Factory.StartNew(() =>
                        {
                            //更新程序位于主程序安装目录的“UpdateApp”文件夹里面
                            string exePath = $@"{AppDomain.CurrentDomain.BaseDirectory}AppUpdate\UpdateApp.exe";
                            if (File.Exists(exePath))
                            {
                                System.Diagnostics.Process p = new System.Diagnostics.Process();
                                p.StartInfo.FileName = exePath;
                                p.Start();
                            }
                        });
                    }
                    catch { }
                }
                else
                { }
            
                
            }


        }
    }
}
