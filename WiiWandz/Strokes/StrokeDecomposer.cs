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

			Position lastPivot = positions [0];
			Position lastPosition = lastPivot;
			Stroke currentStroke = Stroke.Bumbled;
			foreach (Position position in positions) {
				if (count < increment) {
					count++;
					continue;
				} else {
					count = 0;
				}

				if (currentStroke == Stroke.Bumbled) {
					currentStroke = determineStroke (lastPivot, position);
					lastPosition = position;
				} else if (continuingStroke (currentStroke, lastPosition, position)) {
					lastPosition = position;
					continue;
				} else if (strokeIsValid (currentStroke, lastPivot, lastPosition)) {
					lastPivot = lastPosition;
					lastPosition = position;
					strokes.Add (currentStroke);
					currentStroke = Stroke.Bumbled;
				} else {
					lastPivot = lastPosition;
					lastPosition = position;
					strokes.Add (Stroke.Bumbled);
					currentStroke = Stroke.Bumbled;
				}
			}

			return strokes;

		}

		public Boolean strokesMatch(List<Stroke> allStrokes, List<Stroke> expected)
		{
			Boolean matched = false;

			for (int i = 0; i < (allStrokes.Count - expected.Count + 1); i++) {
				for (int j = 0; j < expected.Count && (i + j) < allStrokes.Count; j++) {
					Stroke stroke = allStrokes[i+j];
					Stroke expectedStroke = expected [j];
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
		public Stroke determineStroke(Position start, Position end)
		{
			Stroke stroke = Stroke.Bumbled;

			int deltaX = start.point.X - end.point.X;
			int deltaY = start.point.Y - end.point.Y;

			// Avoid divide by zero
			if (deltaX == 0) {
				if (deltaY > 0) {
					stroke = Stroke.Up;
				} else {
					stroke = Stroke.Down;
				}
				return stroke;
			}

			float slope = deltaY / deltaX;

			if (slope < -4 || slope >= 4) {
				if (deltaY > 0) {
					stroke = Stroke.Up;
				} else {
					stroke = Stroke.Down;
				}
			} else if (slope >= -4 && slope < -0.25) {
				if (deltaX > 0) {
					stroke = Stroke.DownToTheRight;
				} else {
					stroke = Stroke.UpToTheLeft;
				}
			} else if (slope >= -0.25 && slope < 0.25) {
				if (deltaX > 0) {
					stroke = Stroke.Right;
				} else {
					stroke = Stroke.Left;
				}
			} else if (slope >= 0.25 && slope < 4) {
				if (deltaX > 0) {
					stroke = Stroke.UpToTheRight;
				} else {
					stroke = Stroke.DownToTheLeft;
				}
			}

			return stroke;
		}

		public Boolean strokeIsValid(Stroke stroke, Position start, Position end)
		{
			int deltaX = start.point.X - end.point.X;
			int deltaY = start.point.Y - end.point.Y;

			double length = Math.Sqrt (deltaX ^ 2 + deltaY ^ 2);
			double percent = length / Math.Max (maxX, maxY);
            percent *= 100;

			return percent >= minPctForStroke;
		}

		public Boolean continuingStroke(Stroke stroke, Position start, Position end) {
			Stroke newStroke = determineStroke (start, end);
			return newStroke == stroke;
		}
	}
}

