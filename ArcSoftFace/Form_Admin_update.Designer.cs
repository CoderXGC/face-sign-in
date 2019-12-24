namespace ArcSoftFace
{
    partial class Form_Admin_update
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Admin_update));
            this.logBox = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.idtb = new System.Windows.Forms.TextBox();
            this.imageLists = new System.Windows.Forms.ImageList(this.components);
            this.imageList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.Color.White;
            this.logBox.Location = new System.Drawing.Point(80, 300);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(641, 88);
            this.logBox.TabIndex = 69;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(466, 118);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(42, 23);
            this.button4.TabIndex = 68;
            this.button4.Text = "添加";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(435, 269);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(42, 23);
            this.button3.TabIndex = 67;
            this.button3.Text = "返回";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(360, 269);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(42, 23);
            this.button2.TabIndex = 66;
            this.button2.Text = "清空";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(291, 269);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 23);
            this.button1.TabIndex = 65;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 63;
            this.label4.Text = "员工ID：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 62;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(258, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 59;
            this.label1.Text = "人脸信息：";
            // 
            // idtb
            // 
            this.idtb.Location = new System.Drawing.Point(329, 47);
            this.idtb.Name = "idtb";
            this.idtb.Size = new System.Drawing.Size(128, 21);
            this.idtb.TabIndex = 61;
            this.idtb.Enter += new System.EventHandler(this.idtb_Enter);
            this.idtb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.idtb_KeyPress);
            this.idtb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.idtb_KeyUp);
            this.idtb.Leave += new System.EventHandler(this.idtb_Leave);
            // 
            // imageLists
            // 
            this.imageLists.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageLists.ImageStream")));
            this.imageLists.TransparentColor = System.Drawing.Color.Transparent;
            this.imageLists.Images.SetKeyName(0, "alai_152784032385984494.jpg");
            // 
            // imageList
            // 
            this.imageList.LargeImageList = this.imageLists;
            this.imageList.Location = new System.Drawing.Point(329, 100);
            this.imageList.Name = "imageList";
            this.imageList.Size = new System.Drawing.Size(128, 115);
            this.imageList.TabIndex = 70;
            this.imageList.UseCompatibleStateImageBehavior = false;
            // 
            // Form_Admin_update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.imageList);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.idtb);
            this.Controls.Add(this.label1);
            this.Name = "Form_Admin_update";
            this.Text = "更新员工信息";
            this.Load += new System.EventHandler(this.Form_Admin_update_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox idtb;
        private System.Windows.Forms.ImageList imageLists;
        private System.Windows.Forms.ListView imageList;
    }
}