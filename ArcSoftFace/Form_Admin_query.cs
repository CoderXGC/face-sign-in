using MySql.Data.MySqlClient;
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
    public partial class Form_Admin_query : Form
    {
        public Form_Admin_query()
        {
            InitializeComponent();
        
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Value.ToString("yyyy-MM-dd");
            int compNum = DateTime.Compare(dateTimePicker1.Value.Date, DateTime.Now);
            if (compNum > 0)
            {
                MessageBox.Show("查询错误，请选择比当前日期小的时间。");
                button1.Enabled = false;
            }
            else {

                DataMysql data = new DataMysql();
                data.dataCon();
                //查询数据库
                string cmdStr = "Select signlog.id,user.name,signlog.signtime from user,signlog where daytime='"+ dateTimePicker1.Value.ToString("yyyy-MM-dd") +"' and signlog.id=user.id ";
                MySqlConnection con = new MySqlConnection("server=116.62.110.115;port=3306;user=facesign;password=99d44172db8d6d58;database=facesign");
                MySqlCommand sqlCmd = new MySqlCommand(cmdStr, con);
                MySqlDataAdapter sda = new MySqlDataAdapter(sqlCmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "sign");
                ds = data.getDataSet(cmdStr);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show(dateTimePicker1.Value.ToString("yyyy-MM-dd") + "当天没有人员签到哦");
                    button1.Enabled = false;
                }
                else { dataGridView1.DataSource = ds.Tables[0];
                    button1.Enabled = true;
                }

            }

        }

        private void Form_Admin_query_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//设置列标题文字居中
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//设置单元格内容居中
            dataGridView1.AllowUserToResizeColumns = false;//禁止用户改变DataGridView1的所有列的列宽
            dataGridView1.AllowUserToResizeRows = false;//禁止用户改变DataGridView1の所有行的行高
            dataGridView1.RowHeadersVisible = false;//第一列空白
            button1.Enabled = false;//禁止导出按钮
            //this.pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\bg.jpg");
            //this.pictureBox1.SendToBack();//将背景图片放到最下面
            //      this.dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;//Datagridview中的数据很多，加载完数据后滚动条自动滚动到最后一行
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel文件到";

            DateTime now = DateTime.Now;
            saveFileDialog.FileName = dateTimePicker1.Value.ToString("yyyy-MM-dd")+"人员签到情况表";
            saveFileDialog.ShowDialog();

            Stream myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
            string str = "";
            try
            {
                //写标题     
                for (int i = 0; i < this.dataGridView1.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        str += "\t";
                    }
                    str += this.dataGridView1.Columns[i].HeaderText;
                }
                sw.WriteLine(str);
                //写内容   
                for (int j = 0; j < this.dataGridView1.Rows.Count; j++)
                {
                    string tempStr = "";
                    for (int k = 0; k < this.dataGridView1.Columns.Count; k++)
                    {
                        if (k > 0)
                        {
                            tempStr += "\t";
                        }
                        tempStr += this.dataGridView1.Rows[j].Cells[k].Value.ToString();
                    }
                    sw.WriteLine(tempStr);
                }
                sw.Close();
                myStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
