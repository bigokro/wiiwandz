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
			List<Stroke> strokes = new List<Stroke> ();

			if (positions.Count == 0) {
				return strokes;
			}

			int increment = 3;
			int count = 0;
            int lastPivotIdx = 0;

			Position lastPivot = positions [0];
			Position lastPosition = lastPivot;
			StrokeDirection currentStroke = StrokeDirection.Bumbled;
			for (int i = 0; i < positions.Count; i++) {
                Position position = positions[i];
				if (count < increment) {
					count++;
					continue;
				} else {
					count = 0;
				}

				if (currentStroke == StrokeDirection.Bumbled) {
					currentStroke = determineDirection (lastPivot, position);
					lastPosition = position;
				} else if (continuingStroke (currentStroke, lastPosition, position)) {
					lastPosition = position;
					continue;
				} else {
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

            if (strokeIsValid (currentStroke, lastPivot, lastPosition)) {
                strokes.Add(new Stroke(currentStroke, lastPivot, lastPosition));
    		} else {
                strokes.Add(new Stroke(StrokeDirection.Bumbled, lastPivot, lastPosition));
			}

			return strokes;

		}

		public static bool strokesMatch(List<StrokeDirection> allStrokes, List<StrokeDirection> expected)
		{
            // Ignore bumbled strokes
            List<StrokeDirection> allGoodStrokes = new List<StrokeDirection>();
            foreach (StrokeDirection stroke in allStrokes)
            {
                if (stroke != StrokeDirection.Bumbled)
                {
                    allGoodStrokes.Add(stroke);
                }
            }

			bool matched = false;

			for (int i = 0; i < (allGoodStrokes.Count - expected.Count + 1); i++) {
				for (int j = 0; j < expected.Count && (i + j) < allGoodStrokes.Count; j++) {
					StrokeDirection stroke = allGoodStrokes[i+j];
					StrokeDirection expectedStroke = expected [j];
					if (stroke != expectedStroke) {
                        // Break out of this loop
						j = allGoodStrokes.Count;
					} else if (j == expected.Count - 1) {
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

            StrokeDirection stroke = StrokeDirection.Bumbled;

			int deltaX = start.point.X - end.point.X; // 0 is far right for some reason
			int deltaY = end.point.Y - start.point.Y;

			// Avoid divide by zero
			if (deltaX == 0) {
				if (deltaY > 0) {
					stroke = StrokeDirection.Up;
				} else {
					stroke = StrokeDirection.Down;
				}
				return stroke;
			}

			double slope = ((double) deltaY) / deltaX;

			if (slope < -angleFactor || slope >= angleFactor) {
				if (deltaY > 0) {
					stroke = StrokeDirection.Up;
				} else {
					stroke = StrokeDirection.Down;
				}
			} else if (slope >= -angleFactor && slope < -(1/angleFactor)) {
				if (deltaX > 0) {
					stroke = StrokeDirection.DownToTheRight;
				} else {
					stroke = StrokeDirection.UpToTheLeft;
				}
			} else if (slope >= -(1/angleFactor) && slope < (1/angleFactor)) {
				if (deltaX > 0) {
					stroke = StrokeDirection.Right;
				} else {
					stroke = StrokeDirection.Left;
				}
			} else if (slope >= (1/angleFactor) && slope < angleFactor) {
				if (deltaX > 0) {
					stroke = StrokeDirection.UpToTheRight;
				} else {
					stroke = StrokeDirection.DownToTheLeft;
				}
			}

			return stroke;
		}

		public Boolean strokeIsValid(StrokeDirection stroke, Position start, Position end)
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

		public Boolean continuingStroke(StrokeDirection stroke, Position start, Position end) {
			StrokeDirection newStroke = determineDirection (start, end);
			return newStroke == stroke;
		}
	}
}

