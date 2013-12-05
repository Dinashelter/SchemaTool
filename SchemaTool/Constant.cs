using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaTool
{
    public class Constant
    {
        public const string SCHEMATOOL = "Schema Tool";
        public const string SCHEMAFORMATISNOTCORRECT = "Schema format is not correct!";
        public const string SCHEMACHECKWARNING = "----------------------------------Schema Check Warning------------------------------";
        public const string SCHEMACHECKERROR =   "----------------------------------Schema Check Error-----------------------------------";
        public const string ADD = "add";
        public const string QUESTION = "Question";

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
        public const string FIELDTYPEMATCHFIELDFORMAT = "Field type doesn't match field format";
        public const string SCHEMAERROR = "df Schema has some errors, Do you want to continue convertion?";
        #endregion

        #region Schema Warning
        public const string TABLEDONOTHAVEUNIQINDEX = "Provide a non-prim but unique index on all tables when possible and make sure all fields in this index are marked as mandatory (allowing zero but not NULL).(STD-0007)";
        #endregion

        #region Table 
        public const string TABLE = "table";
        public const int TABLEMAXLENGTH = 14;
        public const int TABLEINDEXMAXLENGTH = 28;
        public const int TABLENAMECOLNUM = 1;
        public const string NEWTABLE = "New Table";
        public const string MODIFIEDTABLE = "Modified Table";
        public const string TABLEACTIVITY_CREATE = "create";
        public const string TABLEACTIVITY_MODIFY = "modify";
        public const string CHANGESAREINRED = "(changes are in RED)";
        public const string TABLEAREA_FIN = "FIN";
        public const string TABLEAREA = "area";
        public const string TABLEDESCRIPTION = "description";
        public const string TABLEFROZEN = "frozen";
        public const string TABLEDUMPNAME = "dump-name";
        #endregion

        #region Field
        public const int FIELDMAXLENGTH = 30;
        public const int FIELDNAMECOLNUM = 3;
        public const int FIELDNAMELENGTHCOLUMN = 4;
        public const int FIELDFORMATCOLUMN = 5;
        public const int FIELDMANDITORYCOLNUM = 6;
        public const int FIELDINITIALCOLNUM = 7;
        public const int FIELDLABELCOLNUM = 8;
        public const string FIELD = "Field";
        public const string FIELDFORMAT = "format";
        public const string FIELDINITIAL = "initial";
        public const string FIELDLABEL = "label";
        public const string FIELDPOSITION = "position";
        public const string FIELDMAXWIDTH = "max-width";
        public const string FIELDORDER = "order";
        public const string FIELDMANDATORY = "mandatory";
        public const string CUSTOMFIELD = "custom";
        public const string LASTMODIFIEDFIELD = "lastmodified";
        public const string QADFIELD = "qad";
        #endregion

        #region Index
        public const string INDEX = "index";
        public const int INDEXNAMECOLNUM = 3;
        public const string PRIMINDEX = "prim";
        public const string UNIQINDEX = "uniqidx";
        public const string INDICES = "Indices";
        public const string INDEXAREA_FIN = "FIN_IDX";
        public const string INDEXAREA = "area";
        public const string INDEXUNIQUE = "unique";
        public const string INDEXFIELD = "index-field";
        public const string INDEXASCENDING = "ascending";
        #endregion

        #region Domain
        public const string DOMAIN_BOOLEAN = "BOOLEAN";
        public const string DOMAIN_IDENTITYFIELD = "IDENTITYFIELD";
        public const string DOMAIN_VOUCHERNUMBER = "VOUCHERNUMBER";
        public const string DOMAIN_SHORTCODE = "SHORTCODE";
        public const string DOMAIN_DESCRIPTIONSTRING = "DESCRIPTIONSTRING";
        public const string DOMAIN_EFAS_TC = "EFAS_TC";
        #endregion

        #region Format
        public const string FORMAT_BOOLEAN = "yes/no";
        #endregion

        #region Field Type
        public const string FIELDTYPE_INTEGER = "integer";
        public const string FIELDTYPE_CHARACTER = "character";
        public const string FIELDTYPE_LOGICAL = "logical";
        public const string FIELDTYPE_DATE = "date";
        public const string FIELDTYPE_DECIMAL = "decimal";
        #endregion

        #region Table Relation
        public const string NEWRELATION = "New Relation";
        public const string MULTIPLICITY = "Multiplicity";
        public const string PRIMARY = "Primary";
        public const string PARENTMAND = "Parent Mand";
        public const string DELETECONSTRAINT = "Delete Constraint";
        public const string ONETON = "1-N";
        public const string RESTRICTED = "Restricted";
        #endregion

        #region Columns Caption
        public const string FORMATDOMAIN = "Format/Domain";
        public const string FLAGS = "Flags";
        public const string INITIAL = "Initial";
        public const string SIDELABEL = "Side-Label";
        public const string COLLABEL = "Col-Label";
        public const string COMMENTS = "Comments";
        public const string NOTES = "Notes";
        #endregion

        #region Excel Schema 
        public const string NOTESTEXT = "1. When adding new fields - like Region_ID here - then do list the preceeding and following field so the new field get created in the correct position although in the physical database new fields will always be added at the end of the table";
        public const string SPECIALFIELDS = "Special fields:";
        public const string INCLUDECUSTOMFIELDS = "Include Custom-Fields in this table";
        public const string INCLUDELASTMODIFYFIELDS = "Include LastModified-Fields in this table";
        public const string INCLUDEQADFIELDS = "Include QAD-Reserved-Fields in this table";
        public const string YES = "Yes";
        public const string NO = "No";
        #endregion

        public const int MAXTOLERABLENUMINWHILE = 50;
    }
}
