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
            this.lb_newsReader = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsB_manageRssURL = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_newsReader
            // 
            this.lb_newsReader.FormattingEnabled = true;
            this.lb_newsReader.Location = new System.Drawing.Point(43, 77);
            this.lb_newsReader.Name = "lb_newsReader";
            this.lb_newsReader.Size = new System.Drawing.Size(520, 277);
            this.lb_newsReader.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsB_manageRssURL});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
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
            // RssFeedReaderApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lb_newsReader);
            this.Name = "RssFeedReaderApp";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_newsReader;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsB_manageRssURL;
    }
}

