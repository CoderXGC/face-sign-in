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
    public partial class ControlFm : Form
    {
        public ControlFm()
        {
            InitializeComponent();
        }

        Random r = new Random();
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            label1.ForeColor = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
            label5.Text = DateTime.Now.ToString();


        }

        private void ControlFm_Load(object sender, EventArgs e)
        {
            try
            {
                Task.Factory.StartNew(() => {
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
            string url = "https://www.ylesb.com/csimg/login.jpg";
           this.pictureBox1.Image = Image.FromStream(System.Net.WebRequest.Create(url).GetResponse().GetResponseStream());
            this.pictureBox1.SendToBack();//将背景图片放到最下面
            this.label1.Parent = this.pictureBox1;
            this.label1.BackColor = Color.Transparent;
            this.label2.Parent = this.pictureBox1;
            this.label2.BackColor = Color.Transparent;
            this.label3.Parent = this.pictureBox1;
            this.label3.BackColor = Color.Transparent;
            this.label4.Parent = this.pictureBox1;
            this.label4.BackColor = Color.Transparent;
            this.label5.Parent = this.pictureBox1;
            this.label5.BackColor = Color.Transparent;
            this.radioButton1.Parent= this.pictureBox1;
            this.radioButton1.BackColor = Color.Transparent;
            this.radioButton2.Parent = this.pictureBox1;
            this.radioButton2.BackColor = Color.Transparent;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            label3.Visible = false;
            pwd.Visible = false;
            button1.Visible = false;
            radioButton1.Checked = false;
        }
      private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //FaceForm faceForm = new FaceForm();
            //faceForm.Show();
            Form_Sign form_User = new Form_Sign();
            form_User.Show();
            //Form_Admin form_Admin = new Form_Admin();
            //form_Admin.Show();
            this.Hide();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label3.Visible = true;
            pwd.Visible = true;
            button1.Visible = true;
        }

        private void ControlFm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            Dispose();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pwd.Text.Trim() != "")
            {
                //连接数据库
                DataMysql data = new DataMysql();
                data.dataCon();

                //查询数据库
                string cmdStr = "Select * from admin where pwd='" + pwd.Text +"'";
                DataSet ds;
                ds = data.getDataSet(cmdStr);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    //FaceForm faceForm = new FaceForm();
                    //faceForm.Show();
                    Form_Admin form_Admin = new Form_Admin();
                    form_Admin.StartPosition = FormStartPosition.CenterScreen;
                    form_Admin.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("密码错误!");
                }
            }
                else
                {
                    MessageBox.Show("密码不能为空!");
                }
            }
          
        }
    
    
}
