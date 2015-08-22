using ComponentFactory.Krypton.Toolkit;
using ObAuto.BL;
using ObAuto.Om;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ObAuto
{
    /// <summary>
    /// ObAuto 登录窗体
    /// </summary>
    public partial class FormLogin : KryptonForm
    {
        string userName = "";
        string pwd = "";

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void kryBtnSubmit_Click(object sender, EventArgs e)
        {
            userName = this.kryTbxUserName.Text.Trim();
            pwd = this.kryTbxPwd.Text.Trim();

            if (string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(pwd))
            {
                MessageBox.Show("请输入正确的用户名和密码！");
                return;
            }

            if (VerifyUser())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool VerifyUser()
        {
            bool verify = false;
            UserBL ubl = new UserBL();
            User user = ubl.GetUser(userName);

            if (user != null)
            {
                if (pwd.Equals(user.Pwd))
                {
                    System.Diagnostics.Debug.WriteLine("用户[" + userName + "]登录成功！");
                    verify = true;
                }
                else
                {
                    MessageBox.Show("用户[" + userName + "]密码错误！");
                    this.kryTbxPwd.Clear();
                    this.kryTbxPwd.Focus();
                }
            }
            else
            {
                MessageBox.Show("用户[" + userName + "]不存在！");
                this.kryTbxUserName.Clear();
                this.kryTbxPwd.Clear();
                this.kryTbxUserName.Focus();
            }
            return verify;
        }
    }
}
