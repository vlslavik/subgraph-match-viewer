using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SubgraphViewer
{
    interface ISubGraphViewerDrawer
    {
        void DrawImage(Image im, Point location);
        void DrawArrow(Point start, Point end);
        void DrawString(string text, Point location);
        void DrawEcllipse(Point p, float width, float height);
        void DrawRectangle(Rectangle rc);
    }

    class SubgraphViewerDrawer:ISubGraphViewerDrawer
    {
        private Graphics m_Graphics;
        private Pen m_LinePen;
        private Font m_Font;
        private Brush m_Brush;
        AdjustableArrowCap m_LineCap;

        public Graphics Graphics
        {
            set { m_Graphics = value; }
        }
        public SubgraphViewerDrawer(Graphics g)
        {
            m_Graphics = g;
            m_LinePen = new Pen(Color.Black, ViewerConfig.LinePenWidth);
            m_Font = new Font(ViewerConfig.FontName, ViewerConfig.FontSize);
            m_Brush = new SolidBrush(ViewerConfig.BrushColor);
            m_LineCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5, true);
        }

        public void DrawImage(Image im, Point location)
        {
            m_Graphics.DrawImage(im, location);
        }

        public void DrawArrow(Point start, Point end)
        {
            //CustomLineCap old = m_LinePen.CustomEndCap;
            m_LinePen.CustomEndCap = m_LineCap;
            m_Graphics.DrawLine(m_LinePen, start, end);
            //m_LinePen.CustomEndCap = old;
        }

        public void DrawString(string text, Point location)
        {
            m_Graphics.DrawString(text, m_Font, m_Brush, location);
        }

        public void DrawEcllipse(Point p, float width, float height)
        {
            m_Graphics.DrawEllipse(m_LinePen, p.X, p.Y, width, height);
        }

        public void DrawRectangle(Rectangle rc)
        {
            m_Graphics.DrawRectangle(m_LinePen, rc);
        }

        public void TranslateTransform(int x, int y)
        {
            m_Graphics.TranslateTransform(x, y);
        }
    }
}
