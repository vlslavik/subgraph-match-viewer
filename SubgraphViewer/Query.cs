using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Trinity.GraphDB.Query.Subgraph;
using Trinity.GraphDB.Query;
using Trinity.Configuration;
using Trinity.Core;

namespace SubgraphViewer
{
    public partial class Query : Form
    {
        private ViewerQueryGraph m_ViewerGraph;
        private ToolBox m_ToolBox;
        private HashSet<string> m_LoadedLabelIndex;
        public Query()
        {
            InitializeComponent();
            IntializeFormLayout();
            SubgraphViewerDrawer graphDrawer = new SubgraphViewerDrawer(this.CreateGraphics());
            SubgraphViewerDrawer toolBoxDrawer = new SubgraphViewerDrawer(panel_left.CreateGraphics());
            m_ViewerGraph = new ViewerQueryGraph(graphDrawer);
            m_ToolBox = new ToolBox(toolBoxDrawer);
            TrinityConfig.CurrentRunningMode = RunningMode.Client;
            //List<string> hostNameList = Global.BlackBoard.HostNameList;
            m_LoadedLabelIndex = new HashSet<string>();
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
            m_ToolBox.SetSelectedItem(e.Location);
            panel_left.Invalidate();
        }

        private void buttonMatch_Click(object sender, EventArgs e)
        {
            if (textBoxLabelName.Text.Trim() == "")
            {
                MessageBox.Show("please set the label name first", "Hint");
                return;
            }
            m_ViewerGraph.LabelName = textBoxLabelName.Text.Trim();
            QueryGraph qg = m_ViewerGraph.GetLogicQueryGraph();
            int maxMatchNum = 1024;
            try
            {
                if (textBoxMaxMatchNum.Text.Trim() != "")
                {
                    maxMatchNum = int.Parse(textBoxMaxMatchNum.Text.Trim());
                }
            }
            catch (System.Exception ex)
            {
                maxMatchNum = 1024;
            }
            buttonMatch.Enabled = false;
            if (m_LoadedLabelIndex.Contains(m_ViewerGraph.LabelName) == false)
            {
                SubGraphCoreMatch.LoadLabelDictionaryIndex(m_ViewerGraph.LabelName);
                m_LoadedLabelIndex.Add(m_ViewerGraph.LabelName);
            }
            List<Match> matches = SubGraphCoreMatch.OnLineQuery(qg, m_ViewerGraph.LabelName, maxMatchNum);
            buttonMatch.Enabled = true;
            QueryResult qr = new QueryResult(m_ViewerGraph, matches);
            qr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int sid = int.Parse(textBoxStartVertex.Text.Trim());
                int tid = int.Parse(textBoxEndVertex.Text.Trim());
                m_ViewerGraph.AddEdge(sid, tid);
                Invalidate();
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void UpdateLabelList()
        {
            listBoxLabelList.Items.Clear();
            List<ViewerQueryNode> sortedList = m_ViewerGraph.GetSortedIDNodeList();
            for (int i = 0; i < sortedList.Count; ++i)
            {
                listBoxLabelList.Items.Add(sortedList[i].ID + " " + sortedList[i].Label);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int vid = int.Parse(textBoxVertexID.Text.Trim());
                string label = textBoxVertexLabel.Text.Trim();
                m_ViewerGraph.SetLabel(vid, label);
                UpdateLabelList();
            }
            catch (System.Exception ex)
            {
            	
            }
        }


    }
}
