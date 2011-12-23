using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trinity.GraphDB.Query.Subgraph;
using Trinity.GraphDB;
using Trinity.GraphDB.Query;

namespace SubgraphViewer
{
    class ViewerGraphToLogicQueryTransfer
    {
        private Dictionary<int, long> m_Viewer2LogicDic;
        private Dictionary<long, int> m_Logic2ViewerDic;
        private ViewerQueryGraph m_ViewerQueryGraph;

        public ViewerGraphToLogicQueryTransfer(ViewerQueryGraph vqg)
        {
            m_ViewerQueryGraph = vqg;
            m_Logic2ViewerDic = new Dictionary<long, int>();
            m_Viewer2LogicDic = new Dictionary<int, long>();
        }

        public QueryGraph GetLogicQueryGraph()
        {
            QueryGraph qg = new QueryGraph();
            //create nodes
            foreach (ViewerQueryNode vqn in m_ViewerQueryGraph.NodesList)
            {
                ISubgraphMatchCell c = vqn.ToCell();
                qg.AddCell(c);
                m_Viewer2LogicDic.Add(vqn.ID, c.CellID);
                m_Logic2ViewerDic.Add(c.CellID, vqn.ID);
            }
            //create edges
            foreach (ViewerQueryNode vqn in m_ViewerQueryGraph.NodesList)
            {
                ISubgraphMatchCell c1 = qg.LoadCell(m_Viewer2LogicDic[vqn.ID]);
                foreach (int id in vqn.OutLinkList)
                {
                    ISubgraphMatchCell c2 = qg.LoadCell(m_Viewer2LogicDic[id]);
                    c1.TargetCellSet.Add(c2.CellID);
                    c2.SourceCellSet.Add(c1.CellID);
                }
            }
            return qg;
        }

        public int GetViewerIDByLogicID(long id)
        {
            int ret = -1;
            m_Logic2ViewerDic.TryGetValue(id, out ret);
            return ret;
        }

        public long GetLogicIDByViewerID(int id)
        {
            long ret = -1;
            m_Viewer2LogicDic.TryGetValue(id, out ret);
            return ret;
        }
    }
}
