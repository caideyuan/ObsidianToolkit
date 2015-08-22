using ComponentFactory.Krypton.Toolkit;
using ObAuto.BL;
using ObAuto.Om;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ObAuto
{
    /// <summary>
    /// ObAuto 主界面窗体
    /// </summary>
    public partial class MainForm : KryptonForm
    {
        string dbcs = "";
        string dbName = "";
        public Dictionary<string, string> SqlDict = new Dictionary<string, string>();
        AutoObject ao = null;

        public MainForm()
        {
            InitializeComponent();
            InitMainForm();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (GetMsDatabase())    //获取用户数据库
            {
                this.kryCbxDb.SelectedIndexChanged += kryCbxDb_SelectedIndexChanged;

                this.kryRTbxModel.TextChanged += kryRTbxModel_TextChanged;
                this.kryRTbxQuery.TextChanged += kryRTbxQuery_TextChanged;
                
                this.kryRTbxDAL.TextChanged += kryRTbxDAL_TextChanged;
                this.kryRTbxBLL.TextChanged += kryRTbxBLL_TextChanged;
                this.kryRTbxSDK.TextChanged += kryRTbxSDK_TextChanged;
                this.kryRTbxHtml.TextChanged += kryRTbxHtml_TextChanged;
                this.kryRTbxJs.TextChanged += kryRTbxJs_TextChanged;
                this.kryRTbxSql.TextChanged += kryRTbxSql_TextChanged;

                int dbCnt = this.kryCbxDb.Items.Count;
                if (dbCnt <= 0)
                {
                    string msg = "找不到用户数据库，请确认数据库连接配置是否正确！";
                    KryptonMessageBox.Show(msg, "警告"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                this.Dispose();
            }
        }

        private void kryBtnOpenMng_Click(object sender, EventArgs e)
        {
            FormMng fm = new FormMng();
            fm.ShowDialog();
        }

        void kryCbxDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            dbName = this.kryCbxDb.SelectedValue.ToString();
            GetMsTable(dbName); //获取数据库的用户表
        }

        #region [RichTextBox:TextChanged] TODO实现RichTextBox高亮代码功能（保留）
        void kryRTbxModel_TextChanged(object sender, EventArgs e)
        {
            //string keywords = "(auto|double|int|struct|break|else|long|switch|case|enum|register|typedef|char|extern|return|union|const|float|short|unsigned|continue|for|signed|void|default|goto|sizeof|volatile|do|if|static|while)";
            //Regex rex = new Regex(keywords);
            //MatchCollection mc = rex.Matches(kryRTbxModel.Text);
            //int start = kryRTbxModel.SelectionStart;
            //foreach (Match m in mc)
            //{
            //    int startIndex = m.Index;
            //    int endIndex = m.Length;
            //    kryRTbxModel.Select(startIndex, endIndex);
            //    kryRTbxModel.SelectionColor = Color.Blue;
            //    kryRTbxModel.SelectionStart = start;
            //    kryRTbxModel.SelectionColor = Color.Black;
            //}

            //AutoHighLight ahl = new AutoHighLight();
            //ahl.SetWords();
            //ahl.SetRtbTextColor(kryRTbxModel);
        }
        void kryRTbxQuery_TextChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        void kryRTbxDAL_TextChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        void kryRTbxBLL_TextChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        void kryRTbxSDK_TextChanged(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
        }
        void kryRTbxHtml_TextChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        void kryRTbxJs_TextChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        void kryRTbxSql_TextChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        #endregion

        private void kryBtnDo_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (string.IsNullOrWhiteSpace(dbName))
            {
                msg = "请选择数据库";
                KryptonMessageBox.Show(msg, "警告"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int tbIndex = this.kryLbxDbTb.SelectedIndex;
            if (tbIndex >= 0)
            {
                string tbName = this.kryLbxDbTb.SelectedValue.ToString();
                SetAutoObject(tbName);

                DataTable dt = GetMsField();
                int rowCnt = dt.Rows.Count;
                if (rowCnt <= 0)
                {
                    KryptonMessageBox.Show("获取不到表[" + ao.TbName + "]的数据库字段！", "警告"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ClearRichTextBox();

                CreateAutoCode(dt);
            }
            else
            {
                KryptonMessageBox.Show("请先选择一个表！", "警告"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kryBtnSave_Click(object sender, EventArgs e)
        {
            //保存代码到指定目录
        }

        private void InitMainForm()
        {
            GetMsSqlDBCS();
            MsSqlHelper.ConnectionString(dbcs); //获取数据库连接串
            GetSqlSentence();   //获取读取数据库，表，字段的SQL语句
        }

        private void ClearRichTextBox()
        {
            this.kryRTbxBLL.Clear();
            this.kryRTbxDAL.Clear();
            this.kryRTbxHtml.Clear();
            this.kryRTbxJs.Clear();
            this.kryRTbxModel.Clear();
            this.kryRTbxQuery.Clear();
            this.kryRTbxSDK.Clear();
            this.kryRTbxSql.Clear();
        }

        private void CreateAutoCode(DataTable dt)
        {
            AutoCoder ac = new AutoCoder(ao, dt);
            
            this.kryRTbxModel.Text = ac.CreateModelCode();  //1.Model
            this.kryRTbxQuery.Text = ac.CreateQueryCode();  //1.Query
            this.kryRTbxPermit.Text = ac.CreatePermitCode();//1.Permit

            this.kryRTbxDAL.Text = ac.CreateDALCode();      //2.DAL
            this.kryRTbxBLL.Text = ac.CreateBLLCode();      //2.BLL
            
            this.kryRTbxHtml.Text = ac.CreateASPXCode();    //3.ASPX
            this.kryRTbxJs.Text = ac.CreateJSCode();        //3.JS

            this.kryRTbxSDK.Text = ac.CreateSDKCode();      //4.SDK
            this.kryRTbxCtrl.Text = ac.CreateCtrlCode();    //4.Ctrl
            this.kryRTbxView.Text = ac.CreateViewCode();    //4.View

            this.kryRTbxSql.Text = ac.CreateSQLCode();          //5.SQL
            this.kryRTbxDescri.Text = ac.CreateDescription();   //6.Descri
        }
        #region 辅助方法

        void SetAutoObject(string tbName)
        {
            ao = new AutoObject();
            ao.ModelNS = ConfigurationManager.AppSettings["ModelNS"];
            ao.QueryNS = ConfigurationManager.AppSettings["QueryNS"];
            ao.PermitNS = ConfigurationManager.AppSettings["PermitNS"];

            ao.DalNS = ConfigurationManager.AppSettings["DALNS"];
            ao.BllNS = ConfigurationManager.AppSettings["BLLNS"];

            ao.SdkNS = ConfigurationManager.AppSettings["SDKNS"];
            ao.CtrlNS = ConfigurationManager.AppSettings["CtrlNS"];
            ao.ViewNS = ConfigurationManager.AppSettings["ViewNS"];

            ao.BllBaseCls = ConfigurationManager.AppSettings["BLLBaseCls"];
            ao.SdkBaseCls = ConfigurationManager.AppSettings["SDKBaseCls"];
            ao.CtrlBaseCls = ConfigurationManager.AppSettings["CtrlBaseCls"];
            //
            ao.DbName = dbName;
            ao.TbName = tbName;
            ao.ClsName = StringUtil.FirstUpper(tbName, '_');
        }

        void GetMsSqlDBCS()
        {
            dbcs =  ConfigurationManager.ConnectionStrings["DBCS"] != null 
                ? ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString
                : "";
            if (string.IsNullOrWhiteSpace(dbcs))
            {
                //从SQLite读取
                DbConnBL bl = new DbConnBL();
                string connString = "data source={0};initial catalog={1};user id={2};Password={3}";
                DbConn conn = bl.GetDbConn("DEFAULT");
                if (conn != null)
                    dbcs = string.Format(connString, conn.DataSource, conn.Catalog, conn.UserId, conn.Password);

                if (string.IsNullOrWhiteSpace(dbcs))
                    KryptonMessageBox.Show("没有配置master数据库连接字符串！\r\n请配置后重启应用程序！"
                    , "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void GetSqlSentence()
        {
            SqlSentenceBL bl = new SqlSentenceBL();
            List<SqlSentence> sqls = bl.GetSqls();
            if (sqls.Count > 0)
            {
                foreach (SqlSentence sql in sqls)
                {
                    SqlDict.Add(sql.SqlName, sql.SqlValue);
                }
            }
        }

        bool GetMsDatabase()
        {
            try
            {
                AutoUtil au = new AutoUtil();
                string sql = SqlDict["DBS"];
                DataTable dt = au.GetDatabases(sql);

                this.kryCbxDb.DataSource = dt;
                this.kryCbxDb.DisplayMember = "name";
                this.kryCbxDb.ValueMember = "name";
                this.kryCbxDb.SelectedIndex = 0;
                return true;
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("连接不到master数据库信息！\r\n1)请确认数据库连接配置是否正确！\r\n2)请确认SQLServer服务是否已启用！"
                    , "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        void GetMsTable(string dbName)
        {
            if (string.IsNullOrWhiteSpace(dbName))
            {
                string msg = "请选择数据库";
                KryptonMessageBox.Show(msg, "警告"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AutoUtil au = new AutoUtil();
            string sql = SqlDict["TBS"];
            DataTable dt = au.GetTables(dbName, sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                this.kryLbxDbTb.DataSource = dt;
                this.kryLbxDbTb.DisplayMember = "name";
                this.kryLbxDbTb.ValueMember = "name";
                this.kryLbxDbTb.SelectedIndex = -1;

                string tip = "共有用户表{0}个.";
                this.kryLblDbTip.Text = string.Format(tip, dt.Rows.Count.ToString());
            }
            else
            {
                KryptonMessageBox.Show("获取不到当前数据库的用户表！"
                    , "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        DataTable GetMsField()
        {
            AutoUtil au = new AutoUtil();
            string sql = SqlDict["FDS"];
            DataTable fdDT = au.GetFields(ao.DbName, ao.TbName, sql);
            return fdDT == null ? new DataTable() : fdDT;
        }
        #endregion
    }
}
