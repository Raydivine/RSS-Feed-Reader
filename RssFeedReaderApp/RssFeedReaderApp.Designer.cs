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
            this.lb_newsReader = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lb_newsReader
            // 
            this.lb_newsReader.FormattingEnabled = true;
            this.lb_newsReader.Location = new System.Drawing.Point(44, 55);
            this.lb_newsReader.Name = "lb_newsReader";
            this.lb_newsReader.Size = new System.Drawing.Size(520, 277);
            this.lb_newsReader.TabIndex = 0;
            // 
            // RssFeedReaderApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lb_newsReader);
            this.Name = "RssFeedReaderApp";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_newsReader;
    }
}

