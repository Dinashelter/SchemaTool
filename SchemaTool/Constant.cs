using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaTool
{
    public class Constant
    {
        public const string NEWTABLE = "new table";
        public const string MODIFIEDTABLE = "modified table";
        public const string TABLEACTIVITY_CREATE = "create";
        public const string TABLEACTIVITY_MODIFY = "modify";
        public const string FIELD = "field";
        public const string INDICES = "indices";
        public const string CHANGESAREINRED = "(changes are in red)";
        public const string TABLEAREA_FIN = "FIN";
        public const string INDEXAREA_FIN = "FIN_IDX";
        public const string PRIMINDEX = "prim";
        public const string UNIQINDEX = "uniqidx";

        #region Schema Ok
        public const string SCHEMACHECKISOK = "Schema check is OK!";
        #endregion

        #region Schema Error
        public const string TABLENAMETOOLONG = "Table name is too Long; Table names cannot be longer than 14 characters (STD-0013).";
        public const string FIELDNAMETOOLONG = "Field name is too Long; Field names cannot be longer than 30 characters (STD-0014).";
        public const string INDEXNAMETOOLONG = "Index name is too Long; The index-name cannot be longer than 28 characters minus the length of the table name to which they belong. (STD-0286).";
        #endregion

        #region Table 
        public const int TABLEMAXLENGTH = 14;
        public const int TABLEINDEXMAXLENGTH = 28;
        public const int TABLENAMECOLNUM = 1;
        #endregion

        #region Field
        public const int FIELDMAXLENGTH = 30;
        public const int FIELDNAMECOLNUM = 3;
        public const int FIELDFORMATCOLNUM = 5;
        public const int FIELDMANDITORYCOLNUM = 6;
        public const int FIELDINITIALCOLNUM = 7;
        public const int FIELDLABELCOLNUM = 8;
        #endregion

        #region Index
        public const int INDEXNAMECOLNUM = 3;
     
        #endregion

        public const int MAXTOLERABLENUMINWHILE = 50;
    }
}
