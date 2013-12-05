using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaTool
{
    public class Field
    {
        private string _fieldTableName;
        private string _fieldName;
        private string _fieldType;
        private string _fieldFormat;
        private string _fieldInitialValue;
        private string _fieldLabel;
        private int _fieldPosition;
        private int _fieldMaxWidth;
        private int _fieldOrder;
        private bool _fieldIsMandatory;
        private Position _fieldPositionInFile;

        public Field()
        { 
        }

        public Field(string fieldTableName, string fieldName)
        {
            _fieldName = fieldName;
            _fieldTableName = fieldTableName;
            _fieldPositionInFile = new Position();
        }

        public Field(string _fieldTableName, string fieldName, string fieldType, string fieldFormat, string fieldInitialValue, string fieldLabel, int fieldPosition, int fieldMaxWidth, int fieldOrder, bool fieldIsMandatory)
        {
            _fieldTableName = FieldTableName;
            _fieldName = fieldName;
            _fieldType = fieldType;
            _fieldFormat = fieldFormat;
            _fieldInitialValue = fieldInitialValue;
            _fieldLabel = fieldLabel;
            _fieldPosition = fieldPosition;
            _fieldMaxWidth = fieldMaxWidth;
            _fieldOrder = fieldOrder;
            _fieldIsMandatory = fieldIsMandatory;
            _fieldPositionInFile = new Position();
        }

        public Field(string fieldTableName, string fieldName, string fieldFormat, string fieldInitialValue, string fieldLabel, bool fieldIsMandatory)
        {
            _fieldTableName = fieldTableName;
            _fieldName = fieldName;
            _fieldFormat = fieldFormat;
            _fieldInitialValue = fieldInitialValue;
            _fieldLabel = fieldLabel;
            _fieldIsMandatory = fieldIsMandatory;
            _fieldPositionInFile = new Position();
        }

        ~Field()
        {
        }

        public string FieldTableName
        {
            get { return _fieldTableName; }
            set { _fieldTableName = value; }
        }

        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        public string FieldType
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        public string FieldFormat
        {
            get { return _fieldFormat; }
            set { _fieldFormat = value; }
        }

        public string FieldInitialValue
        {
            get { return _fieldInitialValue; }
            set { _fieldInitialValue = value; }
        }

        public string FieldLabel
        {
            get { return _fieldLabel; }
            set { _fieldLabel = value; }
        }

        public int FieldPosition
        {
            get { return _fieldPosition; }
            set { _fieldPosition = value; }
        }

        public int FieldMaxWidth
        {
            get { return _fieldMaxWidth; }
            set { _fieldMaxWidth = value; }
        }

        public int FieldOrder
        {
            get { return _fieldOrder; }
            set { _fieldOrder = value; }
        }

        public bool FieldIsMandatory
        {
            get { return _fieldIsMandatory; }
            set { _fieldIsMandatory = value; }
        }

        public Position FieldPositionInFile
        {
            get { return _fieldPositionInFile; }
            set { _fieldPositionInFile = value; }
        }

        public bool CheckFieldNameLength()
        {
            if (_fieldName.Length > Constant.FIELDMAXLENGTH)
                return false;
            else
                return true;
        }

        public bool CheckFieldStartWithTableName()
        {
            if (_fieldName.Contains("_ID"))
                return true;
            else
            {
                if (_fieldName.IndexOf(_fieldTableName) != 0)
                    return false;
                else
                    return true;
            }
        }

        public bool CheckLogicalFieldIsManditory()
        {
            if (_fieldFormat.ToLower() == Constant.DOMAIN_BOOLEAN.ToLower() ||
                _fieldFormat.ToLower() == Constant.FORMAT_BOOLEAN)
            {
                if (!_fieldIsMandatory)
                    return false;
                else
                    return true;
            }
            else return true;
        }

        public bool CheckLogicalFieldHasKeyWordIs()
        {
            if (_fieldFormat.ToLower() == Constant.DOMAIN_BOOLEAN.ToLower() ||
                _fieldFormat.ToLower() == Constant.FORMAT_BOOLEAN)
            {
                if (!_fieldName.ToLower().Contains("is"))
                    return false;
                else
                    return true;
            }
            else return true;
        }

        public bool CheckFieldTypeMatchFieldFormat()
        {
            bool IsMatch = false;
            //There is no field type in excel schema
            if (_fieldType == "")
                IsMatch = true;
            else
            {
                switch (_fieldType.ToLower())
                {
                    case Constant.FIELDTYPE_INTEGER:
                        {
                            if (_fieldFormat.Contains("9") &&
                                !_fieldFormat.Contains(".") &&
                                !_fieldFormat.Contains("/") &&
                                !_fieldFormat.Contains("x"))
                                IsMatch = true;
                            break;
                        }
                    case Constant.FIELDTYPE_CHARACTER:
                        {
                            if (_fieldFormat.Contains("x") &&
                                _fieldFormat.Contains("(") &&
                                _fieldFormat.Contains(")"))
                                IsMatch = true;
                            break;
                        }
                    case Constant.FIELDTYPE_LOGICAL:
                        {
                            if (_fieldFormat == Constant.FORMAT_BOOLEAN)
                                IsMatch = true;
                            break;
                        }
                    case Constant.FIELDTYPE_DATE:
                        {
                            if (_fieldFormat.Contains("9") &&
                                !_fieldFormat.Contains(".") &&
                                _fieldFormat.Contains("/") &&
                                !_fieldFormat.Contains("x"))
                                IsMatch = true;
                            break;
                        }
                    case Constant.FIELDTYPE_DECIMAL:
                        {
                            if (_fieldFormat.Contains("9") &&
                                _fieldFormat.Contains(".") &&
                                !_fieldFormat.Contains("/") &&
                                !_fieldFormat.Contains("x"))
                                IsMatch = true;
                            break;
                        }
                    default:
                        {
                            IsMatch = false;
                            break;
                        }
                }
            }
            return IsMatch;
        }

        public string GetFieldPosInfo()
        {
            string fieldPosInfo;
            fieldPosInfo = _fieldName + ": row " + _fieldPositionInFile.Row;

            if (_fieldPositionInFile.Column != 0)
                fieldPosInfo += (", column " + _fieldPositionInFile.Column);
            return fieldPosInfo;
        }

    }
}
