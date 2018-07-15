namespace RssFeedReaderApp
{
    partial class RssFeedReaderApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RssFeedReaderApp));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsB_manageRssURL = new System.Windows.Forms.ToolStripButton();
            this.tsm_newPeriod = new System.Windows.Forms.ToolStripSplitButton();
            this.tsm_1day = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_2day = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_3day = new System.Windows.Forms.ToolStripMenuItem();
            this.tsb_help = new System.Windows.Forms.ToolStripButton();
            this.gv_News = new System.Windows.Forms.DataGridView();
            this.colDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColHeadLines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLink = new System.Windows.Forms.DataGridViewLinkColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_News)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsB_manageRssURL,
            this.tsm_newPeriod,
            this.tsb_help});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(848, 26);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsB_manageRssURL
            // 
            this.tsB_manageRssURL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tsB_manageRssURL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsB_manageRssURL.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsB_manageRssURL.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tsB_manageRssURL.Image = ((System.Drawing.Image)(resources.GetObject("tsB_manageRssURL.Image")));
            this.tsB_manageRssURL.ImageTransparentColor = System.Drawing.Color.Indigo;
            this.tsB_manageRssURL.Name = "tsB_manageRssURL";
            this.tsB_manageRssURL.Size = new System.Drawing.Size(124, 23);
            this.tsB_manageRssURL.Text = "Register RSS URL";
            this.tsB_manageRssURL.Click += new System.EventHandler(this.tsB_manageRssURL_Click);
            // 
            // tsm_newPeriod
            // 
            this.tsm_newPeriod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.tsm_newPeriod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsm_newPeriod.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_1day,
            this.tsm_2day,
            this.tsm_3day});
            this.tsm_newPeriod.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsm_newPeriod.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsm_newPeriod.Image = ((System.Drawing.Image)(resources.GetObject("tsm_newPeriod.Image")));
            this.tsm_newPeriod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsm_newPeriod.Name = "tsm_newPeriod";
            this.tsm_newPeriod.Size = new System.Drawing.Size(185, 23);
            this.tsm_newPeriod.Text = "News Expire Days Frame";
            // 
            // tsm_1day
            // 
            this.tsm_1day.Name = "tsm_1day";
            this.tsm_1day.Size = new System.Drawing.Size(120, 24);
            this.tsm_1day.Text = "1 day";
            this.tsm_1day.Click += new System.EventHandler(this.tsm_1day_Click);
            // 
            // tsm_2day
            // 
            this.tsm_2day.Name = "tsm_2day";
            this.tsm_2day.Size = new System.Drawing.Size(120, 24);
            this.tsm_2day.Text = "2 days";
            this.tsm_2day.Click += new System.EventHandler(this.tsm_2day_Click);
            // 
            // tsm_3day
            // 
            this.tsm_3day.Name = "tsm_3day";
            this.tsm_3day.Size = new System.Drawing.Size(120, 24);
            this.tsm_3day.Text = "3 days";
            this.tsm_3day.Click += new System.EventHandler(this.tsm_3day_Click);
            // 
            // tsb_help
            // 
            this.tsb_help.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsb_help.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tsb_help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_help.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_help.Image = ((System.Drawing.Image)(resources.GetObject("tsb_help.Image")));
            this.tsb_help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_help.Name = "tsb_help";
            this.tsb_help.Size = new System.Drawing.Size(43, 23);
            this.tsb_help.Text = "Help";
            this.tsb_help.Click += new System.EventHandler(this.tsb_help_Click);
            // 
            // gv_News
            // 
            this.gv_News.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gv_News.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gv_News.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_News.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDateTime,
            this.ColHeadLines,
            this.ColLink});
            this.gv_News.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gv_News.Location = new System.Drawing.Point(0, 26);
            this.gv_News.Name = "gv_News";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gv_News.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.gv_News.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_News.Size = new System.Drawing.Size(848, 470);
            this.gv_News.TabIndex = 2;
            this.gv_News.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.girdView_News_CellContentClick);
            // 
            // colDateTime
            // 
            this.colDateTime.DataPropertyName = "updateTime";
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDateTime.DefaultCellStyle = dataGridViewCellStyle7;
            this.colDateTime.HeaderText = "PublishDate";
            this.colDateTime.Name = "colDateTime";
            this.colDateTime.ReadOnly = true;
            this.colDateTime.Width = 140;
            // 
            // ColHeadLines
            // 
            this.ColHeadLines.DataPropertyName = "title";
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColHeadLines.DefaultCellStyle = dataGridViewCellStyle8;
            this.ColHeadLines.HeaderText = "HeadLines";
            this.ColHeadLines.Name = "ColHeadLines";
            this.ColHeadLines.ReadOnly = true;
            this.ColHeadLines.Width = 500;
            // 
            // ColLink
            // 
            this.ColLink.DataPropertyName = "link";
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColLink.DefaultCellStyle = dataGridViewCellStyle9;
            this.ColLink.HeaderText = "Link";
            this.ColLink.Name = "ColLink";
            this.ColLink.ReadOnly = true;
            this.ColLink.Text = "";
            this.ColLink.Width = 200;
            // 
            // RssFeedReaderApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 496);
            this.Controls.Add(this.gv_News);
            this.Controls.Add(this.toolStrip1);
            this.Name = "RssFeedReaderApp";
            this.Text = "RSS Feed Reader";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RssFeedReaderApp_FormClosed);
            this.Load += new System.EventHandler(this.RssFeedReaderApp_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_News)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsB_manageRssURL;
        private System.Windows.Forms.DataGridView gv_News;
        private System.Windows.Forms.ToolStripSplitButton tsm_newPeriod;
        private System.Windows.Forms.ToolStripMenuItem tsm_1day;
        private System.Windows.Forms.ToolStripMenuItem tsm_2day;
        private System.Windows.Forms.ToolStripMenuItem tsm_3day;
        private System.Windows.Forms.ToolStripButton tsb_help;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColHeadLines;
        private System.Windows.Forms.DataGridViewLinkColumn ColLink;
    }
}

