using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trinity.GraphDB.Query.Subgraph;
using System.Drawing;

namespace SubgraphViewer
{
    class ViewerResult
    {
        List<ViewerResultGraph> m_AllResultGraph;
        public ViewerResult(ISubGraphViewerDrawer drawer, List<Match> matches, ViewerQueryGraph vqg)
        {
            m_AllResultGraph = new List<ViewerResultGraph>();
            ConstructAllResultGraph(drawer, matches, vqg);
        }

        private void ConstructAllResultGraph(ISubGraphViewerDrawer drawer, List<Match> matches, ViewerQueryGraph vqg)
        {
            int width = ViewerConfig.QueryResultWidth;
            int margin = ViewerConfig.QueryResultMargin;
            Rectangle queryRegion = vqg.Region;
            int col = (width - margin) / (queryRegion.Width + margin);
            int count = matches.Count;
            int row = count / col;
            int index = 0;
            Point rowStartLocation = new Point();
            for (int i = 0; i < row; ++i)
            {
                rowStartLocation.X = margin;
                rowStartLocation.Y = margin + i * (queryRegion.Height + margin);
                for (int j = 0; j < col; ++j)
                {
                    Point myLocation = new Point();
                    myLocation.X = rowStartLocation.X + j * (queryRegion.Width + margin);
                    myLocation.Y = rowStartLocation.Y;
                    ViewerResultGraph vrg = new ViewerResultGraph(index, drawer, myLocation, vqg, matches[index]);
                    index += 1;
                    m_AllResultGraph.Add(vrg);
                }
            }
            //last row
            rowStartLocation.X = margin;
            rowStartLocation.Y = margin + row * (queryRegion.Height + margin);
            int left = count % col;
            for (int j = 0; j < left; ++j)
            {
                Point myLocation = new Point();
                myLocation.X = rowStartLocation.X + j * (queryRegion.Width + margin);
                myLocation.Y = rowStartLocation.Y;
                ViewerResultGraph vrg = new ViewerResultGraph(index, drawer, myLocation, vqg, matches[index]);
                index += 1;
                m_AllResultGraph.Add(vrg);
            }
        }

        public ViewerResultGraph GetOverlapGraph(Point p)
        {
            foreach (ViewerResultGraph vrg in m_AllResultGraph)
            {
                if (vrg.Contains(p) == true)
                {
                    return vrg;
                }
            }
            return null;
        }

        public void Draw()
        {
            foreach (ViewerResultGraph vrg in m_AllResultGraph)
            {
                vrg.Draw();
            }
        }
    }
}
