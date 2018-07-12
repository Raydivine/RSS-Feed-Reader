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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsB_manageRssURL = new System.Windows.Forms.ToolStripButton();
            this.girdView_News = new System.Windows.Forms.DataGridView();
            this.colDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColHeadLines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLink = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ColStory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRemove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.girdView_News)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsB_manageRssURL});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(656, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsB_manageRssURL
            // 
            this.tsB_manageRssURL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsB_manageRssURL.Image = ((System.Drawing.Image)(resources.GetObject("tsB_manageRssURL.Image")));
            this.tsB_manageRssURL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsB_manageRssURL.Name = "tsB_manageRssURL";
            this.tsB_manageRssURL.Size = new System.Drawing.Size(54, 22);
            this.tsB_manageRssURL.Text = "RSS URL";
            this.tsB_manageRssURL.Click += new System.EventHandler(this.tsB_manageRssURL_Click);
            // 
            // girdView_News
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.girdView_News.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.girdView_News.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.girdView_News.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDateTime,
            this.ColHeadLines,
            this.ColLink,
            this.ColStory,
            this.ColRemove});
            this.girdView_News.Location = new System.Drawing.Point(21, 28);
            this.girdView_News.Name = "girdView_News";
            this.girdView_News.Size = new System.Drawing.Size(605, 333);
            this.girdView_News.TabIndex = 2;
            this.girdView_News.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.girdView_News_CellContentClick);
            // 
            // colDateTime
            // 
            this.colDateTime.HeaderText = "DateTime";
            this.colDateTime.Name = "colDateTime";
            this.colDateTime.ReadOnly = true;
            // 
            // ColHeadLines
            // 
            this.ColHeadLines.HeaderText = "HeadLines";
            this.ColHeadLines.Name = "ColHeadLines";
            this.ColHeadLines.ReadOnly = true;
            // 
            // ColLink
            // 
            this.ColLink.HeaderText = "Link";
            this.ColLink.Name = "ColLink";
            this.ColLink.ReadOnly = true;
            this.ColLink.Text = "";
            // 
            // ColStory
            // 
            this.ColStory.HeaderText = "Story";
            this.ColStory.Name = "ColStory";
            this.ColStory.ReadOnly = true;
            // 
            // ColRemove
            // 
            this.ColRemove.HeaderText = "Remove";
            this.ColRemove.Name = "ColRemove";
            this.ColRemove.ReadOnly = true;
            this.ColRemove.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColRemove.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // RssFeedReaderApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 437);
            this.Controls.Add(this.girdView_News);
            this.Controls.Add(this.toolStrip1);
            this.Name = "RssFeedReaderApp";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.girdView_News)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsB_manageRssURL;
        private System.Windows.Forms.DataGridView girdView_News;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColHeadLines;
        private System.Windows.Forms.DataGridViewLinkColumn ColLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStory;
        private System.Windows.Forms.DataGridViewButtonColumn ColRemove;
    }
}

