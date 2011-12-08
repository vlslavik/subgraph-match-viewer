using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Trinity.GraphDB.Query.Subgraph;

namespace SubgraphViewer
{
    class ViewerResultNode : ViewerQueryNode
    {
        private long m_MatchID;
        public ViewerResultNode(ViewerQueryNode vqn, long matchID):base(vqn)
        {
            m_MatchID = matchID;
        }

    }
    class ViewerResultGraph
    {
        Dictionary<int, ViewerResultNode> m_AllNodes;
        ISubGraphViewerDrawer m_Drawer;
        Rectangle m_Region;
        List<ViewerEdge> m_Edges;
        int m_ID;

        public ViewerResultGraph(int id, ISubGraphViewerDrawer drawer, Point leftTop, ViewerQueryGraph vqg, Match m)
        {
            m_AllNodes = new Dictionary<int, ViewerResultNode>();
            m_ID = id;
            m_Drawer = drawer;
            m_Region = new Rectangle(leftTop, new Size(vqg.Region.Width, vqg.Region.Height));
            
            Construct(vqg, m);
            Transfer(leftTop, vqg);
            GetAllEdges();
        }

        private void Construct(ViewerQueryGraph vqg, Match m)
        {
            ViewerGraphToLogicQueryTransfer transfer = vqg.Transfer;
            Dictionary<long, long> dicMatch = new Dictionary<long, long>();
            foreach (NodePair np in m.PartialMatch)
            {
                dicMatch.Add(np.node1, np.node2);
            }
            foreach (ViewerQueryNode vqn in vqg.NodesList)
            {
                ViewerResultNode vrn = new ViewerResultNode(vqn, dicMatch[transfer.GetLogicIDByViewerID(vqn.ID)]);
                m_AllNodes.Add(vrn.ID, vrn);
            }
        }

        private void Transfer(Point leftTop, ViewerQueryGraph vqg)
        {
            Point transferV = new Point();
            transferV.X = leftTop.X - vqg.Region.X;
            transferV.Y = leftTop.Y - vqg.Region.Y;
            foreach (ViewerResultNode vrn in m_AllNodes.Values)
            {
                vrn.Transfer(transferV);
            }
        }

        public void GetAllEdges()
        {
            m_Edges = new List<ViewerEdge>();
            foreach (ViewerResultNode startNode in m_AllNodes.Values)
            {
                foreach (int tid in startNode.OutLinkList)
                {
                    ViewerQueryNode endNode = m_AllNodes[tid];
                    ViewerEdge vqe = new ViewerEdge(startNode, endNode);
                    if (vqe.Valid())
                    {
                        m_Edges.Add(vqe);
                    }
                }
            }
        }

        public void Draw()
        {
            Image nodeIcon = ImageResources.GetImageByName("node");
            foreach (KeyValuePair<int, ViewerResultNode> kv in m_AllNodes)
            {
                ViewerQueryNode vqn = kv.Value;
                vqn.Draw(m_Drawer, nodeIcon);
            }

            foreach (ViewerEdge vqe in m_Edges)
            {
                vqe.Draw(m_Drawer);
            }
        }
    }
}
