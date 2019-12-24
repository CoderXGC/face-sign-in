namespace ArcSoftFace
{
    partial class Form_Sign
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows 窗体设计器生成的代码
  
        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.videoSource = new AForge.Controls.VideoSourcePlayer();
            this.SuspendLayout();
            // 
            // videoSource
            // 
            this.videoSource.Location = new System.Drawing.Point(127, 76);
            this.videoSource.Name = "videoSource";
            this.videoSource.Size = new System.Drawing.Size(494, 362);
            this.videoSource.TabIndex = 39;
            this.videoSource.Text = "videoSource";
            this.videoSource.VideoSource = null;
            this.videoSource.Paint += new System.Windows.Forms.PaintEventHandler(this.videoSource_Paint);
            // 
            // Form_Sign
            // 
            this.ClientSize = new System.Drawing.Size(769, 563);
            this.Controls.Add(this.videoSource);
            this.Name = "Form_Sign";
            this.Text = "欢迎使用人脸考勤系统";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Sign_FormClosed);
            this.Load += new System.EventHandler(this.Form_User_Load);
            this.ResumeLayout(false);

        }





        #endregion
        private System.Windows.Forms.ImageList imageLists;
        private System.Windows.Forms.ListView imageList;
        private AForge.Controls.VideoSourcePlayer videoSource;
    }
  

        }