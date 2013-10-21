using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaTool
{
    public class Table
    {
        private string _tableName;
        private string _tableArea;
        private string _tableDescription;
        private bool _tableIsFrozen;
        private string _tableDumpName;
        private string _tableActivity;
        private Position _tablePositionInFile;

        public Table()
        {
        }

        public Table(string tableName, string tableActivity)
        {
            _tableName = tableName;
            _tableArea = "";
            _tableDescription = "";
            _tableIsFrozen = false;
            _tableDumpName = "";
            _tableActivity = tableActivity;
            _tablePositionInFile = new Position();
        }

        public Table(string tableName, string tableArea, string tableDescription, bool tableIsFrozen, string tableDumpName)
        {
            _tableName = tableName;
            _tableArea = tableArea;
            _tableDescription = tableDescription;
            _tableIsFrozen = tableIsFrozen;
            _tableDumpName = tableDumpName;
            _tablePositionInFile = new Position();
        }

        ~Table()
        {
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public string TableArea
        {
            get { return _tableArea; }
            set { _tableArea = value; }
        }

        public string TableDescription
        {
            get { return _tableDescription; }
            set { _tableDescription = value; }
        } 

        public bool TableIsFrozen
        {
            get { return _tableIsFrozen; }
            set { _tableIsFrozen = value; }
        }

        public string TableDumpName
        {
            get { return _tableDumpName; }
            set { _tableDumpName = value; }
        }

        public string TableActivity
        {
            get { return _tableActivity; }
            set { _tableActivity = value; }
        }

        public Position TablePositionInFile
        {
            get { return _tablePositionInFile; }
            set { _tablePositionInFile = value; }
        }

        public bool CheckTableNameLength()
        {
            if (_tableName.Length > Constant.TABLEMAXLENGTH)
                return false;
            else
                return true;
        }

        public string GetTablePosInfo()
        {
            string tablePosInfo;
            tablePosInfo = TableName + ": row " + _tablePositionInFile.Row;

            if (_tablePositionInFile.Column != 0)
                tablePosInfo += (", column " + _tablePositionInFile.Column);
            return tablePosInfo;
        }
    }
}
