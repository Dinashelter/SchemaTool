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

namespace SchemaTool
{
    public partial class SchemaToolMainForm : Form
    {
        #region global variables
        private ExcelSchema excelSchema;

        #endregion

        #region initialization
        public SchemaToolMainForm()
        {
            InitializeComponent();      
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

            excelSchema = new ExcelSchema((Excel.Application)(oApplication));
            EnableMenu();
        }

        private void checkSchemaExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            excelSchema.CheckSchemaExcel();
        }
        #endregion

        #region Other Method
        
        private void DisableMenu()
        {
            this.checkSchemaExcelToolStripMenuItem.Enabled = false;
        }

        private void EnableMenu()
        {
            this.checkSchemaExcelToolStripMenuItem.Enabled = true;
        }

        public void KillExcel()
        {
            //Find the named process and terminate it
            foreach (Process winProc in Process.GetProcesses())
            {

                //use equals for the task in case we kill
                //a wrong process
                if (winProc.ProcessName.ToLower().Equals("excel"))
                {
                    winProc.Kill();
                }
            }
        }


        #endregion

      
    }
}
