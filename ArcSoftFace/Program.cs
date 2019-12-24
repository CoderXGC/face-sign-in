using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form_Start());
            Task.Factory.StartNew(() => {
                //更新程序位于主程序安装目录的“UpdateApp”文件夹里面
                string exePath = $@"{AppDomain.CurrentDomain.BaseDirectory}UpdateApp\UpdateApp.exe";
                if (File.Exists(exePath))
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = exePath;
                    p.Start();
                }
            });
        }
    }
}
