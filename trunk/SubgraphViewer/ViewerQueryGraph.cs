﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SubgraphViewer
{

    class ViewerQueryEdge
    {
        public Point StartLocation;
        public Point EndLocation;
        public int StartNodeID;
        public int EndNodeID;
        public ViewerQueryEdge(ViewerQueryNode startNode, ViewerQueryNode endNode)
        {
            StartNodeID = startNode.ID;
            EndNodeID = endNode.ID;
            StartLocation = new Point(-1, -1);
            EndLocation = new Point(-1, -1);
        }

        private void CoreCaluteEdge(double sx, double sy, double ex, double ey, Point center1, Point center2, double radius)
        {
            double startX, startY;
            double endX, endY;
            AnalyticGeometryService.SegmentIntesectCycle(sx, sy, ex, ey, center1, radius, out endX, out endY);
            AnalyticGeometryService.SegmentIntesectCycle(sx, sy, ex, ey, center2, radius, out startX, out startY);
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
            double delta = ((double)radius) / 2;
            if(relativeX >= 0 && relativeY > 0)
            {
                CoreCaluteEdge(startCenter.X + delta, startCenter.Y, endCenter.Y + delta, endCenter.Y, startCenter, endCenter, radius);
            }
            else if (relativeY > 0 && relativeY <= 0)
            {
                if (relativeY == 0)
                {
                    CoreCaluteEdge(startCenter.X, startCenter.Y - delta, endCenter.X, endCenter.X - delta, startCenter, endCenter, radius);
                }
                else
                {
                    CoreCaluteEdge(startCenter.X - delta, startCenter.Y, endCenter.X - delta, endCenter.Y, startCenter, endCenter, radius);
                }
            }
            else if (relativeX <= 0 && relativeY < 0)
            {
                CoreCaluteEdge(startCenter.X - delta, startCenter.Y, endCenter.Y - delta, endCenter.Y, startCenter, endCenter, radius);
            }
            else
            {
                if (relativeY == 0)
                {
                    CoreCaluteEdge(startCenter.X, startCenter.Y + delta, endCenter.X, endCenter.Y + delta, startCenter, endCenter, radius);
                }
                else
                {
                    CoreCaluteEdge(startCenter.X + delta, startCenter.Y, endCenter.X + delta, endCenter.X, startCenter, endCenter, radius);
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
            return false;
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
            return false;
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
            labelLocation.Y = Center.Y - (int)ViewerConfig.NodeRadius;
            drawer.DrawString(m_ID.ToString(), labelLocation);
        }
    }

    class ViewerQueryGraph
    {
        private Dictionary<int, ViewerQueryNode> m_AllNodes;
        private int m_IDIndex = 0;
        private ISubGraphViewerDrawer m_Drawer;

        public ViewerQueryGraph(ISubGraphViewerDrawer drawer)
        {
            m_Drawer = drawer;
            m_AllNodes = new Dictionary<int, ViewerQueryNode>();
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
            return true;
        }

        public void AddEdge(int sid, int tid)
        {
            ViewerQueryNode startNode = m_AllNodes[sid];
            ViewerQueryNode endNode = m_AllNodes[tid];
            startNode.AddOutLink(tid);
            endNode.AddInLink(sid);
        }

        public void Remove(Rectangle removeArea)
        {
            foreach (KeyValuePair<int, ViewerQueryNode> kv in m_AllNodes)
            {
                if (kv.Value.Overlap(removeArea) == true)
                {
                    RemoveNode(kv.Key);
                    return;
                }
            }
            List<ViewerQueryEdge> allEdges = GetAllEdges();
            foreach (ViewerQueryEdge vqe in allEdges)
            {
                if (vqe.OverLap(removeArea) == true)
                {
                    RemoveEdge(vqe.StartNodeID, vqe.EndNodeID);
                }
            }

        }

        public List<ViewerQueryEdge> GetAllEdges()
        {
            List<ViewerQueryEdge> res = new List<ViewerQueryEdge>();
            foreach (KeyValuePair<int, ViewerQueryNode> kv in m_AllNodes)
            {
                ViewerQueryNode startNode = kv.Value;
                foreach (int tid in startNode.OutLinkList)
                {
                    ViewerQueryNode endNode = m_AllNodes[tid];
                    ViewerQueryEdge vqe = new ViewerQueryEdge(startNode, endNode);
                    if (vqe.Valid())
                    {
                        res.Add(vqe);
                    }
                    
                }
            }
            return res;
        }

        public void RemoveNode(int id)
        {
            ViewerQueryNode removeNode = m_AllNodes[id];
            foreach (int tid in removeNode.OutLinkList)
            {
                RemoveEdge(id, tid);
            }
            foreach (int sid in removeNode.InLinkList)
            {
                RemoveEdge(sid, id);
            }
            m_AllNodes.Remove(id);
        }

        public void RemoveEdge(int sid, int tid)
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

            List<ViewerQueryEdge> allEdges = GetAllEdges();
            foreach (ViewerQueryEdge vqe in allEdges)
            {
                vqe.Draw(m_Drawer);
            }

        }
    }
}