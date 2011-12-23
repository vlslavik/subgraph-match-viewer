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
using Trinity.Networking;

namespace SubgraphViewer
{
    public partial class Query : Form
    {
        private ViewerQueryGraph m_ViewerGraph;
        private ToolBox m_ToolBox;
        private bool m_IndexLoaded;
        private Rectangle m_ClientRegion;
        public Query()
        {
            InitializeComponent();
            IntializeFormLayout();
            SubgraphViewerDrawer graphDrawer = new SubgraphViewerDrawer(this.CreateGraphics());
            SubgraphViewerDrawer toolBoxDrawer = new SubgraphViewerDrawer(panel_left.CreateGraphics());
            m_ViewerGraph = new ViewerQueryGraph(graphDrawer);
            m_ToolBox = new ToolBox(toolBoxDrawer);
            TrinityConfig.CurrentRunningMode = RunningMode.Client;
            m_IndexLoaded = false;
            ConnectServer();
            //m_ViewerGraph = SampleQueryGraph();
        }


        private void ConnectServer()
        {
            List<string> hostNameList = BlackboardClient.HostNameList;
        }

        private void IntializeFormLayout()
        {
            this.Text = "QueryPanel";
            this.ClientSize = new Size(ViewerConfig.QueryPanleWidth, ViewerConfig.QueryPanelHeight);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            //MessageBox.Show(this.BackColor.R.ToString() + " " + this.BackColor.G.ToString() + " " + this.BackColor.B.ToString());
            panel_left.Width = ViewerConfig.LeftPanelWidth;
            panel_bottom.Height = ViewerConfig.BottomPanelHeight;
            buttonMatch.Width = panel_bottom.Width;
            buttonMatch.Height = 20;
            m_ClientRegion = new Rectangle();
            m_ClientRegion.X = panel_left.Width + (int)ViewerConfig.NodeRadius;
            m_ClientRegion.Y = (int)ViewerConfig.NodeRadius;
            m_ClientRegion.Width = ViewerConfig.QueryPanleWidth - panel_left.Width - 2 * (int)ViewerConfig.NodeRadius;
            m_ClientRegion.Height = ViewerConfig.QueryPanelHeight - panel_bottom.Height - 2 * (int)ViewerConfig.NodeRadius;
            //buttonMatch.Left = panel_bottom.Location.X;
            //buttonMatch.Top = panel_bottom.Location.Y - buttonMatch.Height;
        }

        private void Query_MouseMove(object sender, MouseEventArgs e)
        {
            //textBox_StartVertex.Text = e.X.ToString() + " " + e.Y.ToString();
        }

        private void Query_MouseDown(object sender, MouseEventArgs e)
        {
            //textBoxStartVertex.Text = e.X.ToString() + " " + e.Y.ToString();
            if (m_ToolBox.SelectedItem != null && m_ToolBox.SelectedItem.Name == "node")
            {
                if (m_ClientRegion.Contains(e.Location) == false || m_ViewerGraph.AddNode(e.Location) == false)
                {
                    MessageBox.Show("please select another proper location!", "Hint");
                }
                UpdateLabelList();
                this.Invalidate();
            }
        }

        private void PanelLeft_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Query_Paint(object sender, PaintEventArgs e)
        {
            m_ViewerGraph.Draw();
            //Pen p = new Pen(Color.Black);
            //e.Graphics.DrawRectangle(p, m_ClientRegion);
        }

        private void panel_left_Paint(object sender, PaintEventArgs e)
        {
            m_ToolBox.Draw();
        }

        private void panel_left_MouseDown(object sender, MouseEventArgs e)
        {
            //textBoxStartVertex.Text = e.X.ToString() + " " + e.Y.ToString();
            m_ToolBox.SetSelectedItem(e.Location);
            panel_left.Invalidate();
        }

        private void buttonMatch_Click(object sender, EventArgs e)
        {
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
            if (m_IndexLoaded == false)
            {
                SubGraphCoreMatch.LoadLabelDictionaryIndex();
                m_IndexLoaded = true;
            }
            List<Match> matches = SubGraphCoreMatch.OnLineQuery(qg, maxMatchNum);
            //List<Match> matches = SampleMatches(qg);
            buttonMatch.Enabled = true;
            QueryResult qr = new QueryResult(m_ViewerGraph, matches);
            qr.Show();
        }

        private List<Match> SampleMatches(QueryGraph qg)
        {
            int matchNum = 20;
            Random ra = new Random();
            List<Match> res = new List<Match>();
            for (int i = 0; i < matchNum; ++i)
            {
                Match m = new Match();
                foreach (long cid in qg.CellIDSet)
                {
                    NodePair np = new NodePair(cid, ra.Next());
                    m.PartialMatch.Add(np);
                }
                res.Add(m);
            }
            return res;
        }

        private ViewerQueryGraph SampleQueryGraph()
        {
            ViewerQueryGraph vqg = new ViewerQueryGraph(new SubgraphViewerDrawer(this.CreateGraphics()));
            Point p1 = new Point(150, 150);
            Point p2 = new Point(120,270);
            Point p3 = new Point(180, 270);
            vqg.AddNode(p1);
            vqg.AddNode(p2);
            vqg.AddNode(p3);
            vqg.SetLabel(1, "1");
            vqg.SetLabel(2, "1");
            vqg.SetLabel(3, "1");
            vqg.AddEdge(1, 2);
            vqg.AddEdge(1, 3);
            UpdateLabelList();
            return vqg;
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
