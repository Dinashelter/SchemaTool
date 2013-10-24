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
        protected List<string> ResultList;

        #region public method
        public void CheckSchemaExcel()
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
            ResultList = new List<string>();
        }      

        protected void ClearDataInList()
        {
            tableList.Clear();
            fieldList.Clear();
            indexList.Clear();
        }

        protected virtual void PrepareData()
        {
            Table newTable = new Table();
            AddFieldToTable(newTable);
            AddIndexToTable(newTable);
        }

        protected virtual void AddFieldToTable(Table table)
        {
        }

        protected virtual void AddIndexToTable(Table table)
        {
        }

        protected void CheckSchemaChange()
        {
            ResultList.Clear();
            CheckTable();
            CheckFieldsInTable();
            CheckIndicesInTable();
        }

        protected void OutputResult()
        {
            string result = "";
            SchemaCheckResultForm schemaCheckResultForm = new SchemaCheckResultForm();
            if (ResultList.Count == 0)
                schemaCheckResultForm.ResultTextBox.Text = Constant.SCHEMACHECKISOK;
            else
            {
                for (int resultNum = 0; resultNum < ResultList.Count; resultNum++)
                {
                    result += (ResultList[resultNum] + "\n");
                }
                schemaCheckResultForm.ResultTextBox.Text = result;
            }
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
                    CheckTableHasIdField(table);
            }
        }

        protected void CheckTableNameLength(Table table)
        {
            bool tableNameIsOk = false;
            string result = "";
            string tablePosInfo;

            tableNameIsOk = table.CheckTableNameLength();

            if (!tableNameIsOk)
            {
                tablePosInfo = table.GetTablePosInfo();
                result = tablePosInfo + "\n" + Constant.TABLENAMETOOLONG + "\n";
                ResultList.Add(result);
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
                ResultList.Add(result);
            }
        }
        #endregion

        #region Check fields
        protected void CheckFieldsInTable()
        {        
            for (int fieldNum = 0; fieldNum < fieldList.Count; fieldNum++)
            {
                CheckFieldNameLength(fieldList[fieldNum]);
                CheckFieldStartWithTableName(fieldList[fieldNum]);
                CheckLogicalField(fieldList[fieldNum]);
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
                ResultList.Add(result);
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
                ResultList.Add(result);
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
                ResultList.Add(result);
            }
        }
       
        #endregion

        #region Check Index
        protected void CheckIndicesInTable()
        {
            for (int indexNum = 0; indexNum < indexList.Count; indexNum++)
            {
                CheckIndexNameLength(indexList[indexNum]);
            }
        }

        protected void CheckIndexNameLength(Index index)
        {
            bool indexNameIsOk = false;
            string result = "";
            string indexPosInfo;

            indexNameIsOk = index.CheckIndexNameLength();

            if (!indexNameIsOk)
            {
                indexPosInfo = index.GetIndexPosInfo();
                result = indexPosInfo + "\n" + Constant.INDEXNAMETOOLONG + "\n";
                ResultList.Add(result);
            }
        }
        #endregion Check Index
        #endregion
    }
}
