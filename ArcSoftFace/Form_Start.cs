using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace
{
    public partial class Form_Start : Form
    {
        int iCount = 0;
        public Form_Start()
        {
            InitializeComponent();
            //启动画面
            this.FormBorderStyle = FormBorderStyle.None;//去掉外框
           string url = "https://www.ylesb.com/csimg/bg.jpg";
            //this.pictureBox.Image = Image.FromStream(System.Net.WebRequest.Create(url).GetResponse().GetResponseStream());
            this.BackgroundImage = Image.FromStream(System.Net.WebRequest.Create(url).GetResponse().GetResponseStream());//背景图片
            this.StartPosition = FormStartPosition.CenterScreen;//屏幕中央
        }

        private void Form_Start_Load(object sender, EventArgs e)
        {
            //启动定时器
            timer1.Start();
            timer1.Interval = 100;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (iCount > 700)
            {
           
                //Application.Exit();//启动动画完了之后..
                ControlFm controlFm = new ControlFm();
                //Form_Admin form_Admin = new Form_Admin();
                //form_Admin.Show();
                controlFm.StartPosition = FormStartPosition.CenterScreen;
                controlFm.Show();
                this.Hide();
                timer1.Stop();
            }
            iCount += 30;
            this.Opacity = 1 - Convert.ToDouble(iCount) / 1000;//窗体透明度减小
        }
    
    }
}
