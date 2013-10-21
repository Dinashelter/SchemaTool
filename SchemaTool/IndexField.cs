using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaTool
{
    public class IndexField
    {
        private string _indexFieldName;
        private bool _indexIsAscending;

        public IndexField(string indexFieldName, bool indexIsAscending)
        {
            _indexFieldName = indexFieldName;
            _indexIsAscending = indexIsAscending;
        }

        ~IndexField()
        {
        }

        public string IndexFieldName
        {
            get { return _indexFieldName; }
            set { _indexFieldName = value; }
        }

        public bool IndexIsAscending
        {
            get { return _indexIsAscending; }
            set { _indexIsAscending = value; }
        }

    }
}
