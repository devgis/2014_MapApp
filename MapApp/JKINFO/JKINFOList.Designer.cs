namespace MapApp.JKINFO
{
    partial class JKINFOList
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MAPINFO_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FZH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NDH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SBLX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADDR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BJZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DDZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MAPINFO_ID,
            this.FZH,
            this.DKH,
            this.NDH,
            this.SBLX,
            this.DW,
            this.ADDR,
            this.SJ,
            this.ZT,
            this.BJZ,
            this.DDZ});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(626, 266);
            this.dataGridView1.TabIndex = 0;
            // 
            // MAPINFO_ID
            // 
            this.MAPINFO_ID.DataPropertyName = "MAPINFO_ID";
            this.MAPINFO_ID.HeaderText = "MAPINFO_ID";
            this.MAPINFO_ID.Name = "MAPINFO_ID";
            this.MAPINFO_ID.ReadOnly = true;
            this.MAPINFO_ID.Visible = false;
            // 
            // FZH
            // 
            this.FZH.DataPropertyName = "FZH";
            this.FZH.HeaderText = "分站号";
            this.FZH.Name = "FZH";
            this.FZH.ReadOnly = true;
            // 
            // DKH
            // 
            this.DKH.DataPropertyName = "DKH";
            this.DKH.HeaderText = "端口号";
            this.DKH.Name = "DKH";
            this.DKH.ReadOnly = true;
            // 
            // NDH
            // 
            this.NDH.DataPropertyName = "NDH";
            this.NDH.HeaderText = "内点号";
            this.NDH.Name = "NDH";
            this.NDH.ReadOnly = true;
            // 
            // SBLX
            // 
            this.SBLX.DataPropertyName = "SBLX";
            this.SBLX.HeaderText = "设备类型";
            this.SBLX.Name = "SBLX";
            this.SBLX.ReadOnly = true;
            // 
            // DW
            // 
            this.DW.DataPropertyName = "DW";
            this.DW.HeaderText = "单位";
            this.DW.Name = "DW";
            this.DW.ReadOnly = true;
            // 
            // ADDR
            // 
            this.ADDR.DataPropertyName = "ADDR";
            this.ADDR.HeaderText = "安装地点";
            this.ADDR.Name = "ADDR";
            this.ADDR.ReadOnly = true;
            // 
            // SJ
            // 
            this.SJ.DataPropertyName = "SJ";
            this.SJ.HeaderText = "实时数据";
            this.SJ.Name = "SJ";
            this.SJ.ReadOnly = true;
            // 
            // ZT
            // 
            this.ZT.DataPropertyName = "ZT";
            this.ZT.HeaderText = "状态";
            this.ZT.Name = "ZT";
            this.ZT.ReadOnly = true;
            // 
            // BJZ
            // 
            this.BJZ.DataPropertyName = "BJZ";
            this.BJZ.HeaderText = "报警值设定";
            this.BJZ.Name = "BJZ";
            this.BJZ.ReadOnly = true;
            // 
            // DDZ
            // 
            this.DDZ.DataPropertyName = "DDZ";
            this.DDZ.HeaderText = "断电值设定";
            this.DDZ.Name = "DDZ";
            this.DDZ.ReadOnly = true;
            // 
            // JKINFOList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 266);
            this.Controls.Add(this.dataGridView1);
            this.Name = "JKINFOList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "列表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.JKINFOList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAPINFO_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FZH;
        private System.Windows.Forms.DataGridViewTextBoxColumn DKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn NDH;
        private System.Windows.Forms.DataGridViewTextBoxColumn SBLX;
        private System.Windows.Forms.DataGridViewTextBoxColumn DW;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADDR;
        private System.Windows.Forms.DataGridViewTextBoxColumn SJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZT;
        private System.Windows.Forms.DataGridViewTextBoxColumn BJZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn DDZ;
    }
}