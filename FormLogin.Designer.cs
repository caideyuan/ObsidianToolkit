namespace ObAuto
{
    partial class FormLogin
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
            this.krypMngLogin = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryTheme = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.kryPnlLogin = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryBtnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryTbxPwd = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryTbxUserName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.pbxPwd = new System.Windows.Forms.PictureBox();
            this.pbxUserName = new System.Windows.Forms.PictureBox();
            this.pbxLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryPnlLogin)).BeginInit();
            this.kryPnlLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPwd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // krypMngLogin
            // 
            this.krypMngLogin.GlobalPalette = this.kryTheme;
            this.krypMngLogin.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Custom;
            // 
            // kryTheme
            // 
            this.kryTheme.BasePaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            // 
            // kryPnlLogin
            // 
            this.kryPnlLogin.Controls.Add(this.kryBtnSubmit);
            this.kryPnlLogin.Controls.Add(this.kryTbxPwd);
            this.kryPnlLogin.Controls.Add(this.kryTbxUserName);
            this.kryPnlLogin.Controls.Add(this.pbxPwd);
            this.kryPnlLogin.Controls.Add(this.pbxUserName);
            this.kryPnlLogin.Controls.Add(this.pbxLogo);
            this.kryPnlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryPnlLogin.Location = new System.Drawing.Point(0, 0);
            this.kryPnlLogin.Name = "kryPnlLogin";
            this.kryPnlLogin.Size = new System.Drawing.Size(284, 244);
            this.kryPnlLogin.TabIndex = 0;
            // 
            // kryBtnSubmit
            // 
            this.kryBtnSubmit.Location = new System.Drawing.Point(102, 200);
            this.kryBtnSubmit.Name = "kryBtnSubmit";
            this.kryBtnSubmit.Size = new System.Drawing.Size(121, 25);
            this.kryBtnSubmit.TabIndex = 7;
            this.kryBtnSubmit.Values.Text = "Login";
            this.kryBtnSubmit.Click += new System.EventHandler(this.kryBtnSubmit_Click);
            // 
            // kryTbxPwd
            // 
            this.kryTbxPwd.Location = new System.Drawing.Point(102, 156);
            this.kryTbxPwd.MaxLength = 32;
            this.kryTbxPwd.Name = "kryTbxPwd";
            this.kryTbxPwd.PasswordChar = '●';
            this.kryTbxPwd.Size = new System.Drawing.Size(123, 20);
            this.kryTbxPwd.TabIndex = 5;
            this.kryTbxPwd.UseSystemPasswordChar = true;
            // 
            // kryTbxUserName
            // 
            this.kryTbxUserName.Location = new System.Drawing.Point(102, 112);
            this.kryTbxUserName.MaxLength = 32;
            this.kryTbxUserName.Name = "kryTbxUserName";
            this.kryTbxUserName.Size = new System.Drawing.Size(123, 20);
            this.kryTbxUserName.TabIndex = 4;
            // 
            // pbxPwd
            // 
            this.pbxPwd.BackColor = System.Drawing.Color.Transparent;
            this.pbxPwd.Image = global::ObAuto.Properties.Resources.pwd;
            this.pbxPwd.Location = new System.Drawing.Point(58, 156);
            this.pbxPwd.Name = "pbxPwd";
            this.pbxPwd.Size = new System.Drawing.Size(26, 26);
            this.pbxPwd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxPwd.TabIndex = 2;
            this.pbxPwd.TabStop = false;
            // 
            // pbxUserName
            // 
            this.pbxUserName.BackColor = System.Drawing.Color.Transparent;
            this.pbxUserName.Image = global::ObAuto.Properties.Resources.login;
            this.pbxUserName.Location = new System.Drawing.Point(58, 112);
            this.pbxUserName.Name = "pbxUserName";
            this.pbxUserName.Size = new System.Drawing.Size(26, 26);
            this.pbxUserName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxUserName.TabIndex = 1;
            this.pbxUserName.TabStop = false;
            // 
            // pbxLogo
            // 
            this.pbxLogo.BackColor = System.Drawing.Color.Transparent;
            this.pbxLogo.Image = global::ObAuto.Properties.Resources.Obsidian;
            this.pbxLogo.Location = new System.Drawing.Point(58, 9);
            this.pbxLogo.Name = "pbxLogo";
            this.pbxLogo.Size = new System.Drawing.Size(167, 91);
            this.pbxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLogo.TabIndex = 0;
            this.pbxLogo.TabStop = false;
            // 
            // FormLogin
            // 
            this.AcceptButton = this.kryBtnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 244);
            this.Controls.Add(this.kryPnlLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ObAuto Login";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryPnlLogin)).EndInit();
            this.kryPnlLogin.ResumeLayout(false);
            this.kryPnlLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPwd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager krypMngLogin;
        public ComponentFactory.Krypton.Toolkit.KryptonPalette kryTheme;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryPnlLogin;
        private System.Windows.Forms.PictureBox pbxLogo;
        private System.Windows.Forms.PictureBox pbxUserName;
        private System.Windows.Forms.PictureBox pbxPwd;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox kryTbxUserName;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox kryTbxPwd;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryBtnSubmit;
    }
}