using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ObAuto
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

            //登录窗体判断
            FormLogin flogin = new FormLogin();
            flogin.ShowDialog();
            if (flogin.DialogResult == DialogResult.OK)
            {
                MainForm mf = new MainForm();
                Application.Run(mf);
            }
        }
    }
}
