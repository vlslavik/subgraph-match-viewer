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
        private SubgraphViewerDrawer m_Drawer;
        private CellProperty m_CellPropertyForm;
        public QueryResult()
        {
            InitializeComponent();
            InitializeFormLayout();
            m_CellPropertyForm = null;
        }

        public QueryResult(ViewerQueryGraph vqg, List<Match> matches)
        {
            InitializeComponent();
            InitializeFormLayout();
            m_Drawer = new SubgraphViewerDrawer(this.CreateGraphics());
            m_ViewerResult = new ViewerResult(m_Drawer, matches, vqg);
        }

        private void InitializeFormLayout()
        {
            //this.Width = ViewerConfig.QueryResultWidth;
            //this.Height = ViewerConfig.QueryResultHeight;
            this.ClientSize = new Size(ViewerConfig.QueryResultWidth, ViewerConfig.QueryResultHeight);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(ViewerConfig.QueryPanleWidth, 20000);
        }

        private void QueryResult_Paint(object sender, PaintEventArgs e)
        {
            m_Drawer.Graphics = e.Graphics;
            m_Drawer.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);
            m_ViewerResult.Draw();
        }

        private void QueryResult_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ViewerResultGraph vrg = m_ViewerResult.GetOverlapGraph(e.Location);
            if(vrg != null)
            {
                m_CellPropertyForm = new CellProperty(vrg);
                m_CellPropertyForm.Show();
            }
        }


    }
}
