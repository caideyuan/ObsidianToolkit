namespace ObAuto
{
    partial class FormMng
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
            this.kryMngMng = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.krySplitCntMng = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.kryDgvMng = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.bindSrcUsers = new System.Windows.Forms.BindingSource(this.components);
            this.bindSrcDbConns = new System.Windows.Forms.BindingSource(this.components);
            this.bindSrcSqls = new System.Windows.Forms.BindingSource(this.components);
            this.kryCbxSel = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryBtnSel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryBtnAdd = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryBtnEdit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryBtnDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryBtnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.krySplitCntMng)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.krySplitCntMng.Panel1)).BeginInit();
            this.krySplitCntMng.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.krySplitCntMng.Panel2)).BeginInit();
            this.krySplitCntMng.Panel2.SuspendLayout();
            this.krySplitCntMng.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryDgvMng)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindSrcUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindSrcDbConns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindSrcSqls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryCbxSel)).BeginInit();
            this.SuspendLayout();
            // 
            // kryMngMng
            // 
            this.kryMngMng.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Custom;
            // 
            // krySplitCntMng
            // 
            this.krySplitCntMng.Cursor = System.Windows.Forms.Cursors.Default;
            this.krySplitCntMng.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krySplitCntMng.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.krySplitCntMng.IsSplitterFixed = true;
            this.krySplitCntMng.Location = new System.Drawing.Point(0, 0);
            this.krySplitCntMng.Name = "krySplitCntMng";
            this.krySplitCntMng.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // krySplitCntMng.Panel1
            // 
            this.krySplitCntMng.Panel1.Controls.Add(this.kryBtnSubmit);
            this.krySplitCntMng.Panel1.Controls.Add(this.kryBtnDelete);
            this.krySplitCntMng.Panel1.Controls.Add(this.kryBtnEdit);
            this.krySplitCntMng.Panel1.Controls.Add(this.kryBtnAdd);
            this.krySplitCntMng.Panel1.Controls.Add(this.kryBtnSel);
            this.krySplitCntMng.Panel1.Controls.Add(this.kryCbxSel);
            // 
            // krySplitCntMng.Panel2
            // 
            this.krySplitCntMng.Panel2.Controls.Add(this.kryDgvMng);
            this.krySplitCntMng.Size = new System.Drawing.Size(752, 364);
            this.krySplitCntMng.SplitterDistance = 52;
            this.krySplitCntMng.TabIndex = 0;
            // 
            // kryDgvMng
            // 
            this.kryDgvMng.AllowUserToAddRows = false;
            this.kryDgvMng.AllowUserToDeleteRows = false;
            this.kryDgvMng.AllowUserToResizeRows = false;
            this.kryDgvMng.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kryDgvMng.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryDgvMng.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.kryDgvMng.Location = new System.Drawing.Point(0, 0);
            this.kryDgvMng.MultiSelect = false;
            this.kryDgvMng.Name = "kryDgvMng";
            this.kryDgvMng.ReadOnly = true;
            this.kryDgvMng.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.kryDgvMng.RowTemplate.Height = 23;
            this.kryDgvMng.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.kryDgvMng.Size = new System.Drawing.Size(752, 307);
            this.kryDgvMng.TabIndex = 0;
            // 
            // kryCbxSel
            // 
            this.kryCbxSel.DropDownWidth = 103;
            this.kryCbxSel.Items.AddRange(new object[] {
            "1.User",
            "2.DbConn",
            "3.SqlSentence"});
            this.kryCbxSel.Location = new System.Drawing.Point(3, 3);
            this.kryCbxSel.Name = "kryCbxSel";
            this.kryCbxSel.Size = new System.Drawing.Size(103, 21);
            this.kryCbxSel.TabIndex = 3;
            this.kryCbxSel.Text = "配置表";
            // 
            // kryBtnSel
            // 
            this.kryBtnSel.Location = new System.Drawing.Point(112, 3);
            this.kryBtnSel.Name = "kryBtnSel";
            this.kryBtnSel.Size = new System.Drawing.Size(59, 44);
            this.kryBtnSel.TabIndex = 4;
            this.kryBtnSel.Values.Text = "选择";
            this.kryBtnSel.Click += new System.EventHandler(this.kryBtnSel_Click);
            // 
            // kryBtnAdd
            // 
            this.kryBtnAdd.Location = new System.Drawing.Point(177, 3);
            this.kryBtnAdd.Name = "kryBtnAdd";
            this.kryBtnAdd.Size = new System.Drawing.Size(59, 44);
            this.kryBtnAdd.TabIndex = 4;
            this.kryBtnAdd.Values.Text = "添加";
            this.kryBtnAdd.Click += new System.EventHandler(this.kryBtnSel_Click);
            // 
            // kryBtnEdit
            // 
            this.kryBtnEdit.Location = new System.Drawing.Point(242, 3);
            this.kryBtnEdit.Name = "kryBtnEdit";
            this.kryBtnEdit.Size = new System.Drawing.Size(59, 44);
            this.kryBtnEdit.TabIndex = 4;
            this.kryBtnEdit.Values.Text = "编辑";
            this.kryBtnEdit.Click += new System.EventHandler(this.kryBtnSel_Click);
            // 
            // kryBtnDelete
            // 
            this.kryBtnDelete.Location = new System.Drawing.Point(307, 3);
            this.kryBtnDelete.Name = "kryBtnDelete";
            this.kryBtnDelete.Size = new System.Drawing.Size(59, 44);
            this.kryBtnDelete.TabIndex = 4;
            this.kryBtnDelete.Values.Text = "删除";
            this.kryBtnDelete.Click += new System.EventHandler(this.kryBtnSel_Click);
            // 
            // kryBtnSubmit
            // 
            this.kryBtnSubmit.Location = new System.Drawing.Point(372, 3);
            this.kryBtnSubmit.Name = "kryBtnSubmit";
            this.kryBtnSubmit.Size = new System.Drawing.Size(59, 44);
            this.kryBtnSubmit.TabIndex = 4;
            this.kryBtnSubmit.Values.Text = "确定";
            this.kryBtnSubmit.Click += new System.EventHandler(this.kryBtnSel_Click);
            // 
            // FormMng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 364);
            this.Controls.Add(this.krySplitCntMng);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMng";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormMng";
            this.TextExtra = "ObAuto Toolkit";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormMng_Load);
            ((System.ComponentModel.ISupportInitialize)(this.krySplitCntMng.Panel1)).EndInit();
            this.krySplitCntMng.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.krySplitCntMng.Panel2)).EndInit();
            this.krySplitCntMng.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.krySplitCntMng)).EndInit();
            this.krySplitCntMng.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryDgvMng)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindSrcUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindSrcDbConns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindSrcSqls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryCbxSel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryMngMng;
        public ComponentFactory.Krypton.Toolkit.KryptonPalette kryTheme;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer krySplitCntMng;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView kryDgvMng;
        private System.Windows.Forms.BindingSource bindSrcUsers;
        private System.Windows.Forms.BindingSource bindSrcDbConns;
        private System.Windows.Forms.BindingSource bindSrcSqls;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox kryCbxSel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryBtnSel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryBtnAdd;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryBtnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryBtnDelete;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryBtnEdit;
    }
}