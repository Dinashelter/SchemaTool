using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    else if (dfSchemaLineArray[1].ToString().ToLower() == Constant.FIELD)
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


                if (dfSchemaLine == "" || streamReader.EndOfStream)
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
        #endregion
    }       
}
