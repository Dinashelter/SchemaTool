namespace SchemaTool
{
    partial class SchemaToolMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaToolMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openExcelFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opendfFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.functionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkSchemaExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkSchemadfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axWebBrowser = new AxSHDocVw.AxWebBrowser();
            this.dfSchemaTextBox = new System.Windows.Forms.RichTextBox();
            this.savedfFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsdfFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.functionToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openExcelFileToolStripMenuItem,
            this.opendfFileToolStripMenuItem,
            this.savedfFileToolStripMenuItem,
            this.saveAsdfFileToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // openExcelFileToolStripMenuItem
            // 
            this.openExcelFileToolStripMenuItem.Name = "openExcelFileToolStripMenuItem";
            resources.ApplyResources(this.openExcelFileToolStripMenuItem, "openExcelFileToolStripMenuItem");
            this.openExcelFileToolStripMenuItem.Click += new System.EventHandler(this.openExcelFileToolStripMenuItem_Click);
            // 
            // opendfFileToolStripMenuItem
            // 
            this.opendfFileToolStripMenuItem.Name = "opendfFileToolStripMenuItem";
            resources.ApplyResources(this.opendfFileToolStripMenuItem, "opendfFileToolStripMenuItem");
            this.opendfFileToolStripMenuItem.Click += new System.EventHandler(this.opendfFileToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // functionToolStripMenuItem
            // 
            this.functionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkSchemaExcelToolStripMenuItem,
            this.checkSchemadfToolStripMenuItem});
            this.functionToolStripMenuItem.Name = "functionToolStripMenuItem";
            resources.ApplyResources(this.functionToolStripMenuItem, "functionToolStripMenuItem");
            // 
            // checkSchemaExcelToolStripMenuItem
            // 
            this.checkSchemaExcelToolStripMenuItem.Name = "checkSchemaExcelToolStripMenuItem";
            resources.ApplyResources(this.checkSchemaExcelToolStripMenuItem, "checkSchemaExcelToolStripMenuItem");
            this.checkSchemaExcelToolStripMenuItem.Click += new System.EventHandler(this.checkSchemaExcelToolStripMenuItem_Click);
            // 
            // checkSchemadfToolStripMenuItem
            // 
            this.checkSchemadfToolStripMenuItem.Name = "checkSchemadfToolStripMenuItem";
            resources.ApplyResources(this.checkSchemadfToolStripMenuItem, "checkSchemadfToolStripMenuItem");
            this.checkSchemadfToolStripMenuItem.Click += new System.EventHandler(this.checkSchemadfToolStripMenuItem_Click);
            // 
            // axWebBrowser
            // 
            resources.ApplyResources(this.axWebBrowser, "axWebBrowser");
            this.axWebBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser.OcxState")));
            this.axWebBrowser.NavigateComplete2 += new AxSHDocVw.DWebBrowserEvents2_NavigateComplete2EventHandler(this.axWebBrowser_NavigateComplete2);
            // 
            // dfSchemaTextBox
            // 
            resources.ApplyResources(this.dfSchemaTextBox, "dfSchemaTextBox");
            this.dfSchemaTextBox.Name = "dfSchemaTextBox";
            // 
            // savedfFileToolStripMenuItem
            // 
            this.savedfFileToolStripMenuItem.Name = "savedfFileToolStripMenuItem";
            resources.ApplyResources(this.savedfFileToolStripMenuItem, "savedfFileToolStripMenuItem");
            this.savedfFileToolStripMenuItem.Click += new System.EventHandler(this.savedfFileToolStripMenuItem_Click);
            // 
            // saveAsdfFileToolStripMenuItem
            // 
            this.saveAsdfFileToolStripMenuItem.Name = "saveAsdfFileToolStripMenuItem";
            resources.ApplyResources(this.saveAsdfFileToolStripMenuItem, "saveAsdfFileToolStripMenuItem");
            this.saveAsdfFileToolStripMenuItem.Click += new System.EventHandler(this.saveAsdfFileToolStripMenuItem_Click);
            // 
            // SchemaToolMainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dfSchemaTextBox);
            this.Controls.Add(this.axWebBrowser);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SchemaToolMainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem functionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openExcelFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private AxSHDocVw.AxWebBrowser axWebBrowser;
        private System.Windows.Forms.ToolStripMenuItem checkSchemaExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkSchemadfToolStripMenuItem;
        private System.Windows.Forms.RichTextBox dfSchemaTextBox;
        private System.Windows.Forms.ToolStripMenuItem opendfFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savedfFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsdfFileToolStripMenuItem;
    }
}

