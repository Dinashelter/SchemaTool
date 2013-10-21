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

        public void CheckSchemaExcel()
        {
            ClearDataInList();
            PrepareData();
            CheckSchemaChange();
            OutputResult();
        }

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
                CheckTableNameLength(tableList[tableNum]);
            }
        }

        protected void CheckTableNameLength(Table table)
        {
            bool tableNameIsOk;
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
        #endregion

        #region Check fields
        protected void CheckFieldsInTable()
        {
            for (int fieldNum = 0; fieldNum < fieldList.Count; fieldNum++)
            {
                CheckFieldNameLength(fieldList[fieldNum]);
            }
        }

        protected void CheckFieldNameLength(Field field)
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
            bool indexNameIsOk;
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
    }
}
