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
using System.Threading;

namespace SubgraphViewer
{
    public partial class Query : Form
    {
        private ViewerQueryGraph m_ViewerGraph;
        private ToolBox m_ToolBox;
        private bool m_IndexLoaded;
        private Rectangle m_ClientRegion;
        private SubgraphViewerDrawer m_GraphDrawer;
        public Query()
        {
            InitializeComponent();
            IntializeFormLayout();
            
            m_GraphDrawer = new SubgraphViewerDrawer(this.CreateGraphics());
            SubgraphViewerDrawer toolBoxDrawer = new SubgraphViewerDrawer(panel_left.CreateGraphics());
            m_ViewerGraph = new ViewerQueryGraph(m_GraphDrawer);
            m_ToolBox = new ToolBox(toolBoxDrawer);
            m_IndexLoaded = false;
            //m_ViewerGraph = SampleQueryGraph();
            
        }


        private void ConnectServer()
        {
            Thread t = new Thread(new ThreadStart(ViewerConfig.ConfigureServer));
            t.Start();
            SplashForm sf = new SplashForm("connect to servers...");
            sf.Show();
            DateTime startTime = DateTime.Now;
            while (t.IsAlive == true)
            {
                if (DateTime.Now.Subtract(startTime).TotalSeconds >= 6)
                {
                    t.Abort();
                    break;
                }
                sf.Update();
                Thread.Sleep(100);
            }
            sf.Close();
            
        }

        private void IntializeFormLayout()
        {
            this.Text = "QueryPanel";
            this.ClientSize = new Size(ViewerConfig.QueryPanleWidth, ViewerConfig.QueryPanelHeight);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
            //MessageBox.Show(this.BackColor.R.ToString() + " " + this.BackColor.G.ToString() + " " + this.BackColor.B.ToString());
            panel_left.Width = ViewerConfig.LeftPanelWidth;
            panel_bottom.Height = ViewerConfig.BottomPanelHeight;
            buttonMatch.Width = panel_bottom.Width;
            buttonMatch.Height = 20;
            buttonClear.Width = panel_bottom.Width;
            buttonClear.Height = 20;
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
            if (m_ToolBox.SelectedItem != null && m_ToolBox.SelectedItem.Name == "eraser")
            {
                Cursor = MyCursor.EraserCursor;
            }
            else
            {
                Cursor = Cursors.Default;
            }
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
            else if (m_ToolBox.SelectedItem != null && m_ToolBox.SelectedItem.Name == "eraser")
            {
                m_ViewerGraph.Remove(new Rectangle(e.Location.X, e.Location.Y, ViewerConfig.MouseEraserWidth, ViewerConfig.MouseEraserHeight));
                UpdateLabelList();
                this.Invalidate();
            }
        }

        private void PanelLeft_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
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
            //textBoxStartVertex.Text = e.X.ToString() + " " + e.Y.ToString();
            m_ToolBox.SetSelectedItem(e.Location);
            panel_left.Invalidate();
            if (m_ToolBox.SelectedItem != null && m_ToolBox.SelectedItem.Name == "eraser")
            {
                Cursor = MyCursor.EraserCursor;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private void buttonMatch_Click(object sender, EventArgs e)
        {
            string msg;
            if (m_ViewerGraph.Valid(out msg) == false)
            {
                MessageBox.Show(msg);
                return;
            }
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
            List<Match> matches = new List<Match>();
            try
            {
                if (m_IndexLoaded == false)
                {
                    SubGraphCoreMatch.LoadLabelDictionaryIndex();
                    m_IndexLoaded = true;
                }
                matches = SubGraphCoreMatch.OnLineQuery(qg, maxMatchNum);
            }
            catch (System.Exception ex)
            {
                buttonMatch.Enabled = true;
                return;
            }
            //matches = SampleMatches(qg);
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

        private void Query_Load(object sender, EventArgs e)
        {
            this.Hide();
            ConnectServer();
            if (ViewerConfig.HostNameList == null)
            {
                MessageBox.Show("can not connect to servers");
            }
            this.Visible = true;
        }

        private void Clear()
        {
            m_ViewerGraph = new ViewerQueryGraph(m_GraphDrawer);
            textBoxEndVertex.Text = "";
            textBoxMaxMatchNum.Text = "";
            textBoxStartVertex.Text = "";
            textBoxVertexID.Text = "";
            textBoxVertexLabel.Text = "";
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
            this.Invalidate();
            UpdateLabelList();
        }

        private void panel_bottom_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
        }


    }
}
