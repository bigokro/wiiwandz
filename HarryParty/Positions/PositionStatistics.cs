using System;
using System.Collections.Generic;

namespace WiiWandz.Positions
{
    public class PositionStatistics
    {
        public static int MAX_X = 1023;
        public static int MAX_Y = 760;

        public List<Position> positions;

        public int xMin;
        public int xMax;
        public int yMin;
        public int yMax;

        public PositionStatistics(List<Position> positions)
        {
            this.positions = positions;

            xMin = MAX_X;
            xMax = 0;
            yMin = MAX_Y;
            yMax = 0;

            foreach (var position in positions)
            {
                if (position.point.X < xMin)
                {
                    xMin = position.point.X;
                }
                if (position.point.X > xMax)
                {
                    xMax = position.point.X;
                }
                if (position.point.Y < yMin)
                {
                    yMin = position.point.Y;
                }
                if (position.point.Y > yMax)
                {
                    yMax = position.point.Y;
                }
            }
            
        }

        public int Width()
        {
            return xMax - xMin;
        }

        public int Height()
        {
            return yMax - yMin;
        }

        public double Diagonal()
        {
            return Math.Sqrt(Math.Pow(Width(), 2) + Math.Pow(Height(), 2));
        }

        public double FractionOfTotal(Position start, Position end)
        {
            double size = Distance(start, end);
            double totalSize = Diagonal();
            if (totalSize == 0.0)
            {
                return double.PositiveInfinity;
            }
            return size / totalSize;
        }

        public Position Start()
        {
            return positions[0];
        }

        public Position End()
        {
            return positions[positions.Count - 1];
        }

        public static double Slope(Position start, Position end)
        {
            var calculate = end.point.X - start.point.X;
            return calculate == 0 ? double.PositiveInfinity : ((double)end.point.Y - start.point.Y) / (end.point.X - start.point.X);
        }

        public static double Distance(Position start, Position end)
        {
            return Math.Sqrt(
                Math.Pow(end.point.X - start.point.X, 2) +
                Math.Pow(end.point.Y - start.point.Y, 2)
            );
        }
    }
}
