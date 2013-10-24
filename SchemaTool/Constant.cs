using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaTool
{
    public class Constant
    {     
        public const string SCHEMACHECKWARNING = "-------------------------------------------------Schema Check Warning---------------------------------------------";
        public const string SCHEMACHECKERROR =   "--------------------------------------------------Schema Check Error--------------------------------------------------";

        #region Schema Ok
        public const string SCHEMACHECKISOK = "Schema check is OK!";
        #endregion

        #region Schema Error
        public const string TABLENAMETOOLONG = "Table name is too Long; Table names cannot be longer than 14 characters (STD-0013).";
        public const string TABLEDONOTHAVEIDFIELD = "Every new created table will hold a field that is called \"<table-name>\"_ID";
        public const string TABLEDONOTHAVEPRIMINDEX = "Every new created Table should have a prim index";
        public const string FIELDNAMETOOLONG = "Field name is too Long; Field names cannot be longer than 30 characters (STD-0014).";
        public const string FIELDNAMESHOULDSTARTWITHTABLENAME = "Field names should start with the name of the table (exception on the fields that represent a relation to other tables and on the LastModified- and on the Custom-fields and on the QAD-reserved fields). (STD-0014)";        
        public const string LOGICALFIELDNAMEERROR = "Fields of type \'logical\' should be mandatory and should start with the table-name followed by \"Is\", followed by a word that reflects the function of the field. (STD-0014)";
        public const string INDEXNAMETOOLONG = "Index name is too Long; The index-name cannot be longer than 28 characters minus the length of the table name to which they belong. (STD-0286).";
        public const string PRIMINDEXERROR = "Every table will hold a field that is called \"<table-name>_ID\" and the primary index on this table will just hold this single field. (STD-0327) (STD-0286) (STD-0014)";
        public const string TABLERELATIONERROR = "There should be a table relation named ";
        #endregion

        #region Schema Warning
        public const string TABLEDONOTHAVEUNIQINDEX = "Provide a non-prim but unique index on all tables when possible and make sure all fields in this index are marked as mandatory (allowing zero but not NULL).(STD-0007)";
        #endregion

        #region Table 
        public const int TABLEMAXLENGTH = 14;
        public const int TABLEINDEXMAXLENGTH = 28;
        public const int TABLENAMECOLNUM = 1;
        public const string NEWTABLE = "new table";
        public const string MODIFIEDTABLE = "modified table";
        public const string TABLEACTIVITY_CREATE = "create";
        public const string TABLEACTIVITY_MODIFY = "modify";
        public const string CHANGESAREINRED = "(changes are in RED)";
        public const string TABLEAREA_FIN = "FIN";
        #endregion

        #region Field
        public const int FIELDMAXLENGTH = 30;
        public const int FIELDNAMECOLNUM = 3;
        public const int FIELDFORMATCOLNUM = 5;
        public const int FIELDMANDITORYCOLNUM = 6;
        public const int FIELDINITIALCOLNUM = 7;
        public const int FIELDLABELCOLNUM = 8;
        public const string FIELD = "field";
        #endregion

        #region Index
        public const int INDEXNAMECOLNUM = 3;
        public const string PRIMINDEX = "prim";
        public const string UNIQINDEX = "uniqidx";
        public const string INDICES = "indices";
        public const string INDEXAREA_FIN = "FIN_IDX";
        #endregion

        #region Domain
        public const string DOMAIN_BOOLEAN = "boolean";
        #endregion

        #region Format
        public const string FORMAT_BOOLEAN = "yes/no";
        #endregion

        #region Field Type
        public const string FIELDTYPE_INTEGER = "integer";
        public const string FIELDTYPE_CHARACTER = "character";
        public const string FIELDTYPE_LOGICAL = "logical";
        #endregion

        #region Table Relation
        public const string NEWRELATION = "new relation";
        #endregion

        public const int MAXTOLERABLENUMINWHILE = 50;
    }
}
