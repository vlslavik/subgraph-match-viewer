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
    public partial class Query : Form
    {
        private ViewerQueryGraph m_ViewerGraph;
        private ToolBox m_ToolBox;

        public Query()
        {
            InitializeComponent();
            IntializeFormLayout();
            SubgraphViewerDrawer graphDrawer = new SubgraphViewerDrawer(this.CreateGraphics());
            SubgraphViewerDrawer toolBoxDrawer = new SubgraphViewerDrawer(panel_left.CreateGraphics());
            m_ViewerGraph = new ViewerQueryGraph(graphDrawer);
            m_ToolBox = new ToolBox(toolBoxDrawer);
        }

        private void IntializeFormLayout()
        {
            this.Text = "QueryPanel";
            this.Height = ViewerConfig.QueryPanelHeight;
            this.Width = ViewerConfig.QueryPanleWidth;
            //MessageBox.Show(this.BackColor.R.ToString() + " " + this.BackColor.G.ToString() + " " + this.BackColor.B.ToString());
            panel_left.Width = ViewerConfig.LeftPanelWidth;
            panel_bottom.Height = ViewerConfig.BottomPanelHeight;
            buttonMatch.Width = panel_bottom.Width;
            buttonMatch.Height = 20;
            buttonMatch.Left = panel_bottom.Location.X;
            buttonMatch.Top = panel_bottom.Location.Y - buttonMatch.Height;
        }

        private void Query_MouseMove(object sender, MouseEventArgs e)
        {
            //textBox_StartVertex.Text = e.X.ToString() + " " + e.Y.ToString();
        }

        private void Query_MouseDown(object sender, MouseEventArgs e)
        {
            //textBox_StartVertex.Text = e.X.ToString() + " " + e.Y.ToString();
            if (m_ToolBox.SelectedItem != null && m_ToolBox.SelectedItem.Name == "node")
            {
                if (m_ViewerGraph.AddNode(e.Location) == false)
                {
                    MessageBox.Show("please select another proper location!", "Hint");
                }
                this.Invalidate();
            }
        }

        private void PanelLeft_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Query_Paint(object sender, PaintEventArgs e)
        {
            m_ViewerGraph.Draw();
        }

        private void panel_left_Paint(object sender, PaintEventArgs e)
        {
            m_ToolBox.Draw();
        }

        private void panel_left_MouseDown(object sender, MouseEventArgs e)
        {
            textBox_StartVertex.Text = e.X.ToString() + " " + e.Y.ToString();
            m_ToolBox.SetSelectedItem(e.Location);
            panel_left.Invalidate();
        }

        private void buttonMatch_Click(object sender, EventArgs e)
        {
            if (m_ViewerGraph.LabelName == null)
            {
                MessageBox.Show("please set the label name first", "Hint");
                return;
            }

        }


    }
}
