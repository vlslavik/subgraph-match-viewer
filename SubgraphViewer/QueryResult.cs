using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SubgraphViewer
{
    public partial class QueryResult : Form
    {
        public QueryResult()
        {
            InitializeComponent();
        }

        private void InitializeFormLayout()
        {
            this.Width = ViewerConfig.QueryResultWidth;
            this.Height = ViewerConfig.QueryResultHeight;
        }
    }
}
