using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubgraphViewer
{
    class CellProperTable
    {
        private List<string> m_PropertyNameList;
        private List<int> m_IDList;
        private List<long> m_CellIDList;
        private Dictionary<string, List<string>> m_KeyValueTable;
        private int m_Row;
        private int m_Col;

        public int Row
        {
            get { return m_Row; }
        }

        public int Col
        {
            get { return m_Col; }
        }

        public Dictionary<string, List<string>> KeyValueTable
        {
            get { return m_KeyValueTable; }
        }
        public CellProperTable(ViewerResultGraph vrg, List<string> propertyList = null)
        {
            m_PropertyNameList = new List<string>();
            if (propertyList != null)
            {
                m_PropertyNameList.AddRange(propertyList);
            }
            m_KeyValueTable = new Dictionary<string, List<string>>();
            Initial(vrg);
        }

        private void Initial(ViewerResultGraph vrg)
        {
            m_IDList = new List<int>();
            m_CellIDList = new List<long>();
            m_KeyValueTable.Add("ID", new List<string>());
            m_KeyValueTable.Add("CellID", new List<string>());
            List<ViewerResultNode> nodeList = vrg.GetSortedIDNodeList();
            for (int i = 0; i < nodeList.Count; ++i)
            {
                m_IDList.Add(nodeList[i].ID);
                m_CellIDList.Add(nodeList[i].MatchID);
                m_KeyValueTable["ID"].Add(nodeList[i].ID.ToString());
                m_KeyValueTable["CellID"].Add(nodeList[i].MatchID.ToString());
            }
            m_Row = m_IDList.Count;
            m_Col = m_KeyValueTable.Keys.Count;
        }
    }
}
