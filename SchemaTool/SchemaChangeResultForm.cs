using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchemaTool
{
    public partial class SchemaCheckResultForm : Form
    {
        public SchemaCheckResultForm()
        {
            InitializeComponent();
            SetControls();
        }

        private void SetControls()
        {
            this.Width = 600;
            this.Height = 400;
        }
    }
}
