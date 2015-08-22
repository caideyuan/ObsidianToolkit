using ComponentFactory.Krypton.Toolkit;
using ObAuto.BL;
using ObAuto.Om;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ObAuto
{
    /// <summary>
    /// ObAuto 管理窗体
    /// </summary>
    public partial class FormMng : KryptonForm
    {
        public FormMng()
        {
            InitializeComponent();
        }

        private void FormMng_Load(object sender, EventArgs e)
        {

        }

        private void kryBtnSel_Click(object sender, EventArgs e)
        {
            int selIndex = this.kryCbxSel.SelectedIndex;
            switch (selIndex)
            {
                case 0:
                    {
                        UserBL bl = new UserBL();
                        List<User> users = bl.GetUsers();
                        this.bindSrcUsers.DataSource = users;
                        kryDgvMng.DataSource = this.bindSrcUsers;
                        break;
                    }
                case 1:
                    {
                        DbConnBL bl = new DbConnBL();
                        List<DbConn> conns = bl.GetDbConns();
                        this.bindSrcDbConns.DataSource = conns;
                        kryDgvMng.DataSource = this.bindSrcDbConns;
                        break;
                    }
                case 2:
                    {
                        SqlSentenceBL bl = new SqlSentenceBL();
                        List<SqlSentence> sqls = bl.GetSqls();
                        this.bindSrcSqls.DataSource = sqls;
                        kryDgvMng.DataSource = this.bindSrcSqls;
                        break;
                    }
                default:
                    KryptonMessageBox.Show("请选择配置表！");
                    break;
            }
        }
    }
}
