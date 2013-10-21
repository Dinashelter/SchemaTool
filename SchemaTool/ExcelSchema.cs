using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace SchemaTool
{
    public class ExcelSchema : Schema
    {
        #region global variables
        private Excel.Application _app;
        private Excel.Workbook _workBook;
        private Excel.Workbooks _workBooks;
        private Excel.Sheets _workSheets;
        private Excel.Worksheet _tableSheet;
        int usedRowCnt;
        int usedColumnCnt;
        #endregion

        public ExcelSchema(Excel.Application app)
        {
            _app = app;
            InitializeExcelVariable();
            InitializeGlobalVariables();
        }

        public Excel.Application App
        {
            get { return _app; }
            set { _app = value; }
        }

        private void InitializeExcelVariable()
        {
            _workBook = _app.ActiveWorkbook;
            _workBooks = _app.Workbooks;
            _workSheets = _app.Worksheets;
            _tableSheet = _workSheets[1];
            usedRowCnt = _tableSheet.UsedRange.Rows.Count;
            usedColumnCnt = _tableSheet.UsedRange.Columns.Count;
        }

        protected override void PrepareData()
        {
            string tableName;
            string cellValue;

            for (int row = 1; row < usedRowCnt; row++)
            {
                cellValue = ((Range)_tableSheet.Cells[row, 1]).Text.ToString();
                //new table
                if (cellValue.ToLower().Contains(Constant.NEWTABLE))
                {
                    tableName = cellValue.ToLower().Split(':')[1].Trim();
                    Table newTable = new Table(tableName, Constant.TABLEACTIVITY_CREATE);
                    newTable.TablePositionInFile.Row = row;
                    newTable.TablePositionInFile.Column = Constant.TABLENAMECOLNUM;

                    AddFieldToTable(newTable);
                    AddIndexToTable(newTable);
                    tableList.Add(newTable);
                }
                //modify table
                else if (cellValue.ToLower().Contains(Constant.MODIFIEDTABLE))
                {
                    tableName = cellValue.ToLower().Split(':')[1].Replace(Constant.CHANGESAREINRED,"").Trim();
                    Table modifyTable = new Table(tableName, Constant.TABLEACTIVITY_MODIFY);
                    modifyTable.TablePositionInFile.Row = row;
                    modifyTable.TablePositionInFile.Column = Constant.TABLENAMECOLNUM;
                    
                    AddFieldToTable(modifyTable);
                    AddIndexToTable(modifyTable);
                    tableList.Add(modifyTable);
                }
                else
                    continue;
            }
        }

        protected override void AddFieldToTable(Table table)
        {
            int targetRow = table.TablePositionInFile.Row;
            string cellValue = "";
            int whileExecuteCnt = 0;

            while (cellValue.ToLower() != Constant.FIELD && whileExecuteCnt < 50)
            {
                cellValue = ((Range)_tableSheet.Cells[targetRow, Constant.FIELDNAMECOLNUM]).Text.ToString();
                targetRow++;
                whileExecuteCnt++;
            }

            whileExecuteCnt = 0;
            while (cellValue != "" && whileExecuteCnt < 50)
            {
                cellValue = ((Range)_tableSheet.Cells[targetRow, Constant.FIELDNAMECOLNUM]).Text.ToString();
                if (cellValue != "")
                {
                    string fieldName = ((Range)_tableSheet.Cells[targetRow, Constant.FIELDNAMECOLNUM]).Text.ToString();
                    string fieldFormat = ((Range)_tableSheet.Cells[targetRow, Constant.FIELDFORMATCOLNUM]).Text.ToString();
                    bool fieldIsMandatory = ((Range)_tableSheet.Cells[targetRow, Constant.FIELDMANDITORYCOLNUM]).Text.ToString() == "M" ? true : false;
                    string fieldInitialValue = ((Range)_tableSheet.Cells[targetRow, Constant.FIELDINITIALCOLNUM]).Text.ToString();
                    string fieldLabel = ((Range)_tableSheet.Cells[targetRow, Constant.FIELDLABELCOLNUM]).Text.ToString();

                    Field newField = new Field(table.TableName, fieldName, fieldFormat, fieldInitialValue, fieldLabel, fieldIsMandatory);
                    newField.FieldPositionInFile.Row = targetRow;
                    newField.FieldPositionInFile.Column = Constant.FIELDNAMECOLNUM;

                    fieldList.Add(newField);
                }
                targetRow++;
                whileExecuteCnt++;
            }
        }

        protected override void AddIndexToTable(Table table)
        {
            int targetRow = table.TablePositionInFile.Row;
            string cellValue = "";
            int whileExecuteCnt = 0;

            while (cellValue.ToLower() != Constant.INDICES && whileExecuteCnt < 50)
            {
                cellValue = ((Range)_tableSheet.Cells[targetRow, 2]).Text.ToString();
                targetRow++;
                whileExecuteCnt++;
            }

            whileExecuteCnt = 0;
            while (cellValue != "" && whileExecuteCnt < 50)
            {
                cellValue = ((Range)_tableSheet.Cells[targetRow, Constant.INDEXNAMECOLNUM]).Text.ToString();
                if (cellValue != "")
                {
                    string indexName = cellValue.Split(':')[0];
                    string indexTableName = table.TableName;

                    Index newIndex = new Index(indexTableName, indexName);
                    newIndex.IndexArea = Constant.INDEXAREA_FIN;

                    if (indexName.ToLower() == Constant.PRIMINDEX.ToLower())
                        newIndex.IndexIsPrimary = true;
                    else
                        newIndex.IndexIsPrimary = false;

                    if (indexName.ToLower() == Constant.UNIQINDEX.ToLower() ||
                        indexName.ToLower() == Constant.PRIMINDEX.ToLower())
                        newIndex.IndexIsUnique = true;
                    else
                        newIndex.IndexIsUnique = false;

                    string indexArea = cellValue.Split(':')[1].Trim();

                    for (int indexFieldCnt = 1; indexFieldCnt < indexArea.Split('+').Length - 1; indexFieldCnt++)
                    {
                        IndexField newIndexField = new IndexField(indexArea.Split('+')[indexFieldCnt].ToString().Trim(), true);
                        newIndex.IndexFieldList.Add(newIndexField);
                    }
                    newIndex.IndexPositionInFile.Row = targetRow;
                    newIndex.IndexPositionInFile.Column = Constant.INDEXNAMECOLNUM;

                    indexList.Add(newIndex);
                }
                targetRow++;
                whileExecuteCnt++;
            }
        }

        #region Close and Kill Excel Process
        public void ExitExcel()
        {
            //Release Com Object
            if (_workSheets != null)
                ReleaseComObject(_workSheets);
            if (_workBook != null)
            {
                _workBook.Close();
                ReleaseComObject(_workBook);
            }
            if (_workBooks != null)
                ReleaseComObject(_workBooks);

            if (_app != null)
            {
                _app.Quit();
                ReleaseComObject(_app);
            }
        }

        private void ReleaseComObject(Object o)
        {
            try { System.Runtime.InteropServices.Marshal.ReleaseComObject(o); }
            catch { }
            finally { o = null; }
        }
      
        #endregion
    }
}
