using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SubgraphViewer
{
    class AnalyticGeometryService
    {
        public static void SegmentIntesectCycle(double p1x, double p1y, double p2x, double p2y, Point center2, double r2, out double intersectX, out double intersectY)
        {
            intersectX = -1;
            intersectY = -1;
            double intersectX1, intersectX2, intersectY1, intersectY2;
            LineIntesectCycle(p1x, p1y, p2x, p2y, center2, r2, out intersectX1, out intersectY1, out intersectX2, out intersectY2);
            if (LocateInSection(intersectX1, p1x, p2x) == true || LocateInSection(intersectY1, p1y, p2y) == true)
            {
                intersectX = intersectX1;
                intersectY = intersectY1;
            }
            else if (LocateInSection(intersectX2, p1x, p2x) == true || LocateInSection(intersectY2, p1y, p2y) == true)
            {
                intersectX = intersectX2;
                intersectY = intersectY2;
            }
        }

        public static void LineIntesectCycle(double p1x, double p1y, double p2x, double p2y, Point center2, double r2, out double intersectX1, out double intersectY1, out double intersectX2, out double intersectY2)
        {
            intersectX1 = -1;
            intersectY1 = -1;
            intersectX2 = -1;
            intersectY2 = -1;
            if (p1x - p2x < 1e-10)
            {
                double p12x = p1x;
                if (p12x - center2.X > r2)
                {
                    return;
                }
                double y1, y2;
                double discriminant = Math.Sqrt(r2 * r2 - Math.Pow((p12x - center2.X), 2));
                y1 = center2.Y + discriminant;
                y2 = center2.Y - discriminant;
                intersectX1 = intersectX2 = p12x;
                intersectY1 = y1;
                intersectY2 = y2;
            }
            if (true)
            {
                //judge intersect possible
            }
            double x1, x2;
            double k, b1;
            GetKB(p1x, p1y, p2x, p2y, out k, out b1);
            double a = k * k + 1;
            double b2 = (2 * k * b1 - 2 * k * center2.Y - 2 * center2.X);
            double c = (b1 - center2.Y) * (b1 - center2.Y) - r2 * r2;
            GetRoot(a, b2, c, out x1, out x2);
            intersectX1 = x1;
            intersectX2 = x2;
            intersectY1 = k * x1 + b1;
            intersectY2 = k * x2 + b1;
        }

        public static void GetKB(double x1, double y1, double x2, double y2, out double k, out double b)
        {
            if (x1 - x2 < 1e-10)
            {
                k = 0;
                b = x1;
                return;
            }
            k = (y2 - y1) / (x2 - x1);
            b = y1 - k * x1;
        }

        public static void GetRoot(double a, double b, double c, out double x1, out double x2)
        {
            if (b * b - 4 * a * c < 1e-10)
            {
                throw new Exception("discriminant should be non negative");
            }
            double discriminant = Math.Sqrt(b * b - 4 * a * c);
            x1 = (discriminant - b) / (2 * a);
            x2 = (b - discriminant) / (2 * a);
        }

        public static bool LocateInSection(double guess, double x1, double x2)
        {
            return (guess >= x1 && guess <= x2) || (guess >= x2 && guess <= x1);
        }

        public static double PointDistance(Point p1, Point p2)
        {
            double r = Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2);
            return Math.Sqrt(r);
        }

    }
}
