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
            this.gv_News = new System.Windows.Forms.DataGridView();
            this.colDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColHeadLines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLink = new System.Windows.Forms.DataGridViewLinkColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_News)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsB_manageRssURL});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(848, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsB_manageRssURL
            // 
            this.tsB_manageRssURL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsB_manageRssURL.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tsB_manageRssURL.Image = ((System.Drawing.Image)(resources.GetObject("tsB_manageRssURL.Image")));
            this.tsB_manageRssURL.ImageTransparentColor = System.Drawing.Color.Indigo;
            this.tsB_manageRssURL.Name = "tsB_manageRssURL";
            this.tsB_manageRssURL.Size = new System.Drawing.Size(70, 24);
            this.tsB_manageRssURL.Text = "RSS URL";
            this.tsB_manageRssURL.Click += new System.EventHandler(this.tsB_manageRssURL_Click);
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
            this.gv_News.Location = new System.Drawing.Point(0, 27);
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
            this.gv_News.Size = new System.Drawing.Size(848, 130);
            this.gv_News.TabIndex = 2;
            this.gv_News.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.girdView_News_CellContentClick);
            // 
            // colDateTime
            // 
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDateTime.DefaultCellStyle = dataGridViewCellStyle7;
            this.colDateTime.HeaderText = "DateTime";
            this.colDateTime.Name = "colDateTime";
            this.colDateTime.ReadOnly = true;
            // 
            // ColHeadLines
            // 
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColHeadLines.DefaultCellStyle = dataGridViewCellStyle8;
            this.ColHeadLines.HeaderText = "HeadLines";
            this.ColHeadLines.Name = "ColHeadLines";
            this.ColHeadLines.ReadOnly = true;
            this.ColHeadLines.Width = 500;
            // 
            // ColLink
            // 
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColLink.DefaultCellStyle = dataGridViewCellStyle9;
            this.ColLink.HeaderText = "Link";
            this.ColLink.Name = "ColLink";
            this.ColLink.ReadOnly = true;
            this.ColLink.Text = "";
            this.ColLink.Width = 200;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 178);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(848, 199);
            this.dataGridView1.TabIndex = 3;
            // 
            // RssFeedReaderApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 496);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.gv_News);
            this.Controls.Add(this.toolStrip1);
            this.Name = "RssFeedReaderApp";
            this.Text = "RSS Feed Reader";
            this.Load += new System.EventHandler(this.RssFeedReaderApp_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_News)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsB_manageRssURL;
        private System.Windows.Forms.DataGridView gv_News;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColHeadLines;
        private System.Windows.Forms.DataGridViewLinkColumn ColLink;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

