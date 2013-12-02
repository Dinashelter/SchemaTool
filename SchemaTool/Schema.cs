using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaTool
{
    public class Schema
    {
        protected List<Table> tableList;
        protected List<Field> fieldList;
        protected List<Index> indexList;
        protected List<string> errorList;
        protected List<string> warningList;

        #region public method
        public void CheckSchema()
        {
            ClearDataInList();
            PrepareData();
            CheckSchemaChange();
            OutputResult();
        }
        #endregion

        #region protected method
        protected void InitializeGlobalVariables()
        {
            tableList = new List<Table>();
            fieldList = new List<Field>();
            indexList = new List<Index>();
            errorList = new List<string>();
            warningList = new List<string>();
        }      

        protected void ClearDataInList()
        {
            tableList.Clear();
            fieldList.Clear();
            indexList.Clear();
        }

        protected virtual void PrepareData(){}
      
        protected virtual void CheckSchemaChange()
        {
            errorList.Clear();
            warningList.Clear();
            CheckTable();
            CheckFieldsInTable();
            CheckIndicesInTable();
        }

        protected void OutputResult()
        {
            string warning = "";
            string error = "";
            SchemaCheckResultForm schemaCheckResultForm = new SchemaCheckResultForm();
            for (int warningNum = 0; warningNum < warningList.Count; warningNum++)
            {
                if (warningNum == 0)
                    warning += (Constant.SCHEMACHECKWARNING + "\n");
                warning += (warningList[warningNum] + "\n");
            }
            
            if (errorList.Count == 0)
                error = Constant.SCHEMACHECKISOK;
            else
            {
                for (int errorNum = 0; errorNum < errorList.Count; errorNum++)
                {
                    if (errorNum == 0)
                        error += (Constant.SCHEMACHECKERROR + "\n");
                    error += (errorList[errorNum] + "\n");
                }                
            }
            schemaCheckResultForm.ResultTextBox.Text = warning + error;
            schemaCheckResultForm.ShowDialog();
        }

        #region Check Table
        protected void CheckTable()
        {
            for (int tableNum = 0; tableNum < tableList.Count; tableNum++)
            {
                Table table = tableList[tableNum];
                CheckTableNameLength(table);
                if (table.TableActivity == Constant.TABLEACTIVITY_CREATE)
                {
                    CheckTableHasIdField(table);
                    CheckTableHasPrimIndexAndUniqIndex(table);
                }
            }
        }

        private void CheckTableNameLength(Table table)
        {
            bool tableNameIsOk = false;
            string result = "";
            string tablePosInfo;

            tableNameIsOk = table.CheckTableNameLength();

            if (!tableNameIsOk)
            {
                tablePosInfo = table.GetTablePosInfo();
                result = tablePosInfo + "\n" + Constant.TABLENAMETOOLONG + "\n";
                errorList.Add(result);
            }
        }

        private void CheckTableHasIdField(Table table)
        {
            bool tableHasIDField = false;
            string result = "";
            for (int fieldNum = 0; fieldNum < fieldList.Count; fieldNum++)
            {
                Field field = fieldList[fieldNum];
                if (field.FieldTableName == table.TableName &&
                    field.FieldName == table.TableName + "_ID")
                {
                    tableHasIDField = true;
                    break;
                }
            }

            if (!tableHasIDField)
            {
                result = table.TableName + "\n" + Constant.TABLEDONOTHAVEIDFIELD + "\n";
                errorList.Add(result);
            }
        }

        private void CheckTableHasPrimIndexAndUniqIndex(Table table)
        {
            bool tableHasPrimIndex = false;
            bool tableHasUniqIndex = false;
            string result = "";

            for (int indexNum = 0; indexNum < indexList.Count; indexNum++)
            {
                Index index = indexList[indexNum];
                if (!tableHasPrimIndex && (index.IndexTableName == table.TableName))
                    tableHasPrimIndex = index.IsPrimIndex();
                if (!tableHasUniqIndex && (index.IndexTableName == table.TableName))
                    tableHasUniqIndex = index.IsUniqIndex();
            }

            if (!tableHasPrimIndex)
            {
                result = table.TableName + "\n" + Constant.TABLEDONOTHAVEPRIMINDEX + "\n";
                errorList.Add(result);
            }

            if (!tableHasUniqIndex)
            {
                result = table.TableName + "\n" + Constant.TABLEDONOTHAVEUNIQINDEX + "\n";
                warningList.Add(result);
            }
        }
        #endregion

        #region Check fields
        protected void CheckFieldsInTable()
        {        
            for (int fieldNum = 0; fieldNum < fieldList.Count; fieldNum++)
            {
                Field field = fieldList[fieldNum];
                CheckFieldNameLength(field);
                CheckFieldStartWithTableName(field);
                CheckLogicalField(field);
                CheckFieldTypeMatchFieldFormat(field);
            }        
        }

        private void CheckFieldNameLength(Field field)
        {
            bool fieldNameIsOk;
            string result = "";
            string fieldPosInfo;

            fieldNameIsOk = field.CheckFieldNameLength();

            if (!fieldNameIsOk)
            {
                fieldPosInfo = field.GetFieldPosInfo();
                result = fieldPosInfo + "\n" + Constant.FIELDNAMETOOLONG + "\n";
                errorList.Add(result);
            }
        }

        private void CheckFieldStartWithTableName(Field field)
        {
            bool fieldNameIsOk = false;
            string result = "";
            string fieldPosInfo;

            fieldNameIsOk = field.CheckFieldStartWithTableName();

            if (!fieldNameIsOk)
            {
                fieldPosInfo = field.GetFieldPosInfo();
                result = fieldPosInfo + "\n" + Constant.FIELDNAMESHOULDSTARTWITHTABLENAME + "\n";
                errorList.Add(result);
            }
        }

        private void CheckLogicalField(Field field)
        {
            bool logicalFieldNameIsOk = false;
            string result = "";
            string fieldPosInfo;

            logicalFieldNameIsOk = field.CheckLogicalFieldIsManditory();

            if (logicalFieldNameIsOk)
                logicalFieldNameIsOk = field.CheckLogicalFieldHasKeyWordIs();

            if (!logicalFieldNameIsOk)
            {
                fieldPosInfo = field.GetFieldPosInfo();
                result = fieldPosInfo + "\n" + Constant.LOGICALFIELDNAMEERROR + "\n";
                errorList.Add(result);
            }
        }

        private void CheckFieldTypeMatchFieldFormat(Field field)
        {
            bool fieldTypeMatchFieldFormat = false;
            string result = "";
            string fieldPosInfo;

            fieldTypeMatchFieldFormat = field.CheckFieldTypeMatchFieldFormat();

            if (!fieldTypeMatchFieldFormat)
            {
                fieldPosInfo = field.GetFieldPosInfo();
                result = fieldPosInfo + "\n" + Constant.FIELDTYPEMATCHFIELDFORMAT + "\n";
                errorList.Add(result);
            }
        }
       
        #endregion

        #region Check Index
        protected void CheckIndicesInTable()
        {
            for (int indexNum = 0; indexNum < indexList.Count; indexNum++)
            {
                Index index = indexList[indexNum];
                CheckIndexNameLength(index);
                CheckPrimIndex(index);
            }
        }

        private void CheckIndexNameLength(Index index)
        {
            bool indexNameIsOk = false;
            string result = "";
            string indexPosInfo;

            indexNameIsOk = index.CheckIndexNameLength();

            if (!indexNameIsOk)
            {
                indexPosInfo = index.GetIndexPosInfo();
                result = indexPosInfo + "\n" + Constant.INDEXNAMETOOLONG + "\n";
                errorList.Add(result);
            }
        }

        private void CheckPrimIndex(Index index)
        {
            if (index.IndexName.ToLower() != Constant.PRIMINDEX)
                return;

            bool primIndexIsOk = false;
            string result = "";
            string indexPosInfo;

            primIndexIsOk = index.CheckPrimIndexField();

            if (!primIndexIsOk)
            {
                indexPosInfo = index.GetIndexPosInfo();
                result = indexPosInfo + "\n" + Constant.PRIMINDEXERROR + "\n";
                errorList.Add(result);
            }
        }

        #endregion Check Index

        #endregion
    }
}
