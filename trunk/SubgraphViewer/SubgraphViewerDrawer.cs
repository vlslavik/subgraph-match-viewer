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
    }

    class SubgraphViewerDrawer:ISubGraphViewerDrawer
    {
        private Graphics m_Graphics;
        private Pen m_LinePen;
        private Font m_Font;
        private Brush m_Brush;

        public SubgraphViewerDrawer(Graphics g)
        {
            m_Graphics = g;
            m_LinePen = new Pen(Color.Black, ViewerConfig.LinePenWidth);
            m_Font = new Font(ViewerConfig.FontName, ViewerConfig.FontSize);
            m_Brush = new SolidBrush(ViewerConfig.BrushColor);
        }

        public void DrawImage(Image im, Point location)
        {
            m_Graphics.DrawImage(im, location);
        }

        public void DrawArrow(Point start, Point end)
        {
            m_LinePen.EndCap = LineCap.ArrowAnchor;
            m_Graphics.DrawLine(m_LinePen, start, end);
            m_LinePen.EndCap = LineCap.NoAnchor;
        }

        public void DrawString(string text, Point location)
        {
            m_Graphics.DrawString(text, m_Font, m_Brush, location);
        }
    }
}
