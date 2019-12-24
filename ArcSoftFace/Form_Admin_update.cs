using ArcSoftFace.SDKModels;
using ArcSoftFace.SDKUtil;
using ArcSoftFace.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace ArcSoftFace
{
    public partial class Form_Admin_update : Form
    {
        //引擎Handle
        private IntPtr pImageEngine = IntPtr.Zero;

        //保存对比图片的列表
        private List<string> imagePathList = new List<string>();

        //左侧图库人脸特征列表
        private List<IntPtr> imagesFeatureList = new List<IntPtr>();

  
        private string[] path;
        public Form_Admin_update()
        {
            InitializeComponent();
            InitEngines();
        }
        private void InitEngines()
        {
            //读取配置文件
            AppSettingsReader reader = new AppSettingsReader();
            string appId = (string)reader.GetValue("APP_ID", typeof(string));
            string sdkKey64 = (string)reader.GetValue("SDKKEY64", typeof(string));
            string sdkKey32 = (string)reader.GetValue("SDKKEY32", typeof(string));

            var is64CPU = Environment.Is64BitProcess;
            if (is64CPU)
            {
                if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(sdkKey64))
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    MessageBox.Show("请在App.config配置文件中先配置APP_ID和SDKKEY64!");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(sdkKey32))
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    MessageBox.Show("请在App.config配置文件中先配置APP_ID和SDKKEY32!");
                    return;
                }
            }

            //激活引擎    如出现错误，1.请先确认从官网下载的sdk库已放到对应的bin中，2.当前选择的CPU为x86或者x64
            int retCode = 0;

            try
            {
                retCode = ASFFunctions.ASFActivation(appId, is64CPU ? sdkKey64 : sdkKey32);
            }
            catch (Exception ex)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                if (ex.Message.IndexOf("无法加载 DLL") > -1)
                {
                    MessageBox.Show("请将sdk相关DLL放入bin对应的x86或x64下的文件夹中!");
                }
                else
                {
                    MessageBox.Show("激活引擎失败!");
                }
                return;
            }
            Console.WriteLine("Activate Result:" + retCode);

            //初始化引擎
            uint detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;
            //检测脸部的角度优先值
            int detectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_HIGHER_EXT;
            //人脸在图片中所占比例，如果需要调整检测人脸尺寸请修改此值，有效数值为2-32
            int detectFaceScaleVal = 16;
            //最大需要检测的人脸个数
            int detectFaceMaxNum = 5;
            //引擎初始化时需要初始化的检测功能组合
            int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE;
            //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e
            retCode = ASFFunctions.ASFInitEngine(detectMode, detectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pImageEngine);
            Console.WriteLine("InitEngine Result:" + retCode);
            AppendText((retCode == 0) ? "引擎初始化成功!请执行录入!\n" : string.Format("引擎初始化失败!错误码为:{0}\n", retCode));
            if (retCode != 0)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }


            ////初始化视频模式下人脸检测引擎
            //uint detectModeVideo = DetectionMode.ASF_DETECT_MODE_VIDEO;
            //int combinedMaskVideo = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION;
            //retCode = ASFFunctions.ASFInitEngine(detectModeVideo, detectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMaskVideo, ref pVideoEngine);

            ////视频专用FR引擎
            //detectFaceMaxNum = 1;
            //combinedMask = FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_FACE3DANGLE;
            //retCode = ASFFunctions.ASFInitEngine(detectMode, detectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pVideoImageEngine);
            //Console.WriteLine("InitVideoEngine Result:" + retCode);


        }
        private void AppendText(string message)
        {
            logBox.AppendText(message);
        }
        private object locker = new object();

        private void button4_Click_1(object sender, EventArgs e)
        {
            lock (locker)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "选择图片";
                openFileDialog.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.png";
                openFileDialog.Multiselect = true;
                openFileDialog.FileName = string.Empty;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    List<string> imagePathListTemp = new List<string>();
                    var numStart = imagePathList.Count;
                    int isGoodImage = 0;

                    //保存图片路径并显示
                    string[] fileNames = openFileDialog.FileNames;
                    path = openFileDialog.FileNames;

                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        imagePathListTemp.Add(fileNames[i]);
                    }



                    //人脸检测以及提取人脸特征
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                    {
                        //禁止点击按钮
                        Invoke(new Action(delegate
                        {
                            button1.Enabled = false;
                            button2.Enabled = false;
                            button3.Enabled = false;
                            button4.Enabled = false;
                        }));

                        //人脸检测和剪裁
                        for (int i = 0; i < imagePathListTemp.Count; i++)
                        {
                            Image image = Image.FromFile(imagePathListTemp[i]);
                            if (image.Width % 4 != 0)
                            {
                                image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
                            }
                            ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, image);

                            if (multiFaceInfo.faceNum > 0)
                            {
                                imagePathList.Add(imagePathListTemp[i]);
                                MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects);
                                image = ImageUtil.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);
                            }
                            else
                            {
                                continue;
                            }

                            this.Invoke(new Action(delegate
                            {
                                if (image == null)
                                {
                                    image = Image.FromFile(imagePathListTemp[i]);
                                }
                                imageLists.Images.Add(imagePathListTemp[i], image);
                                imageList.Items.Add((numStart + isGoodImage) + "号", imagePathListTemp[i]);
                                isGoodImage += 1;
                                image = null;
                            }));
                        }


                        //提取人脸特征
                        for (int i = numStart; i < imagePathList.Count; i++)
                        {
                            ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
                            IntPtr feature = FaceUtil.ExtractFeature(pImageEngine, Image.FromFile(imagePathList[i]), out singleFaceInfo);
                            this.Invoke(new Action(delegate
                            {
                                if (singleFaceInfo.faceRect.left == 0 && singleFaceInfo.faceRect.right == 0)
                                {
                                    AppendText(string.Format("{0}号未检测到人脸\r\n", i));
                                }
                                else
                                {
                                    AppendText(string.Format("已提取{0}号人脸特征值，[left:{1},right:{2},top:{3},bottom:{4},orient:{5}]\r\n", i, singleFaceInfo.faceRect.left, singleFaceInfo.faceRect.right, singleFaceInfo.faceRect.top, singleFaceInfo.faceRect.bottom, singleFaceInfo.faceOrient));
                                    imagesFeatureList.Add(feature);
                                }
                            }));
                        }
                        //允许点击按钮
                        Invoke(new Action(delegate
                        {
                            button1.Enabled = true;
                            button2.Enabled = true;
                            button3.Enabled = true;
                            button4.Enabled = true;
                            button4.Enabled = true;
                        }));
                    }));

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //销毁引擎
            int retCode = ASFFunctions.ASFUninitEngine(pImageEngine);
            Console.WriteLine("UninitEngine pImageEngine Result:" + retCode);
            this.Close();
        }


        private void idtb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键  
            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数  
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符  
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            AppendText("清空成功");
            imageLists.Images.Clear();
            imageList.Items.Clear();
            imagesFeatureList.Clear();
            imagePathList.Clear();
            idtb.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(path[0])))
            {
                
                        string url = "https://www.ylesb.com/csimg/pound.php";
                        try
                        {
                            WebClient client = new WebClient();
                            client.Credentials = CredentialCache.DefaultCredentials;
                            client.Headers.Add("Content-Type", "application/form-data");//注意头部必须是form-data
                            string filename = idtb.Text + Path.GetFileName(path[0]);
                            client.QueryString["file_name"] = filename;
                            byte[] fileb = client.UploadFile(new Uri(url), "POST", path[0]);
                            string res = Encoding.UTF8.GetString(fileb);
                        }
                        catch (Exception ex)
                        {
                            AppendText(ex.Message);
                        }

                        DataMysql datasql = new DataMysql();
                        datasql.dataCon();
                        string imgname = "https://www.ylesb.com/csimg/" + idtb.Text + Path.GetFileName(path[0]);
                        string update = "update user set faceimg ='" + imgname + "' where id='" + idtb.Text + "'";
                        if (datasql.sqlExec(update))
                        {
                            AppendText("更新成功，请访问'" + imgname + "'查看是否本人\n");
                            imageLists.Images.Clear();
                            imageList.Items.Clear();
                            imagesFeatureList.Clear();
                            imagePathList.Clear();
                            idtb.Text = "";

                        }
                        else
                        {
                            AppendText("录入失败，网络出现异常，请稍后再试！");
                            imageLists.Images.Clear();
                            imageList.Items.Clear();
                            imagesFeatureList.Clear();
                            imagePathList.Clear();
                            idtb.Text = "";

                        }
            }
            else
            {
                MessageBox.Show("您未选择录入图片，请选择后提交!");
            }

        }

        private void Form_Admin_update_Load(object sender, EventArgs e)
        {
            idtb.Text = "请输入员工数字ID";
            button1.Enabled = false;
        }

        private void idtb_Leave(object sender, EventArgs e)
        {
            if (this.idtb.Text.Length == 0)
            {
                this.idtb.Text = "请输入员工数字ID";
            }

        }

        private void idtb_Enter(object sender, EventArgs e)
        {
            if (this.idtb.Text == "请输入员工数字ID")
            {
                this.idtb.Text = "";
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            lock (locker)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "选择图片";
                openFileDialog.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.png";
                openFileDialog.Multiselect = true;
                openFileDialog.FileName = string.Empty;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    List<string> imagePathListTemp = new List<string>();
                    var numStart = imagePathList.Count;
                    int isGoodImage = 0;

                    //保存图片路径并显示
                    string[] fileNames = openFileDialog.FileNames;
                    path = openFileDialog.FileNames;

                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        imagePathListTemp.Add(fileNames[i]);
                    }



                    //人脸检测以及提取人脸特征
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                    {
                        //禁止点击按钮
                        Invoke(new Action(delegate
                        {
                            button1.Enabled = false;
                            button2.Enabled = false;
                            button3.Enabled = false;
                            button4.Enabled = false;
                        }));

                        //人脸检测和剪裁
                        for (int i = 0; i < imagePathListTemp.Count; i++)
                        {
                            Image image = Image.FromFile(imagePathListTemp[i]);
                            if (image.Width % 4 != 0)
                            {
                                image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
                            }
                            ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, image);

                            if (multiFaceInfo.faceNum > 0)
                            {
                                imagePathList.Add(imagePathListTemp[i]);
                                MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects);
                                image = ImageUtil.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);
                            }
                            else
                            {
                                continue;
                            }

                            this.Invoke(new Action(delegate
                            {
                                if (image == null)
                                {
                                    image = Image.FromFile(imagePathListTemp[i]);
                                }
                                imageLists.Images.Add(imagePathListTemp[i], image);
                                imageList.Items.Add((numStart + isGoodImage) + "号", imagePathListTemp[i]);
                                isGoodImage += 1;
                                image = null;
                            }));
                        }


                        //提取人脸特征
                        for (int i = numStart; i < imagePathList.Count; i++)
                        {
                            ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
                            IntPtr feature = FaceUtil.ExtractFeature(pImageEngine, Image.FromFile(imagePathList[i]), out singleFaceInfo);
                            this.Invoke(new Action(delegate
                            {
                                if (singleFaceInfo.faceRect.left == 0 && singleFaceInfo.faceRect.right == 0)
                                {
                                    AppendText(string.Format("{0}号未检测到人脸\r\n", i));
                                }
                                else
                                {
                                    AppendText(string.Format("已提取{0}号人脸特征值，[left:{1},right:{2},top:{3},bottom:{4},orient:{5}]\r\n", i, singleFaceInfo.faceRect.left, singleFaceInfo.faceRect.right, singleFaceInfo.faceRect.top, singleFaceInfo.faceRect.bottom, singleFaceInfo.faceOrient));
                                    imagesFeatureList.Add(feature);
                                }
                            }));
                        }
                        //允许点击按钮
                        Invoke(new Action(delegate
                        {
                            button1.Enabled = true;
                            button2.Enabled = true;
                            button3.Enabled = true;
                            button4.Enabled = true;
                            button4.Enabled = true;
                        }));
                    }));

                }
            }
        }

        private void idtb_KeyUp(object sender, KeyEventArgs e)
        {
            if (idtb.Text == "请输入员工数字ID")
            {
                MessageBox.Show("您还没有输入员工数字ID");
            }
            else
            {
                DataMysql data = new DataMysql();
                data.dataCon();
                string cmdStr = "Select * from user where id='" + idtb.Text + "'";
                DataSet ds;
                ds = data.getDataSet(cmdStr);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    button1.Enabled = true;
                }
                else { if (!(String.IsNullOrEmpty(idtb.Text)))
                 {
                        MessageBox.Show("未找到此员工信息，请添加！");
                    }
                }
            }
        }
    }
  
    
}
