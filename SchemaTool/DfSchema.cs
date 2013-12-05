using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace SchemaTool
{
    public class DfSchema : Schema
    {
        private string _dfSchemaText;
        private string _dfSchemaTextFilePath;

        #region public method
        public DfSchema(string dfSchemaText, string dfSchemaTextFilePath)
        {
            _dfSchemaText = dfSchemaText;
            _dfSchemaTextFilePath = dfSchemaTextFilePath;
            InitializeGlobalVariables();
        }

        public string DfSchemaText
        {
            get { return _dfSchemaText; }
            set { _dfSchemaText = value; }
        }

        public string DfSchemaTextFilePath
        {
            get { return _dfSchemaTextFilePath; }
            set { _dfSchemaTextFilePath = value; }
        }

        public void ConvertToExcel()
        {
            //open module excel
            Excel.Application schemaExcel = new Excel.Application();
            string path = System.IO.Directory.GetCurrentDirectory();
            schemaExcel.Visible = true;
            Excel.Workbook schemaBook = schemaExcel.Workbooks.Open(path + "\\Module.xlsx");
            Excel.Worksheet schemaSheet = schemaExcel.Worksheets[1];
            Excel.Worksheet tableRelationSheet = schemaExcel.Worksheets[3];

            ConvertSchemaToExcel(schemaSheet);
            ConvertTableRelationToExcel(tableRelationSheet);
        }
        #endregion

        #region protected method
        protected override void PrepareData()
        {
            //Convert string to stream
            byte[] dfSchemaByte = Encoding.ASCII.GetBytes(_dfSchemaText);
            MemoryStream dfSchemaStream = new MemoryStream(dfSchemaByte);    
            StreamReader streamReader = new StreamReader(dfSchemaStream);
            int lineCnt = 0;
            string dfSchemaLine;
            string dfLineType = "";
            string[] dfSchemaLineArray;
            string currTableName = "";
            string currFieldName = "";
            string currIndexName = "";
            Table table = new Table();
            Field field = new Field();
            Index index = new Index();

            while ((dfSchemaLine = streamReader.ReadLine()) != null)
            {
                lineCnt++;

                dfSchemaLineArray = dfSchemaLine.Trim().Split(' ');
                string tableActivity = "";

                //new block
                if (dfSchemaLineArray[0].ToString().ToLower() == Constant.ADD)
                {
                    //Table block
                    if (dfSchemaLineArray[1].ToString().ToLower() == Constant.TABLE)
                    {
                        currTableName = dfSchemaLineArray[2].ToString().Replace("\"", "");
                        currFieldName = "";
                        currIndexName = "";

                        tableActivity = Constant.TABLEACTIVITY_CREATE;
                        table = new Table(currTableName, tableActivity);
                        table.TablePositionInFile.Row = lineCnt;
                        dfLineType = Constant.TABLE;
                    }
                    //Field block
                    else if (dfSchemaLineArray[1].ToString().ToLower() == Constant.FIELD.ToLower())
                    {
                        if (currTableName != dfSchemaLineArray[4].ToString().Replace("\"", ""))
                        {
                            tableActivity = Constant.TABLEACTIVITY_MODIFY;
                            currTableName = dfSchemaLineArray[4].ToString().Replace("\"", "");
                            table = new Table(currTableName, tableActivity);
                            tableList.Add(table);
                        }
                        currFieldName = dfSchemaLineArray[2].ToString().Replace("\"", "");
                        currIndexName = "";

                        field = new Field(currTableName, currFieldName);
                        field.FieldType = dfSchemaLineArray[6].ToString();
                        field.FieldPositionInFile.Row = lineCnt;

                        dfLineType = Constant.FIELD;
                    }
                    //Index block
                    else if (dfSchemaLineArray[1].ToString().ToLower() == Constant.INDEX)
                    {
                        if (currTableName != dfSchemaLineArray[4].ToString().Replace("\"", ""))
                        {
                            tableActivity = Constant.TABLEACTIVITY_MODIFY;
                            currTableName = dfSchemaLineArray[4].ToString().Replace("\"", "");
                            table = new Table(currTableName, tableActivity);
                            tableList.Add(table);
                        }
                        currFieldName = "";
                        currIndexName = dfSchemaLineArray[2].ToString().Replace("\"", "");

                        index = new Index(currTableName, currIndexName);
                        index.IndexPositionInFile.Row = lineCnt;
                        dfLineType = Constant.INDEX;
                    }
                    else return;
                }
                else
                {
                    //Table
                    if (currTableName != "" &&
                        currFieldName == "" &&
                        currIndexName == "")
                    {
                        AddTableInfo(table, dfSchemaLine);
                        dfLineType = Constant.TABLE;
                    }
                    //Field
                    else if (currTableName != "" &&
                        currFieldName != "" &&
                        currIndexName == "")
                    {
                        AddFieldInfo(field, dfSchemaLine);
                        dfLineType = Constant.FIELD;
                    }
                    //Index
                    else if (currTableName != "" &&
                        currFieldName == "" &&
                        currIndexName != "")
                    {
                        AddIndexInfo(index, dfSchemaLineArray);
                        dfLineType = Constant.INDEX;
                    }
                    else return;
                }


                if (dfSchemaLine.Trim() == "" || streamReader.EndOfStream)
                {
                    switch (dfLineType)
                    {
                        case Constant.TABLE:
                            {
                                tableList.Add(table);
                                break;
                            }
                        case Constant.FIELD:
                            {
                                if (field.FieldName.ToLower().StartsWith(Constant.CUSTOMFIELD))
                                    table.TableHasCustomField = true;
                                else if (field.FieldName.ToLower().StartsWith(Constant.LASTMODIFIEDFIELD))
                                    table.TableHasLastModifiedField = true;
                                else if (field.FieldName.ToLower().StartsWith(Constant.QADFIELD))
                                    table.TableHasQADField = true;
                                else
                                    fieldList.Add(field);
                                break;
                            }
                        case Constant.INDEX:
                            {
                                indexList.Add(index);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }
        }
        #endregion

        #region private method
        private void AddTableInfo(Table table, string tableInfo)
        {
            string[] tableInfoArray = tableInfo.Trim().Split(' ');
            switch (tableInfoArray[0].ToString().ToLower())
            {
                case Constant.TABLEAREA:
                    {
                        table.TableArea = tableInfoArray[1].ToString().Replace("\"", "");
                        break;
                    }
                case Constant.TABLEDESCRIPTION:
                    {
                        table.TableDescription = tableInfo.Trim().Split('\"')[1];
                        break;
                    }
                case Constant.TABLEFROZEN:
                    {
                        table.TableIsFrozen = true;
                        break;
                    }
                case Constant.TABLEDUMPNAME:
                    {
                        table.TableDumpName = tableInfoArray[1].ToString().Replace("\"", "");
                        break;
                    }
                default :
                    {
                        break;
                    }
            }
        }

        private void AddFieldInfo(Field field, string fieldInfo)
        {
            string[] fieldInfoArray = fieldInfo.Trim().Split(' ');
            switch (fieldInfoArray[0].ToString().ToLower())
            {
                case Constant.FIELDFORMAT:
                    {
                        field.FieldFormat = fieldInfoArray[1].ToString().Replace("\"", "");
                        break;
                    }
                case Constant.FIELDINITIAL:
                    {
                        field.FieldInitialValue = fieldInfoArray[1].ToString().Replace("\"", "");
                        break;
                    }
                case Constant.FIELDLABEL:
                    {
                        field.FieldLabel = fieldInfo.Trim().Split('\"')[1];
                        break;
                    }
                case Constant.FIELDPOSITION:
                    {
                        field.FieldPosition = Int32.Parse(fieldInfoArray[1].ToString());
                        break;
                    }
                case Constant.FIELDMAXWIDTH:
                    {
                        field.FieldMaxWidth = Int32.Parse(fieldInfoArray[1].ToString());
                        break;
                    }
                case Constant.FIELDORDER:
                    {
                        field.FieldOrder = Int32.Parse(fieldInfoArray[1].ToString());
                        break;
                    }
                case Constant.FIELDMANDATORY:
                    {
                        field.FieldIsMandatory = true;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void AddIndexInfo(Index index, string[] indexInfo)
        {
            if (index.IndexName.ToLower() == Constant.PRIMINDEX)
                index.IndexIsPrimary = true;

            switch (indexInfo[0].ToString().ToLower())
            {
                case Constant.INDEXAREA:
                    {
                        index.IndexArea = indexInfo[1].ToString().Replace("\"", "");
                        break;
                    }
                case Constant.INDEXUNIQUE:
                    { 
                        index.IndexIsUnique = true;                                                
                        break;
                    }
                case Constant.INDEXFIELD:
                    {
                        string indexFieldName = indexInfo[1].ToString().Replace("\"", "");
                        bool indexAscending = false;
                        if (indexInfo[2].ToString().ToLower() == Constant.INDEXASCENDING) 
                            indexAscending = true;
                        IndexField indexField = new IndexField(indexFieldName, indexAscending);
                        index.IndexFieldList.Add(indexField);
                        break;
                    }             
                default:
                    {
                        break;
                    }
            }
        }

        private void ConvertSchemaToExcel(Worksheet schemaSheet)
        {
            int lineCnt = 1;

            //add new table
            for (int tableNum = 0; tableNum < tableList.Count; tableNum++)
            {
                if (tableList[tableNum].TableActivity == Constant.TABLEACTIVITY_CREATE)
                {
                    Table table = tableList[tableNum];
                    //New Table
                    lineCnt++;
                    schemaSheet.Cells[lineCnt, Constant.TABLENAMECOLNUM] = Constant.NEWTABLE + ": " + table.TableName;
                    schemaSheet.Cells[lineCnt, Constant.TABLENAMECOLNUM].Font.Bold = true;
                    //Caption
                    lineCnt++;
                    schemaSheet.Cells[lineCnt, Constant.FIELDNAMECOLNUM] = Constant.FIELD;
                    schemaSheet.Cells[lineCnt, Constant.FIELDFORMATCOLUMN] = Constant.FORMATDOMAIN;
                    schemaSheet.Cells[lineCnt, Constant.FIELDMANDITORYCOLNUM] = Constant.FLAGS;
                    schemaSheet.Cells[lineCnt, Constant.FIELDINITIALCOLNUM] = Constant.INITIAL;
                    schemaSheet.Cells[lineCnt, Constant.FIELDLABELCOLNUM] = Constant.SIDELABEL;
                    schemaSheet.Cells[lineCnt, 8] = Constant.COLLABEL;
                    schemaSheet.Cells[lineCnt, 9] = Constant.COMMENTS;
                    Range blackRange = schemaSheet.get_Range(((char)67) + lineCnt.ToString() + ":" + ((char)73) + lineCnt.ToString());
                    blackRange.Font.Bold = true;
                    //Field lines
                    for (int fieldNum = 0; fieldNum < fieldList.Count; fieldNum++)
                    {
                        Field field = fieldList[fieldNum];
                        if (field.FieldTableName == table.TableName)
                        {
                            lineCnt++;
                            WriteFieldToExcel(schemaSheet, lineCnt, field);
                        }
                    }

                    lineCnt += 2;
                    schemaSheet.Cells[lineCnt, 2] = Constant.INDICES;
                    schemaSheet.Cells[lineCnt, 2].Font.Bold = true;
                    //Index lines
                    for (int indexNum = 0; indexNum < indexList.Count; indexNum++)
                    {
                        Index index = indexList[indexNum];
                        if (index.IndexTableName == table.TableName)
                        {
                            lineCnt++;
                            WriteIndexToExcel(schemaSheet, lineCnt, index);
                        }
                    }

                    //Special Fields
                    lineCnt++;
                    schemaSheet.Cells[lineCnt, 2] = Constant.SPECIALFIELDS;
                    schemaSheet.Cells[lineCnt, 2].Font.Bold = true;
                    lineCnt++;
                    schemaSheet.Cells[lineCnt, 3] = Constant.INCLUDECUSTOMFIELDS;
                    schemaSheet.Cells[lineCnt, 6] = table.TableHasCustomField ? Constant.YES : Constant.NO;
                    lineCnt++;
                    schemaSheet.Cells[lineCnt, 3] = Constant.INCLUDELASTMODIFYFIELDS;
                    schemaSheet.Cells[lineCnt, 6] = table.TableHasLastModifiedField ? Constant.YES : Constant.NO;
                    lineCnt++;
                    schemaSheet.Cells[lineCnt, 3] = Constant.INCLUDEQADFIELDS;
                    schemaSheet.Cells[lineCnt, 6] = table.TableHasQADField ? Constant.YES : Constant.NO;
                    lineCnt++;
                }
            }
            //add modified table
            for (int tableNum = 0; tableNum < tableList.Count; tableNum++)
            {
                if (tableList[tableNum].TableActivity == Constant.TABLEACTIVITY_MODIFY)
                {
                    Table table = tableList[tableNum];
                    //Modified Table
                    lineCnt++;
                    schemaSheet.Cells[lineCnt, Constant.TABLENAMECOLNUM] = Constant.MODIFIEDTABLE + ": " + table.TableName
                        + " " + Constant.CHANGESAREINRED;
                    schemaSheet.Cells[lineCnt, Constant.TABLENAMECOLNUM].Font.Bold = true;
                    //Caption
                    lineCnt++;
                    schemaSheet.Cells[lineCnt, Constant.FIELDNAMECOLNUM] = Constant.FIELD;
                    schemaSheet.Cells[lineCnt, Constant.FIELDFORMATCOLUMN] = Constant.FORMATDOMAIN;
                    schemaSheet.Cells[lineCnt, Constant.FIELDMANDITORYCOLNUM] = Constant.FLAGS;
                    schemaSheet.Cells[lineCnt, Constant.FIELDINITIALCOLNUM] = Constant.INITIAL;
                    schemaSheet.Cells[lineCnt, Constant.FIELDLABELCOLNUM] = Constant.SIDELABEL;
                    schemaSheet.Cells[lineCnt, 8] = Constant.COLLABEL;
                    schemaSheet.Cells[lineCnt, 9] = Constant.COMMENTS;
                    Range blackRange = schemaSheet.get_Range(((char)67) + lineCnt.ToString() + ":" + ((char)73) + lineCnt.ToString());
                    blackRange.Font.Bold = true;

                    int startLineNum = lineCnt + 1;
                    //Field lines
                    for (int fieldNum = 0; fieldNum < fieldList.Count; fieldNum++)
                    {
                        Field field = fieldList[fieldNum];
                        if (field.FieldTableName == table.TableName)
                        {
                            lineCnt++;
                            WriteFieldToExcel(schemaSheet, lineCnt, field);
                        }
                    }
                    int endLineNum = lineCnt;

                    Range redRange = schemaSheet.get_Range(((char)67) + startLineNum.ToString() + ":" + ((char)73) + endLineNum.ToString());
                    redRange.Font.Color = System.Drawing.Color.Red;
                }
            }

            //Notes
            lineCnt++;
            schemaSheet.Cells[lineCnt, 2] = Constant.NOTES;
            schemaSheet.Cells[lineCnt, 2].Font.Bold = true;
            lineCnt++;
            schemaSheet.Cells[lineCnt, 3] = Constant.NOTESTEXT;
            schemaSheet.Cells[lineCnt, 3].WrapText = true;
            schemaSheet.Cells[lineCnt, 3].RowHeight = 15;       
        }

        private void ConvertTableRelationToExcel(Worksheet tableRelationSheet)
        {
            int lineCnt = 0;

            for (int fieldNum = 0; fieldNum < fieldList.Count; fieldNum++)
            {
                if (fieldList[fieldNum].FieldName.Contains("_ID") &&
                    fieldList[fieldNum].FieldName != (fieldList[fieldNum].FieldTableName + "_ID"))
                {
                    Field field = fieldList[fieldNum];

                    //Table relation name
                    lineCnt++;
                    tableRelationSheet.Cells[lineCnt, 1] = Constant.NEWRELATION + ": " +
                                                           field.FieldName.ToUpper().Replace("_ID", "") +
                                                           "IN" + field.FieldTableName.ToUpper();
                    tableRelationSheet.Cells[lineCnt, 1].Font.Bold = true;

                    //Table Relation caption
                    lineCnt++;
                    tableRelationSheet.Cells[lineCnt, 2] = Constant.MULTIPLICITY;
                    tableRelationSheet.Cells[lineCnt, 3] = Constant.PRIMARY;
                    tableRelationSheet.Cells[lineCnt, 4] = Constant.PARENTMAND;
                    tableRelationSheet.Cells[lineCnt, 5] = Constant.DELETECONSTRAINT;
                    tableRelationSheet.Rows[lineCnt].Font.Bold = true;
                    tableRelationSheet.Columns[2].ColumnWidth = 10;
                    tableRelationSheet.Columns[3].ColumnWidth = 10;
                    tableRelationSheet.Columns[4].ColumnWidth = 15;
                    tableRelationSheet.Columns[5].ColumnWidth = 20;

                    //Table relation value
                    lineCnt++;
                    tableRelationSheet.Cells[lineCnt, 2] = Constant.ONETON;
                    tableRelationSheet.Cells[lineCnt, 3] = Constant.NO;
                    tableRelationSheet.Cells[lineCnt, 4] = field.FieldIsMandatory ? Constant.YES : Constant.NO;
                    tableRelationSheet.Cells[lineCnt, 5] = Constant.RESTRICTED;
                }
            }
        }

        private void WriteFieldToExcel(Worksheet schemaSheet, int lineCnt, Field field)
        {
            schemaSheet.Cells[lineCnt, Constant.FIELDNAMECOLNUM] = field.FieldName;
            schemaSheet.Cells[lineCnt, Constant.FIELDNAMELENGTHCOLUMN] = field.FieldName.Length;
            schemaSheet.Cells[lineCnt, Constant.FIELDINITIALCOLNUM] = field.FieldInitialValue;
            schemaSheet.Cells[lineCnt, Constant.FIELDLABELCOLNUM] = field.FieldLabel;

            if (field.FieldIsMandatory)
                schemaSheet.Cells[lineCnt, Constant.FIELDMANDITORYCOLNUM] = "M";

            switch (field.FieldFormat)
            {
                case "9999999999":
                    {
                        schemaSheet.Cells[lineCnt, Constant.FIELDFORMATCOLUMN] = Constant.DOMAIN_IDENTITYFIELD;
                        break;
                    }
                case "999999999":
                    {
                        schemaSheet.Cells[lineCnt, Constant.FIELDFORMATCOLUMN] = Constant.DOMAIN_VOUCHERNUMBER;
                        break;
                    }
                case "yes/no":
                    {
                        schemaSheet.Cells[lineCnt, Constant.FIELDFORMATCOLUMN] = Constant.DOMAIN_BOOLEAN;
                        break;
                    }
                case "x(20)":
                    {
                        schemaSheet.Cells[lineCnt, Constant.FIELDFORMATCOLUMN] = Constant.DOMAIN_SHORTCODE;
                        break;
                    }
                case "x(40)":
                    {
                        schemaSheet.Cells[lineCnt, Constant.FIELDFORMATCOLUMN] = Constant.DOMAIN_DESCRIPTIONSTRING;
                        break;
                    }
                case "->>>,>>>,>>>,>>9.99":
                    {
                        schemaSheet.Cells[lineCnt, Constant.FIELDFORMATCOLUMN] = Constant.DOMAIN_EFAS_TC;
                        break;
                    }
                default:
                    {
                        schemaSheet.Cells[lineCnt, Constant.FIELDFORMATCOLUMN] = field.FieldFormat;
                        break;
                    }
            }
        }

        private void WriteIndexToExcel(Worksheet schemaSheet, int lineCnt, Index index)
        {
            string indexFields = index.IndexName + ":";
            for (int indexFieldNum = 0; indexFieldNum < index.IndexFieldList.Count; indexFieldNum++)
            {
                indexFields += (" + " + index.IndexFieldList[indexFieldNum].IndexFieldName);
            }

            if (index.IndexIsUnique)
                indexFields += (" " + Constant.INDEXUNIQUE.ToUpper());

            schemaSheet.Cells[lineCnt, Constant.INDEXNAMECOLNUM] = indexFields;
        }
        #endregion
    }       
}
