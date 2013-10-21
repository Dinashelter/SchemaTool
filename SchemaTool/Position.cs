using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaTool
{
    public class Position
    {
        private int _row;
        private int _column;

        public Position(int row, int column)
        {
            _row = row;
            _column = column;
        }

        public Position()
        {
            _row = 0;
            _column = 0;
        }

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }

        public int Column
        {
            get { return _column; }
            set { _column = value; }
        }
    }
}
