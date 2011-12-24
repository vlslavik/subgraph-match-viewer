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
            if (Math.Abs(p1x - p2x) < 1e-10)
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
                return;
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
            double c = (b1 - center2.Y) * (b1 - center2.Y) - r2 * r2 + center2.X * center2.X;
            GetRoot(a, b2, c, out x1, out x2);
            intersectX1 = x1;
            intersectX2 = x2;
            intersectY1 = k * x1 + b1;
            intersectY2 = k * x2 + b1;
        }

        public static void GetKB(double x1, double y1, double x2, double y2, out double k, out double b)
        {
            if (Math.Abs(x1 - x2) < 1e-10)
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
            x1 = (-b + discriminant ) / (2 * a);
            x2 = (-b - discriminant) / (2 * a);
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

        public static int CrossProduct(Point v1, Point v2)
        {
            int z = v1.X * v2.Y - v2.X * v1.Y;
            return z;
        }

        public static int Direction(Point p1, Point p2, Point p3)
        {
            Point v1 = new Point(p2.X - p1.X, p2.Y - p1.Y);
            Point v2 = new Point(p3.X -p1.X, p3.Y - p1.Y);
            return CrossProduct(v1, v2);
        }

        public static bool SegmentIntersect(Point p1, Point p2, Point p3, Point p4)
        {
            int d1 = Direction(p3, p4, p1);
            int d2 = Direction(p3, p4, p2);
            int d3 = Direction(p1, p2, p3);
            int d4 = Direction(p1, p2, p4);
            if (d1 * d2 < 0 && d3 * d4 < 0)
            {
                return true;
            }

            if (d1 == 0 && LocateInSection(p1.X, p3.X, p4.X))
            {
                return true;
            }
            if (d2 == 0 && LocateInSection(p2.X, p3.X, p4.X))
            {
                return true;
            }
            if (d3 == 0 && LocateInSection(p3.X, p1.X, p2.X))
            {
                return true;
            }
            if (d4 == 0 && LocateInSection(p4.X, p1.X, p2.X))
            {
                return true;
            }
            return false;
        }

        public static bool SegmentIntersectRectangle(Point p1, Point p2, Rectangle rec)
        {
            return SegmentIntersect(p1, p2, new Point(rec.Left, rec.Top), new Point(rec.Right, rec.Top))
                || SegmentIntersect(p1, p2, new Point(rec.Right, rec.Top), new Point(rec.Right, rec.Bottom))
                || SegmentIntersect(p1, p2, new Point(rec.Right, rec.Bottom), new Point(rec.Left, rec.Bottom))
                || SegmentIntersect(p1, p2, new Point(rec.Left, rec.Bottom), new Point(rec.Left, rec.Top));
        }

    }
}
