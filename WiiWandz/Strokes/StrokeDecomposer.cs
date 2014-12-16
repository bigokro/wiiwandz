using System;
using System.Collections.Generic;

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

			int increment = 5;
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

		public Boolean strokesMatch(List<StrokeDirection> allStrokes, List<StrokeDirection> expected)
		{
			Boolean matched = false;

			for (int i = 0; i < (allStrokes.Count - expected.Count + 1); i++) {
				for (int j = 0; j < expected.Count && (i + j) < allStrokes.Count; j++) {
					StrokeDirection stroke = allStrokes[i+j];
					StrokeDirection expectedStroke = expected [j];
					if (stroke != expectedStroke) {
                        // Break out of this loop
						j = allStrokes.Count;
					} else if (j == expected.Count - 1) {
						matched = true;
                        break;
					}
				}
			}

			return matched;
		}

		// TODO: Not handling curved lines
		public StrokeDirection determineDirection(Position start, Position end)
		{
			StrokeDirection stroke = StrokeDirection.Bumbled;

			int deltaX = start.point.X - end.point.X;
			int deltaY = start.point.Y - end.point.Y;

			// Avoid divide by zero
			if (deltaX == 0) {
				if (deltaY > 0) {
					stroke = StrokeDirection.Down;
				} else {
					stroke = StrokeDirection.Up;
				}
				return stroke;
			}

			float slope = deltaY / deltaX;

			if (slope < -4 || slope >= 4) {
				if (deltaY > 0) {
					stroke = StrokeDirection.Down;
				} else {
					stroke = StrokeDirection.Up;
				}
			} else if (slope >= -4 && slope < -0.25) {
				if (deltaX > 0) {
					stroke = StrokeDirection.UpToTheRight;
				} else {
					stroke = StrokeDirection.DownToTheLeft;
				}
			} else if (slope >= -0.25 && slope < 0.25) {
				if (deltaX > 0) {
					stroke = StrokeDirection.Right;
				} else {
					stroke = StrokeDirection.Left;
				}
			} else if (slope >= 0.25 && slope < 4) {
				if (deltaX > 0) {
					stroke = StrokeDirection.DownToTheRight;
				} else {
					stroke = StrokeDirection.UpToTheLeft;
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

