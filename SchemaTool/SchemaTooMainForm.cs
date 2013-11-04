using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace SchemaTool
{
    public partial class SchemaToolMainForm : Form
    {
        #region global variables
        private ExcelSchema excelSchema;
        private DfSchema dfSchema;

        #endregion

        #region initialization
        public SchemaToolMainForm()
        {
            InitializeComponent();
            SetControls();
            KillExcel();
            DisableMenu();
        }
        #endregion

        #region event
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axWebBrowser.Dispose();
            if (excelSchema != null)
                excelSchema.ExitExcel();
            System.Windows.Forms.Application.Exit();
        }

        private void openExcelFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            filePath = openFileDialog.FileName;

            this.axWebBrowser.Visible = true;
            this.dfSchemaTextBox.Visible = false;
            this.checkSchemadfToolStripMenuItem.Enabled = false;
            //Show excel in a special browse
            axWebBrowser.Navigate(filePath);
        }

        private void axWebBrowser_NavigateComplete2(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event e)
        {
            //Add Microsoft Excel Tool Bar
            axWebBrowser.ExecWB(SHDocVw.OLECMDID.OLECMDID_HIDETOOLBARS, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER);

            //Assign public variables
            Object o = e.pDisp;
            Object oDocument = o.GetType().InvokeMember("Document", BindingFlags.GetProperty, null, o, null);
            Object oApplication = o.GetType().InvokeMember("Application", BindingFlags.GetProperty, null, oDocument, null);

            Excel.Application app = (Excel.Application)(oApplication);

            //Basic check if worksheet has three sheets
            if (app.Worksheets.Count != 3)
            {
                MessageBox.Show(Constant.SCHEMAFORMATISNOTCORRECT);
                return;
            }

            excelSchema = new ExcelSchema(app);
            this.checkSchemaExcelToolStripMenuItem.Enabled = true;
        }

        private void checkSchemaExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            excelSchema.CheckSchema();
        }

        private void checkSchemadfToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            dfSchema.CheckSchema();
        }

        private void opendfFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "df Files|*.df";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            this.dfSchemaTextBox.Visible = true;
            this.axWebBrowser.Visible = false;
            this.checkSchemaExcelToolStripMenuItem.Enabled = false;

            if (excelSchema != null)
                excelSchema.ExitExcel();

            string dfSchemaText;
            StreamReader streamReader = new StreamReader(openFileDialog.FileName, false);
            dfSchemaText = streamReader.ReadToEnd().ToString();
            streamReader.Close();
            this.dfSchemaTextBox.Text = dfSchemaText;
            dfSchema = new DfSchema(this.dfSchemaTextBox.Text.ToString(), openFileDialog.FileName);

            this.checkSchemadfToolStripMenuItem.Enabled = true;
        }

        private void savedfFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter(dfSchema.DfSchemaTextFilePath, false);
            streamWriter.Write(this.dfSchemaTextBox.Text);
            streamWriter.Close();
        }

        private void saveAsdfFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "df Files|*.df";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false);
            streamWriter.Write(this.dfSchemaTextBox.Text);
            streamWriter.Close();
        }

        #endregion

        #region Other Method        
        private void DisableMenu()
        {
            this.checkSchemaExcelToolStripMenuItem.Enabled = false;
            this.checkSchemadfToolStripMenuItem.Enabled = false;
        }

        private void KillExcel()
        {
            //Find the named process and terminate it
            foreach (Process winProc in Process.GetProcesses())
            {

                //use equals for the task in case we kill a wrong process
                if (winProc.ProcessName.ToLower().Equals("excel"))
                {
                    winProc.Kill();
                }
            }
        }

        private void SetControls()
        {
            this.Text = Constant.SCHEMATOOL;
            this.dfSchemaTextBox.Visible = false;
        }

        #endregion
       
    }
}
