using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SubgraphViewer
{

    class ToolItem
    {
        private string m_Name;
        private Point m_Location;
        private int m_ItemSize;
        bool m_Selected;
        public string Name
        {
            get { return m_Name; }
        }

        public ToolItem(string name, Point location, int itemSize)
        {
            m_Name = name;
            m_ItemSize = itemSize;
            m_Location = location;
            m_Selected = false;
        }

        public bool IsMe(Point p)
        {
            Size s = new Size(m_ItemSize, m_ItemSize);
            Rectangle rc = new Rectangle(m_Location, s);
            return rc.Contains(p);
        }

        public bool Selected(Point p)
        {
            Size s = new Size(ViewerConfig.ToolItemSize, ViewerConfig.ToolItemSize);
            Rectangle rc = new Rectangle(m_Location, s);
            if (rc.Contains(p) == true)
            {
                m_Selected = true;
                return true;
            }
            else
            {
                m_Selected = false;
                return false;
            }
        }

        public void Draw(ISubGraphViewerDrawer drawer)
        {
            if (m_Selected == false)
            {
                drawer.DrawImage(ImageResources.GetImageByName(m_Name), m_Location);
            }
            else
            {
                drawer.DrawImage(ImageResources.GetImageByName(m_Name + "_selected"), m_Location);
            }
        }
    }

    class ToolBox
    {
        private int m_ItemSize = ViewerConfig.ToolItemSize;
        private List<ToolItem> m_ToolItemList;
        private ISubGraphViewerDrawer m_Drawer;
        private Dictionary<string, ToolItem> m_Name2ToolItemDic;
        private ToolItem m_SelectedItem;

        public ToolItem SelectedItem
        {
            get { return m_SelectedItem; }
        }

        public ToolBox(ISubGraphViewerDrawer drawer)
        {
            m_Drawer = drawer;
            m_ToolItemList = new List<ToolItem>();
            m_Name2ToolItemDic = new Dictionary<string, ToolItem>();
            LoadResouces();
        }

        private void LoadResouces()
        {
            Point p = new Point(5, 5);
            ToolItem node = new ToolItem("node", p, m_ItemSize);
            m_ToolItemList.Add(node);
            m_Name2ToolItemDic.Add(node.Name, node);
            p.Y += (m_ItemSize + 15);
            ToolItem eraser = new ToolItem("eraser", p, m_ItemSize);
            m_Name2ToolItemDic.Add(eraser.Name, eraser);
            m_ToolItemList.Add(eraser);
        }

        public ToolItem GetToolItemByName(string name)
        {
            ToolItem ret;
            m_Name2ToolItemDic.TryGetValue(name, out ret);
            return ret;
        }

        public void Draw()
        {
            foreach (ToolItem ti in m_ToolItemList)
            {
                ti.Draw(m_Drawer);
            }
        }

        public void SetSelectedItem(Point location)
        {
            if (m_SelectedItem != null)
            {
                m_SelectedItem.Selected(location);
            }
            foreach (ToolItem ti in m_ToolItemList)
            {
                if (ti.Selected(location) == true)
                {
                    m_SelectedItem = ti;
                    return;
                }
            }
            m_SelectedItem = null;
        }
    }
}
