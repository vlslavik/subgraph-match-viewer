using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Trinity.GraphDB.Query.Subgraph;

namespace SubgraphViewer
{
    partial class QueryResult : Form
    {
        private ViewerResult m_ViewerResult;
        public QueryResult()
        {
            InitializeComponent();
            InitializeFormLayout();
        }

        public QueryResult(ViewerQueryGraph vqg, List<Match> matches)
        {
            InitializeComponent();
            InitializeFormLayout();
            SubgraphViewerDrawer drawer = new SubgraphViewerDrawer(this.CreateGraphics());
            m_ViewerResult = new ViewerResult(drawer, matches, vqg);
        }

        private void InitializeFormLayout()
        {
            this.Width = ViewerConfig.QueryResultWidth;
            this.Height = ViewerConfig.QueryResultHeight;
        }

        private void QueryResult_Paint(object sender, PaintEventArgs e)
        {
            m_ViewerResult.Draw();
        }


    }
}
