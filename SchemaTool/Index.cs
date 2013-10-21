using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaTool
{
    public class Index
    {
        private string _indexTableName;
        private string _indexName;
        private string _indexArea;
        private bool _indexIsUnique;
        private bool _indexIsPrimary;
        private List<IndexField> _indexFieldList;
        private Position _indexPositionInFile;

        public Index(string indexTableName, string indexName, string indexArea, bool indexIsUnque, bool indexIsPrimary, List<IndexField> indexFieldList)
        {
            _indexTableName = indexTableName;
            _indexName = indexName;
            _indexArea = indexArea;
            _indexIsUnique = indexIsUnque;
            _indexIsPrimary = indexIsPrimary;
            _indexFieldList = indexFieldList;
        }

        public Index(string indexTableName, string indexName)
        {
            _indexTableName = indexTableName;
            _indexName = indexName;
            _indexFieldList = new List<IndexField>();
            _indexPositionInFile = new Position();
        }

        ~Index()
        {
        }

        public string IndexTableName
        {
            get { return _indexTableName; }
            set { _indexTableName = value; }
        }

        public string IndexName
        {
            get { return _indexName; }
            set { _indexName = value; }
        }

        public string IndexArea
        {
            get { return _indexArea; }
            set { _indexArea = value; }
        }

        public bool IndexIsUnique
        {
            get { return _indexIsUnique; }
            set { _indexIsUnique = value; }
        }

        public bool IndexIsPrimary
        {
            get { return _indexIsPrimary; }
            set { _indexIsPrimary = value; }
        }

        public List<IndexField> IndexFieldList
        {
            get { return _indexFieldList; }
            set { _indexFieldList = value; }
        }

        public Position IndexPositionInFile
        {
            get { return _indexPositionInFile; }
            set { _indexPositionInFile = value; }
        }

        public bool CheckIndexNameLength()
        {
            if (_indexName.Length > Constant.TABLEINDEXMAXLENGTH - _indexTableName.Length)
                return false;
            else
                return true;
        }

        public string GetIndexPosInfo()
        {
            string fieldPosInfo;
            fieldPosInfo = _indexName + ": row " + _indexPositionInFile.Row;

            if (_indexPositionInFile.Column != 0)
                fieldPosInfo += (", column " + _indexPositionInFile.Column);
            return fieldPosInfo;
        }
    }
}
