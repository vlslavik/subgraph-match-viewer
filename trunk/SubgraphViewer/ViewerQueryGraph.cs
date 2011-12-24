using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Trinity.GraphDB;
using Trinity.Core.Lib;
using Trinity.GraphDB.Query.Subgraph;
using Trinity.GraphDB.Query;

namespace SubgraphViewer
{

    class ViewerEdge
    {
        public Point StartLocation;
        public Point EndLocation;
        public int StartNodeID;
        public int EndNodeID;
        public ViewerEdge(ViewerQueryNode startNode, ViewerQueryNode endNode)
        {
            StartNodeID = startNode.ID;
            EndNodeID = endNode.ID;
            StartLocation = new Point(-1, -1);
            EndLocation = new Point(-1, -1);
            CaculateLine(startNode, endNode);
        }

        private void CoreCaluteEdge(double sx, double sy, double ex, double ey, Point startCenter, Point endCenter, double radius)
        {
            double startX, startY;
            double endX, endY;
            AnalyticGeometryService.SegmentIntesectCycle(sx, sy, ex, ey, endCenter, radius, out endX, out endY);
            AnalyticGeometryService.SegmentIntesectCycle(sx, sy, ex, ey, startCenter, radius, out startX, out startY);
            if (startX > 0 && startY > 0 && endX > 0 && endY > 0)
            {
                StartLocation.X = (int)startX;
                StartLocation.Y = (int)startY;
                EndLocation.X = (int)endX;
                EndLocation.Y = (int)endY;
            }
        }

        private void SingleLink(ViewerQueryNode startNode, ViewerQueryNode endNode)
        {
            Point startCenter = startNode.Center;
            Point endCenter = endNode.Center;
            double radius = ViewerConfig.NodeRadius;
            CoreCaluteEdge(startCenter.X, startCenter.Y, endCenter.X, endCenter.Y, startCenter, endCenter, radius);
        }

        private void DoubleLink(ViewerQueryNode startNode, ViewerQueryNode endNode)
        {
            Point startCenter = startNode.Center;
            Point endCenter = endNode.Center;
            double radius = ViewerConfig.NodeRadius;

            int relativeX, relativeY;
            relativeX = endCenter.X - startCenter.X;
            relativeY = endCenter.Y - startCenter.Y;
            double delta = radius / 3 * 2;
            delta = 0;
            if(relativeX >= 0 && relativeY < 0)
            {
                CoreCaluteEdge(startCenter.X + delta, startCenter.Y, endCenter.X + delta, endCenter.Y, startCenter, endCenter, radius);
            }
            else if (relativeX > 0 && relativeY >= 0)
            {
                if (relativeY == 0)
                {
                    CoreCaluteEdge(startCenter.X, startCenter.Y + delta, endCenter.X, endCenter.Y + delta, startCenter, endCenter, radius);
                }
                else
                {
                    CoreCaluteEdge(startCenter.X - delta, startCenter.Y, endCenter.X - delta, endCenter.Y, startCenter, endCenter, radius);
                }
            }
            else if (relativeX <= 0 && relativeY > 0)
            {
                CoreCaluteEdge(startCenter.X - delta, startCenter.Y, endCenter.X - delta, endCenter.Y, startCenter, endCenter, radius);
            }
            else
            {
                if (relativeY == 0)
                {
                    CoreCaluteEdge(startCenter.X, startCenter.Y - delta, endCenter.X, endCenter.Y - delta, startCenter, endCenter, radius);
                }
                else
                {
                    CoreCaluteEdge(startCenter.X + delta, startCenter.Y, endCenter.X + delta, endCenter.Y, startCenter, endCenter, radius);
                }
            }
        }

        private void CaculateLine(ViewerQueryNode startNode, ViewerQueryNode endNode)
        {
            double radius = ViewerConfig.NodeRadius;
            if (AnalyticGeometryService.PointDistance(startNode.Center, endNode.Center) < radius)
            {
                return;
            }
            
            if (endNode.OutLinkList.Contains(startNode.ID) == false)
            {
                //single link
                SingleLink(startNode, endNode);
            }
            else
            {
                //double link
                DoubleLink(startNode, endNode);
            }
        }

        public bool OverLap(Rectangle rec)
        {
            return AnalyticGeometryService.SegmentIntersectRectangle(StartLocation, EndLocation, rec);
        }

        public void Draw(ISubGraphViewerDrawer drawer)
        {
            drawer.DrawArrow(StartLocation, EndLocation);
        }

        public bool Valid()
        {
            return StartLocation.X >= 0 && StartLocation.Y >= 0 && EndLocation.X >= 0 && EndLocation.Y >= 0;
        }
    }

    class ViewerQueryNode
    {
        private int m_ID;
        private Point m_Center;
        private HashSet<int> m_OutLinkList;
        private HashSet<int> m_InLinkList;
        private string m_Label;

        public string Label
        {
            get { return m_Label; }
            set { m_Label = value; }
        }

        public int ID
        {
            get { return m_ID; }
        }

        public Point Center
        {
            get { return m_Center; }
        }

        public HashSet<int> OutLinkList
        {
            get { return m_OutLinkList; } 
        }

        public HashSet<int> InLinkList
        {
            get { return m_InLinkList; }
        }

        public ViewerQueryNode(int id, Point location)
        {
            m_ID = id;
            m_Center = location;
            m_OutLinkList = new HashSet<int>();
            m_InLinkList = new HashSet<int>();
        }

        public ViewerQueryNode(ViewerQueryNode vqn)
        {
            m_ID = vqn.ID;
            m_Center = vqn.Center;
            m_OutLinkList = new HashSet<int>(vqn.OutLinkList);
            m_InLinkList = new HashSet<int>(vqn.InLinkList);
        }

        public void AddOutLink(int targetID)
        {
            m_OutLinkList.Add(targetID);
        }

        public void RemoveOutLink(int targetID)
        {
            m_OutLinkList.Remove(targetID);
        }

        public void AddInLink(int sourceID)
        {
            m_InLinkList.Add(sourceID);
        }

        public void RemoveInLink(int sourceID)
        {
            m_InLinkList.Remove(sourceID);
        }

        public bool Overlap(Rectangle rec)
        {
            int radius = (int)ViewerConfig.NodeRadius;
            Rectangle rec2 = new Rectangle(m_Center.X - radius, m_Center.Y - radius, 2 * radius, 2 * radius);
            return !(rec2.Left > rec.Right
                || rec2.Right < rec.Left
                || rec2.Top > rec.Bottom
                || rec2.Bottom < rec.Top);
        }

        public bool Overlap(Point p, double radius)
        {
            double myRadius = ViewerConfig.NodeRadius;
            if (AnalyticGeometryService.PointDistance(p, m_Center) > (myRadius + radius + 15))
            {
                return false;
            }
            return true;
        }

        public void Draw(ISubGraphViewerDrawer drawer, Image icon)
        {
            Point location = new Point();
            location.X = Center.X - (int)ViewerConfig.NodeRadius;
            location.Y = Center.Y - (int)ViewerConfig.NodeRadius;
            drawer.DrawImage(icon, location);
            Point labelLocation = new Point();
            labelLocation.X = Center.X - (int)ViewerConfig.NodeRadius / 2;
            labelLocation.Y = Center.Y - (int)ViewerConfig.NodeRadius / 2;
            drawer.DrawString(m_ID.ToString(), labelLocation);
            //drawer.DrawEcllipse(location, 32, 32);
        }

        public ISubgraphMatchCell ToCell()
        {
            try
            {
                SimpleSubgraphMatchCell c = new SimpleSubgraphMatchCell(Int64ID.NewID());
                c.SetLabel(BitConverter.GetBytes(int.Parse(m_Label)));
                return c;
            }
            catch (FormatException e)
            {
                throw e;
            }
            
        }

        public void Transfer(Point transferV)
        {
            m_Center.X += transferV.X;
            m_Center.Y += transferV.Y;
        }

        public bool Valid(out string msg)
        {
            msg = "ok";
            if (m_Label == null)
            {
                msg = "vertex " + m_ID + " label not set";
                return false;
            }
            return true;
        }
    }

    class ViewerQueryGraph
    {
        private Dictionary<int, ViewerQueryNode> m_AllNodes;
        private int m_IDIndex = 0;
        private ISubGraphViewerDrawer m_Drawer;
        private Rectangle m_Region;
        private ViewerGraphToLogicQueryTransfer m_Transfer;

        public List<ViewerQueryNode> NodesList
        {
            get { return m_AllNodes.Values.ToList(); }
        }

        public ViewerGraphToLogicQueryTransfer Transfer
        {
            get { return m_Transfer; }
        }

        public Rectangle Region
        {
            get { return m_Region; }
        }

        public ViewerQueryGraph(ISubGraphViewerDrawer drawer)
        {
            m_Drawer = drawer;
            m_AllNodes = new Dictionary<int, ViewerQueryNode>();
            m_Transfer = new ViewerGraphToLogicQueryTransfer(this);
            m_Region = new Rectangle(-1, -1, 0, 0);
        }

        public bool NodeOverlap(Point location)
        {
            foreach (ViewerQueryNode vqn in m_AllNodes.Values)
            {
                if (vqn.Overlap(location, ViewerConfig.NodeRadius) == true)
                {
                    return true;
                }
            }
            return false;
        }

        public bool AddNode(Point location)
        {
            if (NodeOverlap(location) == true)
            {
                return false;
            }
            m_IDIndex += 1;
            ViewerQueryNode vqn = new ViewerQueryNode(m_IDIndex, location);
            m_AllNodes.Add(m_IDIndex, vqn);
            UpdateRegion();
            return true;
        }

        public void UpdateRegion()
        {
            int left = 10000;
            int right = -10000;
            int top = 10000;
            int bottom = -10000;
            int radius = (int)ViewerConfig.NodeRadius;
            m_Region = new Rectangle(-1, -1, 0, 0);
            foreach (ViewerQueryNode vqn in m_AllNodes.Values)
            {
                Point center = vqn.Center;
                if (left > center.X - radius)
                {
                    left = center.X - radius; 
                }
                if (right < center.X + radius)
                {
                    right = center.X + radius;
                }
                if (top > center.Y - radius)
                {
                    top = center.Y - radius;
                }
                if (bottom < center.Y + radius)
                {
                    bottom = center.Y + radius;
                }
            }
            m_Region = new Rectangle(left, top, right - left, bottom - top);

        }

        public void AddEdge(int sid, int tid)
        {
            if (sid != tid)
            {
                if (m_AllNodes.ContainsKey(sid) && m_AllNodes.ContainsKey(tid))
                {
                    ViewerQueryNode startNode = m_AllNodes[sid];
                    ViewerQueryNode endNode = m_AllNodes[tid];
                    startNode.AddOutLink(tid);
                    endNode.AddInLink(sid);
                }
            }
        }

        public void Remove(Rectangle removeArea)
        {
            int nodeID = -1;
            foreach (KeyValuePair<int, ViewerQueryNode> kv in m_AllNodes)
            {
                if (kv.Value.Overlap(removeArea) == true)
                {
                    nodeID = kv.Key;
                    break;
                }
            }
            if (nodeID != -1)
            {
                RemoveNode(nodeID);
                return;
            }
            List<ViewerEdge> allEdges = GetAllEdges();
            foreach (ViewerEdge vqe in allEdges)
            {
                if (vqe.OverLap(removeArea) == true)
                {
                    RemoveEdge(vqe.StartNodeID, vqe.EndNodeID);
                    return;
                }
            }
        }

        public List<ViewerEdge> GetAllEdges()
        {
            List<ViewerEdge> res = new List<ViewerEdge>();
            foreach (KeyValuePair<int, ViewerQueryNode> kv in m_AllNodes)
            {
                ViewerQueryNode startNode = kv.Value;
                foreach (int tid in startNode.OutLinkList)
                {
                    ViewerQueryNode endNode = m_AllNodes[tid];
                    ViewerEdge vqe = new ViewerEdge(startNode, endNode);
                    if (vqe.Valid())
                    {
                        res.Add(vqe);
                    }
                    
                }
            }
            return res;
        }

        private void RemoveNode(int id)
        {
            ViewerQueryNode removeNode = m_AllNodes[id];
            List<int> removeList = removeNode.OutLinkList.ToList();
            foreach (int tid in removeList)
            {
                RemoveEdge(id, tid);
            }
            removeList = removeNode.InLinkList.ToList();
            foreach (int sid in removeList)
            {
                RemoveEdge(sid, id);
            }
            m_AllNodes.Remove(id);
            UpdateRegion();
        }

        private void RemoveEdge(int sid, int tid)
        {
            ViewerQueryNode startNode = m_AllNodes[sid];
            ViewerQueryNode endNode = m_AllNodes[tid];
            startNode.RemoveOutLink(tid);
            endNode.RemoveInLink(sid);

        }

        public void Draw()
        {
            Image nodeIcon = ImageResources.GetImageByName("node");
            foreach (KeyValuePair<int, ViewerQueryNode> kv in m_AllNodes)
            {
                ViewerQueryNode vqn = kv.Value;
                vqn.Draw(m_Drawer, nodeIcon);
            }

            List<ViewerEdge> allEdges = GetAllEdges();
            foreach (ViewerEdge vqe in allEdges)
            {
                vqe.Draw(m_Drawer);
            }

            //if (m_Region.X != -1)
            //{
            //    m_Drawer.DrawRectangle(m_Region);
            //}
            

        }

        public QueryGraph GetLogicQueryGraph()
        {
            m_Transfer = new ViewerGraphToLogicQueryTransfer(this);
            return m_Transfer.GetLogicQueryGraph();
        }

        public void SetLabel(int vid, string label)
        {
            if (m_AllNodes.ContainsKey(vid))
            {
                m_AllNodes[vid].Label = label;
            }
        }

        public List<ViewerQueryNode> GetSortedIDNodeList()
        {
            List<ViewerQueryNode> res = new List<ViewerQueryNode>();
            List<int> idList = new List<int>(m_AllNodes.Keys);
            idList.Sort();
            for (int i = 0; i < idList.Count; ++i)
            {
                res.Add(m_AllNodes[idList[i]]);
            }
            return res;
        }

        public bool Valid(out string msg)
        {
            msg = "ok";
            foreach (ViewerQueryNode vqn in m_AllNodes.Values)
            {
                if (vqn.Valid(out msg) == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
