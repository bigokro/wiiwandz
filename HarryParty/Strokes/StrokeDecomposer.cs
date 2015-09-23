using System;
using System.Collections.Generic;
using WiiWandz.Positions;

namespace WiiWandz.Strokes
{
	public class StrokeDecomposer
	{
		public int maxX;
		public int maxY;
		public int minPctForStroke;

		public StrokeDecomposer (int maxX, int maxY, int minPctForStroke)
		{
			this.maxX = maxX;
			this.maxY = maxY;
			this.minPctForStroke = minPctForStroke;
		}

		public List<Stroke> determineStrokes(List<Position> positions)
		{
			var strokes = new List<Stroke> ();

			if (positions.Count == 0) {
				return strokes;
			}

			int increment = 3;
			int count = 0;
            int lastPivotIdx = 0;

			var lastPivot = positions [0];
			var lastPosition = lastPivot;
			var currentStroke = StrokeDirection.Bumbled;
			for (int i = 0; i < positions.Count; i++) {       
                var position = positions[i];
				if (count < increment)
                {
					count++;
					continue;
				}
                else
                {
					count = 0;
				}

				if (currentStroke == StrokeDirection.Bumbled)
                {
					currentStroke = determineDirection (lastPivot, position);
					lastPosition = position;
				}
                else if (continuingStroke (currentStroke, lastPosition, position))
                {
					lastPosition = position;
					continue;
				}
                else
                {
                    // Find end point
                    int iOrig = i;
                    for (i -= 1; i > lastPivotIdx && !continuingStroke(currentStroke, lastPosition, position); i--)
                    {
                        position = positions[i];
                        lastPosition = positions[i - 1];
                    }
                    if (i > lastPivotIdx)
                    {
                        lastPosition = position;
                        lastPivotIdx = i;
                        position = positions[++i];
                    }
                    else
                    {
                        i = iOrig;
                        position = positions[i];
                        lastPosition = positions[i - 1];
                        lastPivotIdx = i - 1;
                    }

                    if (strokeIsValid(currentStroke, lastPivot, lastPosition))
                    {
                        strokes.Add(new Stroke(currentStroke, lastPivot, lastPosition));
                    }
                    else
                    {
                        strokes.Add(new Stroke(StrokeDirection.Bumbled, lastPivot, lastPosition));
                    }
                    lastPivot = lastPosition;
                    currentStroke = StrokeDirection.Bumbled;
                    count = 0;
                }
			}

            if (strokeIsValid (currentStroke, lastPivot, lastPosition))
            {
                strokes.Add(new Stroke(currentStroke, lastPivot, lastPosition));
    		}
            else
            {
                strokes.Add(new Stroke(StrokeDirection.Bumbled, lastPivot, lastPosition));
			}

			return strokes;

		}

		public static bool strokesMatch(List<StrokeDirection> allStrokes, List<StrokeDirection> expected)
		{
            // Ignore bumbled strokes
            var allGoodStrokes = new List<StrokeDirection>();
            foreach (StrokeDirection stroke in allStrokes)
            {
                if (stroke != StrokeDirection.Bumbled)
                {
                    allGoodStrokes.Add(stroke);
                }
            }

			bool matched = false;
            int allStrokesCount = (allGoodStrokes.Count - expected.Count + 1);

            for (int i = 0; i < allStrokesCount; i++)
            {
				for (int j = 0; j < expected.Count && (i + j) < allGoodStrokes.Count; j++)
                {
					var stroke = allGoodStrokes[i+j];
                    var expectedStroke = expected [j];

					if (stroke != expectedStroke) {
                        // Break out of this loop
						j = allGoodStrokes.Count;
					}
                    else if (j == expected.Count - 1)
                    {
						matched = true;
                        break;
					}
				}
			}

			return matched;
		}

		// TODO: Not handling curved lines
		public static StrokeDirection determineDirection(Position start, Position end)
		{
            double angleFactor = 2.0;
            double ratioXToY = ((double)PositionStatistics.MAX_X) / PositionStatistics.MAX_Y;
            angleFactor = angleFactor * ratioXToY;

            var stroke = StrokeDirection.Bumbled;

			int deltaX = start.point.X - end.point.X; // 0 is far right for some reason
			int deltaY = end.point.Y - start.point.Y;

			// Avoid divide by zero
			if (deltaX == 0)
                return deltaY > 0 ? StrokeDirection.Up : StrokeDirection.Down;

			double slope = ((double) deltaY) / deltaX;

			if (slope < -angleFactor || slope >= angleFactor)
            {
                stroke = deltaY > 0 ? StrokeDirection.Up : StrokeDirection.Down;
			}
            else if (slope >= -angleFactor && slope < -(1/angleFactor))
            {
                stroke = deltaX > 0 ? StrokeDirection.DownToTheRight : StrokeDirection.UpToTheLeft;
			}
            else if (slope >= -(1 / angleFactor) && slope < (1 / angleFactor))
            {
                stroke = deltaX > 0 ? StrokeDirection.Right : StrokeDirection.Left;
			}
            else if (slope >= (1/angleFactor) && slope < angleFactor)
            {
                stroke = deltaX > 0 ? StrokeDirection.UpToTheRight : StrokeDirection.DownToTheLeft;
			}

			return stroke;
		}

		public bool strokeIsValid(StrokeDirection stroke, Position start, Position end)
		{
			int deltaX = start.point.X - end.point.X;
			int deltaY = start.point.Y - end.point.Y;

			double length = Math.Sqrt (Math.Pow(deltaX,2) + Math.Pow(deltaY,2));
			double percent = length / Math.Max (maxX, maxY);
            if (length > 0)
            {
                percent *= 100;
            }

            return percent >= minPctForStroke;
		}

		public bool continuingStroke(StrokeDirection stroke, Position start, Position end)
        {
			return determineDirection(start, end) == stroke;
		}
	}
}

