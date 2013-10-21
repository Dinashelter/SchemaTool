namespace SchemaTool
{
    partial class SchemaCheckResultForm
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
            this.ResultTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultTextBox.Location = new System.Drawing.Point(0, 0);
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.Size = new System.Drawing.Size(410, 395);
            this.ResultTextBox.TabIndex = 0;
            this.ResultTextBox.Text = "";
            // 
            // SchemaCheckResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 395);
            this.Controls.Add(this.ResultTextBox);
            this.Name = "SchemaCheckResultForm";
            this.Text = "Schema Check Result";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox ResultTextBox;
    }
}